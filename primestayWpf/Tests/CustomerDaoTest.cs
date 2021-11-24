using DataAccessLayer;
using DataAccessLayer.DAO;
using DataAccessLayer.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
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
            IDao<UserDto> userDao = DaoFactory.Create<UserDto>(_dataContext, Auth.AccessToken);
            var token = (userDao as IDaoAuthExtension<UserDto>).login("admin", "admin").Token;

            IDao<CustomerDto> dao = DaoFactory.Create<CustomerDto>(_dataContext, Auth.AccessToken);

            //act
            var customers = dao.ReadAll(null);

            //assert
            Assert.IsNotNull(customers);
            Assert.IsTrue(customers.Any());
        }

        [TestMethod()]
        public void CreateCustomerTest()
        {
            IDao<UserDto> userDao = DaoFactory.Create<UserDto>(_dataContext, Auth.AccessToken);
            var token = (userDao as IDaoAuthExtension<UserDto>).login("admin", "admin").Token;

            IDao<CustomerDto> dao = DaoFactory.Create<CustomerDto>(_dataContext, Auth.AccessToken);

            var model = new CustomerDto()
            {
                Name = "Test",
                Email = "Test",
                Phone = "12345678",
                BirthDay = DateTime.Parse("2001-09-01"),
            };
            string href = dao.Create(model);

            Assert.IsTrue(string.IsNullOrEmpty(href));
            Assert.AreEqual(href, "api/customer/13");
        }

        [TestMethod()]
        public void DeleteCustomer()
        {

        }

        [TestMethod()]
        public void updateCustomer()
        {
           
        }
    }
}

    
