using MikroTik_Device_Manager.helpers;
using MikroTik_Device_Manager.managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MikroTik_Device_Manager.models
{
    public class LeaseInfo(string mac)
    {       

        public string Mac { get; private set; } = mac;

        public string Ip { get; set; } = string.Empty;

        public bool Dynamic { get; set; } = false;

        public string AddressList { get; set; } = "not list";

        public bool FillLeaseInfo(MikroTikManager manager)
        {
            string result = string.Empty;
            if (manager.ExecuteCommand(RouterCommands.GetLesesInfo(Mac), out result))
            {
                List<string> list = result.TrimEnd('\r', '\n').Split(';').ToList();
                Ip = list[0];

                if (list[1] == "true")
                    Dynamic = true;

                if (list[2].Length > 0 && list[2] != string.Empty)
                    AddressList = list[2];

                return true;
            }
            else
            {
                return false;
            }                
        }

        public string ShowInfo()
        {
            return new string($"{Environment.NewLine}MAC: {Mac};{Environment.NewLine}IP: {Ip};{Environment.NewLine}Dynamic: {Dynamic.ToString()};{Environment.NewLine}Address List: {AddressList}");
        }
    }
}
