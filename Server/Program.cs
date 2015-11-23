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
            DatabaseWorker worker = new DatabaseWorker();
            User test2 = new User();
            User test = new User();
            string startupPath = Environment.CurrentDirectory;
            Console.WriteLine(startupPath);
            Console.Read();
        }
    }

    

    
}
