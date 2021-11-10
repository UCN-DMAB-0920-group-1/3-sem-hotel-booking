using PrimeStayApi.DataAccessLayer;
using System.Data;
using System.Data.SqlClient;

namespace PrimeStayApi
{
    public class SqlDataContext : IDataContext<IDbConnection>
    {
        private string _connectionString;

        public SqlDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection Open()
        {
            // ADO.NET creates a pool for the specific connectionstring supplied ot the contructor og the sqlconnection object.
            // There is no need to worry about it opening lots of connection, everytime the method is called.
            // Remember to call connection.Close() og connection.Dispose() after use, so that the connection will be returned to the connection pool.
            // More info https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-connection-pooling
            IDbConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }
    }
}
