using MikroTik_Device_Manager.models;
using Renci.SshNet;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace MikroTik_Device_Manager.managers
{
    public class MikroTikManager(models.ConnectionInfo info)
    {
        private SshClient? _ssh;

        public string LastError { get; private set; } = "";

        #region Connection

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

        // Асинхронна обгортка
        public Task<bool> ConnectSSHAsync()
        {
            return Task.Run(ConnectSSH);
        }

        public bool IsConnected()
        {
            return _ssh is not null && _ssh.IsConnected;
        }

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

        public Task<bool> ExecuteCommandAsync(string command)
        {
            return Task.Run(() => ExecuteCommandCore(command));
        }

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

        public Task<(bool Success, string Result)> ExecuteCommandWithResultAsync(string command)
        {
            return Task.Run(() => ExecuteCommandWithResultCore(command));
        }

        #endregion

    }
}
