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
            // ADO.NET creates a pool for the specific connectionstring supplied ot the contructor og the sqlconnection object.
            // There is no need to worry about it opening lots of connection, everytime the method is called.
            // Remember to call connection.Close() og connection.Dispose() after use, so that the connection will be returned to the connection pool.
            IDbConnection conn = new SqlConnection(ENV.ConnectionString);
            conn.Open();
            return conn;
        }
    }
}
