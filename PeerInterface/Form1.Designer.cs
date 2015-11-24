namespace PeerInterface
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
            this.Txt_Username = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.LstView_Peers = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LstView_Requests = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LstView_Friends = new System.Windows.Forms.ListView();
            this.columnPeer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.loginPage = new System.Windows.Forms.TabPage();
            this.Txt_Password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Btn_Connect = new System.Windows.Forms.Button();
            this.Txt_Address = new System.Windows.Forms.TextBox();
            this.commPage = new System.Windows.Forms.TabPage();
            this.registerPage = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.loginPage.SuspendLayout();
            this.registerPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // Txt_Username
            // 
            this.Txt_Username.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Username.Location = new System.Drawing.Point(88, 6);
            this.Txt_Username.Name = "Txt_Username";
            this.Txt_Username.Size = new System.Drawing.Size(183, 22);
            this.Txt_Username.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Username";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.LstView_Peers);
            this.splitContainer1.Panel1.Controls.Add(this.LstView_Requests);
            this.splitContainer1.Panel1.Controls.Add(this.LstView_Friends);
            this.splitContainer1.Panel1MinSize = 200;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(584, 301);
            this.splitContainer1.SplitterDistance = 210;
            this.splitContainer1.TabIndex = 2;
            // 
            // LstView_Peers
            // 
            this.LstView_Peers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.LstView_Peers.Dock = System.Windows.Forms.DockStyle.Top;
            this.LstView_Peers.Location = new System.Drawing.Point(0, 200);
            this.LstView_Peers.Name = "LstView_Peers";
            this.LstView_Peers.Size = new System.Drawing.Size(210, 97);
            this.LstView_Peers.TabIndex = 2;
            this.LstView_Peers.UseCompatibleStateImageBehavior = false;
            this.LstView_Peers.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Connected Peers";
            this.columnHeader2.Width = 200;
            // 
            // LstView_Requests
            // 
            this.LstView_Requests.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.LstView_Requests.Dock = System.Windows.Forms.DockStyle.Top;
            this.LstView_Requests.Location = new System.Drawing.Point(0, 103);
            this.LstView_Requests.Name = "LstView_Requests";
            this.LstView_Requests.Size = new System.Drawing.Size(210, 97);
            this.LstView_Requests.TabIndex = 1;
            this.LstView_Requests.UseCompatibleStateImageBehavior = false;
            this.LstView_Requests.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Friend Requests";
            this.columnHeader1.Width = 200;
            // 
            // LstView_Friends
            // 
            this.LstView_Friends.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPeer});
            this.LstView_Friends.Dock = System.Windows.Forms.DockStyle.Top;
            this.LstView_Friends.Location = new System.Drawing.Point(0, 0);
            this.LstView_Friends.Name = "LstView_Friends";
            this.LstView_Friends.Size = new System.Drawing.Size(210, 103);
            this.LstView_Friends.TabIndex = 0;
            this.LstView_Friends.UseCompatibleStateImageBehavior = false;
            this.LstView_Friends.View = System.Windows.Forms.View.Details;
            // 
            // columnPeer
            // 
            this.columnPeer.Text = "Friends";
            this.columnPeer.Width = 200;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.tabControl1);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(370, 301);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.registerPage);
            this.tabControl1.Controls.Add(this.loginPage);
            this.tabControl1.Controls.Add(this.commPage);
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(364, 294);
            this.tabControl1.TabIndex = 0;
            // 
            // loginPage
            // 
            this.loginPage.Controls.Add(this.Txt_Password);
            this.loginPage.Controls.Add(this.label3);
            this.loginPage.Controls.Add(this.Btn_Connect);
            this.loginPage.Controls.Add(this.label1);
            this.loginPage.Controls.Add(this.Txt_Username);
            this.loginPage.Controls.Add(this.Txt_Address);
            this.loginPage.Location = new System.Drawing.Point(4, 22);
            this.loginPage.Name = "loginPage";
            this.loginPage.Padding = new System.Windows.Forms.Padding(3);
            this.loginPage.Size = new System.Drawing.Size(356, 268);
            this.loginPage.TabIndex = 0;
            this.loginPage.Text = "Login";
            this.loginPage.UseVisualStyleBackColor = true;
            // 
            // Txt_Password
            // 
            this.Txt_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Password.Location = new System.Drawing.Point(88, 34);
            this.Txt_Password.Name = "Txt_Password";
            this.Txt_Password.Size = new System.Drawing.Size(183, 22);
            this.Txt_Password.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password";
            // 
            // Btn_Connect
            // 
            this.Btn_Connect.Location = new System.Drawing.Point(6, 78);
            this.Btn_Connect.Name = "Btn_Connect";
            this.Btn_Connect.Size = new System.Drawing.Size(265, 23);
            this.Btn_Connect.TabIndex = 0;
            this.Btn_Connect.Text = "Login";
            this.Btn_Connect.UseVisualStyleBackColor = true;
            this.Btn_Connect.Click += new System.EventHandler(this.Btn_Connect_Click);
            // 
            // Txt_Address
            // 
            this.Txt_Address.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Address.Location = new System.Drawing.Point(6, 240);
            this.Txt_Address.Name = "Txt_Address";
            this.Txt_Address.ReadOnly = true;
            this.Txt_Address.Size = new System.Drawing.Size(183, 22);
            this.Txt_Address.TabIndex = 3;
            // 
            // commPage
            // 
            this.commPage.Location = new System.Drawing.Point(4, 22);
            this.commPage.Name = "commPage";
            this.commPage.Padding = new System.Windows.Forms.Padding(3);
            this.commPage.Size = new System.Drawing.Size(356, 268);
            this.commPage.TabIndex = 1;
            this.commPage.Text = "Communicate";
            this.commPage.UseVisualStyleBackColor = true;
            // 
            // registerPage
            // 
            this.registerPage.Controls.Add(this.textBox1);
            this.registerPage.Location = new System.Drawing.Point(4, 22);
            this.registerPage.Name = "registerPage";
            this.registerPage.Padding = new System.Windows.Forms.Padding(3);
            this.registerPage.Size = new System.Drawing.Size(356, 268);
            this.registerPage.TabIndex = 2;
            this.registerPage.Text = "Register";
            this.registerPage.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(60, 93);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 301);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.loginPage.ResumeLayout(false);
            this.loginPage.PerformLayout();
            this.registerPage.ResumeLayout(false);
            this.registerPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox Txt_Username;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView LstView_Friends;
        private System.Windows.Forms.ColumnHeader columnPeer;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox Txt_Address;
        private System.Windows.Forms.Button Btn_Connect;
        private System.Windows.Forms.ListView LstView_Peers;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView LstView_Requests;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage loginPage;
        private System.Windows.Forms.TabPage commPage;
        private System.Windows.Forms.TextBox Txt_Password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage registerPage;
        private System.Windows.Forms.TextBox textBox1;
    }
}

