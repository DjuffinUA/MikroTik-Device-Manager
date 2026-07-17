using System;
using System.Collections.Generic;
using System.Text;

namespace MikroTik_Device_Manager.models
{
    /// <summary>
    /// Зберігає параметри підключення до MikroTik, які вводить або обирає користувач.
    /// </summary>
    public class ConnectionInfo
    {
        // IP-адреса або DNS-ім'я роутера.
        public string Ip { get; set; } = "";

        // SSH-порт. Наразі MikroTikManager використовує стандартний конструктор і фактично підключається до 22 порту.
        public int Port { get; set; } = 22;

        // Ім'я користувача RouterOS.
        public string Login { get; set; } = "";

        // Пароль користувача RouterOS.
        public string Password { get; set; } = "";

        // Довільний коментар для зручного розпізнавання збереженого підключення у списку.
        public string Comment { get; set; } = "";

    }
}
