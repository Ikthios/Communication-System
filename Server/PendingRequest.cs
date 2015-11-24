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
        string friendUsername;
        string ip;

        public PendingRequest()
        {
            Username = "";
            FriendUsername = "";
            IP = "";
        }

        public PendingRequest(string username, string friendUsername, string ip)
        {
            Username = username;
            FriendUsername = friendUsername;
            IP = ip;
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
