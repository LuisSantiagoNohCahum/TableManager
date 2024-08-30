using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableManager.Helpers
{
    public class SqlCustomConnection
    {
        public string ConnectionString 
        {
            get 
            { 
                return ConnectionBuilder.ConnectionString; 
            }
            set 
            {
                this.ConnectionBuilder.ConnectionString = value;
            }
        }

        public static SqlCustomConnection Instance = null;

        public static SqlCustomConnection GetInstance() 
        {
            if (Instance is null)
            {
                Instance = new SqlCustomConnection();
            }

            return Instance;
        }

        private SqlConnectionStringBuilder ConnectionBuilder;
        public SqlCustomConnection() 
        {
            ConnectionBuilder = new SqlConnectionStringBuilder();
        }

        public void Load(string Server, string Database, string User, string Password, int TimeOut)
        {
            ConnectionBuilder.DataSource = Server;
            ConnectionBuilder.InitialCatalog = Database;
            ConnectionBuilder.UserID = User;
            ConnectionBuilder.Password = Password;
            ConnectionBuilder.Authentication = SqlAuthenticationMethod.SqlPassword;
            ConnectionBuilder.ConnectTimeout = TimeOut;
            ConnectionBuilder.TrustServerCertificate = true;
            ConnectionBuilder.IntegratedSecurity = false;
        }
    }
}
