using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class FriendNotificationHandler
    {
        DatabaseWorker worker = new DatabaseWorker();
        Socket sending_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        int sendingPort = 5000;

        public void startFriendNotificationHandler()
        {
            Thread senderThread = new Thread(NotificationSender);
            senderThread.Start();
        }

        public void NotificationSender()
        {
            while (true)
            {
                List<PendingRequest> sendTo = worker.GetPendingFriendRequests();

                foreach (var request in sendTo)
                {
                    IPAddress sendAddress = IPAddress.Parse(request.IP);
                    IPEndPoint sendEnpoint = new IPEndPoint(sendAddress, sendingPort);

                    string message = request.Username + "," + request.FriendUsername;
                    byte[] converted = Encoding.ASCII.GetBytes(message);

                    try
                    {
                        sending_socket.SendTo(converted, sendEnpoint);
                    }
                    catch(Exception e)
                    {
                        
                    }
                }

                Thread.Sleep(10000);
            }
        }
    }
}
