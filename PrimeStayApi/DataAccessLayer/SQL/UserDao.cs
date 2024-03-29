﻿using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer.SQL
{
    internal class UserDao : BaseDao<IDataContext<IDbConnection>>, IDao<UserEntity>
    {
        #region SQL-Queries
        private readonly static string INSERT_USER = @"INSERT INTO [User] ([username], [password], [salt], [role_id]) " +
                                                    @"VALUES(@username, @password, @salt, (SELECT [id] FROM [Role] WHERE [Role].[name] = @Role))";

        private readonly static string SELECT_USER = @"SELECT [User].[id], [username], [password], [salt], [Role].[name] AS [role] FROM [User] " +
                                                    @"JOIN [Role] ON [User].[role_id] = [Role].[id] " +
                                                    @"WHERE [User].[username] = @Username";

        #endregion
        public UserDao(IDataContext<IDbConnection> dataContext) : base(dataContext)
        {
        }

        public int Create(UserEntity model)
        {
            using IDbConnection connection = DataContext.Open();

            try
            {
                return connection.Execute(INSERT_USER, model);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw new DaoException("Could not create new user");
            }
        }

        public int Delete(UserEntity model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserEntity> ReadAll(UserEntity model)
        {
            using IDbConnection connection = DataContext.Open();

            try
            {
                return connection.Query<UserEntity>(SELECT_USER, model);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                throw new DaoException($"Could not find user with username: {model.Username}");
            }
        }

        public UserEntity ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(UserEntity model)
        {
            throw new NotImplementedException();
        }
    }
}
