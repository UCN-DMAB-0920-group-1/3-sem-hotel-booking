using API.Controllers;
using API.Models;
using API.Services;
using DataAccessLayer;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Tests.Extensions;
using Tests.Integration.Common;

namespace Tests.Integration
{
    [TestClass]
    public class AccountControllerIntegrationTest : BaseDbSetup
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
        public void LoginSuccessTest()
        {
            //Arrange 
            IDao<UserEntity> userDao = DaoFactory.Create<UserEntity>(_dataContext);
            IDao<CustomerEntity> customerDao = DaoFactory.Create<CustomerEntity>(_dataContext);
            IAccountService accountService = new AccountService(_conf, userDao, customerDao);
            AccountController controller = new AccountController(accountService);

            LoginRequest user = new()
            {
                Username = "admin",
                Password = "admin"
            };

            //Act 
            ActionResult<LoginResponse> response = controller.Login(user);

            //Assert
            response.Should().NotBeNull();
            response.Result.Should().NotBeNull().And.BeAssignableTo(typeof(ObjectResult));
            response.Result.As<ObjectResult>().StatusCode.Should().Be(200);
            response.GetValue().Should().NotBeNull().And.BeOfType(typeof(LoginResponse));
        }
    }
}
