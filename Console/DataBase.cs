using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataCompany
{
    class DataBase
    {
        static SqlConnection sqlConnection = new SqlConnection(@"Data Source=LAPTOP-R88ICHUA\SEREVERMSSQL; Initial Catalog = Companies; Integrated Security = True");


        public static void openConnection() { 
        
            if(sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        public static void closeConnection()
        {

            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        public static SqlConnection GetConnection()
        {
            return sqlConnection;
        }
    }
}
