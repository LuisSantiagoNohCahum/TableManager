using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableManager.Globals
{
    public class GLOBALS
    {
        public static void Load(string Server, string Database, string User, string Password, int TimeOut = 9999)
        {
            Helpers.SqlCustomConnection connection = Helpers.SqlCustomConnection.GetInstance();
            connection.Load(Server, Database, User, Password, TimeOut);
            ConnectionString = connection.ConnectionString;
        }

        public static string ConnectionString = string.Empty;
        //public static readonly Global CONFIG = new Global();
    }
}
