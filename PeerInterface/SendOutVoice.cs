using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace PeerInterface
{
    class SendOutVoice
    {
        int deviceID;
        int bitRate;
        int bitDepth;
        string destinationIP;
        bool loop = true;

        IPEndPoint outEP;
        WaveInEvent incoming = new WaveInEvent();
        UdpClient sender = new UdpClient();

        public SendOutVoice() { }

        public SendOutVoice(string destinationIP, int deviceID, int bitRate, int bitDepth)
        {
            this.destinationIP = destinationIP;
            this.deviceID = deviceID;
            this.bitRate = bitRate;
            this.bitDepth = bitDepth;
            outEP = new IPEndPoint(IPAddress.Parse(destinationIP), 6700);
        }

        public void InitializeWaveInEvent()
        {
            incoming.BufferMilliseconds = 50;
            incoming.DeviceNumber = deviceID;
            incoming.WaveFormat = new WaveFormat(bitRate, bitDepth, WaveIn.GetCapabilities(deviceID).Channels);
        }

        public void StartSending()
        {
            InitializeWaveInEvent();
            sender.Connect(outEP);
            incoming.DataAvailable += sourcestream_DataAvailable;
            incoming.StartRecording();
            while (loop)
            {

            }
        }

        private void sourcestream_DataAvailable(object notUsed, WaveInEventArgs e)
        {
            try
            {
                byte[] buffer = (e.Buffer);
                sender.Send(buffer, buffer.Length);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public void DisconnectSender()
        {
            sender.Close();
            loop = false;
            //incoming.StopRecording();
            Thread.CurrentThread.Abort();
        }
    }
}

