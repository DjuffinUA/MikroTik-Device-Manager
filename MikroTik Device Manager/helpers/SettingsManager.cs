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

        private static void Save(List<ConnectionInfo> connections)
        {
            string json = JsonSerializer.Serialize(connections, new JsonSerializerOptions
                {
                    WriteIndented = true
                }
            );

            File.WriteAllText(settingsFilePath, json);
        }

        public static List<ConnectionInfo> Load()
        {
            if (!File.Exists(settingsFilePath))
                return new List<ConnectionInfo>();

            string json = File.ReadAllText(settingsFilePath);

            if (string.IsNullOrWhiteSpace(json))
                return new List<ConnectionInfo>();

            return JsonSerializer.Deserialize<List<ConnectionInfo>>(json)
                   ?? new List<ConnectionInfo>();
        }

        public static bool SettingsFileExists()
        {
            return File.Exists(settingsFilePath);
        }

        public static void CreateJsonFile()
        {
            File.WriteAllText(settingsFilePath, []);
        }

        public static void AddConnectionInfo(List<ConnectionInfo> connections, ConnectionInfo info)
        {
            if (!connections.Exists(c => c.Ip == info.Ip && c.Login == info.Login))
            {
                connections.Add(info);
                Save(connections);
            }            
        }
    }
}
