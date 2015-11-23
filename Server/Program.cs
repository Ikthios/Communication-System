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
            test.ClearFriendsTable();
            test.ClearUsersTable();
        }
    }

    

    
} 