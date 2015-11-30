﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PeerInterface
{
    public partial class Form1 : Form
    {
        // Global variables
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        Socket regSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket friendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //Socket sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        UdpClient peerReceiver;
        IPEndPoint hostEP, listenEP;
        // Create sender and listener threads
        Thread sendThread;
        Thread listenThread;

        // Used for gathering peer information from the network
        private string peerString;
        private void setPeerString(string item)
        {
            peerString = item;
        }
        private string getPeerString()
        {
            return peerString;
        }
        // End peer info gathering section

        // Used for storing the server IP address
        private string serverAddress;
        private void setServerAddress(string item)
        {
            serverAddress = item;
        }
        private string getServerAddress()
        {
            return serverAddress;
        }
        // End server IP storage section
        
        private delegate void AddListItem();
        private AddListItem myDelegate;

        private List<string> peerStringList = new List<string>();

        public Form1()
        {
            // Start GUI form
            InitializeComponent();

            // Show peer address to user
            Txt_Address.Text = GetIpAddress();
            setServerAddress("10.134.172.46");
            Txt_LoginServAddress.Text = getServerAddress();

            Thread.Sleep(3000);

            sendThread = new Thread(new ThreadStart(UdpSender));
            listenThread = new Thread(new ThreadStart(UdpListener));
            // Start sender and listener threads
            sendThread.Start();
            listenThread.Start();

            // Set the delegate
            myDelegate = new AddListItem(AddListItemMethod);
        }



        /*
        This is the register feature of the peer interface. It will allow a user
        to input their information and register a user with the login server.
        */
        private void Btn_RegUser_Click(object sender, EventArgs e)
        {
            Thread registerThread = new Thread(new ThreadStart(Register));
            registerThread.Start();
        }

        private void Register()
        {
            IPEndPoint regEP = new IPEndPoint(IPAddress.Parse(Txt_RegServAddr.Text), 7000);

            try
            {
                string regMessage = (Txt_RegUsername.Text + ',' +
                    Txt_RegsiterPassword.Text + ',' +
                    Txt_RegName.Text + ',' +
                    Txt_RegEmail.Text + ',' +
                    Txt_RegHomeAddr.Text + ',' +
                    Txt_RegPhoneNum.Text + ',' +
                    Txt_RegDobYear.Text + ',' +
                    Txt_RegDobMonth.Text + ',' +
                    Txt_RegDobDay.Text + ',' +
                    GetIpAddress() + ',');
                regSocket.Connect(regEP);
                regSocket.Send(Encoding.ASCII.GetBytes(regMessage));

                byte[] buffer = new byte[1500];
                string dataString = "";
                regSocket.Receive(buffer);
                for (int i = 0; i < buffer.Length; i++)
                {
                    dataString += Convert.ToChar(buffer[i]);
                }
                Txt_SuccessAck.Text = dataString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        /*
        This is the friend request feature of the peer interface. It will allow a user
        to send a friend request to the server, which will update the peer with a
        frient request notification.
        */
        private void Btn_FriendRequest_Click(object sender, EventArgs e)
        {
            try
            {
                IPEndPoint friendEP = new IPEndPoint(IPAddress.Parse(Txt_RegServAddr.Text), 8000);
                string friend = LstView_Peers.SelectedItems.ToString();
                string friendRequest = "ADD," + friend;
                friendSocket.Connect(friendEP);
                friendSocket.Send(Encoding.ASCII.GetBytes(friendRequest));

                byte[] buffer = new byte[1500];
                string dataString = "";
                friendSocket.Receive(buffer);
                for (int i = 0; i < buffer.Length; i++)
                {
                    dataString += Convert.ToChar(buffer[i]);
                }
                Txt_FriendSuccess.Text = dataString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /*
        This is the login feature for the peer interface. It will start a new
        thread which will attempt to connect to the login server and retreive
        its' information.
        */
        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            //Thread loginThread = new Thread(new ThreadStart(Login));
            //loginThread.Start();

            string username = Txt_Username.Text;
            string password = Txt_Password.Text;
            Socket loginSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint loginEP = new IPEndPoint(IPAddress.Parse(Txt_LoginServAddress.Text), 6000);
            string loginMessage = username + ',' + password + ',' + GetIpAddress() + ',';

            try
            {
                // Login and get user information
                loginSocket.Connect(loginEP);
                loginSocket.Send(Encoding.ASCII.GetBytes(loginMessage));

                byte[] buffer = new byte[1500];
                string dataString = "";
                loginSocket.Receive(buffer);
                for (int i = 0; i < buffer.Length; i++)
                {
                    dataString += Convert.ToChar(buffer[i]);
                }
                string[] tokens = dataString.Split(',');
                if (tokens[0].Equals("SUCCESS"))
                    Btn_Connect.Enabled = false;
                Txt_AccountInfo.Text = dataString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                Socket friendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint friendEP = new IPEndPoint(IPAddress.Parse(Txt_LoginServAddress.Text), 9000);
                friendSocket.Connect(friendEP);
                // Send username and get friend listing
                friendSocket.Send(Encoding.ASCII.GetBytes(username));
                byte[] friendBuffer = new byte[1500];
                string dataString = "";
                friendSocket.Receive(friendBuffer);
                for (int i = 0; i < friendBuffer.Length; i++)
                {
                    dataString += Convert.ToChar(friendBuffer[i]);
                }
                string[] tokens = dataString.Split(',');
                foreach (var friend in tokens)
                {
                    LstView_Friends.Items.Add(friend);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Login()
        {
            string username = Txt_Username.Text;
            string password = Txt_Password.Text;
            Socket loginSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint loginEP = new IPEndPoint(IPAddress.Parse(Txt_LoginServAddress.Text), 6000);
            string loginMessage = username + ',' + password + ',' + GetIpAddress() + ',';

            try
            {
                // Login and get user information
                loginSocket.Connect(loginEP);
                loginSocket.Send(Encoding.ASCII.GetBytes(loginMessage));

                byte[] buffer = new byte[1500];
                string dataString = "";
                loginSocket.Receive(buffer);
                for (int i = 0; i < buffer.Length; i++)
                {
                    dataString += Convert.ToChar(buffer[i]);
                }
                string[] tokens = dataString.Split(',');
                if (tokens[0].Equals("SUCCESS"))
                    Btn_Connect.Enabled = false;
                Txt_AccountInfo.Text = dataString;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void GetFriendList()
        {
            string username = Txt_Username.Text;
            string password = Txt_Password.Text;
            Socket loginSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint loginEP = new IPEndPoint(IPAddress.Parse(Txt_LoginServAddress.Text), 6000);

            // Send username and get friend listing
            loginSocket.Send(Encoding.ASCII.GetBytes(username));
            byte[] friendBuffer = new byte[1500];
            string dataString = "";
            loginSocket.Receive(friendBuffer);
            for (int i = 0; i < friendBuffer.Length; i++)
            {
                dataString += Convert.ToChar(friendBuffer[i]);
            }
            string[] tokens = dataString.Split(',');
            foreach (var friend in tokens)
            {
                LstView_Friends.Items.Add(friend);
            }
        }



        /*
        This is the P2P communication feature of the peer interface. It will send
        out UDP signals with the username and peer IP address in an attempt to
        connect to other peers on the network and get their username and IP address
        so the user can select and send a friend request.
        */
        // This will broadcast the peer information to the network
        private void UdpSender()
        {
            string username = Txt_Username.Text;
            string message = "username," + GetIpAddress();
            byte[] byteMessage = Encoding.ASCII.GetBytes(message);
            hostEP = new IPEndPoint(IPAddress.Broadcast, 6500);


            // Loop the broadcast signal
            try
            {
                while (true)
                {
                    socket.Connect(hostEP);
                    socket.Send(Encoding.ASCII.GetBytes(message));
                    Debug.WriteLine("Send message: " + message);
                    Thread.Sleep(5000);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }

        // This will listen to the network for peer information
        private void UdpListener()
        {
            peerReceiver = new UdpClient(6500);
            listenEP = new IPEndPoint(IPAddress.Any, 6500);
            string incomingMessage;
            byte[] dataBytes;

            try
            {
                while (true)
                {
                    dataBytes = peerReceiver.Receive(ref listenEP);
                    incomingMessage = Encoding.ASCII.GetString(dataBytes, 0, dataBytes.Length);
                    Debug.WriteLine("Received message: " + incomingMessage);
                    setPeerString(incomingMessage);
                    Invoke(myDelegate);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                MessageBox.Show(ex.ToString());
                peerReceiver.Close();
            }
        }

        // This will allow the UdpListener thread to access the main forms listview element
        private void AddListItemMethod()
        {
            int count = 0;
            string peerString = getPeerString();

            foreach(string element in peerStringList)
            {
                if (element.Equals(peerString))
                {
                    count++;
                }
            }
            
            if (count == 0)
            {
                peerStringList.Add(peerString);
                LstView_Peers.Items.Add(peerString);
            }
            else
            {
                Debug.WriteLine(peerString + " already in peerStringList.");
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
