namespace UHFDesk
{
    partial class UHFDeskMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.标签读写ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_writeTag = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_readTag = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_about = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_register = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.标签读写ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(889, 25);
            this.menuStrip1.TabIndex = 23;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 标签读写ToolStripMenuItem
            // 
            this.标签读写ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_writeTag,
            this.menu_readTag});
            this.标签读写ToolStripMenuItem.Name = "标签读写ToolStripMenuItem";
            this.标签读写ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.标签读写ToolStripMenuItem.Text = "标签读写";
            // 
            // menu_writeTag
            // 
            this.menu_writeTag.Name = "menu_writeTag";
            this.menu_writeTag.Size = new System.Drawing.Size(152, 22);
            this.menu_writeTag.Text = "写标签";
            this.menu_writeTag.Click += new System.EventHandler(this.menu_writeTag_Click);
            // 
            // menu_readTag
            // 
            this.menu_readTag.Name = "menu_readTag";
            this.menu_readTag.Size = new System.Drawing.Size(152, 22);
            this.menu_readTag.Text = "读标签";
            this.menu_readTag.Visible = false;
            this.menu_readTag.Click += new System.EventHandler(this.menu_readTag_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_about,
            this.menu_register});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // menu_about
            // 
            this.menu_about.Name = "menu_about";
            this.menu_about.Size = new System.Drawing.Size(152, 22);
            this.menu_about.Text = "关于";
            this.menu_about.Visible = false;
            this.menu_about.Click += new System.EventHandler(this.menu_about_Click);
            // 
            // menu_register
            // 
            this.menu_register.Name = "menu_register";
            this.menu_register.Size = new System.Drawing.Size(152, 22);
            this.menu_register.Text = "注册";
            this.menu_register.Visible = false;
            this.menu_register.Click += new System.EventHandler(this.menu_register_Click);
            // 
            // UHFDesk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 540);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.Name = "UHFDesk";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "超高频RFID读写程序";
            this.Load += new System.EventHandler(this.RFIDeskUHF_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 标签读写ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_writeTag;
        private System.Windows.Forms.ToolStripMenuItem menu_readTag;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_about;
        private System.Windows.Forms.ToolStripMenuItem menu_register;


    }
}

