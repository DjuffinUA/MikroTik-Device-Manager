using MikroTik_Device_Manager.helpers;
using MikroTik_Device_Manager.managers;
using MikroTik_Device_Manager.models;
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
        private string _MAC { get; set; } = string.Empty;

        public Device(DeviceManager form, MikroTikManager manager)
        {
            InitializeComponent();
            _form = form;
            _manager = manager;

            controlsVisible = new List<Control>
            {
                b_ClearForm,
                L_AvailableAction,
                b_MakeStatic,
                b_RemoveIP,
                b_RemoveAddressList,
                b_AddAddressList,
                L_AddressList,
                L_AddAddressList,
                comBox_AddAddressList,
                L_IP,
                L_IP_Now
            };

            controlsText = new List<Control>
            {
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
        }

        private void Device_FormClosed(object sender, FormClosedEventArgs e)
        {
            _form.ControlBox = true;
            _form.b_FindMAC.Enabled = true;
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
                tB_Mac.Text = _MAC;
                return true;
            }
            else
                return false;
        }

        private async void b_Find_Click(object sender, EventArgs e)
        {
            await RefreshLeaseInfoAsync();
        }

        private async Task RefreshLeaseInfoAsync()
        {
            NullStatusForm();

            if (!NormalizeMac())
            {
                tB_Monitor.Text = "MAC address entry error";
                return;
            }

            string tmp = string.Empty;

            b_ClearForm.Visible = true;
            b_ClearForm.Enabled = true;

            var response = await _manager.ExecuteCommandWithResultAsync(
                RouterCommands.GetDHCPLeaseByMAC(_MAC));

            if (!response.Success)
            {
                tB_Monitor.Text = _manager.LastError;
                return;
            }

            tmp = response.Result;

            List<string> splitText = tmp
                .Split(new[] { "\r\n" }, StringSplitOptions.None)
                .ToList();

            StringBuilder sb = new();

            foreach (string line in splitText)
            {
                if (line.Length > 0)
                    sb.AppendLine(line);
            }

            if (sb.Length == 0)
            {
                tB_Monitor.Text = "There is no device with this MAC address.";
                return;
            }

            L_TextBoard.Visible = true;
            L_TextBoard.Enabled = true;

            LeaseInfo lease = new LeaseInfo(_MAC);

            if (!lease.FillLeaseInfo(_manager))
            {
                tB_Monitor.Text = _manager.LastError;
                return;
            }

            sb.AppendLine(lease.ShowInfo());

            FillLeaseInfo(lease);

            tB_Monitor.Text = sb.ToString();
        }

        /// <summary>
        /// Заповнює елементи форми відповідно до стану DHCP Lease.
        /// </summary>
        private void FillLeaseInfo(LeaseInfo lease)
        {
            L_IP.Visible = true;
            L_IP.Enabled = true;

            L_IP_Now.Text = lease.Ip;
            L_IP_Now.Visible = true;
            L_IP_Now.Enabled = true;

            L_AddAddressList.Visible = true;
            L_AddAddressList.Enabled = true;

            L_AddressList.Text = lease.AddressList;
            L_AddressList.Visible = true;
            L_AddressList.Enabled = true;

            if (lease.Dynamic)
            {
                b_MakeStatic.Visible = true;
                b_MakeStatic.Enabled = true;

                return;
            }

            b_RemoveIP.Visible = true;
            b_RemoveIP.Enabled = true;

            if (lease.AddressList == "not list")
            {
                LoadAddressLists();

                comBox_AddAddressList.Visible = true;
                comBox_AddAddressList.Enabled = true;

                b_AddAddressList.Visible = true;
                b_AddAddressList.Enabled = true;
            }
            else
            {
                b_RemoveAddressList.Visible = true;
                b_RemoveAddressList.Enabled = true;
            }
        }

        private async Task LoadAddressListsAsync()
        {
            comBox_AddAddressList.Items.Clear();

            var response = await _manager.ExecuteCommandWithResultAsync(
                RouterCommands.ListOfAddressLists());

            if (!response.Success)
            {
                tB_Monitor.Text = _manager.LastError;
                return;
            }

            List<string> splitText = response.Result
                .Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Distinct()
                .ToList();

            foreach (string line in splitText)
                comBox_AddAddressList.Items.Add(line);
        }

        private void b_ClearForm_Click(object sender, EventArgs e)
        {
            NullStatusForm();
            _MAC = string.Empty;
            tB_Mac.Clear();
        }

        private void ExecuteAndRefresh(string command)
        {
            if (_manager.ExecuteCommand(command))
                RefreshLeaseInfo();
            else
                tB_Monitor.AppendText(Environment.NewLine + _manager.LastError);
        }

        private void b_MakeStatic_Click(object sender, EventArgs e)
        {
            ExecuteAndRefresh(RouterCommands.MakeStatic(_MAC));
        }

        private void b_RemoveIP_Click(object sender, EventArgs e)
        {
            ExecuteAndRefresh(RouterCommands.RemoveLease(_MAC));
        }

        private void b_AddAddressList_Click(object sender, EventArgs e)
        {
            ExecuteAndRefresh(RouterCommands.SetAddressList(_MAC, comBox_AddAddressList.Text));
        }

        private void b_RemoveAddressList_Click(object sender, EventArgs e)
        {
            ExecuteAndRefresh(RouterCommands.ClearAddressList(_MAC));
        }
    }
}
