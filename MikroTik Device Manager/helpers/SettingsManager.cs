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
        private static bool TrySave(List<ConnectionInfo> connections, out string error)
        {
            try
            {
                string json = JsonSerializer.Serialize(connections, new JsonSerializerOptions
                    {
                        WriteIndented = true
                    }
                );

                File.WriteAllText(settingsFilePath, json);
                error = string.Empty;
                return true;
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                error = $"Unable to save settings.json: {ex.Message}";
                return false;
            }
        }

        /// <summary>
        /// Читає збережені підключення; якщо файл відсутній або порожній — повертає порожній список.
        /// </summary>
        public static bool TryLoad(out List<ConnectionInfo> connections, out string error)
        {
            try
            {
                if (!File.Exists(settingsFilePath))
                {
                    connections = new List<ConnectionInfo>();
                    error = string.Empty;
                    return true;
                }

                string json = File.ReadAllText(settingsFilePath);

                connections = string.IsNullOrWhiteSpace(json)
                    ? new List<ConnectionInfo>()
                    : JsonSerializer.Deserialize<List<ConnectionInfo>>(json) ?? new List<ConnectionInfo>();

                error = string.Empty;
                return true;
            }
            catch (Exception ex) when (ex is JsonException or IOException or UnauthorizedAccessException)
            {
                connections = new List<ConnectionInfo>();
                error = $"Unable to read settings.json: {ex.Message}";
                return false;
            }
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
        public static bool TryCreateJsonFile(out string error)
        {
            if (File.Exists(settingsFilePath))
            {
                error = string.Empty;
                return true;
            }

            return TrySave([], out error);
        }

        /// <summary>
        /// Додає нове підключення, якщо такого IP+Login ще немає, і одразу зберігає список.
        /// </summary>
        public static bool TryAddConnectionInfo(List<ConnectionInfo> connections, ConnectionInfo info, out string error)
        {
            if (connections.Exists(c => c.Ip == info.Ip && c.Login == info.Login))
            {
                error = string.Empty;
                return true;
            }

            List<ConnectionInfo> updatedConnections = new(connections) { info };

            if (!TrySave(updatedConnections, out error))
                return false;

            connections.Add(info);
            return true;
        }
    }
}
