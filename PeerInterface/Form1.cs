using System;
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
            Txt_LoginServAddress.Text = "10.134.172.46";

            sendThread = new Thread(new ThreadStart(UdpSender));
            listenThread = new Thread(new ThreadStart(UdpListener));
            // Start sender and listener threads
            sendThread.Start();
            listenThread.Start();
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
            Socket regSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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
        This is the login feature for the peer interface. It will start a new
        thread which will attempt to connect to the login server and retreive
        its' information.
        */
        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            /*
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
            }*/

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
            //string message = username + "," + GetIpAddress();
            string message = "username," + GetIpAddress();

            hostEP = new IPEndPoint(IPAddress.Broadcast, 6500);

            // Loop the broadcast signal
            while (true)
            {
                socket.Connect(hostEP);
                socket.Send(Encoding.ASCII.GetBytes(message));
                Thread.Sleep(5000);
            }
        }

        // This will listen to the network for peer information
        private void UdpListener()
        {
            peerReceiver = new UdpClient();
            listenEP = new IPEndPoint(IPAddress.Any, 6500);

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
