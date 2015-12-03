using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TEST
{
    class ReceiveVoice
    {
        UdpClient udpListener = new UdpClient();
        IPEndPoint listeningEP = new IPEndPoint(IPAddress.Any, 6000);
        BufferedWaveProvider waveProvider = null;
        WaveIn sourceStream = null;
        WaveOut waveout = new WaveOut();

        public void InitializeStream()
        {
            sourceStream = new WaveIn();
            sourceStream.BufferMilliseconds = 50;
            sourceStream.DeviceNumber = 0;
            sourceStream.WaveFormat = new WaveFormat(44100, 16, WaveIn.GetCapabilities(0).Channels);
        }

        public void startWavePrider()
        {
            waveProvider = new BufferedWaveProvider(sourceStream.WaveFormat);
            waveout.Init(waveProvider);
            waveout.Play();
        }

        public void startListening()
        {
            udpListener.Client.Bind(listeningEP);
            while(true)
            {
                byte[] buffer = udpListener.Receive(ref listeningEP);
                waveProvider.AddSamples(buffer, 0, buffer.Length);
            }
        }

        static void Main(string[] args)
        {
            ReceiveVoice mine = new ReceiveVoice();
            mine.InitializeStream();
            mine.startWavePrider();
            mine.startListening();
        }
    }
}
