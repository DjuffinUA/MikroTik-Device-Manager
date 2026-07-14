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
        private Main mainForm;
        public DeviceManager(Main form)
        {
            InitializeComponent();
            mainForm = form;
        }

        private void DeviceManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.Show();
        }
    }
}
