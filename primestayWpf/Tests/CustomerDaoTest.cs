using DataAccessLayer;
using DataAccessLayer.DAO;
using DataAccessLayer.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using WinApp;
using WinApp.src.auth;

namespace Test
{
    [TestClass()]
    public class CustomerDaoTest
    {
        private static IDataContext<IRestClient> _dataContext = RestDataContext.GetInstance();

        [TestMethod()]
        public void ReadAllTest()
        {
            //assign
            IDao<UserDto> userDao = DaoFactory.Create<UserDto>(_dataContext, null);
            var token = (userDao as IDaoAuthExtension<UserDto>).login("admin", "admin").Token;

            IDao<CustomerDto> dao = DaoFactory.Create<CustomerDto>(_dataContext, token);

            //act
            var customers = dao.ReadAll(null);

            //assert
            Assert.IsNotNull(customers);
            Assert.IsInstanceOfType(customers, typeof(IEnumerable<CustomerDto>));
            Assert.IsTrue(customers.Any());
        }

        [TestMethod()]
        public void ReadAllTestFail()
        {
            //assign
            IDao<UserDto> userDao = DaoFactory.Create<UserDto>(_dataContext, null);
            var token = (userDao as IDaoAuthExtension<UserDto>).login("admin", "admin").Token;

            IDao<CustomerDto> dao = DaoFactory.Create<CustomerDto>(_dataContext, token);

            //act
            var customers = dao.ReadAll(new CustomerDto { Email = "test@test.test" });

            //assert
            Assert.IsNotNull(customers);
            Assert.IsTrue(!customers.Any());
        }

        [TestMethod()]
        public void UpdateUserTest()
        {
            //assign
            IDao<UserDto> userDao = DaoFactory.Create<UserDto>(_dataContext, null);
            var token = (userDao as IDaoAuthExtension<UserDto>).login("admin", "admin").Token;

            IDao<CustomerDto> dao = DaoFactory.Create<CustomerDto>(_dataContext, token);
            var changedCustomer = new CustomerDto
            {
                Href = "api/customer/5",
                Name = "Michael Graversen",
                Phone = "24267667",
                Email = "michael-graversen@hotmail.com",
                BirthDay = DateTime.Parse("2001-03-11")
            };

            //act
            int updated = dao.Update(changedCustomer);

            //assert
            Assert.AreEqual(1, updated);
        }
    }
}