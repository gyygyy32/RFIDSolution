namespace RFIDMain
{
    partial class WriteTag
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
            this.tbx_SerialWrite = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ckClearOperationRec = new System.Windows.Forms.CheckBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtCloseReader = new System.Windows.Forms.Button();
            this.BtRead = new System.Windows.Forms.Button();
            this.tbx_status = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbx_ff = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbx_celldate = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbx_packdate = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbx_ipm = new System.Windows.Forms.TextBox();
            this.tbx_vpm = new System.Windows.Forms.TextBox();
            this.tbx_isc = new System.Windows.Forms.TextBox();
            this.tbx_voc = new System.Windows.Forms.TextBox();
            this.tbx_pmax = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_prodtype = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkbox_burningTag = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rb_UHF = new System.Windows.Forms.RadioButton();
            this.rb_HF = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ivCurves1 = new CustomControl.IVCurves();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lrtxtLog = new CustomControl.LogRichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbx_SerialWrite
            // 
            this.tbx_SerialWrite.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbx_SerialWrite.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.tbx_SerialWrite.Font = new System.Drawing.Font("宋体", 20F);
            this.tbx_SerialWrite.Location = new System.Drawing.Point(181, 21);
            this.tbx_SerialWrite.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbx_SerialWrite.Name = "tbx_SerialWrite";
            this.tbx_SerialWrite.Size = new System.Drawing.Size(367, 46);
            this.tbx_SerialWrite.TabIndex = 5;
            this.tbx_SerialWrite.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbx_SerialWrite_KeyDown);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 16F);
            this.label3.Location = new System.Drawing.Point(13, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 27);
            this.label3.TabIndex = 4;
            this.label3.Text = "组件序列号";
            // 
            // ckClearOperationRec
            // 
            this.ckClearOperationRec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ckClearOperationRec.AutoSize = true;
            this.ckClearOperationRec.Checked = true;
            this.ckClearOperationRec.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckClearOperationRec.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.ckClearOperationRec.Location = new System.Drawing.Point(103, 5);
            this.ckClearOperationRec.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckClearOperationRec.Name = "ckClearOperationRec";
            this.ckClearOperationRec.Size = new System.Drawing.Size(91, 24);
            this.ckClearOperationRec.TabIndex = 23;
            this.ckClearOperationRec.Text = "自动清空";
            this.ckClearOperationRec.UseVisualStyleBackColor = true;
            // 
            // label35
            // 
            this.label35.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label35.Location = new System.Drawing.Point(11, 5);
            this.label35.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(73, 20);
            this.label35.TabIndex = 22;
            this.label35.Text = "操作记录:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(8, 88);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "Pmax";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtCloseReader);
            this.groupBox1.Controls.Add(this.BtRead);
            this.groupBox1.Controls.Add(this.tbx_status);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.tbx_ff);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.tbx_celldate);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tbx_packdate);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tbx_ipm);
            this.groupBox1.Controls.Add(this.tbx_vpm);
            this.groupBox1.Controls.Add(this.tbx_isc);
            this.groupBox1.Controls.Add(this.tbx_voc);
            this.groupBox1.Controls.Add(this.tbx_pmax);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 156);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(741, 443);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // BtCloseReader
            // 
            this.BtCloseReader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.BtCloseReader.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtCloseReader.ForeColor = System.Drawing.Color.Blue;
            this.BtCloseReader.Location = new System.Drawing.Point(21, 410);
            this.BtCloseReader.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtCloseReader.Name = "BtCloseReader";
            this.BtCloseReader.Size = new System.Drawing.Size(141, 30);
            this.BtCloseReader.TabIndex = 54;
            this.BtCloseReader.Text = "断开读写器(&T)";
            this.BtCloseReader.UseVisualStyleBackColor = false;
            this.BtCloseReader.Click += new System.EventHandler(this.BtCloseReader_Click);
            // 
            // BtRead
            // 
            this.BtRead.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.BtRead.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtRead.ForeColor = System.Drawing.Color.Blue;
            this.BtRead.Location = new System.Drawing.Point(604, 406);
            this.BtRead.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtRead.Name = "BtRead";
            this.BtRead.Size = new System.Drawing.Size(131, 30);
            this.BtRead.TabIndex = 53;
            this.BtRead.Text = "读取 (&F)";
            this.BtRead.UseVisualStyleBackColor = false;
            this.BtRead.Click += new System.EventHandler(this.BtRead_Click);
            // 
            // tbx_status
            // 
            this.tbx_status.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tbx_status.Location = new System.Drawing.Point(73, 32);
            this.tbx_status.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbx_status.Name = "tbx_status";
            this.tbx_status.ReadOnly = true;
            this.tbx_status.Size = new System.Drawing.Size(145, 27);
            this.tbx_status.TabIndex = 52;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(13, 35);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(39, 20);
            this.label16.TabIndex = 51;
            this.label16.Text = "状态";
            // 
            // textBox5
            // 
            this.textBox5.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBox5.Location = new System.Drawing.Point(381, 188);
            this.textBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(279, 27);
            this.textBox5.TabIndex = 50;
            this.textBox5.Text = "China";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label15.Location = new System.Drawing.Point(267, 191);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 20);
            this.label15.TabIndex = 49;
            this.label15.Text = "生产国家";
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBox4.Location = new System.Drawing.Point(381, 344);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(279, 27);
            this.textBox4.TabIndex = 48;
            this.textBox4.Text = "IEC TUV";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Location = new System.Drawing.Point(251, 348);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 20);
            this.label14.TabIndex = 47;
            this.label14.Text = "ISO9001认证";
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBox3.Location = new System.Drawing.Point(381, 292);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(279, 27);
            this.textBox3.TabIndex = 46;
            this.textBox3.Text = "TUV SUD Product Service GmbH";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label13.Location = new System.Drawing.Point(259, 295);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 20);
            this.label13.TabIndex = 45;
            this.label13.Text = "证书颁发者";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBox1.Location = new System.Drawing.Point(381, 240);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(279, 27);
            this.textBox1.TabIndex = 44;
            this.textBox1.Text = "March 2016";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label11.Location = new System.Drawing.Point(256, 242);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 20);
            this.label11.TabIndex = 43;
            this.label11.Text = "IEC证书日期";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.textBox2.Location = new System.Drawing.Point(381, 32);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(279, 27);
            this.textBox2.TabIndex = 42;
            this.textBox2.Text = "RISEN ENERGYCO.,LTD";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label12.Location = new System.Drawing.Point(249, 35);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(105, 20);
            this.label12.TabIndex = 41;
            this.label12.Text = "组件/电池厂商";
            // 
            // tbx_ff
            // 
            this.tbx_ff.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tbx_ff.Location = new System.Drawing.Point(73, 344);
            this.tbx_ff.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbx_ff.Name = "tbx_ff";
            this.tbx_ff.ReadOnly = true;
            this.tbx_ff.Size = new System.Drawing.Size(145, 27);
            this.tbx_ff.TabIndex = 40;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label10.Location = new System.Drawing.Point(20, 348);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 20);
            this.label10.TabIndex = 39;
            this.label10.Text = "FF";
            // 
            // tbx_celldate
            // 
            this.tbx_celldate.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tbx_celldate.Location = new System.Drawing.Point(381, 136);
            this.tbx_celldate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbx_celldate.Name = "tbx_celldate";
            this.tbx_celldate.ReadOnly = true;
            this.tbx_celldate.Size = new System.Drawing.Size(279, 27);
            this.tbx_celldate.TabIndex = 38;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label9.Location = new System.Drawing.Point(252, 139);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 20);
            this.label9.TabIndex = 37;
            this.label9.Text = "电池生产日期";
            // 
            // tbx_packdate
            // 
            this.tbx_packdate.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tbx_packdate.Location = new System.Drawing.Point(381, 84);
            this.tbx_packdate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbx_packdate.Name = "tbx_packdate";
            this.tbx_packdate.ReadOnly = true;
            this.tbx_packdate.Size = new System.Drawing.Size(279, 27);
            this.tbx_packdate.TabIndex = 36;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label8.Location = new System.Drawing.Point(252, 88);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 20);
            this.label8.TabIndex = 35;
            this.label8.Text = "组件生产日期";
            // 
            // tbx_ipm
            // 
            this.tbx_ipm.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tbx_ipm.Location = new System.Drawing.Point(73, 292);
            this.tbx_ipm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbx_ipm.Name = "tbx_ipm";
            this.tbx_ipm.ReadOnly = true;
            this.tbx_ipm.Size = new System.Drawing.Size(145, 27);
            this.tbx_ipm.TabIndex = 32;
            // 
            // tbx_vpm
            // 
            this.tbx_vpm.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tbx_vpm.Location = new System.Drawing.Point(73, 240);
            this.tbx_vpm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbx_vpm.Name = "tbx_vpm";
            this.tbx_vpm.ReadOnly = true;
            this.tbx_vpm.Size = new System.Drawing.Size(145, 27);
            this.tbx_vpm.TabIndex = 31;
            // 
            // tbx_isc
            // 
            this.tbx_isc.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tbx_isc.Location = new System.Drawing.Point(73, 188);
            this.tbx_isc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbx_isc.Name = "tbx_isc";
            this.tbx_isc.ReadOnly = true;
            this.tbx_isc.Size = new System.Drawing.Size(145, 27);
            this.tbx_isc.TabIndex = 30;
            // 
            // tbx_voc
            // 
            this.tbx_voc.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tbx_voc.Location = new System.Drawing.Point(73, 136);
            this.tbx_voc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbx_voc.Name = "tbx_voc";
            this.tbx_voc.ReadOnly = true;
            this.tbx_voc.Size = new System.Drawing.Size(145, 27);
            this.tbx_voc.TabIndex = 29;
            // 
            // tbx_pmax
            // 
            this.tbx_pmax.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.tbx_pmax.Location = new System.Drawing.Point(73, 84);
            this.tbx_pmax.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbx_pmax.Name = "tbx_pmax";
            this.tbx_pmax.ReadOnly = true;
            this.tbx_pmax.Size = new System.Drawing.Size(145, 27);
            this.tbx_pmax.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label6.Location = new System.Drawing.Point(13, 295);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 20);
            this.label6.TabIndex = 27;
            this.label6.Text = "Ipm";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label5.Location = new System.Drawing.Point(11, 242);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 20);
            this.label5.TabIndex = 27;
            this.label5.Text = "Vpm";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label4.Location = new System.Drawing.Point(19, 191);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 20);
            this.label4.TabIndex = 27;
            this.label4.Text = "Isc";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label2.Location = new System.Drawing.Point(13, 139);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 20);
            this.label2.TabIndex = 27;
            this.label2.Text = "Voc";
            // 
            // tbx_prodtype
            // 
            this.tbx_prodtype.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.tbx_prodtype.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbx_prodtype.Location = new System.Drawing.Point(181, 96);
            this.tbx_prodtype.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbx_prodtype.Name = "tbx_prodtype";
            this.tbx_prodtype.ReadOnly = true;
            this.tbx_prodtype.Size = new System.Drawing.Size(367, 42);
            this.tbx_prodtype.TabIndex = 34;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 16F);
            this.label7.Location = new System.Drawing.Point(16, 102);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 27);
            this.label7.TabIndex = 33;
            this.label7.Text = "产品类型";
            // 
            // chkbox_burningTag
            // 
            this.chkbox_burningTag.AutoSize = true;
            this.chkbox_burningTag.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkbox_burningTag.Location = new System.Drawing.Point(588, 102);
            this.chkbox_burningTag.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkbox_burningTag.Name = "chkbox_burningTag";
            this.chkbox_burningTag.Size = new System.Drawing.Size(114, 31);
            this.chkbox_burningTag.TabIndex = 28;
            this.chkbox_burningTag.Text = "启用烧录";
            this.chkbox_burningTag.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rb_UHF);
            this.groupBox2.Controls.Add(this.rb_HF);
            this.groupBox2.Controls.Add(this.tbx_prodtype);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.chkbox_burningTag);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbx_SerialWrite);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(741, 156);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            // 
            // rb_UHF
            // 
            this.rb_UHF.AutoSize = true;
            this.rb_UHF.Location = new System.Drawing.Point(588, 66);
            this.rb_UHF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rb_UHF.Name = "rb_UHF";
            this.rb_UHF.Size = new System.Drawing.Size(73, 19);
            this.rb_UHF.TabIndex = 36;
            this.rb_UHF.Text = "超高频";
            this.rb_UHF.UseVisualStyleBackColor = true;
            this.rb_UHF.CheckedChanged += new System.EventHandler(this.rb_UHF_CheckedChanged);
            // 
            // rb_HF
            // 
            this.rb_HF.AutoSize = true;
            this.rb_HF.Location = new System.Drawing.Point(588, 31);
            this.rb_HF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rb_HF.Name = "rb_HF";
            this.rb_HF.Size = new System.Drawing.Size(58, 19);
            this.rb_HF.TabIndex = 35;
            this.rb_HF.Text = "高频";
            this.rb_HF.UseVisualStyleBackColor = true;
            this.rb_HF.CheckedChanged += new System.EventHandler(this.rb_HF_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ivCurves1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(741, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(716, 599);
            this.groupBox3.TabIndex = 53;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "曲线";
            // 
            // ivCurves1
            // 
            this.ivCurves1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ivCurves1.Location = new System.Drawing.Point(3, 20);
            this.ivCurves1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ivCurves1.Name = "ivCurves1";
            this.ivCurves1.Size = new System.Drawing.Size(710, 577);
            this.ivCurves1.TabIndex = 25;
            this.ivCurves1.Text = "ivCurves1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lrtxtLog);
            this.groupBox4.Controls.Add(this.panel1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Location = new System.Drawing.Point(0, 599);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(1457, 271);
            this.groupBox4.TabIndex = 54;
            this.groupBox4.TabStop = false;
            // 
            // lrtxtLog
            // 
            this.lrtxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lrtxtLog.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lrtxtLog.Location = new System.Drawing.Point(3, 52);
            this.lrtxtLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lrtxtLog.Name = "lrtxtLog";
            this.lrtxtLog.Size = new System.Drawing.Size(1451, 217);
            this.lrtxtLog.TabIndex = 3;
            this.lrtxtLog.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ckClearOperationRec);
            this.panel1.Controls.Add(this.label35);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 20);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1451, 32);
            this.panel1.TabIndex = 24;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(741, 599);
            this.panel2.TabIndex = 55;
            // 
            // WriteTag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1457, 870);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox4);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "WriteTag";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "写标签";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.WriteTag_Load);
            this.Shown += new System.EventHandler(this.WriteTag_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControl.LogRichTextBox lrtxtLog;
        private System.Windows.Forms.TextBox tbx_SerialWrite;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ckClearOperationRec;
        private System.Windows.Forms.Label label35;
        private CustomControl.IVCurves ivCurves1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbx_ipm;
        private System.Windows.Forms.TextBox tbx_vpm;
        private System.Windows.Forms.TextBox tbx_isc;
        private System.Windows.Forms.TextBox tbx_voc;
        private System.Windows.Forms.TextBox tbx_pmax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbx_prodtype;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbx_packdate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbx_celldate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbx_ff;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkbox_burningTag;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbx_status;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rb_UHF;
        private System.Windows.Forms.RadioButton rb_HF;
        private System.Windows.Forms.Button BtRead;
        private System.Windows.Forms.Button BtCloseReader;
    }
}