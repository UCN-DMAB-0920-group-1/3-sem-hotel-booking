﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Enviroment;
using PrimeStayApi.Model;
using PrimeStayApi.Models;
using PrimeStayApi.Services;
using PrimeStayApi.Services.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Version = PrimeStayApi.Database.Version;

namespace PrimeStayApi.Test
{
    [TestClass]
    public class AccountAuthenticationTest
    {
        #region setup
        private string connectionString = new ENV().ConnectionStringTest;
        private static IDataContext<IDbConnection> _dataContext;
        private static List<Action> _dropDatabaseActions = new();

        [TestInitialize]
        public void SetUp()
        {
            _dataContext = new SqlDataContext(connectionString);
            Version.Upgrade(connectionString);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            Parallel.Invoke(_dropDatabaseActions.ToArray());
        }

        [TestCleanup]
        public void CleanUp()
        {
            _dropDatabaseActions.Add(() => Version.Drop(connectionString));
        }
        #endregion

        [TestMethod]
        public void AuthenticationFailTest()
        {
            //Arrange 
            IDao<UserEntity> dao = DaoFactory.Create<UserEntity>(_dataContext);
            IAccountService accountService = new AccountService(null, dao);

            LoginRequest user = new()
            {
                Username = "Michael",
                Password = "qwertyuiopå"
            };

            //Act 
            Userinfo authUser = accountService.Authenticate(user.Username, user.Password);
            
            //Assert
            Assert.IsNull(authUser);
        }

        [TestMethod]
        public void AuthenticationSuccessTest()
        {
            //Arrange 
            IDao<UserEntity> dao = DaoFactory.Create<UserEntity>(_dataContext);
            IAccountService accountService = new AccountService(null, dao);

            LoginRequest user = new()
            {
                Username = "Michael",
                Password = "qwertyuiop"
            };

            //Act 
            Userinfo authUser = accountService.Authenticate(user.Username, user.Password);

            //Assert
            Assert.IsNotNull(authUser);
            Assert.AreEqual(user.Username, authUser.Username);
        }
    }
}