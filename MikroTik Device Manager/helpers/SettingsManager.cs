using MikroTik_Device_Manager.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MikroTik_Device_Manager.helpers
{
    /// <summary>
    /// Відповідає за збереження та читання локального файлу налаштувань із підключеннями.
    /// </summary>
    public static class SettingsManager
    {
        // settings.json лежить поряд із виконуваним файлом, щоб дані були доступні між запусками програми.
        private static readonly string settingsFilePath = Path.Combine(AppContext.BaseDirectory, "settings.json");

        /// <summary>
        /// Серіалізує список підключень у JSON і записує його на диск.
        /// </summary>
        private static void Save(List<ConnectionInfo> connections)
        {
            string json = JsonSerializer.Serialize(connections, new JsonSerializerOptions
                {
                    WriteIndented = true
                }
            );

            File.WriteAllText(settingsFilePath, json);
        }

        /// <summary>
        /// Читає збережені підключення; якщо файл відсутній або порожній — повертає порожній список.
        /// </summary>
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

        /// <summary>
        /// Перевіряє наявність файлу налаштувань перед завантаженням головної форми.
        /// </summary>
        public static bool SettingsFileExists()
        {
            return File.Exists(settingsFilePath);
        }

        /// <summary>
        /// Створює порожній JSON-файл для першого запуску застосунку.
        /// </summary>
        public static void CreateJsonFile()
        {
            File.WriteAllText(settingsFilePath, []);
        }

        /// <summary>
        /// Додає нове підключення, якщо такого IP+Login ще немає, і одразу зберігає список.
        /// </summary>
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
