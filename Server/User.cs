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

        public User()
        {
            Username = "";
            Password = "";
            Name = "";
            Email = "";
            Address = "";
            Phone = "";
            DateOfBirth = new DateTime(1900, 1, 1);
        }

        public User(string username, string password, string name, string email, string address, string phone, int year, int month, int day)
        {
            Username = username;
            Password = password;
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            DateOfBirth = new DateTime(year, month, day);
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
    }
}
