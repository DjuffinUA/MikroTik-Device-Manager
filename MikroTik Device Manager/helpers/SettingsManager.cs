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

        public static void Save(List<ConnectionInfo> settings)
        {
            string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions
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
            File.WriteAllText(settingsFilePath, "");
        }
    }
}
