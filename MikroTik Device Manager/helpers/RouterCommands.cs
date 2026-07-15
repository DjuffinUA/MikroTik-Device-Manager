using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MikroTik_Device_Manager.helpers
{
    public static class RouterCommands
    {
        public const string GetSystemIdentity = "/system identity print";

        public const string GetDHCPLeases = "/ip dhcp-server lease print";

        public const string GetFirewallAddressesLists = "/ip firewall address-list print";

        public static string GetDHCPLeaseByMAC(string macAddress)
        {
            return $"/ip dhcp-server lease print where mac-address=\"{macAddress}\"";
        }
    }
}
