using Dapper;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PrimeStayApi.DataAccessLayer.SQL
{
    internal class UserDao : BaseDao<IDataContext<IDbConnection>>, IDao<UserEntity>
    {
        #region SQL-Queries
        private readonly static string INSERTUSER = @"INSERT INTO User (username, password, role, salt)" +
                                                    @"VALUES(@username, @password, @role, @salt)";
        #endregion
        public UserDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        {
        }

        public int Create(UserEntity model)
        {
            using IDbConnection connection = DataContext.Open();

            try
            {
                connection.Execute(INSERTUSER, model);
            }
            catch (SqlException e)
            {
                throw new Exception("User alreade exists"); //TODO custom exception
            }

            return -1;
        }

        public int Delete(UserEntity model)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserEntity> ReadAll(UserEntity model)
        {
            throw new System.NotImplementedException();
        }

        public UserEntity ReadById(int id)
        {
            throw new System.NotImplementedException();
        }

        public int Update(UserEntity model)
        {
            throw new System.NotImplementedException();
        }
    }
}
