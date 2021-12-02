using API.Controllers;
using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using Tests.Integration.Common;

namespace Tests.Integration
{
    [TestClass]
    public class CustomerTest : BaseDbSetup
    {
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
        [TestMethod]
        public void DeleteCustomer()
        {
            //Arrange
            var customer = new CustomerEntity()
            {
                Id = 2,
                Name = "Delete test",
                Email = "Delete test",
                Phone = "Delete test",
                Birthday = DateTime.Parse("1990-01-01"),
            };
            CustomerController customerCtrl = new CustomerController(DaoFactory.Create<CustomerEntity>(_dataContext));
            //Act
            var res = customerCtrl.Delete(customer.Map());
            //Assert
            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(OkObjectResult));
        }
        [TestMethod]
        public void UpdateCustomer()
        {
            //Arrange
            var customer = new CustomerEntity()
            {
                Id = 1,
                Name = "Update test",
                Email = "Update test",
                Phone = "Update test",
                Birthday = DateTime.Parse("1990-01-01"),
            };
            CustomerController customerCtrl = new CustomerController(DaoFactory.Create<CustomerEntity>(_dataContext));
            //Act
            var res = customerCtrl.Edit(customer.Map());
            //Assert
            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(OkObjectResult));
        }
    }
}
