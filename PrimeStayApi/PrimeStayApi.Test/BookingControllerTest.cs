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
                End_date = System.DateTime.Parse("2021-10-10"),
                Start_date = System.DateTime.Parse("2021-01-01"),
                Num_of_guests = 10,
                Room_id = 1,
            };


            //Act
            int id = _dao.Create(booking);

            //Assert
            Assert.IsNotNull(id);
            Assert.IsTrue(id == 2);
        }


        [TestMethod]
        public void TestCreateBookingController()
        {
            //Arrange
            _dao = DaoFactory.Create<BookingEntity>(_dataContext);
            _controllerWithDB = new BookingController(_dao);

            var collection = new FormCollection(new Dictionary<string, StringValues>
            {
                { "numOfGuests", "321"},
                { "roomHref", "api/room/1" }, //Should probably be roomId instead
                { "customerHref", "api/customer/1" },
                { "startDate", "2021-01-01" },
                { "endDate", "2021-10-10" },
            });

            BookingDto booking = new BookingDto()
            {
                CustomerHref = "api/customer/1",
                EndDate = System.DateTime.Now,
                StartDate = System.DateTime.Now,
                NumOfGuests = 1,
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

            //Act
            var bookings = _controllerWithDB.Index(null, null, null, null, null, null);

            //Assert
            Assert.IsTrue(bookings.Count() == 1);
            Assert.IsNotNull(bookings.First());
            Assert.IsNotNull(bookings.First().StartDate);
            Assert.IsTrue(bookings.First().StartDate == System.DateTime.Parse("2010-11-04T00:00:00"));
            Assert.IsTrue(bookings.First().EndDate == System.DateTime.Parse("2010-11-16T00:00:00"));
            Assert.IsTrue(bookings.First().NumOfGuests == 4);
            Assert.IsTrue(bookings.First().RoomHref == "api/Room/1");
            Assert.IsTrue(bookings.First().CustomerHref == "api/Customer/1");
        }

        [TestMethod]
        public void TestReadById()
        {
            _dao = DaoFactory.Create<BookingEntity>(_dataContext);
            _controllerWithDB = new BookingController(_dao);
            int id = 1;

            var booking = _controllerWithDB.Details(id);

            Assert.IsNotNull(booking);
            Assert.IsTrue(booking.NumOfGuests == 4);
        }
    }
}
