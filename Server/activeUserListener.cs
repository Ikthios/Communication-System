using System;
using System.Collections.Generic;
using System.Linq;
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
        private static Semaphore userSemaphore;
        int receivingPort;
        private static System.Timers.Timer checkTimer;
        static HashSet<String> usernames = new HashSet<String>();
        static HashSet<String> ips = new HashSet<String>();
        static List<User> users = new List<User>();

        public ActiveUserListener(int newPort)
        {
            receivingPort = newPort;
        }

        public void StartUDPListener()
        {
            userSemaphore = new Semaphore(1, 1);
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
                    string username = holster[0];
                    string ip = holster[1];
                    userSemaphore.WaitOne();
                    usernames.Add(username);
                    ips.Add(ip);
                    userSemaphore.Release();
                }
            }
            catch (Exception e)
            {

            }
        }

        private static void SetTimer()
        {
            // Create a timer with a twenty second interval.
            checkTimer = new System.Timers.Timer(5000);
            // Hook up the Elapsed event for the timer. 
            checkTimer.Elapsed += OnTimedEvent;
            checkTimer.AutoReset = true;
            checkTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);

            string[] localNames = usernames.ToArray();
            string[] localIPS = ips.ToArray();

            users.Clear();

            for (int i = 0; i < localNames.Length; i++)
            {
                User temp = new User();
                temp.Username = localNames[i];
                temp.IP = localIPS[i];
                users.Add(temp);
            }

            foreach (var user in users)
            {
                SendActiveList(user);
            }
            users.Clear();
        }

        private static void SendActiveList(User destinationUser)
        {
            userSemaphore.WaitOne();
            Socket sending_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            string message;
            string[] localNames = usernames.ToArray();
            string[] localIPS = ips.ToArray();

            foreach (var user in users)
            {
                message = "";
                for(int i = 0; i < localNames.Length; i++)
                {
                    message += localNames[i];
                    message += " ";
                    message += localIPS[i];
                    message += ',';
                }
                
                IPAddress send_to_address = IPAddress.Parse(destinationUser.IP);
                IPEndPoint sending_end_point = new IPEndPoint(send_to_address, 12001);

                byte[] data = Encoding.ASCII.GetBytes(message);

                try
                {
                    sending_socket.SendTo(data, sending_end_point);
                }
                catch (Exception e)
                {

                }
            }
            userSemaphore.Release();
        }
    }

}