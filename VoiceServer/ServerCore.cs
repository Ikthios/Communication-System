using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace VoiceServer
{
    class ServerCore
    {
        static void Main(string[] args)
        {
            UdpListener listener = new UdpListener();
            listener.StartUDPListener();
        }
    }
}