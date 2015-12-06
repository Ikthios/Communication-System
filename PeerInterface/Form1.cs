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
    /*
    UDP listener that listens for friend requests on port 5000 and updates
    the Lst_Requests list view. The Lst_Requests listview should clear before
    updating with new requests (clear and fill).

    The requests will come in as: USERNAME IP, USERNAME2, IP2...
    */
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

            /*
            Below here resides the initialization
            code for the voice communication section
            in the peer interface.
            */
            // Disable stop button
            Btn_Stop.Enabled = false;
            Btn_Exit.Enabled = false;
            CmbBox_BitDepth.Enabled = false;
            CmbBox_SampleRate.Enabled = false;

            // Set default values on form load
            CmbBox_SampleRate.SelectedIndex = 7;
            CmbBox_BitDepth.SelectedIndex = 0;
            Txt_ServAddress.Text = "127.0.0.1";
            Txt_ServPort.Text = "6700";

            // List input devices on form load
            GetInputDevices();
            GetInputDevicesList();
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
            //sendThread.Start();
            listenThread.Start();

            // Set the delegate
            myDelegate = new AddListItem(AddListItemMethod);
            requestDelegate = new AddRequestListItem(AddRequestMethod);
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

        // Used for gathering friend requests from the server
        private string requestString;
        private void setRequestString(string item)
        {
            requestString = item;
        }
        private string getRequestString()
        {
            return requestString;
        }
        // End friend request gathering section

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
        private delegate void AddRequestListItem();
        private AddListItem myDelegate;
        private AddRequestListItem requestDelegate;

        private List<string> peerStringList = new List<string>();
        private HashSet<string> requestHash1 = new HashSet<string>();
        private HashSet<string> requestHash2;

        Semaphore sem = new Semaphore(1, 1);

        /*
        This is the register feature of the peer interface. It will allow a user
        to input their information and register a user with the login server.
        */
        private void Btn_RegUser_Click(object sender, EventArgs e)
        {
            Thread registerThread = new Thread(new ThreadStart(Register));
            //registerThread.Start();
            Register();
        }

        private void Register()
        {
            Socket regSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint regEP = new IPEndPoint(IPAddress.Parse(Txt_RegServAddr.Text), 7000);
            string[] tokens;
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
                Debug.WriteLine("Reg: " + dataString);
                //SUCCESS,username,password,name,email,address,phone,dob,ip
                tokens = dataString.Split(',');
                if (tokens[0].Equals("SUCCESS"))
                {
                    Txt_RegUsername.Text = "";
                    Txt_RegsiterPassword.Text = "";
                    Txt_RegName.Text = "";
                    Txt_RegEmail.Text = "";
                    Txt_RegHomeAddr.Text = "";
                    Txt_RegPhoneNum.Text = "";
                    Txt_RegDobYear.Text = "";
                    Txt_RegDobMonth.Text = "";
                    Txt_RegDobDay.Text = "";
                    Txt_SuccessAck.Text = tokens[0];
                    regSocket.Close();
                }
                else
                {
                    Txt_SuccessAck.Text = dataString;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                regSocket.Close();
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
                // Need "ADD,my username, my IP, friend username, friend IP
                //string friend = LstView_Peers.SelectedItems.ToString();
                string friend = Txt_MyUsername.Text + "," + Txt_MyAddress.Text +
                    "," + Txt_FriendUsername.Text + "," + Txt_FriendAddress.Text;
                string friendRequest = "ADD," + friend + ',';
                SendRequest(friendRequest);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Btn_AcceptRequest_Click(object sender, EventArgs e)
        {
            try
            {
                // "ACCEPT,my username, friend username
                string accept = Txt_MyUsername.Text + "," + Txt_MyAddress.Text +
                    "," + Txt_FriendUsername.Text + "," + Txt_FriendAddress.Text;
                string acceptRequest = "ACCEPT," + accept + ',';
                SendRequest(acceptRequest);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Btn_RejectRequest_Click(object sender, EventArgs e)
        {
            try
            {
                // "REJECT,my username, my IP, friend username, friend IP
                string reject = Txt_MyUsername.Text + "," + Txt_FriendUsername.Text;
                string rejectRequest = "REJECT," + reject + ',';
                SendRequest(rejectRequest);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void SendRequest(string request)
        {
            try
            {
                Socket friendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint friendEP = new IPEndPoint(IPAddress.Parse(Txt_LoginServAddress.Text), 8000);
                friendSocket.Connect(friendEP);
                friendSocket.Send(Encoding.ASCII.GetBytes(request));

                byte[] buffer = new byte[1500];
                string dataString = "";

                LstView_Friends.Items.Clear();
                Txt_FriendUsername.Clear();
                Txt_FriendAddress.Clear();

                friendSocket.Receive(buffer);

                for (int i = 0; i < buffer.Length; i++)
                {
                    dataString += Convert.ToChar(buffer[i]);
                }
                
                Txt_FriendSuccess.Text = dataString;
                friendSocket.Close();
                GetFriendList();
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
            string username = Txt_Username.Text;
            string password = Txt_Password.Text;
            Socket loginSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint loginEP = new IPEndPoint(IPAddress.Parse(Txt_LoginServAddress.Text), 6000);
            string loginMessage = username + ',' + password + ',' + GetIpAddress() + ',';
            Thread requestListenerThread = new Thread(new ThreadStart(RequestListener));
            Thread requestUpdateThread = new Thread(new ThreadStart(RequestUpdater));

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
                //SUCCESS,username,password,name,email,address,phone,dob,ip
                Debug.WriteLine("Login: " + dataString);
                string[] tokens = dataString.Split(',');
                string[] dobTokens = tokens[7].Split(' ');
                string userInfo = "Username: " + tokens[1] + "\r\n" +
                                    "Password: " + tokens[2] + "\r\n" +
                                    "Name: " + tokens[3] + "\r\n" +
                                    "Email: " + tokens[4] + "\r\n" +
                                    "Address: " + tokens[5] + "\r\n" +
                                    "Phone Number: " + tokens[6] + "\r\n" +
                                    "DOB: " + dobTokens[0] + "\r\n" +
                                    "IP: " + tokens[8];
                if (tokens[0].Equals("SUCCESS"))
                {
                    Btn_Connect.Enabled = false;
                    Txt_SuccessAck.Text = tokens[0];
                    Txt_AccountInfo.Text = userInfo;
                    Txt_MyUsername.Text = username;
                    Txt_MyAddress.Text = GetIpAddress();
                    GetFriendList();
                    Thread.Sleep(1000);
                    requestListenerThread.Start();
                    requestUpdateThread.Start();
                    sendThread.Start();
                }
                else
                {
                    Txt_SuccessAck.Text = tokens[0];
                }

                loggedIn = true;
                loginSocket.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                loginSocket.Close();
            }
        }

        private void Btn_GetFriendList_Click(object sender, EventArgs e)
        {
            GetFriendList();
        }

        private void GetFriendList()
        {
            string username = Txt_MyUsername.Text;
            try
            {
                Socket friendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint friendEP = new IPEndPoint(IPAddress.Parse(Txt_LoginServAddress.Text), 9000);
                friendSocket.Connect(friendEP);
                // Send username and get friend listing
                friendSocket.Send(Encoding.ASCII.GetBytes(username));

                //LstView_Friends.Clear();

                // Get friend list back
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

                friendSocket.Close();
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
            string username = Txt_MyUsername.Text;
            string message = username + ',' + GetIpAddress();
            byte[] byteMessage = Encoding.ASCII.GetBytes(message);
            //hostEP = new IPEndPoint(IPAddress.Broadcast, 6500);
            hostEP = new IPEndPoint(IPAddress.Parse(Txt_LoginServAddress.Text), 12000);


            // Loop the broadcast signal
            try
            {
                /*
                while (true)
                {
                    socket.Connect(hostEP);
                    socket.Send(Encoding.ASCII.GetBytes(message));
                    Debug.WriteLine("Send message: " + message);
                    Thread.Sleep(5000);
                }
                */
                while (true)
                {
                    socket.Connect(hostEP);
                    socket.Send(Encoding.ASCII.GetBytes(message));
                    Debug.WriteLine("Send message: " + message);
                    Thread.Sleep(1000);
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
            peerReceiver = new UdpClient();
            listenEP = new IPEndPoint(IPAddress.Any, 12001);
            peerReceiver.Client.Bind(listenEP);
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

        private void RequestListener()
        {
            UdpClient requestReceiver = new UdpClient();
            IPEndPoint requestEP = new IPEndPoint(IPAddress.Any, 5000);
            requestReceiver.Client.Bind(requestEP);
            string incomingRequest;
            byte[] dataBytes;

            try
            {
                while (true)
                {
                    dataBytes = requestReceiver.Receive(ref requestEP);
                    incomingRequest = Encoding.ASCII.GetString(dataBytes, 0, dataBytes.Length);
                    Debug.WriteLine("Received request: " + incomingRequest);
                    sem.WaitOne();
                    requestHash1.Add(incomingRequest);
                    sem.Release();
                    //Invoke(requestDelegate);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                MessageBox.Show(ex.ToString());
                requestReceiver.Close();
            }
        }

        // This will allow the UdpListener thread to access the main forms listview element
        private void AddListItemMethod()
        {
            //int count = 0;
            string[] peerString = getPeerString().Split(',');

            LstView_Peers.Items.Clear();
            foreach(string element in peerString)
            {
                LstView_Peers.Items.Add(element);
            }
            /*
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
            */
        }

        private void RequestUpdater()
        {
            while (true)
            {
                Thread.Sleep(15000);
                sem.WaitOne();
                requestHash2 = new HashSet<string>(requestHash1);
                requestHash1.Clear();
                Invoke(requestDelegate);
                sem.Release();
            }
        }

        private void AddRequestMethod()
        {
            LstView_Requests.Items.Clear();
            foreach (string element in requestHash2)
            {
                LstView_Requests.Items.Add(element);
            }
            requestHash2.Clear();
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
            //Btn_Stop.Enabled = true;

            string peerAddress = Txt_ServAddress.Text;
            int bitRate = int.Parse(CmbBox_SampleRate.Text);
            int bitDepth = int.Parse(CmbBox_BitDepth.Text);
            int deviceID = Cmb_InputDevices.SelectedIndex;

            SendOutVoice sov = new SendOutVoice(peerAddress);
            Thread senderThread = new Thread(new ThreadStart(sov.StartSending));
            senderThread.Start();
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

            ReceiveVoice rv = new ReceiveVoice();
            Thread listenerThread = new Thread(new ThreadStart(rv.startListening));
            listenerThread.Start();

            Btn_StartAudioList.Text = "Listening";
            Btn_StartAudioList.BackColor = System.Drawing.Color.Green;
        }

        
        private void GetInputDevicesList()
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
