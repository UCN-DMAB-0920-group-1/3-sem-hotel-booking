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
        public void CreateCustomerTest()
        {
            IDao<UserDto> userDao = DaoFactory.Create<UserDto>(_dataContext, Auth.AccessToken);
            var token = (userDao as IDaoAuthExtension<UserDto>).login("admin", "admin").Token;

            IDao<CustomerDto> dao = DaoFactory.Create<CustomerDto>(_dataContext, token);

            var model = new CustomerDto()
            {
                Name = "Create test",
                Email = "Create@test",
                Phone = "12345678",
                BirthDay = DateTime.Parse("2001-09-01"),
            };
            string href = dao.Create(model);

            Assert.IsTrue(!string.IsNullOrEmpty(href));
            //Assert.AreEqual(href, "api/customer/13");
        }

        [TestMethod()]
        public void UpdateUserTest()
        {
            //assign
            IDao<UserDto> userDao = DaoFactory.Create<UserDto>(_dataContext, null);
            var token = (userDao as IDaoAuthExtension<UserDto>).login("admin", "admin").Token;

            IDao<CustomerDto> dao = DaoFactory.Create<CustomerDto>(_dataContext, token);
            var changedCustomer = new CustomerDto()
            {
                Href = "api/customer/3",
                Name = "Update test",
                Email = "Update@test",
                Phone = "12345678",
                BirthDay = DateTime.Parse("2001-09-01"),
            };

            //act
            int updated = dao.Update(changedCustomer);

            //assert
            Assert.AreEqual(1, updated);
        }

        [TestMethod()]
        public void DeleteCustomer()
        {
            IDao<UserDto> userDao = DaoFactory.Create<UserDto>(_dataContext, Auth.AccessToken);
            var token = (userDao as IDaoAuthExtension<UserDto>).login("admin", "admin").Token;

            IDao<CustomerDto> dao = DaoFactory.Create<CustomerDto>(_dataContext, token);

            var model = new CustomerDto()
            {
                Href = "api/customer/2",
                Name = "Delete test",
                Email = "Delete@test",
                Phone = "12345678",
                BirthDay = DateTime.Parse("2001-09-01"),
            };
            int rowsAffected = dao.Delete(model);


            Assert.AreEqual(1, rowsAffected);
        }
    }
}
