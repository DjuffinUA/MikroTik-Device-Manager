using MikroTik_Device_Manager.models;

namespace MikroTik_Device_Manager
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void But_Connect_Click(object sender, EventArgs e)
        {
            ConnectionInfo info = new ConnectionInfo
            {
                Ip = tB_IP.Text,
                Login = tB_Login.Text,
                Password = tB_Password.Text,
                Comment = tB_Coment.Text,
            };
            DeviceManager dm = new DeviceManager(this);
            dm.Show();
            listBox_LoginIP.Items.Add(info.Login + "; " + info.Ip + "; " + info.Comment + ";"); // test
            this.Hide();
        }
    }
}
