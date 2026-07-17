using MikroTik_Device_Manager.helpers;
using MikroTik_Device_Manager.managers;

namespace MikroTik_Device_Manager.models
{
    /// <summary>
    /// Описує DHCP lease знайденого пристрою та вміє дозавантажувати його деталі з RouterOS.
    /// </summary>
    public class LeaseInfo(string mac)
    {
        // MAC-адреса пристрою, за якою виконуються пошук і зміни DHCP lease.
        public string Mac { get; private set; } = mac;

        // Поточна IP-адреса, прив'язана до lease.
        public string Ip { get; set; } = string.Empty;

        // Ознака динамічного lease: true означає, що запис ще можна зробити статичним.
        public bool Dynamic { get; set; } = false;

        // Назва address-list, прив'язана до lease; "not list" використовується як значення за замовчуванням.
        public string AddressList { get; set; } = "not list";

        /// <summary>
        /// Завантажує з роутера додаткові поля lease і розкладає відповідь RouterOS по властивостях моделі.
        /// </summary>
        public async Task<bool> FillLeaseInfoAsync(MikroTikManager manager)
        {
            var response = await manager.ExecuteCommandWithResultAsync(RouterCommands.GetLesesInfo(Mac));

            if (!response.Success)
                return false;

            List<string> list = response.Result.TrimEnd('\r', '\n').Split(';').ToList();

            if (list.Count > 0)
                Ip = list[0];

            if (list.Count > 1)
                Dynamic = list[1] == "true";

            if (list.Count > 2 && !string.IsNullOrWhiteSpace(list[2]))
                AddressList = list[2];

            return true;
        }

        /// <summary>
        /// Формує текстовий блок із деталями lease для показу у вікні моніторингу.
        /// </summary>
        public string ShowInfo()
        {
            return new string($"{Environment.NewLine}MAC: {Mac};{Environment.NewLine}IP: {Ip};{Environment.NewLine}Dynamic: {Dynamic};{Environment.NewLine}Address List: {AddressList}");
        }
    }
}
