using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class PendingRequest
    {
        string username;
        string userIP;
        string friendUsername;
        string ip;

        public PendingRequest()
        {
            Username = "";
            UserIP = "";
            FriendUsername = "";
            IP = "";
        }

        public PendingRequest(string username, string userIP, string friendUsername, string ip)
        {
            Username = username;
            UserIP = userIP;
            FriendUsername = friendUsername;
            IP = ip;
        }

        public string UserIP
        {
            get { return userIP; }
            set { userIP = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string FriendUsername
        {
            get { return friendUsername; }
            set { friendUsername = value; }
        }

        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }
    }
}
