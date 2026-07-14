using MikroTik_Device_Manager.helpers;
using MikroTik_Device_Manager.models;

namespace MikroTik_Device_Manager
{
    public partial class Main : Form
    {
        private List<ConnectionInfo> connections = new List<ConnectionInfo>();

        public Main()
        {
            InitializeComponent();
        }

        private void ClearControls()
        {
            tB_IP.Clear();
            tB_Login.Clear();
            tB_Password.Clear();
            tB_Coment.Clear();
            L_Warring.Text = "";
            listBox_LoginIP.Items.Clear();
            LoadItemListBox();
        }

        private void But_Connect_Click(object sender, EventArgs e)
        {
            ConnectionInfo info = new ConnectionInfo();

            if (tB_IP.Text != "" || tB_Login.Text != "" || tB_Password.Text != "")
            {
                info = new ConnectionInfo
                {
                    Ip = tB_IP.Text,
                    Login = tB_Login.Text,
                    Password = tB_Password.Text,
                    Comment = tB_Coment.Text,
                };
            }
            else if (listBox_LoginIP.SelectedIndex != -1)
            {
                int selectedIndex = listBox_LoginIP.SelectedIndex;
                info = connections[selectedIndex];
            }

            DeviceManager dm = new DeviceManager(this);
            dm.Show();
            ClearControls();
            this.Hide();

            // listBox_LoginIP.Items.Add(info.Ip + "; " + info.Login + "; " + info.Comment + ";"); // test
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if(SettingsManager.SettingsFileExists())
            {
                LoadItemListBox();
            }
            else
            {
                SettingsManager.CreateJsonFile();
            }
        }

        private void LoadItemListBox()
        {
            listBox_LoginIP.Items.Clear();
            connections = SettingsManager.Load();
            for (int i = 0; i < connections.Count; i++)
            {
                listBox_LoginIP.Items.Add(connections[i].Ip + "; " + connections[i].Login + "; " + connections[i].Comment + ";");
            }
            listBox_LoginIP.ClearSelected();
        }
    }
}
