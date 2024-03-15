using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace miniCRM
{
    internal class DataBase
    {

        //SqlConnection connection =
        //    new SqlConnection(@"Data Source=WIN-1JJAN51R1V4\SQLEXPRESS; Database=clients; Persist Security Info=false; MultipleActiveResultSets=True; Trusted_Connection=True;"); // ;Integradet Security = True , Initial Catalog=clients;
        SqlConnection connection = new SqlConnection(@"Data Source=WIN-1JJAN51R1V4\SQLEXPRESS; Initial Catalog=clients; Integrated Security=True");
        
        public void openConnection()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
            { 
                connection.Open();
            }
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            return connection;
        }
    }
}
