using System;
using System.Collections.Generic;
using System.Text;

namespace MikroTik_Device_Manager.models
{
    public class LeaseInfo
    {       

        public string Mac { get; set; }

        public string Ip { get; set; } = string.Empty;

        public bool Dynamic { get; set; } = false;

        public string AddressList { get; set; } = "not";

        public string ShowInfo()
        {
            return $"MAC: {Mac}," +
                $" IP: {Ip}," +
                $" Dynamic: {Dynamic.ToString()}," +
                $" Address List: {AddressList}";
        }
    }
}
