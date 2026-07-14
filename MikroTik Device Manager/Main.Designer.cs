namespace MikroTik_Device_Manager
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            But_Connect = new Button();
            L_Login = new Label();
            tB_IP = new TextBox();
            L_IP = new Label();
            tB_Login = new TextBox();
            tB_Password = new TextBox();
            L_Password = new Label();
            listBox_LoginIP = new ListBox();
            L_Warring = new Label();
            L_Coment = new Label();
            tB_Coment = new TextBox();
            SuspendLayout();
            // 
            // But_Connect
            // 
            But_Connect.Location = new Point(314, 252);
            But_Connect.Margin = new Padding(4);
            But_Connect.Name = "But_Connect";
            But_Connect.Size = new Size(96, 32);
            But_Connect.TabIndex = 1;
            But_Connect.Text = "Connect";
            But_Connect.UseVisualStyleBackColor = true;
            But_Connect.Click += But_Connect_Click;
            // 
            // L_Login
            // 
            L_Login.AutoSize = true;
            L_Login.Location = new Point(13, 98);
            L_Login.Margin = new Padding(4, 0, 4, 0);
            L_Login.Name = "L_Login";
            L_Login.Size = new Size(56, 21);
            L_Login.TabIndex = 3;
            L_Login.Text = "Login :";
            // 
            // tB_IP
            // 
            tB_IP.Location = new Point(104, 55);
            tB_IP.Margin = new Padding(4);
            tB_IP.Name = "tB_IP";
            tB_IP.Size = new Size(202, 29);
            tB_IP.TabIndex = 4;
            // 
            // L_IP
            // 
            L_IP.AutoSize = true;
            L_IP.Location = new Point(13, 58);
            L_IP.Margin = new Padding(4, 0, 4, 0);
            L_IP.Name = "L_IP";
            L_IP.Size = new Size(30, 21);
            L_IP.TabIndex = 5;
            L_IP.Text = "IP :";
            // 
            // tB_Login
            // 
            tB_Login.Location = new Point(104, 95);
            tB_Login.Margin = new Padding(4);
            tB_Login.Name = "tB_Login";
            tB_Login.Size = new Size(202, 29);
            tB_Login.TabIndex = 6;
            // 
            // tB_Password
            // 
            tB_Password.Location = new Point(104, 135);
            tB_Password.Margin = new Padding(4);
            tB_Password.Name = "tB_Password";
            tB_Password.PasswordChar = '*';
            tB_Password.Size = new Size(202, 29);
            tB_Password.TabIndex = 8;
            // 
            // L_Password
            // 
            L_Password.AutoSize = true;
            L_Password.Location = new Point(13, 138);
            L_Password.Margin = new Padding(4, 0, 4, 0);
            L_Password.Name = "L_Password";
            L_Password.Size = new Size(83, 21);
            L_Password.TabIndex = 9;
            L_Password.Text = "Password :";
            // 
            // listBox_LoginIP
            // 
            listBox_LoginIP.FormattingEnabled = true;
            listBox_LoginIP.Location = new Point(417, 12);
            listBox_LoginIP.Name = "listBox_LoginIP";
            listBox_LoginIP.Size = new Size(378, 193);
            listBox_LoginIP.TabIndex = 10;
            // 
            // L_Warring
            // 
            L_Warring.Location = new Point(12, 297);
            L_Warring.Name = "L_Warring";
            L_Warring.Size = new Size(783, 25);
            L_Warring.TabIndex = 11;
            L_Warring.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // L_Coment
            // 
            L_Coment.AutoSize = true;
            L_Coment.Location = new Point(12, 175);
            L_Coment.Margin = new Padding(4, 0, 4, 0);
            L_Coment.Name = "L_Coment";
            L_Coment.Size = new Size(72, 21);
            L_Coment.TabIndex = 12;
            L_Coment.Text = "Coment :";
            // 
            // tB_Coment
            // 
            tB_Coment.Location = new Point(104, 172);
            tB_Coment.Margin = new Padding(4);
            tB_Coment.Name = "tB_Coment";
            tB_Coment.Size = new Size(202, 29);
            tB_Coment.TabIndex = 13;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(808, 331);
            Controls.Add(tB_Coment);
            Controls.Add(L_Coment);
            Controls.Add(L_Warring);
            Controls.Add(listBox_LoginIP);
            Controls.Add(L_Password);
            Controls.Add(tB_Password);
            Controls.Add(tB_Login);
            Controls.Add(L_IP);
            Controls.Add(tB_IP);
            Controls.Add(L_Login);
            Controls.Add(But_Connect);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "Main";
            Text = "MDM";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tB_NetName;
        private Button But_Connect;
        private Label L_NetName;
        private Label L_Login;
        private TextBox tB_IP;
        private Label L_IP;
        private TextBox tB_Login;
        private TextBox tB_Password;
        private Label L_Password;
        private ListBox listBox_LoginIP;
        private Label L_Warring;
        private Label L_Coment;
        private TextBox tB_Coment;
    }
}
