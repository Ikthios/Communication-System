using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NAudio.Wave;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace VoiceClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Disable start and stop buttons
            Btn_Stop.Enabled = false;
            CmbBox_SampleRate.SelectedIndex = 7;
            Txt_ServAddress.Text = "127.0.0.1";
            Txt_ServPort.Text = "6000";
            GetInputDevices();
        }

        // Global variables
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        UdpClient udpSender;
        UdpClient udpReceiver;
        WaveIn sourcestream = null;
        WaveOut waveout = null;
        WaveInProvider waveIn = null;
        BufferedWaveProvider waveProvider = null;
        IPEndPoint endPoint;

        private void Btn_Start_Click_1(object sender, EventArgs e)
        {
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

                try
                {
                    // Get the endpoint
                    endPoint = new IPEndPoint(IPAddress.Parse(Txt_ServAddress.Text),
                                                                int.Parse(Txt_ServPort.Text));
                    int sampleRate = int.Parse(CmbBox_SampleRate.Text);
                    // Connect to the peer
                    Connect(/*peerEndPoint,*/ LstView_Devices.SelectedItems[0].Index, sampleRate);
                    // Start the listener
                    Thread receivingThread = new Thread(Listener);
                    receivingThread.Start();
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }

        private void Btn_Stop_Click_1(object sender, EventArgs e)
        {
            Btn_Stop.Enabled = false;
            Btn_Start.Enabled = true;

            udpSender.Close();
            udpReceiver.Close();

            if (waveout != null)
            {
                waveout.Stop();
                waveout.Dispose();
                waveout = null;
            }
            if (sourcestream != null)
            {
                sourcestream.StopRecording();
                sourcestream.Dispose();
                sourcestream = null;
            }
        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            Btn_Stop_Click_1(sender, e);
            this.Close();
        }

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

        private void Connect(/*IPEndPoint endPoint,*/ int inputDeviceNumber, int sampleRate)
        {
            sourcestream = new WaveIn();
            sourcestream.BufferMilliseconds = 50;
            sourcestream.DeviceNumber = inputDeviceNumber;
            sourcestream.WaveFormat = new WaveFormat(sampleRate, WaveIn.GetCapabilities(inputDeviceNumber).Channels);
            sourcestream.DataAvailable += sourcestream_DataAvailable;
            sourcestream.StartRecording();

            udpSender = new UdpClient();
            udpReceiver = new UdpClient();

            udpReceiver.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpReceiver.Client.Bind(endPoint);

            udpSender.Connect(endPoint);

            waveout = new WaveOut();
            waveProvider = new BufferedWaveProvider(sourcestream.WaveFormat);

            waveout.Init(waveProvider);
            waveout.Play();

            var state = new ListenerState { EndPoint = endPoint };
            ThreadPool.QueueUserWorkItem(Listener, state);
        }

        private void sourcestream_DataAvailable(object sender, WaveInEventArgs e)
        {
            try
            {
                byte[] buffer = (e.Buffer);
                udpSender.Send(buffer, buffer.Length);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        class ListenerState
        {
            public IPEndPoint EndPoint { get; set; }
        }

        private void Listener(object state)
        {
            var ListenerState = (ListenerState)state;
            //var endPoint = ListenerState.EndPoint;

            try
            {
                while (true)
                {
                    byte[] buffer = udpReceiver.Receive(ref endPoint);
                    waveProvider.AddSamples(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
