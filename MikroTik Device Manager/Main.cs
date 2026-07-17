using MikroTik_Device_Manager.helpers;
using MikroTik_Device_Manager.managers;
using MikroTik_Device_Manager.models;

namespace MikroTik_Device_Manager
{
    /// <summary>
    /// Головна форма: збирає дані для підключення, завантажує збережені профілі та відкриває панель керування роутером.
    /// </summary>
    public partial class Main : Form
    {
        // Кеш збережених підключень, який синхронізується з settings.json.
        private List<ConnectionInfo> connections = new List<ConnectionInfo>();
        // Запам'ятовує попередній стан Enabled для контролів під час довгих SSH-операцій.
        private readonly Dictionary<Control, bool> _enabledStates = new();

        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Очищає поля введення та оновлює список збережених підключень після успішного входу.
        /// </summary>
        private void ClearControls()
        {
            tB_IP.Clear();
            tB_Login.Clear();
            tB_Password.Clear();
            tB_Comment.Clear();
            L_Warning.Text = "";
            listBox_LoginIP.Items.Clear();
            LoadItemListBox();
        }

        /// <summary>
        /// Тимчасово блокує всі елементи форми, щоб користувач не запустив кілька підключень одночасно.
        /// </summary>
        private void LockControls()
        {
            _enabledStates.Clear();

            foreach (Control control in Controls)
            {
                _enabledStates[control] = control.Enabled;
                control.Enabled = false;
            }
        }

        /// <summary>
        /// Відновлює стан елементів форми після завершення фонової операції.
        /// </summary>
        private void UnlockControls()
        {
            foreach (Control control in Controls)
            {
                if (_enabledStates.TryGetValue(control, out bool wasEnabled))
                    control.Enabled = wasEnabled;
            }

            _enabledStates.Clear();
        }

        /// <summary>
        /// Обробляє натискання Connect: бере дані з полів або вибраного профілю та пробує відкрити SSH-сесію.
        /// </summary>
        private async void But_Connect_Click(object sender, EventArgs e)
        {
            ConnectionInfo info = new ConnectionInfo();

            if (tB_IP.Text != "" && tB_Login.Text != "" && tB_Password.Text != "")
            {
                info = new ConnectionInfo
                {
                    Ip = tB_IP.Text,
                    Login = tB_Login.Text,
                    Password = tB_Password.Text,
                    Comment = tB_Comment.Text,
                };
            }
            else if (listBox_LoginIP.SelectedIndex >= 0 && listBox_LoginIP.SelectedIndex < connections.Count)
            {
                info = connections[listBox_LoginIP.SelectedIndex];
            }

            MikroTikManager manager = new MikroTikManager(info);
            LockControls();

            try
            {
                if (await manager.ConnectSSHAsync())
                {
                    L_Warning.Text = "Connected successfully!";
                    SettingsManager.AddConnectionInfo(connections, info);
                    DeviceManager dm = new DeviceManager(this, manager);
                    dm.Show();
                    ClearControls();
                    Hide();
                }
                else
                {
                    L_Warning.Text = manager.LastError;
                }
            }
            finally
            {
                UnlockControls();
            }
        }

        /// <summary>
        /// Під час старту форми завантажує settings.json або створює його, якщо це перший запуск.
        /// </summary>
        private void Main_Load(object sender, EventArgs e)
        {
            if(SettingsManager.SettingsFileExists()) 
                LoadItemListBox();
            else
                SettingsManager.CreateJsonFile();
        }

        /// <summary>
        /// Перечитує список підключень із файлу та показує їх у ListBox.
        /// </summary>
        private void LoadItemListBox()
        {
            listBox_LoginIP.Items.Clear();
            connections = SettingsManager.Load();
            for (int i = 0; i < connections.Count; i++)
                listBox_LoginIP.Items.Add(connections[i].Ip + "; " + connections[i].Login + "; " + connections[i].Comment + ";");
            listBox_LoginIP.ClearSelected();
        }
    }
}
