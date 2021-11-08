using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Enviroment;
using PrimeStayApi.Model;
using System;
using Version = PrimeStayApi.Database.Version;

namespace PrimeStayApi.Test
{
    [TestClass]
    public class BookingDaoTest
    {
        private IDao<BookingEntity> _dao;
        private DataContext _dataContext;


        [TestInitialize]
        public void SetUp()
        {
            Version.Upgrade(ENV.ConnectionStringTest);
            _dataContext = new DataContext(ENV.ConnectionStringTest);

        }
        [TestCleanup]
        public void CleanUp()
        {
            Version.Drop(ENV.ConnectionStringTest);
        }

        [TestMethod]
        public void InsertBookingsTest()
        {
            //Arrange 
            _dao = DaoFactory.Create<BookingEntity>(_dataContext);
            var booking = new BookingEntity()
            {
                Start_date = DateTime.Parse("2020-11-11"),
                End_date = DateTime.Parse("2020-12-12"),
                Customer_id = 1,
                Num_of_guests = 1,
                Room_id = 8,
            };
            //Act 
            var test = _dao.Create(booking);
            //Assert
            Assert.IsTrue(test > 0);
        }
        [TestMethod]
        public void InsertTooManyBookingsTest()
        {
            //Arrange 
            _dao = DaoFactory.Create<BookingEntity>(_dataContext);
            var booking = new BookingEntity()
            {
                Start_date = DateTime.Parse("2020-11-11"),
                End_date = DateTime.Parse("2020-12-12"),
                Customer_id = 1,
                Num_of_guests = 1,
                Room_id = 8,
            };
            //Act 
            _dao.Create(booking);
            var fail = _dao.Create(booking);
            //Assert
            Assert.AreEqual(-1, fail);
        }
    }
}