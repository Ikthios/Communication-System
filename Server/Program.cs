using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        static void Main(string[] args)
        {
            
            DatabaseWorker test = new DatabaseWorker();
            test.ClearUsersTable();
            User user1 = new User();
            User user2 = new User();
            user1.Username = "HEY";
            user1.Password = "HELLO";
            user2.Username = "Mac";
            test.AddUser(user1);
            
            test.AddFriend(user1, user2);
            test.getFriendsList(user1.Username);
            List<string> temp = test.getFriendsList(user1.Username);
            foreach (var a in temp)
            {
                Console.WriteLine(a);
            }
        }
    }

    

    
} 