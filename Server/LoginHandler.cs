using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class LoginHandler
    {
        DatabaseWorker worker = new DatabaseWorker();
        private Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Encoding encoding = Encoding.ASCII; 

        public LoginHandler(Socket client)
        {
            clientSocket = client;
        }

        public void StartHandling()
        {
            Thread handler = new Thread(loginRequest);
            handler.Start();
        }

        public void loginRequest()
        {
            string request = "";

            int byteCount;
            byte[] incoming = new byte[1500];
            byteCount = clientSocket.Receive(incoming);

            request = encoding.GetString(incoming);

            string[] credentials = request.Split(',');

            bool success = worker.login(credentials[0], credentials[1]);

            if(success)
            {
                worker.UpdateIPAddress(credentials[0], credentials[2]);
                SendUserInfo(credentials[0]);
            }
            else
            {
                string failed = "FAILED";
                byte[] message = encoding.GetBytes(failed);

                byteCount = clientSocket.Send(message);
                clientSocket.Close();
            }
        }

        public void SendUserInfo(string username)
        {
            int bytecount;
            List<String> values = worker.getUsernameInfo(username);
            string replyString = "SUCCESS,";

            foreach(var item in values)
            {
                replyString += item + ",";
            }

            byte[] message = encoding.GetBytes(replyString);

            bytecount = clientSocket.Send(message);
            clientSocket.Close();
        }
    }
}
