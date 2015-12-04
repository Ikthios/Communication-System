using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        TcpListener loginListener;
        TcpListener registrationListener;
        TcpListener addFriendListener;
        TcpListener getFriendsListener;

        private void LoginListener()
        {
            loginListener = new TcpListener(IPAddress.Parse(GetIpAddress()), 6000);
            try
            {
                loginListener.Start();
                while (true)
                {
                    Socket connected = loginListener.AcceptSocket();
                    LoginHandler handler = new LoginHandler(connected);
                    handler.StartHandling();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void RegistrationListener()
        {
            registrationListener = new TcpListener(IPAddress.Parse(GetIpAddress()), 7000);
            try
            {
                registrationListener.Start();
                while (true)
                {
                    Socket connected = registrationListener.AcceptSocket();
                    Console.WriteLine("Registraton connected");
                    RegistrationHandler handler = new RegistrationHandler(connected);
                    handler.StartHandling();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void AddFriendListener()
        {
            addFriendListener = new TcpListener(IPAddress.Parse(GetIpAddress()), 8000);
            try
            {
                addFriendListener.Start();
                while (true)
                {
                    Socket connected = addFriendListener.AcceptSocket();
                    AddFriendHandler handler = new AddFriendHandler(connected);
                    handler.StartHandling();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void GetFriendsListener()
        {
            getFriendsListener = new TcpListener(IPAddress.Parse(GetIpAddress()), 9000);
            try
            {
                getFriendsListener.Start();
                while (true)
                {
                    Socket connected = getFriendsListener.AcceptSocket();
                    FriendListHandler handler = new FriendListHandler(connected);
                    handler.StartHandling();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public string GetIpAddress()
        {
            IPHostEntry host;
            string localIp = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress address in host.AddressList)
            {
                if (address.AddressFamily.ToString() == "InterNetwork")
                {
                    localIp = address.ToString();
                }
            }
            return localIp;
        }

        static void Main(string[] args)
        {
            Server temp = new Server();

            Thread registration = new Thread(temp.RegistrationListener);
            registration.Start();

            Thread login = new Thread(temp.LoginListener);
            login.Start();

            Thread addFriend = new Thread(temp.AddFriendListener);
            addFriend.Start();

            Thread friendsList = new Thread(temp.GetFriendsListener);
            friendsList.Start();

            ActiveUserListener active = new ActiveUserListener(12000);
            active.StartUDPListener();

            FriendNotificationHandler notive = new FriendNotificationHandler();
            notive.startFriendNotificationHandler();
        }
    }
} 