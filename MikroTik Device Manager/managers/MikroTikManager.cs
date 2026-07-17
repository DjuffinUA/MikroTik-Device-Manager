using MikroTik_Device_Manager.models;
using Renci.SshNet;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace MikroTik_Device_Manager.managers
{
    /// <summary>
    /// Керує SSH-з'єднанням із MikroTik RouterOS та виконує команди на роутері.
    /// </summary>
    public class MikroTikManager(models.ConnectionInfo info)
    {
        // Активний SSH-клієнт. Null означає, що підключення ще не створене або вже закрите.
        private SshClient? _ssh;

        // Остання помилка, яку можна показати користувачу після невдалої операції.
        public string LastError { get; private set; } = "";

        #region Connection

        /// <summary>
        /// Створює SSH-підключення за даними, переданими у ConnectionInfo.
        /// </summary>
        public bool ConnectSSH()
        {
            _ssh = new SshClient(info.Ip, info.Login, info.Password);

            try
            {
                LastError = "";
                _ssh.Connect();
                return IsConnected();
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
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
            if (_ssh == null)
                return;

            if (_ssh.IsConnected)
                _ssh.Disconnect();

            _ssh.Dispose();
            _ssh = null;
        }

        #endregion

        #region Commands

        /// <summary>
        /// Виконує команду RouterOS, коли потрібен лише факт успішного виконання.
        /// </summary>
        private bool ExecuteCommandCore(string command)
        {
            LastError = "";

            if (!IsConnected())
            {
                LastError = "SSH-з\'єднання не встановлено.";
                return false;
            }

            try
            {
                var cmd = _ssh!.CreateCommand(command);
                cmd.Execute();
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Запускає команду у фоновому потоці, щоб форма залишалась відповідальною.
        /// </summary>
        public Task<bool> ExecuteCommandAsync(string command)
        {
            return Task.Run(() => ExecuteCommandCore(command));
        }

        /// <summary>
        /// Виконує команду RouterOS і повертає текстову відповідь роутера.
        /// </summary>
        private (bool Success, string Result) ExecuteCommandWithResultCore(string command)
        {
            if (!IsConnected())
            {
                LastError = "SSH-з\'єднання не встановлено.";
                return (false, string.Empty);
            }

            try
            {
                LastError = "";

                var cmd = _ssh!.CreateCommand(command);
                return (true, cmd.Execute());
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return (false, string.Empty);
            }
        }

        /// <summary>
        /// Асинхронно виконує команду та повертає її результат для відображення у UI.
        /// </summary>
        public Task<(bool Success, string Result)> ExecuteCommandWithResultAsync(string command)
        {
            return Task.Run(() => ExecuteCommandWithResultCore(command));
        }

        #endregion

    }
}
