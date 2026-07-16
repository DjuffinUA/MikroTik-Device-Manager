using MikroTik_Device_Manager.models;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace MikroTik_Device_Manager.managers
{
    public class MikroTikManager(models.ConnectionInfo info)
    {
        private SshClient? _ssh;
        public string LastError { get; private set; } = "";

        public bool ConnectSSH()
        {
            _ssh = new SshClient(info.Ip, info.Login, info.Password);
            try
            {
                _ssh.Connect();
                return IsConnected();
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        public bool IsConnected()
        {
            if (_ssh != null)
                return _ssh.IsConnected;
            return false;
        }

        public bool ExecuteCommand(string command, out string result)
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
                result = _ssh!.RunCommand(command).Result;
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
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
    }
}
