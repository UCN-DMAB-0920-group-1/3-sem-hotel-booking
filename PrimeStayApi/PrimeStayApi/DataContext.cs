using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.Enviroment;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeStayApi
{
    public class DataContext : IDataContext
    {

        public IDbConnection OpenConnection()
        {
            IDbConnection conn = new SqlConnection(ENV.ConnectionString);
            conn.Open();
            return conn;
        }
    }
}
