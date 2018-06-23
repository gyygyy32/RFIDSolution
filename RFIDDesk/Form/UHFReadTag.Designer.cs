namespace UHFDesk
{
    partial class UHFReadTag
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
            this.btn_ReadTag = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lrtxtLog
            // 
            this.lrtxtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lrtxtLog.Location = new System.Drawing.Point(0, 405);
            this.lrtxtLog.Name = "lrtxtLog";
            this.lrtxtLog.Size = new System.Drawing.Size(750, 132);
            this.lrtxtLog.TabIndex = 2;
            this.lrtxtLog.Text = "";
            // 
            // ckClearOperationRec
            // 
            this.ckClearOperationRec.AutoSize = true;
            this.ckClearOperationRec.Checked = true;
            this.ckClearOperationRec.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckClearOperationRec.Location = new System.Drawing.Point(81, 378);
            this.ckClearOperationRec.Name = "ckClearOperationRec";
            this.ckClearOperationRec.Size = new System.Drawing.Size(72, 16);
            this.ckClearOperationRec.TabIndex = 21;
            this.ckClearOperationRec.Text = "自动清空";
            this.ckClearOperationRec.UseVisualStyleBackColor = true;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(12, 379);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(59, 12);
            this.label35.TabIndex = 20;
            this.label35.Text = "操作记录:";
            // 
            // btn_ReadTag
            // 
            this.btn_ReadTag.Location = new System.Drawing.Point(569, 45);
            this.btn_ReadTag.Name = "btn_ReadTag";
            this.btn_ReadTag.Size = new System.Drawing.Size(75, 23);
            this.btn_ReadTag.TabIndex = 22;
            this.btn_ReadTag.Text = "读标签";
            this.btn_ReadTag.UseVisualStyleBackColor = true;
            this.btn_ReadTag.Click += new System.EventHandler(this.btn_ReadTag_Click);
            // 
            // ReadTag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 537);
            this.Controls.Add(this.btn_ReadTag);
            this.Controls.Add(this.ckClearOperationRec);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.lrtxtLog);
            this.Name = "ReadTag";
            this.Text = "ReadTag";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControl.LogRichTextBox lrtxtLog;
        private System.Windows.Forms.CheckBox ckClearOperationRec;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Button btn_ReadTag;
    }
}