using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeStayApi.Controllers;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Enviroment;
using PrimeStayApi.Model;
using PrimeStayApi.Model.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Version = PrimeStayApi.Database.Version;

namespace PrimeStayApi.Test
{
    [TestClass]
    public class CustomerTest
    {
        private string connectionString = new ENV().ConnectionStringTest;
        private static SqlDataContext _dataContext;
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

        [TestMethod]
        public void GetCustomer()
        {
            //Arrange
            int id = 1;
            CustomerController customerCtrl = new CustomerController(DaoFactory.Create<CustomerEntity>(_dataContext));
            CustomerEntity customer;
            //Act
            customer = customerCtrl.Details(id).Map();
            //Assert
            Assert.IsNotNull(customer);
            Assert.AreEqual(id, customer.Id);
        }
        [TestMethod]
        public void CreateCustomer()
        {
            //Arrange
            var customer = new CustomerEntity()
            {
                Name = "Test",
                Email = "Test",
                Phone = "12345678",
                Birthday = DateTime.Parse("2001-09-01"),
            };
            CustomerController customerCtrl = new CustomerController(DaoFactory.Create<CustomerEntity>(_dataContext));
            //Act
            var res = customerCtrl.Create(customer.Map());
            //Assert
            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(CreatedResult));

        }
    }
}
