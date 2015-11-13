using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace FileClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Start listener thread on startup
            Thread listenerThread = new Thread(Listener);
            listenerThread.Start();
            Btn_RequestFile.Enabled = false;
        }

        // Global variables
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        TcpListener hostListener = null;
        IPEndPoint peerEndPoint;
        string folderPath, file;

        private void setFolderPath(string value)
        {
            folderPath = value;
        }

        private void setFile(string value)
        {
            file = value;
        }

        private void Btn_RequestFile_Click(object sender, EventArgs e)
        {
            setFile(LstView_Files.SelectedItems.ToString());
        }

        private void Btn_FolderLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowse = new FolderBrowserDialog();
            folderBrowse.ShowDialog();
            setFolderPath(folderBrowse.SelectedPath);

            // Fill the list with selected folder files
            string[] filenameArray = Directory.GetFiles(folderPath)
                .Select(path => Path.GetFileName(path))
                .ToArray();
            foreach (string token in filenameArray)
            {
                LstView_Files.Items.Add(token);
            }

            Btn_RequestFile.Enabled = true;
        }

        /*
        Below is the code that will handle connections to the peer.
        */
        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            IPAddress peerIp = IPAddress.Parse(Txt_IpAddress.Text);
            int peerPort = int.Parse(Txt_Port.Text);
            peerEndPoint = new IPEndPoint(peerIp, peerPort);

            try
            {
                socket.Connect(peerEndPoint);
                // Enable the send file button
                Btn_Connect.BackColor = Color.Green;
                Btn_Connect.Text = "Connected";
                Btn_RequestFile.Enabled = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void Disconnect()
        {
            socket.Disconnect(true);
        }

        /*
        Below is the code that will handle sending and receiving of files
        from connected peers.
        */
        private void Listener()
        {
            hostListener = new TcpListener(IPAddress.Parse(GetIpAddress()), 6000);
            try
            {
                hostListener.Start();
                while (true)
                {
                    socket.Accept();
                    Thread sendThread = new Thread(SendFile);
                    Thread receiveThread = new Thread(ReceiveFile);

                    sendThread.Start();
                    receiveThread.Start();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void SendFile()
        {
            // Create the file to send
            string fileName = file;
            byte[] fnByte = Encoding.ASCII.GetBytes(fileName);
            byte[] fileData = File.ReadAllBytes(folderPath + fileName);
            byte[] clientData = new byte[4 + fnByte.Length + fileData.Length];
            byte[] fileNameLength = BitConverter.GetBytes(fnByte.Length);
            fileNameLength.CopyTo(clientData, 0);
            fnByte.CopyTo(clientData, 4);
            fileData.CopyTo(clientData, 4 + fnByte.Length);

            try
            {
                // Send the file to the connected peer
                socket.Send(clientData);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void ReceiveFile()
        {
            // Create byte array for receiving client data
            // 1500 bytes is the standard TCP file transfer size
            byte[] dataArray = new byte[1500];
            int n = socket.Receive(dataArray);

            try
            {
                // Receive the file
                Debug.WriteLine("Receiving requested file.");

                string receivedPath = folderPath;
                int fileNameLen = BitConverter.ToInt32(dataArray, 0);
                string fileName = Encoding.ASCII.GetString(dataArray, 4, fileNameLen);
                BinaryWriter bWrite = new BinaryWriter(File.Open(receivedPath + fileName, FileMode.Append));
                bWrite.Write(dataArray, 4 + fileNameLen, n - 4 - fileNameLen);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public string GetIpAddress()
        {
            IPHostEntry host;
            string localIp = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress address in host.AddressList)
            {
                if (address.AddressFamily.ToString() == "InterNetwork")
                {
                    localIp = address.ToString();
                }
            }
            return localIp;
        }
    }
}
