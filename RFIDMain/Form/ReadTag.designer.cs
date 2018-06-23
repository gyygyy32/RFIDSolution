namespace RFIDMain
{
    partial class ReadTag
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
            this.lrtxtLog = new CustomControl.LogRichTextBox();
            this.ckClearOperationRec = new System.Windows.Forms.CheckBox();
            this.label35 = new System.Windows.Forms.Label();
            this.tbx_serial = new System.Windows.Forms.TextBox();
            this.btn_readTag = new System.Windows.Forms.Button();
            this.tbx_read_vpm = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbx_read_ipm = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbx_read_pmax = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ivCurves1 = new CustomControl.IVCurves();
            this.SuspendLayout();
            // 
            // lrtxtLog
            // 
            this.lrtxtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lrtxtLog.Location = new System.Drawing.Point(0, 435);
            this.lrtxtLog.Margin = new System.Windows.Forms.Padding(4);
            this.lrtxtLog.Name = "lrtxtLog";
            this.lrtxtLog.Size = new System.Drawing.Size(1028, 164);
            this.lrtxtLog.TabIndex = 3;
            this.lrtxtLog.Text = "";
            // 
            // ckClearOperationRec
            // 
            this.ckClearOperationRec.AutoSize = true;
            this.ckClearOperationRec.Checked = true;
            this.ckClearOperationRec.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckClearOperationRec.Location = new System.Drawing.Point(108, 398);
            this.ckClearOperationRec.Margin = new System.Windows.Forms.Padding(4);
            this.ckClearOperationRec.Name = "ckClearOperationRec";
            this.ckClearOperationRec.Size = new System.Drawing.Size(89, 19);
            this.ckClearOperationRec.TabIndex = 23;
            this.ckClearOperationRec.Text = "自动清空";
            this.ckClearOperationRec.UseVisualStyleBackColor = true;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(16, 399);
            this.label35.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(75, 15);
            this.label35.TabIndex = 22;
            this.label35.Text = "操作记录:";
            // 
            // tbx_serial
            // 
            this.tbx_serial.Location = new System.Drawing.Point(164, 78);
            this.tbx_serial.Margin = new System.Windows.Forms.Padding(4);
            this.tbx_serial.Name = "tbx_serial";
            this.tbx_serial.ReadOnly = true;
            this.tbx_serial.Size = new System.Drawing.Size(453, 25);
            this.tbx_serial.TabIndex = 32;
            // 
            // btn_readTag
            // 
            this.btn_readTag.Location = new System.Drawing.Point(164, 26);
            this.btn_readTag.Margin = new System.Windows.Forms.Padding(4);
            this.btn_readTag.Name = "btn_readTag";
            this.btn_readTag.Size = new System.Drawing.Size(100, 29);
            this.btn_readTag.TabIndex = 31;
            this.btn_readTag.Text = "读取";
            this.btn_readTag.UseVisualStyleBackColor = true;
            this.btn_readTag.Click += new System.EventHandler(this.btn_readTag_Click);
            // 
            // tbx_read_vpm
            // 
            this.tbx_read_vpm.Location = new System.Drawing.Point(164, 239);
            this.tbx_read_vpm.Margin = new System.Windows.Forms.Padding(4);
            this.tbx_read_vpm.Name = "tbx_read_vpm";
            this.tbx_read_vpm.ReadOnly = true;
            this.tbx_read_vpm.Size = new System.Drawing.Size(453, 25);
            this.tbx_read_vpm.TabIndex = 30;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(71, 244);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 15);
            this.label10.TabIndex = 29;
            this.label10.Text = "VPM";
            // 
            // tbx_read_ipm
            // 
            this.tbx_read_ipm.Location = new System.Drawing.Point(164, 182);
            this.tbx_read_ipm.Margin = new System.Windows.Forms.Padding(4);
            this.tbx_read_ipm.Name = "tbx_read_ipm";
            this.tbx_read_ipm.ReadOnly = true;
            this.tbx_read_ipm.Size = new System.Drawing.Size(453, 25);
            this.tbx_read_ipm.TabIndex = 28;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(71, 188);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 15);
            this.label9.TabIndex = 27;
            this.label9.Text = "IPM";
            // 
            // tbx_read_pmax
            // 
            this.tbx_read_pmax.Location = new System.Drawing.Point(164, 132);
            this.tbx_read_pmax.Margin = new System.Windows.Forms.Padding(4);
            this.tbx_read_pmax.Name = "tbx_read_pmax";
            this.tbx_read_pmax.ReadOnly = true;
            this.tbx_read_pmax.Size = new System.Drawing.Size(453, 25);
            this.tbx_read_pmax.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(63, 138);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 15);
            this.label8.TabIndex = 25;
            this.label8.Text = "PMAX";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 81);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 15);
            this.label7.TabIndex = 24;
            this.label7.Text = "组件序列号";
            // 
            // ivCurves1
            // 
            this.ivCurves1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ivCurves1.Location = new System.Drawing.Point(654, 78);
            this.ivCurves1.Margin = new System.Windows.Forms.Padding(4);
            this.ivCurves1.Name = "ivCurves1";
            this.ivCurves1.Size = new System.Drawing.Size(331, 272);
            this.ivCurves1.TabIndex = 33;
            this.ivCurves1.Text = "ivCurves1";
            // 
            // ReadTag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 599);
            this.Controls.Add(this.ivCurves1);
            this.Controls.Add(this.tbx_serial);
            this.Controls.Add(this.btn_readTag);
            this.Controls.Add(this.tbx_read_vpm);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tbx_read_ipm);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbx_read_pmax);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ckClearOperationRec);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.lrtxtLog);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ReadTag";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "读标签";
            this.Load += new System.EventHandler(this.ReadTag_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControl.LogRichTextBox lrtxtLog;
        private System.Windows.Forms.CheckBox ckClearOperationRec;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox tbx_serial;
        private System.Windows.Forms.Button btn_readTag;
        private System.Windows.Forms.TextBox tbx_read_vpm;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbx_read_ipm;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbx_read_pmax;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private CustomControl.IVCurves ivCurves1;
    }
}