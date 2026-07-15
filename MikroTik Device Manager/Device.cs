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
    public partial class Device : Form
    {
        private DeviceManager _form;
        private MikroTikManager _manager;
        private List<Control>? controlsVisible;
        private List<Control>? controlsText;
        private string _MAC {  get; set; } = string.Empty;

        public Device(DeviceManager form, MikroTikManager manager)
        {
            InitializeComponent();
            _form = form;
            _manager = manager;

            controlsVisible = new List<Control>
            {
                b_ClearForm,
                L_AvailableAction,
                b_BoundIP,
                b_RemoveIP,
                b_RemoveAddressList,
                b_AddAddressList,
                L_AddressList,
                L_AddAddressList,
                comBox_AddAddressList
            };

            controlsText = new List<Control>
            {
                tB_Mac,
                tB_Monitor,
                L_AddressList,
                comBox_AddAddressList
            };
            NullStatusForm();
        }

        private void NullStatusForm()
        {
            if (controlsText != null)
                foreach (Control control in controlsText)
                {
                    control.Text = string.Empty;
                }

            if (controlsVisible != null)
                foreach (Control control in controlsVisible)
                {
                    control.Visible = false;
                    control.Enabled = false;
                }

            _MAC = string.Empty;
        }

        private void Device_FormClosed(object sender, FormClosedEventArgs e)
        {
            _form.ControlBox = true;
            _form.Activate();
        }

        private bool NormalizeMac()
        {
            string mac = tB_Mac.Text.Trim().ToUpper();
            const string validChars = "0123456789ABCDEF";
            List<char> tmp = new();

            foreach (char c in mac)
            {
                if (validChars.Contains(c))
                {
                    tmp.Add(c);
                }
            }
            if (tmp.Count == 12)
            {
                _MAC = $"{tmp[0]}{tmp[1]}:{tmp[2]}{tmp[3]}:{tmp[4]}{tmp[5]}:{tmp[6]}{tmp[7]}:{tmp[8]}{tmp[9]}:{tmp[10]}{tmp[11]}";
                return true;
            }
            else
                return false;
        }

        private void b_Find_Click(object sender, EventArgs e)
        {
            if (NormalizeMac())
            {
                string tmp = string.Empty;
                _manager.ExecuteCommand(RouterCommands.GetDHCPLeaseByMAC(_MAC), out tmp);
                tB_Monitor.Text = tmp;
            }
            else
            {
                tB_Monitor.Text = "MAC address entry error";
                return;
            }  
        }
    }
}
