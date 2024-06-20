using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pogranichnayia_Slugba
{
    internal class DataBase
    {
        SqlConnection SqlConnection = new SqlConnection(@"Data Source=WIN-S9JE7F1PV37;initial Catalog= Информационная система пограничной службы;Integrated Security=True");

        public void openConnection()
        {
            if (SqlConnection.State == System.Data.ConnectionState.Closed)
            {
                SqlConnection.Open();
            }
        }

        public void closeConnection()
        {
            if (SqlConnection.State != System.Data.ConnectionState.Open)
            {
                SqlConnection.Close();
            }
        }

        public SqlConnection getConnection()
        {
            return SqlConnection;
        }
    }
}
