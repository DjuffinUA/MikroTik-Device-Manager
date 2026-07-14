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
            menuStrip1 = new MenuStrip();
            systemToolStripMenuItem = new ToolStripMenuItem();
            identityToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            iPToolStripMenuItem = new ToolStripMenuItem();
            dHCPServerToolStripMenuItem = new ToolStripMenuItem();
            leasesToolStripMenuItem = new ToolStripMenuItem();
            fireToolStripMenuItem = new ToolStripMenuItem();
            adressListToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tB_Result
            // 
            tB_Result.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tB_Result.Location = new Point(12, 48);
            tB_Result.Multiline = true;
            tB_Result.Name = "tB_Result";
            tB_Result.ScrollBars = ScrollBars.Both;
            tB_Result.Size = new Size(1160, 701);
            tB_Result.TabIndex = 0;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { systemToolStripMenuItem, toolsToolStripMenuItem, iPToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1184, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // systemToolStripMenuItem
            // 
            systemToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { identityToolStripMenuItem });
            systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            systemToolStripMenuItem.Size = new Size(57, 20);
            systemToolStripMenuItem.Text = "System";
            // 
            // identityToolStripMenuItem
            // 
            identityToolStripMenuItem.Name = "identityToolStripMenuItem";
            identityToolStripMenuItem.Size = new Size(114, 22);
            identityToolStripMenuItem.Text = "Identity";
            identityToolStripMenuItem.Click += identityToolStripMenuItem_Click;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(47, 20);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // iPToolStripMenuItem
            // 
            iPToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { dHCPServerToolStripMenuItem, fireToolStripMenuItem });
            iPToolStripMenuItem.Name = "iPToolStripMenuItem";
            iPToolStripMenuItem.Size = new Size(29, 20);
            iPToolStripMenuItem.Text = "IP";
            // 
            // dHCPServerToolStripMenuItem
            // 
            dHCPServerToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { leasesToolStripMenuItem });
            dHCPServerToolStripMenuItem.Name = "dHCPServerToolStripMenuItem";
            dHCPServerToolStripMenuItem.Size = new Size(180, 22);
            dHCPServerToolStripMenuItem.Text = "DHCP Server";
            // 
            // leasesToolStripMenuItem
            // 
            leasesToolStripMenuItem.Name = "leasesToolStripMenuItem";
            leasesToolStripMenuItem.Size = new Size(108, 22);
            leasesToolStripMenuItem.Text = "Leases";
            leasesToolStripMenuItem.Click += leasesToolStripMenuItem_Click;
            // 
            // fireToolStripMenuItem
            // 
            fireToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { adressListToolStripMenuItem });
            fireToolStripMenuItem.Name = "fireToolStripMenuItem";
            fireToolStripMenuItem.Size = new Size(180, 22);
            fireToolStripMenuItem.Text = "Firewall";
            // 
            // adressListToolStripMenuItem
            // 
            adressListToolStripMenuItem.Name = "adressListToolStripMenuItem";
            adressListToolStripMenuItem.Size = new Size(180, 22);
            adressListToolStripMenuItem.Text = "Adress Lists";
            adressListToolStripMenuItem.Click += adressListToolStripMenuItem_Click;
            // 
            // DeviceManager
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1184, 761);
            Controls.Add(tB_Result);
            Controls.Add(menuStrip1);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "DeviceManager";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DeviceManager";
            FormClosed += DeviceManager_FormClosed;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tB_Result;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem systemToolStripMenuItem;
        private ToolStripMenuItem identityToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem iPToolStripMenuItem;
        private ToolStripMenuItem dHCPServerToolStripMenuItem;
        private ToolStripMenuItem leasesToolStripMenuItem;
        private ToolStripMenuItem fireToolStripMenuItem;
        private ToolStripMenuItem adressListToolStripMenuItem;
    }
}