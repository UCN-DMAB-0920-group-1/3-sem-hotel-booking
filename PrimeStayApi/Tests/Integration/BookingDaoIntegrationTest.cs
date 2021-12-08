using DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using Tests.Integration.Common;

namespace Tests.Integration
{
    [TestClass]
    public class BookingDaoIntegrationTest : BaseDbSetup
    {
        [TestMethod]
        public void InsertBookingsTest()
        {
            //Arrange 
            IDao<BookingEntity> dao = DaoFactory.Create<BookingEntity>(_dataContext);
            var booking = new BookingEntity()
            {
                Start_date = DateTime.Parse("2000-11-11"),
                End_date = DateTime.Parse("2000-12-12"),
                Room_type_id = 1,
                Customer_id = 1,
                Guests = 1,
            };
            //Act 
            var test = dao.Create(booking);
            //Assert
            Assert.AreEqual(18, test);
        }
        [TestMethod]
        public void InsertTooManyBookingsTest()
        {
            //Arrange 
            IDao<BookingEntity> dao = DaoFactory.Create<BookingEntity>(_dataContext);
            var booking = new BookingEntity()
            {
                Start_date = DateTime.Parse("2010-12-04"),
                End_date = DateTime.Parse("2010-12-16"),
                Customer_id = 1,
                Guests = 1,
                Room_id = 8,
            };
            //Act 
            dao.Create(booking);
            var fail = dao.Create(booking);
            //Assert
            Assert.AreEqual(-1, fail);
        }
    }
}