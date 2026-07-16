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

        public static string GetLesesInfo(string macAddress)
        {
            string command =    $":put ([/ip dhcp-server lease get [find where mac-address=\"{macAddress}\"] address] . \";\" . " +
                                $"[/ip dhcp-server lease get [find where mac-address=\"{macAddress}\"] dynamic] . \";\" . " +
                                $"[/ip dhcp-server lease get [find where mac-address=\"{macAddress}\"] address-lists])";
            return command ;
        }

        public static string ListOfAddressLists()
        {
            return ":foreach i in=[/ip firewall address-list find] do={ :put [/ip firewall address-list get $i list] }";
        }

        public static string MakeStatic(string mac)
        {
            return $"/ip dhcp-server lease make-static [find where mac-address=\"{mac}\"]";
        }

        public static string RemoveLease(string mac)
        {
            return $"/ip dhcp-server lease remove [find where mac-address=\"{mac}\"]";
        }

        public static string SetAddressList(string mac, string list)
        {
            return $"/ip dhcp-server lease set [find where mac-address=\"{mac}\"] address-lists={list}";
        }

        public static string ClearAddressList(string mac)
        {
            return $"/ip dhcp-server lease set [find where mac-address=\"{mac}\"] address-lists=\"\"";
        }
    }
}
