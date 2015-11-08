using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using NAudio.Wave;

namespace VoiceServer
{
    class UdpListener
    {
        public void StartUDPListener()
        {
            Thread receivingThread = new Thread(messageReceiver);
            receivingThread.Start();
        }

        public void messageReceiver()
        {
            Console.WriteLine("Listening at " + GetIpAddress() + ":6000");
            UdpClient listener = new UdpClient(6000);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(GetIpAddress()), 6000);
            BufferedWaveProvider waveProvider;
            WaveOut waveOut;
            byte[] buffer;
            string data;

            // Audio variables to deconstruct data
            WaveIn sourcestream = new WaveIn();


            try
            {
                waveProvider = new BufferedWaveProvider();
                while (true)
                {
                    buffer = listener.Receive(ref endPoint);
                    waveProvider.AddSamples(buffer, 0, buffer.Length);
                }
            }
            catch(Exception ex)
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
