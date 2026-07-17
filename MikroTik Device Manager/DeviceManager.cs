using MikroTik_Device_Manager.helpers;
using MikroTik_Device_Manager.managers;

namespace MikroTik_Device_Manager
{
    /// <summary>
    /// Основне робоче вікно після підключення до MikroTik: виконує типові команди та відкриває пошук за MAC.
    /// </summary>
    public partial class DeviceManager : Form
    {
        // Посилання на головну форму, щоб повернути її після закриття цього вікна.
        private readonly Main _mainForm;
        // Спільний менеджер SSH-з'єднання, який передається дочірнім формам.
        private readonly MikroTikManager _manager;
        // Зберігає стан кнопок і полів під час виконання команди на роутері.
        private readonly Dictionary<Control, bool> _enabledStates = new();

        public DeviceManager(Main form, MikroTikManager manager)
        {
            InitializeComponent();
            _mainForm = form;
            _manager = manager;
        }

        /// <summary>
        /// Блокує UI на час SSH-команди, щоб уникнути паралельних натискань.
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
        /// Повертає елементи UI у той стан, який був до блокування.
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
        /// При закритті вікна очищає результат, розриває SSH-з'єднання та повертає головну форму.
        /// </summary>
        private void DeviceManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            tB_Result.Clear();
            _manager.Disconnect();
            _mainForm.Show();
        }

        private void HandleBrokenConnection()
        {
            MessageBox.Show(
                "Router is not responding.\n\nThe SSH connection has been terminated.\n\nPlease reconnect to continue working.",
                "SSH connection lost",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            Close();
        }

        /// <summary>
        /// Виконує команду RouterOS і виводить відповідь або помилку у поле результату.
        /// </summary>
        private async Task ExecuteCommandToResultAsync(string command)
        {
            LockControls();

            try
            {
                var response = await _manager.ExecuteCommandWithResultAsync(command);

                if (_manager.IsConnectionBroken)
                {
                    HandleBrokenConnection();
                    return;
                }

                tB_Result.Text = response.Success ? response.Result : _manager.LastError;
            }
            finally
            {
                if (!IsDisposed)
                    UnlockControls();
            }
        }

        /// <summary>
        /// Показує системну identity-назву роутера.
        /// </summary>
        private async void b_RouterName_Click(object sender, EventArgs e)
        {
            await ExecuteCommandToResultAsync(RouterCommands.GetSystemIdentity);
        }

        /// <summary>
        /// Показує поточні DHCP leases.
        /// </summary>
        private async void b_DHCP_ServerLeases_Click(object sender, EventArgs e)
        {
            await ExecuteCommandToResultAsync(RouterCommands.GetDHCPLeases);
        }

        /// <summary>
        /// Показує записи firewall address-list.
        /// </summary>
        private async void b_Firewall_AddressList_Click(object sender, EventArgs e)
        {
            await ExecuteCommandToResultAsync(RouterCommands.GetFirewallAddressesLists);
        }

        /// <summary>
        /// Відкриває дочірню форму для пошуку та редагування lease за MAC-адресою.
        /// </summary>
        private async void b_FindMAC_Click(object sender, EventArgs e)
        {
            await Task.CompletedTask;

            Device _form = new(this, _manager);
            _form.Show();
            ControlBox = false;
            b_FindMAC.Enabled = false;
            _form.Activate();
        }
    }
}
