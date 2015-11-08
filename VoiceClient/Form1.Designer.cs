namespace VoiceClient
{
    partial class Form1
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.LstView_Devices = new System.Windows.Forms.ListView();
            this.DeviceColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChannelColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Txt_ServAddress = new System.Windows.Forms.TextBox();
            this.Txt_ServPort = new System.Windows.Forms.TextBox();
            this.CmbBox_SampleRate = new System.Windows.Forms.ComboBox();
            this.Btn_Exit = new System.Windows.Forms.Button();
            this.Btn_Stop = new System.Windows.Forms.Button();
            this.Btn_Start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.LstView_Devices);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.Txt_ServAddress);
            this.splitContainer1.Panel2.Controls.Add(this.Txt_ServPort);
            this.splitContainer1.Panel2.Controls.Add(this.CmbBox_SampleRate);
            this.splitContainer1.Panel2.Controls.Add(this.Btn_Exit);
            this.splitContainer1.Panel2.Controls.Add(this.Btn_Stop);
            this.splitContainer1.Panel2.Controls.Add(this.Btn_Start);
            this.splitContainer1.Size = new System.Drawing.Size(584, 261);
            this.splitContainer1.SplitterDistance = 233;
            this.splitContainer1.TabIndex = 0;
            // 
            // LstView_Devices
            // 
            this.LstView_Devices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DeviceColumn,
            this.ChannelColumn});
            this.LstView_Devices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LstView_Devices.Location = new System.Drawing.Point(0, 0);
            this.LstView_Devices.Name = "LstView_Devices";
            this.LstView_Devices.Size = new System.Drawing.Size(233, 261);
            this.LstView_Devices.TabIndex = 0;
            this.LstView_Devices.UseCompatibleStateImageBehavior = false;
            this.LstView_Devices.View = System.Windows.Forms.View.Details;
            // 
            // DeviceColumn
            // 
            this.DeviceColumn.Text = "Device";
            this.DeviceColumn.Width = 160;
            // 
            // ChannelColumn
            // 
            this.ChannelColumn.Text = "Channel";
            // 
            // Txt_ServAddress
            // 
            this.Txt_ServAddress.Location = new System.Drawing.Point(3, 203);
            this.Txt_ServAddress.Name = "Txt_ServAddress";
            this.Txt_ServAddress.Size = new System.Drawing.Size(128, 20);
            this.Txt_ServAddress.TabIndex = 13;
            // 
            // Txt_ServPort
            // 
            this.Txt_ServPort.Location = new System.Drawing.Point(3, 229);
            this.Txt_ServPort.Name = "Txt_ServPort";
            this.Txt_ServPort.Size = new System.Drawing.Size(128, 20);
            this.Txt_ServPort.TabIndex = 12;
            // 
            // CmbBox_SampleRate
            // 
            this.CmbBox_SampleRate.FormattingEnabled = true;
            this.CmbBox_SampleRate.Items.AddRange(new object[] {
            "1000",
            "2000",
            "4000",
            "8000",
            "11025",
            "22050",
            "32000",
            "44100",
            "48000"});
            this.CmbBox_SampleRate.Location = new System.Drawing.Point(3, 12);
            this.CmbBox_SampleRate.Name = "CmbBox_SampleRate";
            this.CmbBox_SampleRate.Size = new System.Drawing.Size(128, 21);
            this.CmbBox_SampleRate.TabIndex = 11;
            // 
            // Btn_Exit
            // 
            this.Btn_Exit.Location = new System.Drawing.Point(3, 97);
            this.Btn_Exit.Name = "Btn_Exit";
            this.Btn_Exit.Size = new System.Drawing.Size(128, 23);
            this.Btn_Exit.TabIndex = 10;
            this.Btn_Exit.Text = "Exit";
            this.Btn_Exit.UseVisualStyleBackColor = true;
            this.Btn_Exit.Click += new System.EventHandler(this.Btn_Exit_Click);
            // 
            // Btn_Stop
            // 
            this.Btn_Stop.Location = new System.Drawing.Point(3, 68);
            this.Btn_Stop.Name = "Btn_Stop";
            this.Btn_Stop.Size = new System.Drawing.Size(128, 23);
            this.Btn_Stop.TabIndex = 8;
            this.Btn_Stop.Text = "Stop";
            this.Btn_Stop.UseVisualStyleBackColor = true;
            this.Btn_Stop.Click += new System.EventHandler(this.Btn_Stop_Click_1);
            // 
            // Btn_Start
            // 
            this.Btn_Start.Location = new System.Drawing.Point(3, 39);
            this.Btn_Start.Name = "Btn_Start";
            this.Btn_Start.Size = new System.Drawing.Size(128, 23);
            this.Btn_Start.TabIndex = 7;
            this.Btn_Start.Text = "Start";
            this.Btn_Start.UseVisualStyleBackColor = true;
            this.Btn_Start.Click += new System.EventHandler(this.Btn_Start_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(137, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "Sample Rate";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(137, 232);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(137, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "IP Address";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button Btn_Stop;
        private System.Windows.Forms.Button Btn_Start;
        private System.Windows.Forms.ListView LstView_Devices;
        private System.Windows.Forms.ColumnHeader DeviceColumn;
        private System.Windows.Forms.ColumnHeader ChannelColumn;
        private System.Windows.Forms.Button Btn_Exit;
        private System.Windows.Forms.ComboBox CmbBox_SampleRate;
        private System.Windows.Forms.TextBox Txt_ServAddress;
        private System.Windows.Forms.TextBox Txt_ServPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

