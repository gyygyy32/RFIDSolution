namespace UHFDesk
{
    partial class UHFWriteTag
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
            this.tbx_SerialWrite = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lrtxtLog
            // 
            this.lrtxtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lrtxtLog.Location = new System.Drawing.Point(0, 376);
            this.lrtxtLog.Name = "lrtxtLog";
            this.lrtxtLog.Size = new System.Drawing.Size(728, 132);
            this.lrtxtLog.TabIndex = 2;
            this.lrtxtLog.Text = "";
            // 
            // ckClearOperationRec
            // 
            this.ckClearOperationRec.AutoSize = true;
            this.ckClearOperationRec.Checked = true;
            this.ckClearOperationRec.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckClearOperationRec.Location = new System.Drawing.Point(81, 348);
            this.ckClearOperationRec.Name = "ckClearOperationRec";
            this.ckClearOperationRec.Size = new System.Drawing.Size(72, 16);
            this.ckClearOperationRec.TabIndex = 21;
            this.ckClearOperationRec.Text = "自动清空";
            this.ckClearOperationRec.UseVisualStyleBackColor = true;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(12, 349);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(59, 12);
            this.label35.TabIndex = 20;
            this.label35.Text = "操作记录:";
            // 
            // tbx_SerialWrite
            // 
            this.tbx_SerialWrite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.tbx_SerialWrite.Font = new System.Drawing.Font("SimSun", 20F);
            this.tbx_SerialWrite.Location = new System.Drawing.Point(242, 126);
            this.tbx_SerialWrite.Name = "tbx_SerialWrite";
            this.tbx_SerialWrite.Size = new System.Drawing.Size(362, 38);
            this.tbx_SerialWrite.TabIndex = 23;
            this.tbx_SerialWrite.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbx_SerialWrite_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 16F);
            this.label3.Location = new System.Drawing.Point(116, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 22);
            this.label3.TabIndex = 22;
            this.label3.Text = "组件序列号";
            // 
            // WriteTag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 508);
            this.Controls.Add(this.tbx_SerialWrite);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ckClearOperationRec);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.lrtxtLog);
            this.Name = "WriteTag";
            this.Text = "WriteTag";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControl.LogRichTextBox lrtxtLog;
        private System.Windows.Forms.CheckBox ckClearOperationRec;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox tbx_SerialWrite;
        private System.Windows.Forms.Label label3;
    }
}