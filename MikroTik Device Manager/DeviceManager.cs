using MikroTik_Device_Manager.helpers;
using MikroTik_Device_Manager.managers;

namespace MikroTik_Device_Manager
{
    public partial class DeviceManager : Form
    {
        private Main _mainForm;
        private MikroTikManager _manager;

        public DeviceManager(Main form, MikroTikManager manager)
        {
            InitializeComponent();
            _mainForm = form;
            _manager = manager;
        }

        private void DeviceManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            tB_Result.Clear();
            _mainForm.Show();
        }

        private void b_RouterName_Click(object sender, EventArgs e)
        {
            string result = "";
            if (_manager.ExecuteCommand(RouterCommands.GetSystemIdentity, out result))
                tB_Result.Text = result;
            else
                tB_Result.Text = _manager.LastError;
        }

        private void b_DHCP_ServerLeases_Click(object sender, EventArgs e)
        {
            string result = "";
            if (_manager.ExecuteCommand(RouterCommands.GetDHCPLeases, out result))
                tB_Result.Text = result;
            else
                tB_Result.Text = _manager.LastError;
        }

        private void b_Firewall_AddressList_Click(object sender, EventArgs e)
        {
            string result = "";
            if (_manager.ExecuteCommand(RouterCommands.GetFirewallAddressesLists, out result))
                tB_Result.Text = result;
            else
                tB_Result.Text = _manager.LastError;
        }

        private void b_FindMAC_Click(object sender, EventArgs e)
        {
            Device _form = new Device(this, _manager);
            _form.Show();
            this.ControlBox = false;
            _form.Activate();
        }
    }
}
