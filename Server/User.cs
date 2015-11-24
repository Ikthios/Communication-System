using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class User
    {
        string username;
        string password;
        string name;
        string email;
        string address;
        string phone;
        DateTime dateOfBirth;
        string ip;

        public User()
        {
            Username = "";
            Password = "";
            Name = "";
            Email = "";
            Address = "";
            Phone = "";
            DateOfBirth = new DateTime(1900, 1, 1);
            ip = "";
        }

        public User(string username, string password, string name, string email, string address, string phone, int year, int month, int day, string ip)
        {
            Username = username;
            Password = password;
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            DateOfBirth = new DateTime(year, month, day);
            IP = ip;
        }

        public User(string [] info)
        {
            Username = info[0];
            Password = info[1];
            Name = info[2];
            Email = info[3];
            Address = info[4];
            Phone = info[5];
            DateOfBirth = new DateTime(Int32.Parse(info[6]), Int32.Parse(info[7]), Int32.Parse(info[8]));
            IP = info[9];
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value;  }
        }

        public string IP
        {
            get { return ip; }
            set { ip = value; }
        }
    }
}
