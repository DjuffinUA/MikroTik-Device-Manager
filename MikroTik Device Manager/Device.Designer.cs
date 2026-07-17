namespace MikroTik_Device_Manager
{
    partial class Device
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Device));
            L_MAC = new Label();
            tB_Mac = new TextBox();
            b_Find = new Button();
            b_ClearForm = new Button();
            comBox_AddAddressList = new ComboBox();
            L_AvailableAction = new Label();
            tB_Monitor = new TextBox();
            b_MakeStatic = new Button();
            L_TextBoard = new Label();
            b_RemoveIP = new Button();
            b_RemoveAddressList = new Button();
            b_AddAddressList = new Button();
            L_AddAddressList = new Label();
            L_AddressList = new Label();
            L_IP = new Label();
            L_IP_Now = new Label();
            SuspendLayout();
            // 
            // L_MAC
            // 
            L_MAC.AutoSize = true;
            L_MAC.Location = new Point(12, 37);
            L_MAC.Name = "L_MAC";
            L_MAC.Size = new Size(51, 21);
            L_MAC.TabIndex = 0;
            L_MAC.Text = "MAC :";
            // 
            // tB_Mac
            // 
            tB_Mac.CharacterCasing = CharacterCasing.Upper;
            tB_Mac.Location = new Point(69, 34);
            tB_Mac.MaxLength = 20;
            tB_Mac.Name = "tB_Mac";
            tB_Mac.Size = new Size(158, 29);
            tB_Mac.TabIndex = 1;
            tB_Mac.WordWrap = false;
            // 
            // b_Find
            // 
            b_Find.Location = new Point(600, 30);
            b_Find.Name = "b_Find";
            b_Find.Size = new Size(172, 33);
            b_Find.TabIndex = 2;
            b_Find.Text = "Find MAC";
            b_Find.UseVisualStyleBackColor = true;
            b_Find.Click += b_Find_Click;
            // 
            // b_ClearForm
            // 
            b_ClearForm.Location = new Point(345, 448);
            b_ClearForm.Name = "b_ClearForm";
            b_ClearForm.Size = new Size(97, 33);
            b_ClearForm.TabIndex = 3;
            b_ClearForm.Text = "Clear Form";
            b_ClearForm.UseVisualStyleBackColor = true;
            b_ClearForm.Click += b_ClearForm_Click;
            // 
            // comBox_AddAddressList
            // 
            comBox_AddAddressList.FormattingEnabled = true;
            comBox_AddAddressList.Location = new Point(12, 691);
            comBox_AddAddressList.Name = "comBox_AddAddressList";
            comBox_AddAddressList.Size = new Size(242, 29);
            comBox_AddAddressList.TabIndex = 4;
            // 
            // L_AvailableAction
            // 
            L_AvailableAction.Location = new Point(12, 523);
            L_AvailableAction.Name = "L_AvailableAction";
            L_AvailableAction.Size = new Size(760, 32);
            L_AvailableAction.TabIndex = 5;
            L_AvailableAction.Text = "Available actions";
            L_AvailableAction.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tB_Monitor
            // 
            tB_Monitor.Location = new Point(12, 167);
            tB_Monitor.Multiline = true;
            tB_Monitor.Name = "tB_Monitor";
            tB_Monitor.ReadOnly = true;
            tB_Monitor.ScrollBars = ScrollBars.Both;
            tB_Monitor.Size = new Size(760, 236);
            tB_Monitor.TabIndex = 6;
            tB_Monitor.WordWrap = false;
            // 
            // b_MakeStatic
            // 
            b_MakeStatic.Location = new Point(12, 590);
            b_MakeStatic.Name = "b_MakeStatic";
            b_MakeStatic.Size = new Size(242, 33);
            b_MakeStatic.TabIndex = 7;
            b_MakeStatic.Text = "Make Static";
            b_MakeStatic.UseVisualStyleBackColor = true;
            b_MakeStatic.Click += b_MakeStatic_Click;
            // 
            // L_TextBoard
            // 
            L_TextBoard.Location = new Point(12, 104);
            L_TextBoard.Name = "L_TextBoard";
            L_TextBoard.Size = new Size(760, 21);
            L_TextBoard.TabIndex = 8;
            L_TextBoard.Text = "Detailed information by MAC address";
            L_TextBoard.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // b_RemoveIP
            // 
            b_RemoveIP.Location = new Point(530, 590);
            b_RemoveIP.Name = "b_RemoveIP";
            b_RemoveIP.Size = new Size(242, 33);
            b_RemoveIP.TabIndex = 9;
            b_RemoveIP.Text = "Remove IP";
            b_RemoveIP.UseVisualStyleBackColor = true;
            b_RemoveIP.Click += b_RemoveIP_Click;
            // 
            // b_RemoveAddressList
            // 
            b_RemoveAddressList.Location = new Point(608, 688);
            b_RemoveAddressList.Name = "b_RemoveAddressList";
            b_RemoveAddressList.Size = new Size(164, 33);
            b_RemoveAddressList.TabIndex = 10;
            b_RemoveAddressList.Text = "Remove Address List";
            b_RemoveAddressList.UseVisualStyleBackColor = true;
            b_RemoveAddressList.Click += b_RemoveAddressList_Click;
            // 
            // b_AddAddressList
            // 
            b_AddAddressList.Location = new Point(345, 691);
            b_AddAddressList.Name = "b_AddAddressList";
            b_AddAddressList.Size = new Size(97, 33);
            b_AddAddressList.TabIndex = 11;
            b_AddAddressList.Text = "Add";
            b_AddAddressList.UseVisualStyleBackColor = true;
            b_AddAddressList.Click += b_AddAddressList_Click;
            // 
            // L_AddAddressList
            // 
            L_AddAddressList.Location = new Point(12, 640);
            L_AddAddressList.Name = "L_AddAddressList";
            L_AddAddressList.Size = new Size(134, 32);
            L_AddAddressList.TabIndex = 12;
            L_AddAddressList.Text = "Add Address List :";
            L_AddAddressList.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // L_AddressList
            // 
            L_AddressList.AutoEllipsis = true;
            L_AddressList.Location = new Point(166, 640);
            L_AddressList.Name = "L_AddressList";
            L_AddressList.Size = new Size(242, 32);
            L_AddressList.TabIndex = 13;
            L_AddressList.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // L_IP
            // 
            L_IP.Location = new Point(12, 555);
            L_IP.Name = "L_IP";
            L_IP.Size = new Size(134, 32);
            L_IP.TabIndex = 15;
            L_IP.Text = "IP  :";
            L_IP.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // L_IP_Now
            // 
            L_IP_Now.AutoEllipsis = true;
            L_IP_Now.Location = new Point(166, 555);
            L_IP_Now.Name = "L_IP_Now";
            L_IP_Now.Size = new Size(242, 32);
            L_IP_Now.TabIndex = 16;
            L_IP_Now.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Device
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 761);
            Controls.Add(L_IP_Now);
            Controls.Add(L_IP);
            Controls.Add(L_AddressList);
            Controls.Add(L_AddAddressList);
            Controls.Add(b_AddAddressList);
            Controls.Add(b_RemoveAddressList);
            Controls.Add(b_RemoveIP);
            Controls.Add(L_TextBoard);
            Controls.Add(b_MakeStatic);
            Controls.Add(tB_Monitor);
            Controls.Add(L_AvailableAction);
            Controls.Add(comBox_AddAddressList);
            Controls.Add(b_ClearForm);
            Controls.Add(b_Find);
            Controls.Add(tB_Mac);
            Controls.Add(L_MAC);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "Device";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Device";
            FormClosed += Device_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label L_MAC;
        private TextBox tB_Mac;
        private Button b_Find;
        private Button b_ClearForm;
        private ComboBox comBox_AddAddressList;
        private Label L_AvailableAction;
        private TextBox tB_Monitor;
        private Button b_MakeStatic;
        private Label L_TextBoard;
        private Button b_RemoveIP;
        private Button b_RemoveAddressList;
        private Button b_AddAddressList;
        private Label L_AddAddressList;
        private Label L_AddressList;
        private Label L_IP;
        private Label L_IP_Now;
    }
}