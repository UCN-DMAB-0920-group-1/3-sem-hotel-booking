using Dapper;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Model;
using System.Collections.Generic;
using System.Data;

namespace PrimeStayApi.DataAccessLayer.SQL
{
    internal class UserDao : BaseDao<IDataContext<IDbConnection>>, IDao<UserEntity>
    {
        #region SQL-Queries
        private readonly static string INSERTUSER = @"INSERT INTO USERS (username, password, role, salt)" + 
                                                    @"VALUES(@username, @password, @role, @salt)";
        #endregion
        public UserDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        { 
        }
        public int Create(UserEntity model)
        {
            throw new System.NotImplementedException();
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
