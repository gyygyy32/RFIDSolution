namespace RFIDMain
{
    partial class MainDesk
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainDesk));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.标签读写ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_writeTag = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_readTag = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_about = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_register = new System.Windows.Forms.ToolStripMenuItem();
            this.配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uHFD10xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.标签读写ToolStripMenuItem,
            this.帮助ToolStripMenuItem,
            this.配置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1052, 28);
            this.menuStrip1.TabIndex = 22;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 标签读写ToolStripMenuItem
            // 
            this.标签读写ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_writeTag,
            this.menu_readTag});
            this.标签读写ToolStripMenuItem.Name = "标签读写ToolStripMenuItem";
            this.标签读写ToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.标签读写ToolStripMenuItem.Text = "标签读写";
            // 
            // menu_writeTag
            // 
            this.menu_writeTag.Name = "menu_writeTag";
            this.menu_writeTag.Size = new System.Drawing.Size(123, 24);
            this.menu_writeTag.Text = "写标签";
            this.menu_writeTag.Click += new System.EventHandler(this.menu_writeTag_Click);
            // 
            // menu_readTag
            // 
            this.menu_readTag.Name = "menu_readTag";
            this.menu_readTag.Size = new System.Drawing.Size(123, 24);
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
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // menu_about
            // 
            this.menu_about.Name = "menu_about";
            this.menu_about.Size = new System.Drawing.Size(108, 24);
            this.menu_about.Text = "关于";
            this.menu_about.Visible = false;
            this.menu_about.Click += new System.EventHandler(this.menu_about_Click);
            // 
            // menu_register
            // 
            this.menu_register.Name = "menu_register";
            this.menu_register.Size = new System.Drawing.Size(108, 24);
            this.menu_register.Text = "注册";
            this.menu_register.Click += new System.EventHandler(this.menu_register_Click);
            // 
            // 配置ToolStripMenuItem
            // 
            this.配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uHFD10xToolStripMenuItem});
            this.配置ToolStripMenuItem.Name = "配置ToolStripMenuItem";
            this.配置ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.配置ToolStripMenuItem.Text = "配置";
            // 
            // uHFD10xToolStripMenuItem
            // 
            this.uHFD10xToolStripMenuItem.Name = "uHFD10xToolStripMenuItem";
            this.uHFD10xToolStripMenuItem.Size = new System.Drawing.Size(156, 24);
            this.uHFD10xToolStripMenuItem.Text = "UHF D-10x";
            this.uHFD10xToolStripMenuItem.Click += new System.EventHandler(this.uHFD10xToolStripMenuItem_Click);
            // 
            // MainDesk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 681);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainDesk";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RFID读写程序";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RFIDMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_about;
        private System.Windows.Forms.ToolStripMenuItem 标签读写ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_writeTag;
        private System.Windows.Forms.ToolStripMenuItem menu_readTag;
        private System.Windows.Forms.ToolStripMenuItem menu_register;
        private System.Windows.Forms.ToolStripMenuItem 配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uHFD10xToolStripMenuItem;
    }
}

