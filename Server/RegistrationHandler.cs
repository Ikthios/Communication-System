using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class RegistrationHandler
    {
        DatabaseWorker worker = new DatabaseWorker();
        private Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Encoding encoding = Encoding.ASCII;

        public RegistrationHandler(Socket client)
        {
            clientSocket = client;
        }

        public void StartHandling()
        {
            Thread handler = new Thread(RegistrationRequest);
            handler.Start();
        }

        public void RegistrationRequest()
        {
            string request = "";

            int byteCount;
            byte[] incoming = new byte[1500];
            byteCount = clientSocket.Receive(incoming);

            for(int i = 0; i < incoming.Length; i++)
            {
                request += Convert.ToChar(incoming[i]);
            }

            string[] userInfo = request.Split(',');

            List<String> values = worker.getUsernameInfo(userInfo[0]);

            if(values.Count == 0)
            {
                addUser(userInfo);
            }
            else
            {
                string failed = "FAILED, Username Taken";
                byte[] message = encoding.GetBytes(failed);

                byteCount = clientSocket.Send(message);
                clientSocket.Close();
            }
        }

        public void addUser(string[] info)
        {
            int byteCount;
            User temp = new User(info);
            worker.AddUser(temp);

            string success = "SUCCESS";
            byte[] message = encoding.GetBytes(success);

            byteCount = clientSocket.Send(message);

            clientSocket.Close();
        }
    }
}
