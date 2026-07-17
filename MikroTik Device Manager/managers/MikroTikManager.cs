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
        #region Dinozavr
        //private SshClient? _ssh;
        //public string LastError { get; private set; } = "";

        //public bool ConnectSSH()
        //{
        //    _ssh = new SshClient(info.Ip, info.Login, info.Password);
        //    try
        //    {
        //        _ssh.Connect();
        //        return IsConnected();
        //    }
        //    catch (Exception ex)
        //    {
        //        LastError = ex.Message;
        //        return false;
        //    }
        //}

        //public bool IsConnected()
        //{
        //    if (_ssh != null)
        //        return _ssh.IsConnected;
        //    return false;
        //}

        //public bool ExecuteCommand(string command, out string result)
        //{
        //    result = "";

        //    if (!IsConnected())
        //    {
        //        LastError = "SSH-з'єднання не встановлено.";
        //        return false;
        //    }

        //    try
        //    {
        //        LastError = "";
        //        result = _ssh!.RunCommand(command).Result;
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        LastError = ex.Message;
        //        return false;
        //    }
        //}
        //public bool ExecuteCommand(string command)
        //{
        //    if (!IsConnected())
        //    {
        //        LastError = "SSH-з'єднання не встановлено.";
        //        return false;
        //    }

        //    try
        //    {
        //        LastError = "";
        //        if(_ssh != null)
        //        {
        //            var cmd = _ssh.CreateCommand(command);
        //            cmd.Execute();
        //        }
        //        else
        //        {
        //            LastError = "SSH-з'єднання не встановлено.";
        //            return false;
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        LastError = ex.Message;
        //        return false;
        //    }
        //}

        //public void Disconnect()
        //{
        //    if (_ssh == null)
        //        return;

        //    if (_ssh.IsConnected)
        //        _ssh.Disconnect();

        //    _ssh.Dispose();
        //    _ssh = null;
        //}
        #endregion

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

        // Виконання команди без отримання результату
        public bool ExecuteCommand(string command)
        {
            LastError = "";

            if (!IsConnected())
            {
                LastError = "SSH-з'єднання не встановлено.";
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

        // Асинхронна обгортка
        public Task<bool> ExecuteCommandAsync(string command)
        {
            return Task.Run(() => ExecuteCommand(command));
        }

        // Виконання команди з отриманням результату
        public bool ExecuteCommandWithResult(string command, out string result)
        {
            result = "";

            if (!IsConnected())
            {
                LastError = "SSH-з'єднання не встановлено.";
                return false;
            }

            try
            {
                LastError = "";

                var cmd = _ssh!.CreateCommand(command);
                result = cmd.Execute();

                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        // Асинхронна обгортка
        public Task<(bool Success, string Result)> ExecuteCommandWithResultAsync(string command)
        {
            return Task.Run(() =>
            {
                bool success = ExecuteCommandWithResult(command, out string result);
                return (success, result);
            });
        }

        #endregion

    }
}
