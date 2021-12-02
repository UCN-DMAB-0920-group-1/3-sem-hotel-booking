using API.Models;
using API.Services;
using API.Services.Models;
using DataAccessLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Tests.Integration.Common;

namespace Tests.Integration
{
    [TestClass]
    public class AccountServiceTest : BaseDbSetup
    {
        #region setup
        private static IConfiguration _conf;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _conf = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }
        #endregion

        [TestMethod]
        public void AuthenticationFailTest()
        {
            //Arrange 
            IDao<UserEntity> dao = DaoFactory.Create<UserEntity>(_dataContext);
            IAccountService accountService = new AccountService(_conf, dao);

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
            IAccountService accountService = new AccountService(_conf, dao);

            LoginRequest user = new()
            {
                Username = "Michael",
                Password = "qwertyuiop"
            };

            //Act 
            Userinfo authUser = accountService.Authenticate(user.Username, user.Password);

            //Assert
            Assert.IsNotNull(authUser);
            Assert.IsNotNull(authUser.Token);
            Assert.AreEqual(user.Username, authUser.Username);
        }

        [TestMethod]
        public void UserRegisterSuccessTest()
        {
            //Arrange 
            IDao<UserEntity> dao = DaoFactory.Create<UserEntity>(_dataContext);
            IAccountService accountService = new AccountService(_conf, dao);

            LoginRequest user = new()
            {
                Username = "Nico",
                Password = "123"
            };

            //Act 
            Userinfo authUser = accountService.Save(user.Username, user.Password, "admin");

            //Assert
            Assert.IsNotNull(authUser);
            Assert.IsNotNull(authUser.Token);
            Assert.AreEqual(user.Username, authUser.Username);
        }


        [TestMethod]
        public void UserRegisterFailTest()
        {
            //Arrange 
            IDao<UserEntity> dao = DaoFactory.Create<UserEntity>(_dataContext);
            IAccountService accountService = new AccountService(_conf, dao);

            LoginRequest user = new()
            {
                Username = "Michael",
                Password = "123"
            };

            //Act 
            Userinfo authUser = accountService.Save(user.Username, user.Password, "admin");

            //Assert
            Assert.IsNull(authUser);
        }

        [TestMethod]
        public void UserRegisterRoleFailTest()
        {
            //Arrange 
            IDao<UserEntity> dao = DaoFactory.Create<UserEntity>(_dataContext);
            IAccountService accountService = new AccountService(_conf, dao);

            LoginRequest user = new()
            {
                Username = "admin",
                Password = "admin"
            };

            //Act 
            Userinfo authUser = accountService.Save(user.Username, user.Password, "random");

            //Assert
            Assert.IsNull(authUser);
        }
    }
}