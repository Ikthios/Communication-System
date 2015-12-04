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
        int deviceID = 0;
        int bitRate = 44100;
        int bitDepth = 16;

        string destinationIP;

        IPEndPoint outEP;

        WaveInEvent incoming = new WaveInEvent();

        UdpClient sender = new UdpClient();

        public SendOutVoice(string destinationIP)
        {
            this.destinationIP = destinationIP;
            outEP = new IPEndPoint(IPAddress.Parse(destinationIP), 8000);
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
            while (true)
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

        /*
        static void Main(string[] args)
        {
            
            SendOutVoice mine = new SendOutVoice("10.134.172.46");
            Thread test = new Thread(mine.StartSending);
            test.Start();
        }
        */
    }
}

