using MikroTik_Device_Manager.models;
using Renci.SshNet;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MikroTik_Device_Manager.managers
{
    /// <summary>
    /// Поточний стан SSH-з'єднання з MikroTik.
    /// </summary>
    public enum ConnectionState
    {
        Disconnected,
        Connecting,
        Connected,
        Busy,
        Broken
    }

    /// <summary>
    /// Керує SSH-з'єднанням із MikroTik RouterOS та виконує команди на роутері.
    /// </summary>
    public class MikroTikManager(models.ConnectionInfo info)
    {
        private const int NormalTimeoutSeconds = 3;
        private const int CriticalTimeoutSeconds = 15;
        private const string ConnectionBrokenMessage = "Маршрутизатор не відповідає. SSH-з’єднання перервано. Будь ласка, підключіться знову, щоб продовжити роботу.";

        // Активний SSH-клієнт. Null означає, що підключення ще не створене або вже закрите.
        private SshClient? _ssh;
        private readonly SemaphoreSlim _sshOperationLock = new(1, 1);

        // Остання помилка, яку можна показати користувачу після невдалої операції.
        public string LastError { get; private set; } = "";
        public ConnectionState State { get; private set; } = ConnectionState.Disconnected;
        public bool IsConnectionBroken => State == ConnectionState.Broken;

        #region Connection

        /// <summary>
        /// Створює SSH-підключення за даними, переданими у ConnectionInfo.
        /// </summary>
        public bool ConnectSSH()
        {
            Disconnect();
            State = ConnectionState.Connecting;
            _ssh = new SshClient(info.Ip, info.Login, info.Password);

            try
            {
                LastError = "";
                _ssh.Connect();
                State = IsConnected() ? ConnectionState.Connected : ConnectionState.Disconnected;
                return State == ConnectionState.Connected;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                DisposeSshClient();
                State = ConnectionState.Disconnected;
                return false;
            }
        }

        // Асинхронна обгортка, щоб не блокувати WinForms UI під час підключення.
        public Task<bool> ConnectSSHAsync()
        {
            return Task.Run(ConnectSSH);
        }

        /// <summary>
        /// Перевіряє, чи створений SSH-клієнт і чи він зараз підключений.
        /// </summary>
        public bool IsConnected()
        {
            return _ssh is not null && _ssh.IsConnected;
        }

        /// <summary>
        /// Коректно завершує SSH-сесію та звільняє ресурси клієнта.
        /// </summary>
        public void Disconnect()
        {
            DisposeSshClient();
            State = ConnectionState.Disconnected;
        }

        private void DisposeSshClient()
        {
            if (_ssh == null)
                return;

            try
            {
                if (_ssh.IsConnected)
                    _ssh.Disconnect();
            }
            finally
            {
                _ssh.Dispose();
            }
            _ssh = null;
        }

        private void BreakConnection()
        {
            LastError = ConnectionBrokenMessage;
            State = ConnectionState.Broken;
            DisposeSshClient();
        }

        #endregion

        #region Commands

        private async Task<T> ExecuteSshOperationAsync<T>(Func<SshClient, T> operation, T failureResult)
        {
            await _sshOperationLock.WaitAsync();

            try
            {
                LastError = "";

                if (State == ConnectionState.Broken)
                {
                    LastError = ConnectionBrokenMessage;
                    return failureResult;
                }

                if (!IsConnected())
                {
                    LastError = "SSH-з'єднання не встановлено.";
                    State = ConnectionState.Disconnected;
                    return failureResult;
                }

                State = ConnectionState.Busy;
                SshClient ssh = _ssh!;
                Task<T> operationTask = Task.Run(() => operation(ssh));
                _ = operationTask.ContinueWith(task => _ = task.Exception,
                    TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
                Task normalTimeoutTask = Task.Delay(TimeSpan.FromSeconds(NormalTimeoutSeconds));

                Task completedTask = await Task.WhenAny(operationTask, normalTimeoutTask);

                if (completedTask == operationTask)
                    return await CompleteOperationAsync(operationTask, failureResult);

                Task criticalTimeoutTask = Task.Delay(TimeSpan.FromSeconds(CriticalTimeoutSeconds - NormalTimeoutSeconds));
                completedTask = await Task.WhenAny(operationTask, criticalTimeoutTask);

                if (completedTask == operationTask)
                    return await CompleteOperationAsync(operationTask, failureResult);

                BreakConnection();
                return failureResult;
            }
            finally
            {
                if (State == ConnectionState.Busy)
                    State = IsConnected() ? ConnectionState.Connected : ConnectionState.Disconnected;

                _sshOperationLock.Release();
            }
        }

        private async Task<T> CompleteOperationAsync<T>(Task<T> operationTask, T failureResult)
        {
            try
            {
                return await operationTask;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return failureResult;
            }
        }

        /// <summary>
        /// Запускає команду у фоновому потоці, щоб форма залишалась відповідальною.
        /// </summary>
        public Task<bool> ExecuteCommandAsync(string command)
        {
            return ExecuteSshOperationAsync(ssh =>
            {
                using SshCommand cmd = ssh.CreateCommand(command);
                cmd.Execute();
                return true;
            }, false);
        }

        /// <summary>
        /// Асинхронно виконує команду та повертає її результат для відображення у UI.
        /// </summary>
        public Task<(bool Success, string Result)> ExecuteCommandWithResultAsync(string command)
        {
            return ExecuteSshOperationAsync(ssh =>
            {
                using SshCommand cmd = ssh.CreateCommand(command);
                return (true, cmd.Execute());
            }, (false, string.Empty));
        }

        #endregion

    }
}
