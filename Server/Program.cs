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
                    RegistrationHandler handler = new RegistrationHandler(connected);
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
            string test = "hello,there,clarice,";
            string[] mack = test.Split(',');
            Console.WriteLine(mack.Length);
            Console.Read();
        }
    }
} 