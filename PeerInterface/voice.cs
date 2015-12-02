using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace PeerInterface
{
    class Voice
    {
        // Global variables
        UdpClient udpSender;
        UdpClient udpReceiver;
        WaveIn sourcestream = null;
        WaveOut waveout = null;
        BufferedWaveProvider waveProvider = null;

        public void Start(string peerAddress, string peerPort, string bitRate, string bitDepth, int deviceID)
        {
            try
            {
                // Get the endpoint
                string[] tokens = peerAddress.Split(',');
                IPEndPoint audioEP = new IPEndPoint(IPAddress.Parse(tokens[1].ToString()),
                                                            int.Parse(peerPort));
                // Connect to the peer
                Connect(audioEP, deviceID, int.Parse(bitRate), int.Parse(bitDepth));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void Connect(IPEndPoint audioEP, int deviceID, int bitRate, int bitDepth)
        {
            sourcestream = new WaveIn();
            sourcestream.BufferMilliseconds = 50;
            sourcestream.DeviceNumber = deviceID;
            sourcestream.WaveFormat = new WaveFormat(bitRate, bitDepth, WaveIn.GetCapabilities(deviceID).Channels);
            sourcestream.DataAvailable += sourcestream_DataAvailable;
            sourcestream.StartRecording();

            udpSender = new UdpClient();

            udpSender.Connect(audioEP);

            waveout = new WaveOut();
            waveProvider = new BufferedWaveProvider(sourcestream.WaveFormat);

            waveout.Init(waveProvider);
            waveout.Play();
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

        public void Stop()
        {
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

        private void AudioListener()
        {
            IPEndPoint listeningEP = new IPEndPoint(IPAddress.Any, 6000);
            try
            {
                udpReceiver = new UdpClient();
                udpReceiver.Client.Bind(listeningEP);

                while (true)
                {
                    byte[] buffer = udpReceiver.Receive(ref listeningEP);
                    waveProvider.AddSamples(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void PeerPingSender(IPAddress address, int port)
        {
            TcpClient peerPing = new TcpClient();
            IPEndPoint pingEP = new IPEndPoint(address, port);
            peerPing.Connect(pingEP);
        }

        private void PeerPingReceiver()
        {
            UdpClient peerPing = new UdpClient();
            IPEndPoint pingEP = new IPEndPoint(IPAddress.Any, 6600);
            peerPing.Client.Bind(pingEP);
            byte[] buffer = peerPing.Receive(ref pingEP);
            string dataString = Encoding.ASCII.GetString(buffer);
        }
    }
}
