using API;
using API.Controllers;
using DataAccessLayer;
using DataAccessLayer.DTO;
using Enviroment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Version = Database.Version;

namespace Tests
{
    [TestClass]
    public class BookingControllerTest
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
        public void TestCreateBookingDao()
        {
            //Arrange
            IDao<BookingEntity> dao = DaoFactory.Create<BookingEntity>(_dataContext);
            var booking = new BookingEntity()
            {
                Customer_id = 1,
                End_date = DateTime.Parse("2010-11-01"),
                Start_date = DateTime.Parse("2010-11-02"),
                Guests = 10,
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
            IDao<CustomerEntity> customerDao = DaoFactory.Create<CustomerEntity>(_dataContext);

            BookingController controller = new BookingController(dao, customerDao);


            BookingDto booking = new BookingDto()
            {
                CustomerHref = "api/customer/1",
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
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
            IDao<CustomerEntity> customerDao = DaoFactory.Create<CustomerEntity>(_dataContext);
            BookingController controller = new BookingController(dao, customerDao);

            BookingDto booking = new BookingDto()
            {
                CustomerHref = null,
                EndDate = null,
                StartDate = null,
                Guests = null,
                RoomHref = null,
            };

            //Act
            var res = controller.Index(booking);
            var bookings = (res.Result as OkObjectResult).Value as IEnumerable<BookingDto>;


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
            IDao<CustomerEntity> customerDao = DaoFactory.Create<CustomerEntity>(_dataContext);
            IDao<BookingEntity> dao = DaoFactory.Create<BookingEntity>(_dataContext);
            BookingController controller = new BookingController(dao, customerDao);
            int id = 1;

            var booking = controller.Details(id);

            Assert.IsNotNull(booking);
            Assert.IsTrue(booking.Guests == 4);
        }
    }
}
