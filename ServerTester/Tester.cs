using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerTester
{
    class ServerTester
    {
        Socket regSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        UdpClient activeListener = new UdpClient(1000);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, 1000);

        IPEndPoint server;

        public ServerTester()
        {

        }

        public void Register()
        {
            server = new IPEndPoint(IPAddress.Parse("134.129.51.167"), 7000);
            regSocket.Connect(server);

            string info = "Username,Password,,,,,,,";

            regSocket.Send(Encoding.ASCII.GetBytes(info));

            ReceiveReply();
        }

        public void Login()
        {
            server = new IPEndPoint(IPAddress.Parse("134.129.51.167"), 6000);
            regSocket.Connect(server);

            string info = "Username,Password";

            regSocket.Send(Encoding.ASCII.GetBytes(info));

            ReceiveReply();
        }

        public void AddFriendAdd()
        {
            server = new IPEndPoint(IPAddress.Parse("134.129.51.167"), 8000);
            regSocket.Connect(server);

            string info = "ADD,Username,FriendUsername,100.000.000.000";

            regSocket.Send(Encoding.ASCII.GetBytes(info));

            ReceiveReply();
        }

        public void AddFriendAccept()
        {
            server = new IPEndPoint(IPAddress.Parse("134.129.51.167"), 8000);
            regSocket.Connect(server);

            string info = "ACCEPT,Username,FriendUsername";

            regSocket.Send(Encoding.ASCII.GetBytes(info));

            ReceiveReply();
        }

        public void AddFriendReject()
        {
            server = new IPEndPoint(IPAddress.Parse("134.129.51.167"), 8000);
            regSocket.Connect(server);

            string info = "REJECT,Username,FriendUsername";

            regSocket.Send(Encoding.ASCII.GetBytes(info));

            ReceiveReply();
        }

        public void GetFriendList()
        {
            server = new IPEndPoint(IPAddress.Parse("134.129.51.167"), 9000);
            regSocket.Connect(server);

            string info = "Username";

            regSocket.Send(Encoding.ASCII.GetBytes(info));

            ReceiveReply();
        }

        //Thread me!
        public void pingActive()
        {
            server = new IPEndPoint(IPAddress.Parse("134.129.51.167"), 1000);

            string info = "Username,100.100.100.100";

            while (true)
            {
                udpSocket.SendTo(Encoding.ASCII.GetBytes(info), server);
            }
        }

        //Thread me!
        public void receiveActivePing()
        {
            string received_data;
            byte[] receive_byte_array;

            receive_byte_array = activeListener.Receive(ref groupEP);
            received_data = Encoding.ASCII.GetString(receive_byte_array, 0, receive_byte_array.Length);

            Console.WriteLine(received_data);
        }


        public void ReceiveReply()
        {
            byte[] buffer = new byte[1500];
            string dataString = "";

            regSocket.Receive(buffer);
            for (int i = 0; i < buffer.Length; i++)
            {
                dataString += Convert.ToChar(buffer[i]);
            }

            Console.WriteLine(dataString);
        }

        static void Main(string[] args)
        {
            ServerTester temp = new ServerTester();
            Thread pinger = new Thread(temp.pingActive);
            pinger.Start();

            Thread receive = new Thread(temp.receiveActivePing);
            receive.Start();
        }
    }
}
