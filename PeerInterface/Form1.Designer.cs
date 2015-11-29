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
            this.registerPage = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Txt_SuccessAck = new System.Windows.Forms.TextBox();
            this.Btn_RegUser = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Txt_RegServAddr = new System.Windows.Forms.TextBox();
            this.Txt_RegDobDay = new System.Windows.Forms.TextBox();
            this.Txt_RegDobMonth = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Txt_RegDobYear = new System.Windows.Forms.TextBox();
            this.Txt_RegPhoneNum = new System.Windows.Forms.TextBox();
            this.Txt_RegHomeAddr = new System.Windows.Forms.TextBox();
            this.Txt_RegEmail = new System.Windows.Forms.TextBox();
            this.Txt_RegName = new System.Windows.Forms.TextBox();
            this.Txt_RegsiterPassword = new System.Windows.Forms.TextBox();
            this.Txt_RegUsername = new System.Windows.Forms.TextBox();
            this.loginPage = new System.Windows.Forms.TabPage();
            this.Txt_AccountInfo = new System.Windows.Forms.TextBox();
            this.Txt_LoginServAddress = new System.Windows.Forms.TextBox();
            this.Txt_Password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Btn_Connect = new System.Windows.Forms.Button();
            this.Txt_Address = new System.Windows.Forms.TextBox();
            this.commPage = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.registerPage.SuspendLayout();
            this.loginPage.SuspendLayout();
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
            // registerPage
            // 
            this.registerPage.Controls.Add(this.label13);
            this.registerPage.Controls.Add(this.label12);
            this.registerPage.Controls.Add(this.label11);
            this.registerPage.Controls.Add(this.label10);
            this.registerPage.Controls.Add(this.label9);
            this.registerPage.Controls.Add(this.label8);
            this.registerPage.Controls.Add(this.label7);
            this.registerPage.Controls.Add(this.label6);
            this.registerPage.Controls.Add(this.label5);
            this.registerPage.Controls.Add(this.Txt_SuccessAck);
            this.registerPage.Controls.Add(this.Btn_RegUser);
            this.registerPage.Controls.Add(this.label4);
            this.registerPage.Controls.Add(this.Txt_RegServAddr);
            this.registerPage.Controls.Add(this.Txt_RegDobDay);
            this.registerPage.Controls.Add(this.Txt_RegDobMonth);
            this.registerPage.Controls.Add(this.label2);
            this.registerPage.Controls.Add(this.Txt_RegDobYear);
            this.registerPage.Controls.Add(this.Txt_RegPhoneNum);
            this.registerPage.Controls.Add(this.Txt_RegHomeAddr);
            this.registerPage.Controls.Add(this.Txt_RegEmail);
            this.registerPage.Controls.Add(this.Txt_RegName);
            this.registerPage.Controls.Add(this.Txt_RegsiterPassword);
            this.registerPage.Controls.Add(this.Txt_RegUsername);
            this.registerPage.Location = new System.Drawing.Point(4, 22);
            this.registerPage.Name = "registerPage";
            this.registerPage.Padding = new System.Windows.Forms.Padding(3);
            this.registerPage.Size = new System.Drawing.Size(356, 268);
            this.registerPage.TabIndex = 2;
            this.registerPage.Text = "Register";
            this.registerPage.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(218, 203);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(35, 18);
            this.label13.TabIndex = 22;
            this.label13.Text = "Day";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(113, 203);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 18);
            this.label12.TabIndex = 21;
            this.label12.Text = "Month";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 203);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 18);
            this.label11.TabIndex = 20;
            this.label11.Text = "Year";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(112, 138);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 18);
            this.label10.TabIndex = 19;
            this.label10.Text = "Home Phone";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(112, 112);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 18);
            this.label9.TabIndex = 18;
            this.label9.Text = "Home Address";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(112, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 18);
            this.label8.TabIndex = 17;
            this.label8.Text = "Email";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(112, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 18);
            this.label7.TabIndex = 16;
            this.label7.Text = "Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(112, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 18);
            this.label6.TabIndex = 15;
            this.label6.Text = "Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(112, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 18);
            this.label5.TabIndex = 14;
            this.label5.Text = "Username";
            // 
            // Txt_SuccessAck
            // 
            this.Txt_SuccessAck.Location = new System.Drawing.Point(250, 6);
            this.Txt_SuccessAck.Name = "Txt_SuccessAck";
            this.Txt_SuccessAck.ReadOnly = true;
            this.Txt_SuccessAck.Size = new System.Drawing.Size(100, 20);
            this.Txt_SuccessAck.TabIndex = 13;
            // 
            // Btn_RegUser
            // 
            this.Btn_RegUser.Location = new System.Drawing.Point(222, 241);
            this.Btn_RegUser.Name = "Btn_RegUser";
            this.Btn_RegUser.Size = new System.Drawing.Size(75, 23);
            this.Btn_RegUser.TabIndex = 12;
            this.Btn_RegUser.Text = "Register";
            this.Btn_RegUser.UseVisualStyleBackColor = true;
            this.Btn_RegUser.Click += new System.EventHandler(this.Btn_RegUser_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(112, 246);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 18);
            this.label4.TabIndex = 11;
            this.label4.Text = "Server Address";
            // 
            // Txt_RegServAddr
            // 
            this.Txt_RegServAddr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_RegServAddr.Location = new System.Drawing.Point(6, 242);
            this.Txt_RegServAddr.Name = "Txt_RegServAddr";
            this.Txt_RegServAddr.Size = new System.Drawing.Size(100, 22);
            this.Txt_RegServAddr.TabIndex = 10;
            // 
            // Txt_RegDobDay
            // 
            this.Txt_RegDobDay.Location = new System.Drawing.Point(218, 180);
            this.Txt_RegDobDay.Name = "Txt_RegDobDay";
            this.Txt_RegDobDay.Size = new System.Drawing.Size(100, 20);
            this.Txt_RegDobDay.TabIndex = 9;
            // 
            // Txt_RegDobMonth
            // 
            this.Txt_RegDobMonth.Location = new System.Drawing.Point(112, 180);
            this.Txt_RegDobMonth.Name = "Txt_RegDobMonth";
            this.Txt_RegDobMonth.Size = new System.Drawing.Size(100, 20);
            this.Txt_RegDobMonth.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 159);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Date of Birth";
            // 
            // Txt_RegDobYear
            // 
            this.Txt_RegDobYear.Location = new System.Drawing.Point(6, 180);
            this.Txt_RegDobYear.Name = "Txt_RegDobYear";
            this.Txt_RegDobYear.Size = new System.Drawing.Size(100, 20);
            this.Txt_RegDobYear.TabIndex = 6;
            // 
            // Txt_RegPhoneNum
            // 
            this.Txt_RegPhoneNum.Location = new System.Drawing.Point(6, 136);
            this.Txt_RegPhoneNum.Name = "Txt_RegPhoneNum";
            this.Txt_RegPhoneNum.Size = new System.Drawing.Size(100, 20);
            this.Txt_RegPhoneNum.TabIndex = 5;
            // 
            // Txt_RegHomeAddr
            // 
            this.Txt_RegHomeAddr.Location = new System.Drawing.Point(6, 110);
            this.Txt_RegHomeAddr.Name = "Txt_RegHomeAddr";
            this.Txt_RegHomeAddr.Size = new System.Drawing.Size(100, 20);
            this.Txt_RegHomeAddr.TabIndex = 4;
            // 
            // Txt_RegEmail
            // 
            this.Txt_RegEmail.Location = new System.Drawing.Point(6, 84);
            this.Txt_RegEmail.Name = "Txt_RegEmail";
            this.Txt_RegEmail.Size = new System.Drawing.Size(100, 20);
            this.Txt_RegEmail.TabIndex = 3;
            // 
            // Txt_RegName
            // 
            this.Txt_RegName.Location = new System.Drawing.Point(6, 58);
            this.Txt_RegName.Name = "Txt_RegName";
            this.Txt_RegName.Size = new System.Drawing.Size(100, 20);
            this.Txt_RegName.TabIndex = 2;
            // 
            // Txt_RegsiterPassword
            // 
            this.Txt_RegsiterPassword.Location = new System.Drawing.Point(6, 32);
            this.Txt_RegsiterPassword.Name = "Txt_RegsiterPassword";
            this.Txt_RegsiterPassword.Size = new System.Drawing.Size(100, 20);
            this.Txt_RegsiterPassword.TabIndex = 1;
            // 
            // Txt_RegUsername
            // 
            this.Txt_RegUsername.Location = new System.Drawing.Point(6, 6);
            this.Txt_RegUsername.Name = "Txt_RegUsername";
            this.Txt_RegUsername.Size = new System.Drawing.Size(100, 20);
            this.Txt_RegUsername.TabIndex = 0;
            // 
            // loginPage
            // 
            this.loginPage.Controls.Add(this.Txt_AccountInfo);
            this.loginPage.Controls.Add(this.Txt_LoginServAddress);
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
            // Txt_AccountInfo
            // 
            this.Txt_AccountInfo.Location = new System.Drawing.Point(6, 107);
            this.Txt_AccountInfo.Multiline = true;
            this.Txt_AccountInfo.Name = "Txt_AccountInfo";
            this.Txt_AccountInfo.ReadOnly = true;
            this.Txt_AccountInfo.Size = new System.Drawing.Size(265, 101);
            this.Txt_AccountInfo.TabIndex = 7;
            // 
            // Txt_LoginServAddress
            // 
            this.Txt_LoginServAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_LoginServAddress.Location = new System.Drawing.Point(6, 214);
            this.Txt_LoginServAddress.Name = "Txt_LoginServAddress";
            this.Txt_LoginServAddress.Size = new System.Drawing.Size(183, 22);
            this.Txt_LoginServAddress.TabIndex = 6;
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
            this.Btn_Connect.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Btn_Connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Connect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Btn_Connect.Location = new System.Drawing.Point(6, 78);
            this.Btn_Connect.Name = "Btn_Connect";
            this.Btn_Connect.Size = new System.Drawing.Size(265, 23);
            this.Btn_Connect.TabIndex = 0;
            this.Btn_Connect.Text = "Login";
            this.Btn_Connect.UseVisualStyleBackColor = false;
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
            this.registerPage.ResumeLayout(false);
            this.registerPage.PerformLayout();
            this.loginPage.ResumeLayout(false);
            this.loginPage.PerformLayout();
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
        private System.Windows.Forms.TextBox Txt_RegUsername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Txt_RegServAddr;
        private System.Windows.Forms.TextBox Txt_RegDobDay;
        private System.Windows.Forms.TextBox Txt_RegDobMonth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Txt_RegDobYear;
        private System.Windows.Forms.TextBox Txt_RegPhoneNum;
        private System.Windows.Forms.TextBox Txt_RegHomeAddr;
        private System.Windows.Forms.TextBox Txt_RegEmail;
        private System.Windows.Forms.TextBox Txt_RegName;
        private System.Windows.Forms.TextBox Txt_RegsiterPassword;
        private System.Windows.Forms.Button Btn_RegUser;
        private System.Windows.Forms.TextBox Txt_SuccessAck;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Txt_LoginServAddress;
        private System.Windows.Forms.TextBox Txt_AccountInfo;
    }
}

