using MikroTik_Device_Manager.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MikroTik_Device_Manager.helpers
{
    public static class SettingsManager
    {
        private static readonly string settingsFilePath = Path.Combine(AppContext.BaseDirectory, "settings.json");

        public static void Save(ConnectionInfo settings)
        {

        }

        public static List<ConnectionInfo> Load()
        {
            string json = File.ReadAllText(settingsFilePath);

            List< ConnectionInfo > connections = JsonSerializer.Deserialize<List<ConnectionInfo>>(json);

            return connections;
        }
    }
}
