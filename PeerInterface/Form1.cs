using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PeerInterface
{
    public partial class Form1 : Form
    {
        // Global variables
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        UdpClient peerReceiver;
        IPEndPoint hostEP, listenEP;
        // Create sender and listener threads
        Thread sendThread;
        Thread listenThread;

        public Form1()
        {
            // Start GUI form
            InitializeComponent();

            // Show peer address to user
            Txt_Address.Text = GetIpAddress();
        }

        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            sendThread = new Thread(new ThreadStart(UdpSender));
            listenThread = new Thread(new ThreadStart(UdpListener));

            if (!Txt_Username.Text.Equals(""))
            {
                // Start sender and listener threads
                sendThread.Start();
                listenThread.Start();
            }
            else
            {
                MessageBox.Show("Username required.");
            }
        }

        // This will broadcast the peer information to the network
        private void UdpSender()
        {
            string username = Txt_Username.Text;
            string message = username + "," + GetIpAddress();

            hostEP = new IPEndPoint(IPAddress.Broadcast, 6000);

            // Loop the broadcast signal
            while (true)
            {
                socket.Connect(hostEP);
                socket.Send(ASCIIEncoding.ASCII.GetBytes(message));
            }
        }

        // This will listen to the network for peer information
        private void UdpListener()
        {
            peerReceiver = new UdpClient();
            listenEP = new IPEndPoint(IPAddress.Any, 6000);

            // Create a byte array to hold the incoming data
            byte[] receivedBytes = new byte[1500];
            string incomingMessage = "";

            try
            {
                // Listen for other peers
                peerReceiver.Client.Bind(listenEP);
                peerReceiver.Receive(ref listenEP);

                while (true)
                {
                    // Receive the incoming byte array over the server socket
                    int receivedSize = socket.Receive(receivedBytes);

                    for (int i = 0; i < receivedSize; i++)
                    {
                        incomingMessage += receivedBytes[i];
                    }

                    // Print message to debug console
                    Debug.WriteLine(incomingMessage);
                    // Add the peer to LstView_Peers
                    LstView_Peers.Items.Add(incomingMessage);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // Get peer IP address
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
