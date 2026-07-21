using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MikroTik_Device_Manager.helpers
{
    /// <summary>
    /// Містить шаблони команд RouterOS, які застосунок надсилає на MikroTik через SSH.
    /// </summary>
    public static class RouterCommands
    {
        // Команда для отримання імені роутера.
        public const string GetSystemIdentity = "/system identity print";

        // Команда для виведення всіх DHCP leases.
        public const string GetDHCPLeases = "/ip dhcp-server lease print";

        // Команда для перегляду записів firewall address-list.
        public const string GetFirewallAddressesLists = "/ip firewall address-list print";

        /// <summary>
        /// Формує команду пошуку DHCP lease за MAC-адресою.
        /// </summary>
        public static string GetDHCPLeaseByMAC(string macAddress)
        {
            return $"/ip dhcp-server lease print where mac-address=\"{macAddress}\"";
        }

        /// <summary>
        /// Формує RouterOS-скрипт, який повертає IP, dynamic-статус і address-list через розділювач ;.
        /// </summary>
        public static string GetLesesInfo(string macAddress)
        {
            string command =    $":put ([/ip dhcp-server lease get [find where mac-address=\"{macAddress}\"] address] . \";\" . " +
                                $"[/ip dhcp-server lease get [find where mac-address=\"{macAddress}\"] dynamic] . \";\" . " +
                                $"[/ip dhcp-server lease get [find where mac-address=\"{macAddress}\"] address-lists])";
            return command ;
        }

        /// <summary>
        /// Повертає команду для отримання унікальних назв списків адрес із firewall address-list.
        /// </summary>
        public static string ListOfAddressLists()
        {
            return ":foreach i in=[/ip firewall address-list find] do={ :put [/ip firewall address-list get $i list] }";
        }

        /// <summary>
        /// Повертає команду, яка робить DHCP lease статичним.
        /// </summary>
        public static string MakeStatic(string mac)
        {
            return $"/ip dhcp-server lease make-static [find where mac-address=\"{mac}\"]";
        }

        /// <summary>
        /// Повертає команду видалення DHCP lease за MAC-адресою.
        /// </summary>
        public static string RemoveLease(string mac)
        {
            return $"/ip dhcp-server lease remove [find where mac-address=\"{mac}\"]";
        }

        /// <summary>
        /// Повертає команду призначення address-list для DHCP lease.
        /// </summary>
        public static string SetAddressList(string mac, string list)
        {
            string escapedList = list
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("$", "\\$");

            return $"/ip dhcp-server lease set [find where mac-address=\"{mac}\"] address-lists=\"{escapedList}\"";
        }

        /// <summary>
        /// Повертає команду очищення address-list у DHCP lease.
        /// </summary>
        public static string ClearAddressList(string mac)
        {
            return $"/ip dhcp-server lease set [find where mac-address=\"{mac}\"] address-lists=\"\"";
        }
    }
}
