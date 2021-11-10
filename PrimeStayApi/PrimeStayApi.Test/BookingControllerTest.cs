using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeStayApi.Controllers;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Enviroment;
using PrimeStayApi.Model;
using PrimeStayApi.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Version = PrimeStayApi.Database.Version;

namespace PrimeStayApi.Test
{
    [TestClass]
    public class BookingControllerTest
    {
        private string connectionString = new ENV().ConnectionStringTest;
        private static DataContext _dataContext;
        private static List<Action> _dropDatabaseActions = new();

        [TestInitialize]
        public void SetUp()
        {
            _dataContext = new DataContext(connectionString);
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
        public void TestCreateBookingDao()
        {
            //Arrange
            IDao<BookingEntity> dao = DaoFactory.Create<BookingEntity>(_dataContext);
            var booking = new BookingEntity()
            {
                Customer_id = 1,
                End_date = System.DateTime.Parse("2010-11-01"),
                Start_date = System.DateTime.Parse("2010-11-02"),
                Guests = 10,
                Room_type_id = 1,
            };

            //Act
            int id = dao.Create(booking);

            //Assert
            Assert.IsNotNull(id);
            Assert.AreEqual(18, id);
        }


        [TestMethod]
        public void TestCreateBookingController()
        {
            //Arrange
            IDao<BookingEntity> dao = DaoFactory.Create<BookingEntity>(_dataContext);
            BookingController controller = new BookingController(dao);


            BookingDto booking = new BookingDto()
            {
                CustomerHref = "api/customer/1",
                EndDate = System.DateTime.Now,
                StartDate = System.DateTime.Now,
                Guests = 1,
                RoomHref = "api/room/1"
            };

            //Act
            var actionResult = controller.Create(booking);

            //Assert
            Assert.IsInstanceOfType(actionResult, typeof(CreatedResult));
        }

        [TestMethod]
        public void TestFetchBookings()
        {
            //Arrange
            IDao<BookingEntity> dao = DaoFactory.Create<BookingEntity>(_dataContext);
            BookingController controller = new BookingController(dao);

            BookingDto booking = new BookingDto()
            {
                CustomerHref = null,
                EndDate = null,
                StartDate = null,
                Guests = null,
                RoomHref = null,
            };

            //Act
            var bookings = controller.Index(booking);

            //Assert
            Assert.AreEqual(17, bookings.Count());
            Assert.IsNotNull(bookings.First());
            Assert.IsNotNull(bookings.First().StartDate);
            Assert.AreEqual(DateTime.Parse("2010-11-04T00:00:00"), bookings.First().StartDate);
            Assert.AreEqual(DateTime.Parse("2010-11-16T00:00:00"), bookings.First().EndDate);
            Assert.AreEqual(4, bookings.First().Guests);
            Assert.AreEqual("api/Room/1", bookings.First().RoomHref);
            Assert.AreEqual("api/Customer/1", bookings.First().CustomerHref);
        }

        [TestMethod]
        public void TestReadById()
        {
            IDao<BookingEntity> dao = DaoFactory.Create<BookingEntity>(_dataContext);
            BookingController controller = new BookingController(dao);
            int id = 1;

            var booking = controller.Details(id);

            Assert.IsNotNull(booking);
            Assert.IsTrue(booking.Guests == 4);
        }
    }
}
