using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KChat
{
    public partial class MainForm : Form
    {
        const string userRoot = "HKEY_CURRENT_USER";
        const string subkey = "K-Chat";
        const string keyName = userRoot + "\\" + subkey;
        String strUser;

        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        string readData = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(textBox3.Text + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
            textBox3.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                readData = "Connected to Chat Server ...";
                msg();
                clientSocket.Connect("127.0.0.1", 8888);
                serverStream = clientSocket.GetStream();

                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(textBox2.Text + "$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                Thread ctThread = new Thread(getMessage);
                ctThread.Start();

                if(strUser == null)
                {
                    strUser = textBox2.Text;
                    Registry.SetValue(keyName, "user", strUser);
                }

                buttonSend.Enabled = true; 
            }
            catch(Exception ex)
            {
                buttonSend.Enabled = false;
            }
        }

        private void getMessage()
        {
            while (true)
            {
                try
                {
                    serverStream = clientSocket.GetStream();
                 
                    int buffSize = 0;
                    byte[] inStream = new byte[10025];

                    buffSize = clientSocket.ReceiveBufferSize;
                    serverStream.Read(inStream, 0, buffSize);
                    
                    string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                    readData = "" + returndata;

                    msg();
                }
                catch(Exception e)
                {
                    break;
                }
            }
        }

        private void msg()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(msg));
            else
                textBox1.Text = textBox1.Text + Environment.NewLine + " >> " + readData;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            String strUser = (string)Registry.GetValue(keyName, "user", "");
            if(strUser != null)
            {
                textBox2.Text = strUser;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            clientSocket.Close();
            clientSocket = null;

        }
    }
}
