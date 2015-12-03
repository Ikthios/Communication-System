using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using NAudio.Wave;

namespace PeerInterface
{
    public partial class Form1 : Form
    {
        // Global variables
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        Socket regSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket friendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        UdpClient peerReceiver;
        IPEndPoint hostEP, listenEP;
        // Create sender and listener threads
        Thread sendThread;
        Thread listenThread;

        public Form1()
        {
            // Start GUI form
            InitializeComponent();

            /*
            Below here resides the initialization
            code for the voice communication section
            in the peer interface.
            */
            // Disable stop button
            Btn_Stop.Enabled = false;

            // Set default values on form load
            CmbBox_SampleRate.SelectedIndex = 7;
            CmbBox_BitDepth.SelectedIndex = 0;
            Txt_ServAddress.Text = "127.0.0.1";
            Txt_ServPort.Text = "6000";

            // List input devices on form load
            GetInputDevices();
            /*
            Stop audio section
            */

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
        private bool loggedIn;
        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            Thread loginThread = new Thread(new ThreadStart(Login));
            Thread friendThread = new Thread(new ThreadStart(GetFriendList));
            loginThread.Start();

            while (true)
            {
                if (loggedIn)
                {
                    friendThread.Start();
                    break;
                }
                else
                {
                    Debug.WriteLine("User not logged in yet.");
                }
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

                loggedIn = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GetFriendList()
        {
            string username = Txt_Username.Text;
            string password = Txt_Password.Text;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

        /*
        Below here resides the audio code
        for communicating with other peers
        over the network. The code in this
        section will eventually be moved
        over to the 'voice.cs' class after
        the feature has been confirmed to
        work nicely.
        */
        Voice voice = new Voice();

        private void Btn_Start_Click(object sender, EventArgs e)
        {
            /*
            // Return if no items are selected
            if (LstView_Devices.SelectedItems.Count == 0)
            {
                MessageBox.Show("A input device must be selected.");
            }
            else
            {
                // Disable the start button and enable the stop button
                Btn_Start.Enabled = false;
                Btn_Stop.Enabled = true;

                string peerAddress = Txt_ServAddress.Text;
                int bitRate = int.Parse(CmbBox_SampleRate.Text);
                int bitDepth = int.Parse(CmbBox_BitDepth.Text);
                int deviceID = int.Parse(Cmb_InputDevices.Text);

                voice.Start(peerAddress, deviceID, bitRate, bitDepth);
            }*/
            // Disable the start button and enable the stop button
            Btn_Start.Enabled = false;
            Btn_Stop.Enabled = true;

            string peerAddress = Txt_ServAddress.Text;
            int bitRate = int.Parse(CmbBox_SampleRate.Text);
            int bitDepth = int.Parse(CmbBox_BitDepth.Text);
            int deviceID = Cmb_InputDevices.SelectedIndex;

            voice.Start(peerAddress, deviceID, bitRate, bitDepth);
        }

        private void Btn_Stop_Click_1(object sender, EventArgs e)
        {
            Btn_Stop.Enabled = false;
            Btn_Start.Enabled = true;

            voice.Stop();
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            Btn_Stop_Click_1(sender, e);
            this.Close();
        }

        private void Btn_StartAudioList_Click(object sender, EventArgs e)
        {
            int bitRate = int.Parse(CmbBox_SampleRate.Text);
            int bitDepth = int.Parse(CmbBox_BitDepth.Text);
            int deviceID = Cmb_InputDevices.SelectedIndex;

            Voice voice = new Voice();
            voice.AudioPlayer(deviceID, bitRate, bitDepth);

            Thread listenerThread = new Thread(new ThreadStart(voice.AudioListener));
            listenerThread.Start();

            Btn_StartAudioList.Text = "Listening";
            Btn_StartAudioList.BackColor = System.Drawing.Color.Green;
        }

        /*
        private void GetInputDevices()
        {
            // Get information about connected devices
            List<WaveInCapabilities> deviceList = new List<WaveInCapabilities>();
            for (int waveInDevice = 0; waveInDevice < WaveIn.DeviceCount; waveInDevice++)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(waveInDevice);
                deviceList.Add(WaveIn.GetCapabilities(waveInDevice));
            }

            LstView_Devices.Items.Clear();

            foreach (var device in deviceList)
            {
                ListViewItem item = new ListViewItem(device.ProductName);
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, device.Channels.ToString()));
                LstView_Devices.Items.Add(item);
            }
        }
        */
        private void GetInputDevices()
        {
            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                var capabilities = WaveIn.GetCapabilities(i);
                Cmb_InputDevices.Items.Add(capabilities.ProductName);
            }

            if (Cmb_InputDevices.Items.Count > 0)
            {
                Cmb_InputDevices.SelectedIndex = 0;
            }
        }
    }
}
