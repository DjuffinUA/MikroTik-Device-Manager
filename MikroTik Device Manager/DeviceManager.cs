using MikroTik_Device_Manager.helpers;
using MikroTik_Device_Manager.managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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

        private void identityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string result = "";
            if (_manager.ExecuteCommand(RouterCommands.GetSystemIdentity, out result))
                tB_Result.Text = result;
            else
                tB_Result.Text = _manager.LastError;
        }

        private void leasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string result = "";
            if (_manager.ExecuteCommand(RouterCommands.GetDHCPLeases, out result))
                tB_Result.Text = result;
            else
                tB_Result.Text = _manager.LastError;
        }

        private void adressListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string result = "";
            if (_manager.ExecuteCommand(RouterCommands.GetFirewallAddressesLists, out result))
                tB_Result.Text = result;
            else
                tB_Result.Text = _manager.LastError;
        }
    }
}
