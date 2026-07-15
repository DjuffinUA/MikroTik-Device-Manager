namespace MikroTik_Device_Manager
{
    partial class DeviceManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceManager));
            tB_Result = new TextBox();
            b_RouterName = new Button();
            b_DHCP_ServerLeases = new Button();
            b_Firewall_AddressList = new Button();
            b_FindMAC = new Button();
            SuspendLayout();
            // 
            // tB_Result
            // 
            tB_Result.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tB_Result.Location = new Point(12, 78);
            tB_Result.Multiline = true;
            tB_Result.Name = "tB_Result";
            tB_Result.ReadOnly = true;
            tB_Result.ScrollBars = ScrollBars.Both;
            tB_Result.Size = new Size(1160, 671);
            tB_Result.TabIndex = 0;
            tB_Result.WordWrap = false;
            // 
            // b_RouterName
            // 
            b_RouterName.Location = new Point(12, 12);
            b_RouterName.Name = "b_RouterName";
            b_RouterName.Size = new Size(85, 60);
            b_RouterName.TabIndex = 2;
            b_RouterName.Text = "Router Name";
            b_RouterName.UseVisualStyleBackColor = true;
            b_RouterName.Click += b_RouterName_Click;
            // 
            // b_DHCP_ServerLeases
            // 
            b_DHCP_ServerLeases.Location = new Point(103, 12);
            b_DHCP_ServerLeases.Name = "b_DHCP_ServerLeases";
            b_DHCP_ServerLeases.Size = new Size(117, 60);
            b_DHCP_ServerLeases.TabIndex = 3;
            b_DHCP_ServerLeases.Text = "DHCP Server Leases";
            b_DHCP_ServerLeases.UseVisualStyleBackColor = true;
            b_DHCP_ServerLeases.Click += b_DHCP_ServerLeases_Click;
            // 
            // b_Firewall_AddressList
            // 
            b_Firewall_AddressList.Location = new Point(226, 12);
            b_Firewall_AddressList.Name = "b_Firewall_AddressList";
            b_Firewall_AddressList.Size = new Size(109, 60);
            b_Firewall_AddressList.TabIndex = 4;
            b_Firewall_AddressList.Text = "Firewall Address Lists";
            b_Firewall_AddressList.UseVisualStyleBackColor = true;
            b_Firewall_AddressList.Click += b_Firewall_AddressList_Click;
            // 
            // b_FindMAC
            // 
            b_FindMAC.Location = new Point(341, 12);
            b_FindMAC.Name = "b_FindMAC";
            b_FindMAC.Size = new Size(75, 60);
            b_FindMAC.TabIndex = 5;
            b_FindMAC.Text = "Find MAC";
            b_FindMAC.UseVisualStyleBackColor = true;
            b_FindMAC.Click += b_FindMAC_Click;
            // 
            // DeviceManager
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 761);
            Controls.Add(b_FindMAC);
            Controls.Add(b_Firewall_AddressList);
            Controls.Add(b_DHCP_ServerLeases);
            Controls.Add(b_RouterName);
            Controls.Add(tB_Result);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "DeviceManager";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DeviceManager";
            FormClosed += DeviceManager_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tB_Result;
        private Button b_RouterName;
        private Button b_DHCP_ServerLeases;
        private Button b_Firewall_AddressList;
        private Button b_FindMAC;
    }
}