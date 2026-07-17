using MikroTik_Device_Manager.helpers;
using MikroTik_Device_Manager.managers;

namespace MikroTik_Device_Manager
{
    public partial class DeviceManager : Form
    {
        private readonly Main _mainForm;
        private readonly MikroTikManager _manager;
        private readonly Dictionary<Control, bool> _enabledStates = new();

        public DeviceManager(Main form, MikroTikManager manager)
        {
            InitializeComponent();
            _mainForm = form;
            _manager = manager;
        }

        private void LockControls()
        {
            _enabledStates.Clear();

            foreach (Control control in Controls)
            {
                _enabledStates[control] = control.Enabled;
                control.Enabled = false;
            }
        }

        private void UnlockControls()
        {
            foreach (Control control in Controls)
            {
                if (_enabledStates.TryGetValue(control, out bool wasEnabled))
                    control.Enabled = wasEnabled;
            }

            _enabledStates.Clear();
        }

        private void DeviceManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            tB_Result.Clear();
            _manager.Disconnect();
            _mainForm.Show();
        }

        private async Task ExecuteCommandToResultAsync(string command)
        {
            LockControls();

            try
            {
                var response = await _manager.ExecuteCommandWithResultAsync(command);
                tB_Result.Text = response.Success ? response.Result : _manager.LastError;
            }
            finally
            {
                UnlockControls();
            }
        }

        private async void b_RouterName_Click(object sender, EventArgs e)
        {
            await ExecuteCommandToResultAsync(RouterCommands.GetSystemIdentity);
        }

        private async void b_DHCP_ServerLeases_Click(object sender, EventArgs e)
        {
            await ExecuteCommandToResultAsync(RouterCommands.GetDHCPLeases);
        }

        private async void b_Firewall_AddressList_Click(object sender, EventArgs e)
        {
            await ExecuteCommandToResultAsync(RouterCommands.GetFirewallAddressesLists);
        }

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
