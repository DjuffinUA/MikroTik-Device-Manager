using MikroTik_Device_Manager.helpers;
using MikroTik_Device_Manager.managers;

namespace MikroTik_Device_Manager.models
{
    public class LeaseInfo(string mac)
    {
        public string Mac { get; private set; } = mac;

        public string Ip { get; set; } = string.Empty;

        public bool Dynamic { get; set; } = false;

        public string AddressList { get; set; } = "not list";

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

        public string ShowInfo()
        {
            return new string($"{Environment.NewLine}MAC: {Mac};{Environment.NewLine}IP: {Ip};{Environment.NewLine}Dynamic: {Dynamic};{Environment.NewLine}Address List: {AddressList}");
        }
    }
}
