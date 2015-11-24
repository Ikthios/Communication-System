using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{


    class AddFriendHandler
    {
        DatabaseWorker worker = new DatabaseWorker();
        private Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Encoding encoding = Encoding.ASCII;

        public AddFriendHandler(Socket client)
        {
            clientSocket = client;
        }

        public void StartHandling()
        {
            Thread handler = new Thread(AddFriendRequest);
            handler.Start();
        }

        public void AddFriendRequest()
        {
            string request = "";

            int byteCount;
            byte[] incoming = new byte[1500];
            byteCount = clientSocket.Receive(incoming);

            request = encoding.GetString(incoming);

            string[] package = request.Split(',');

            if(package[0] == "ADD")
            {

            }
            if(package[1] == "ACCEPT")
            {

            }
        }
    }
}
