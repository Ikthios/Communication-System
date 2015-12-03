﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace PeerInterface
{
    class Voice
    {
        // Global variables
        UdpClient udpSender;
        UdpClient udpListener;
        WaveIn sourcestream = null;
        WaveOut waveout = null;
        BufferedWaveProvider waveProvider = null;

        public void Start(string peerAddress, int deviceID, int bitRate, int bitDepth)
        {
            try
            {
                // Get the endpoint
                string[] tokens = peerAddress.Split(',');
                IPEndPoint audioEP = new IPEndPoint(IPAddress.Parse(tokens[1].ToString()), 6000);
                // Connect to the peer
                Connect(audioEP, deviceID, bitRate, bitDepth);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void Connect(IPEndPoint audioEP, int deviceID, int bitRate, int bitDepth)
        {
            sourcestream.StartRecording();
            sourcestream = new WaveIn();
            sourcestream.BufferMilliseconds = 50;
            sourcestream.DeviceNumber = deviceID;
            sourcestream.WaveFormat = new WaveFormat(bitRate, bitDepth, WaveIn.GetCapabilities(deviceID).Channels);
            sourcestream.DataAvailable += sourcestream_DataAvailable;

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
            try
            {
                udpSender.Close();
                udpListener.Close();

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
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void AudioListener()
        {
            IPEndPoint listeningEP = new IPEndPoint(IPAddress.Any, 6000);
            try
            {
                udpListener = new UdpClient();
                udpListener.Client.Bind(listeningEP);

                while (true)
                {
                    byte[] buffer = udpListener.Receive(ref listeningEP);
                    waveProvider.AddSamples(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
