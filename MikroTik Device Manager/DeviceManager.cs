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
            _mainForm.Show();
        }
    }
}
