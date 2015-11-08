namespace FileClient
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
            this.LstView_Files = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Btn_FolderLocation = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_IpAddress = new System.Windows.Forms.TextBox();
            this.Txt_Port = new System.Windows.Forms.TextBox();
            this.Btn_RequestFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.Btn_Connect = new System.Windows.Forms.Button();
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
            this.splitContainer1.Panel1.Controls.Add(this.LstView_Files);
            this.splitContainer1.Panel1MinSize = 100;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Btn_Connect);
            this.splitContainer1.Panel2.Controls.Add(this.Btn_FolderLocation);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.Txt_IpAddress);
            this.splitContainer1.Panel2.Controls.Add(this.Txt_Port);
            this.splitContainer1.Panel2.Controls.Add(this.Btn_RequestFile);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(584, 261);
            this.splitContainer1.SplitterDistance = 292;
            this.splitContainer1.TabIndex = 0;
            // 
            // LstView_Files
            // 
            this.LstView_Files.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.LstView_Files.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LstView_Files.Location = new System.Drawing.Point(0, 0);
            this.LstView_Files.Name = "LstView_Files";
            this.LstView_Files.Size = new System.Drawing.Size(292, 261);
            this.LstView_Files.TabIndex = 0;
            this.LstView_Files.UseCompatibleStateImageBehavior = false;
            this.LstView_Files.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Your Files";
            this.columnHeader1.Width = 194;
            // 
            // Btn_FolderLocation
            // 
            this.Btn_FolderLocation.Location = new System.Drawing.Point(2, 45);
            this.Btn_FolderLocation.Name = "Btn_FolderLocation";
            this.Btn_FolderLocation.Size = new System.Drawing.Size(100, 23);
            this.Btn_FolderLocation.TabIndex = 7;
            this.Btn_FolderLocation.Text = "Folder Location";
            this.Btn_FolderLocation.UseVisualStyleBackColor = true;
            this.Btn_FolderLocation.Click += new System.EventHandler(this.Btn_FolderLocation_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "IP Address";
            // 
            // Txt_IpAddress
            // 
            this.Txt_IpAddress.Location = new System.Drawing.Point(6, 203);
            this.Txt_IpAddress.Name = "Txt_IpAddress";
            this.Txt_IpAddress.Size = new System.Drawing.Size(100, 20);
            this.Txt_IpAddress.TabIndex = 3;
            // 
            // Txt_Port
            // 
            this.Txt_Port.Location = new System.Drawing.Point(6, 229);
            this.Txt_Port.Name = "Txt_Port";
            this.Txt_Port.Size = new System.Drawing.Size(100, 20);
            this.Txt_Port.TabIndex = 2;
            // 
            // Btn_RequestFile
            // 
            this.Btn_RequestFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.Btn_RequestFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_RequestFile.Location = new System.Drawing.Point(0, 0);
            this.Btn_RequestFile.Name = "Btn_RequestFile";
            this.Btn_RequestFile.Size = new System.Drawing.Size(288, 39);
            this.Btn_RequestFile.TabIndex = 1;
            this.Btn_RequestFile.Text = "Send File";
            this.Btn_RequestFile.UseVisualStyleBackColor = true;
            this.Btn_RequestFile.Click += new System.EventHandler(this.Btn_RequestFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Btn_Connect
            // 
            this.Btn_Connect.Location = new System.Drawing.Point(6, 174);
            this.Btn_Connect.Name = "Btn_Connect";
            this.Btn_Connect.Size = new System.Drawing.Size(100, 23);
            this.Btn_Connect.TabIndex = 8;
            this.Btn_Connect.Text = "Connect";
            this.Btn_Connect.UseVisualStyleBackColor = true;
            this.Btn_Connect.Click += new System.EventHandler(this.Btn_Connect_Click);
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
        private System.Windows.Forms.ListView LstView_Files;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button Btn_RequestFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Txt_IpAddress;
        private System.Windows.Forms.TextBox Txt_Port;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button Btn_FolderLocation;
        private System.Windows.Forms.Button Btn_Connect;
    }
}

