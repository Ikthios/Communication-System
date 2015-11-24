using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class FriendListHandler
    {
        DatabaseWorker worker = new DatabaseWorker();
        private Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Encoding encoding = Encoding.ASCII;

        public FriendListHandler(Socket client)
        {
            clientSocket = client;
        }

        public void StartHandling()
        {
            Thread handler = new Thread(getFriendsList);
            handler.Start();
        }

        public void getFriendsList()
        {
            string request = "";

            int byteCount;
            byte[] incoming = new byte[1500];
            byteCount = clientSocket.Receive(incoming);

            request = encoding.GetString(incoming);

            string[] package = request.Split(',');

            List<string> friendsList = worker.getFriendsList(package[0]);

            string reply = "";

            foreach(var friend in friendsList)
            {
                reply += friend + ",";
            }

            byte[] message = encoding.GetBytes(reply);

            byteCount = clientSocket.Send(message);

            clientSocket.Close();
        }
    }
}
