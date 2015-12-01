using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Server
{
    public class ActiveUserListener
    {
        int receivingPort;
        private static System.Timers.Timer checkTimer;
        static List<User> activeUsers = new List<User>();

        public ActiveUserListener(int newPort)
        {
            receivingPort = newPort;
        }

        public void StartUDPListener()
        {
            Thread receivingThread = new Thread(messageReceiver);
            receivingThread.Start();
            SetTimer();
        }

        public void messageReceiver()
        {
            UdpClient activeListener = new UdpClient(receivingPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, receivingPort);
            string received_data;
            byte[] receive_byte_array;
            try
            {
                while (true)
                {
                    receive_byte_array = activeListener.Receive(ref groupEP);
                    received_data = Encoding.ASCII.GetString(receive_byte_array, 0, receive_byte_array.Length);
                    string[] holster = received_data.Split(',');
                    User temp = new User();
                    temp.Username = holster[0];
                    temp.IP = holster[1];
                    activeUsers.Add(temp);
                }
            }
            catch (Exception e)
            {

            }
        }

        private static void SetTimer()
        {
            // Create a timer with a twenty second interval.
            checkTimer = new System.Timers.Timer(20000);
            // Hook up the Elapsed event for the timer. 
            checkTimer.Elapsed += OnTimedEvent;
            checkTimer.AutoReset = true;
            checkTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
            foreach (var user in activeUsers)
            {
                SendActiveList(user);
            }
            activeUsers.Clear();
        }

        private static void SendActiveList(User destinationUser)
        {
            Socket sending_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            string message;
            foreach (var user in activeUsers)
            {
                message = "";
                message = user.Username + ',' + user.IP;
                
                IPAddress send_to_address = IPAddress.Parse(destinationUser.IP);
                IPEndPoint sending_end_point = new IPEndPoint(send_to_address, 1000);

                byte[] data = Encoding.ASCII.GetBytes(message);

                try
                {
                    sending_socket.SendTo(data, sending_end_point);
                }
                catch (Exception e)
                {

                }
            }
        }
    }

}