using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeStayApi.Controllers;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Database;
using PrimeStayApi.Enviroment;
using PrimeStayApi.Model;
using PrimeStayApi.Model.DTO;
using System.Collections.Generic;
using System.Linq;

namespace PrimeStayApi.Test
{
    [TestClass]
    public class BookingControllerTest
    {
        private BookingController _controllerWithDB;
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
        public void TestCreateBookingDao()
        {
            //Arrange
            _dao = DaoFactory.Create<BookingEntity>(_dataContext);
            var booking = new BookingEntity()
            {
                Customer_id = 1,
                End_date = System.DateTime.Parse("2010-11-01"),
                Start_date = System.DateTime.Parse("2010-11-02"),
                Guests = 10,
                //TODO set room_type_id
            };


            //Act
            int id = _dao.Create(booking);

            //Assert

            

            Assert.IsNotNull(id);
            Assert.AreEqual(id, 21);
        }


        [TestMethod]
        public void TestCreateBookingController()
        {
            //Arrange
            _dao = DaoFactory.Create<BookingEntity>(_dataContext);
            _controllerWithDB = new BookingController(_dao);


            BookingDto booking = new BookingDto()
            {
                CustomerHref = "api/customer/1",
                EndDate = System.DateTime.Now,
                StartDate = System.DateTime.Now,
                Guests = 1,
                RoomHref = "api/room/1"
            };

            //Act
            var actionResult = _controllerWithDB.Create(booking);

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(CreatedResult));
        }

        [TestMethod]
        public void TestFetchBookings()
        {
            //Arrange
            _dao = DaoFactory.Create<BookingEntity>(_dataContext);
            _controllerWithDB = new BookingController(_dao);

            BookingDto booking = new BookingDto()
            {
                CustomerHref = null,
                EndDate = null,
                StartDate = null,
                Guests = null,
                RoomHref = null,
            };

            //Act
            var bookings = _controllerWithDB.Index(booking);

            //Assert
            Assert.AreEqual(bookings.Count(), 20);
            Assert.IsNotNull(bookings.First());
            Assert.IsNotNull(bookings.First().StartDate);
            Assert.AreEqual(bookings.First().StartDate,System.DateTime.Parse("2010-11-04T00:00:00"));
            Assert.AreEqual(bookings.First().EndDate,System.DateTime.Parse("2010-11-16T00:00:00"));
            Assert.AreEqual(bookings.First().Guests,4);
            Assert.AreEqual(bookings.First().RoomHref,"api/Room/1");
            Assert.AreEqual(bookings.First().CustomerHref,"api/Customer/1");
        }

        [TestMethod]
        public void TestReadById()
        {
            _dao = DaoFactory.Create<BookingEntity>(_dataContext);
            _controllerWithDB = new BookingController(_dao);
            int id = 1;

            var booking = _controllerWithDB.Details(id);

            Assert.IsNotNull(booking);
            Assert.IsTrue(booking.Guests == 4);
        }
    }
}
