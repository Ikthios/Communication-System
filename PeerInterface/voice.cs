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
        WaveIn listenerstream = null;
        WaveOut listenerout = null;
        WaveOut waveout = null;
        BufferedWaveProvider waveProvider = null;
        BufferedWaveProvider listenerProvider = null;

        int deviceID, bitRate, bitDepth;

        public void Start(string peerAddress, int deviceID, int bitRate, int bitDepth)
        {
            try
            {
                this.deviceID = deviceID;
                this.bitRate = bitRate;
                this.bitDepth = bitDepth;
                // Get the endpoint
                //string[] tokens = peerAddress.Split(',');
                //IPEndPoint audioEP = new IPEndPoint(IPAddress.Parse(tokens[1].ToString()), 6000);
                IPEndPoint audioEP = new IPEndPoint(IPAddress.Parse(peerAddress), 6000);
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

        public void AudioListener()
        {
            IPEndPoint listeningEP = new IPEndPoint(IPAddress.Any, 5000);
            try
            {
                listenerstream = new WaveIn();
                listenerstream.BufferMilliseconds = 50;
                listenerstream.DeviceNumber = this.deviceID;
                listenerstream.WaveFormat = new WaveFormat(this.bitRate, this.bitDepth, WaveIn.GetCapabilities(this.deviceID).Channels);
                listenerstream.DataAvailable += listenerstream_DataAvailable;
                listenerstream.StartRecording();

                udpListener = new UdpClient();
                udpListener.Client.Bind(listeningEP);

                listenerout = new WaveOut();
                listenerProvider = new BufferedWaveProvider(listenerstream.WaveFormat);
                listenerout.Init(listenerProvider);
                listenerout.Play();

                while (true)
                {
                    byte[] buffer = udpListener.Receive(ref listeningEP);
                    listenerProvider.AddSamples(buffer, 0, buffer.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void listenerstream_DataAvailable(object sender, WaveInEventArgs e)
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
    }
}
