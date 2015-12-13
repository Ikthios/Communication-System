using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PeerInterface
{
    class ReceiveVoice
    {
        UdpClient udpListener = new UdpClient();
        IPEndPoint listeningEP = new IPEndPoint(IPAddress.Any, 6700);
        BufferedWaveProvider waveProvider = null;
        WaveInEvent sourceStream = null;
        WaveOut waveout = new WaveOut();
        bool loop = true;

        public void InitializeStream()
        {
            sourceStream = new WaveInEvent();
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
            InitializeStream();
            startWavePrider();
            udpListener.Client.Bind(listeningEP);
            while (loop)
            {
                byte[] buffer = udpListener.Receive(ref listeningEP);
                waveProvider.AddSamples(buffer, 0, buffer.Length);
            }
        }

        public void DisconnectReceiver()
        {
            udpListener.Close();
            loop = false;
            //waveout.Stop();
            Thread.CurrentThread.Abort();
        }
    }
}

