using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//TEST
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
            Thread handler = new Thread(FriendRequest);
            handler.Start();
        }

        public void FriendRequest()
        {
            string request = "";

            int byteCount;
            byte[] incoming = new byte[1500];
            byteCount = clientSocket.Receive(incoming);

            request = encoding.GetString(incoming);

            string[] package = request.Split(',');

            if(package[0] == "ADD")
            {
                User user = new User();
                user.Username = package[1];
                user.IP = package[2];

                User friend = new User();
                friend.Username = package[3];
                friend.IP = package[4];

                bool areFriends = checkIfFriends(user.Username, friend.Username);

                if(areFriends)
                {
                    string info = "Already added by friend.  Accept invite when prompted.";
                    byte[] message = encoding.GetBytes(info);

                    byteCount = clientSocket.Send(message);
                    clientSocket.Close();
                }
                else
                {
                    worker.AddFriend(user, friend);
                    string info = "Friend added.";
                    byte[] message = encoding.GetBytes(info);

                    byteCount = clientSocket.Send(message);
                    clientSocket.Close();
                }
            }

            if(package[0] == "ACCEPT")
            {
                User user = new User();
                //user username, user ip, friend username, friend ip
                user.Username = package[1];
                user.IP = package[2];

                User friend = new User();
                friend.Username = package[3];
                friend.IP = package[4];

                bool success = worker.setToAccept(user, friend);

                if (success)
                {
                    string info = "Friend request accepted.";
                    byte[] message = encoding.GetBytes(info);

                    byteCount = clientSocket.Send(message);
                    clientSocket.Close();
                }
                else
                {
                    string info = "Friend accept invalid";
                    byte[] message = encoding.GetBytes(info);

                    byteCount = clientSocket.Send(message);
                    clientSocket.Close();
                }
            }

            if(package[0] == "REJECT")
            {
                User user = new User();
                user.Username = package[1];

                User friend = new User();
                friend.Username = package[2];

                worker.reject(user, friend);

                string info = "Friend request rejected.";
                byte[] message = encoding.GetBytes(info);

                byteCount = clientSocket.Send(message);
                clientSocket.Close();
            } 
        }

        public bool checkIfFriends(string userName, string FriendUsername)
        {
            bool areFriends = false;
            List<string> friends = worker.getFriendsList(FriendUsername);

            foreach(string f in friends)
            {
                if(f.Equals(userName))
                {
                    areFriends = true;
                }
            }

            return areFriends;
        }
    }
}
