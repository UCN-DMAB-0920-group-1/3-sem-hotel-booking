using API.Controllers;
using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Extensions;
using Tests.Integration.Common;

namespace Tests.Integration
{
    [TestClass]
    public class BookingControllerIntegrationTest : BaseDbSetup
    {
        [TestMethod]
        public void TestCreateBookingDao()
        {
            //Arrange
            IDao<BookingEntity> dao = DaoFactory.Create<BookingEntity>(_dataContext);
            var booking = new BookingEntity()
            {
                Customer_id = 1,
                Room_type_id = 1,
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

            string customerHref = "api/customer/1";
            BookingDto booking = new BookingDto()
            {
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                Guests = 1,
                RoomHref = "api/room/1",
                RoomTypeHref = "api/roomType/1",
                CustomerHref = customerHref,
                Customer = new CustomerDto() { Email = "MiaAfilahk@watersports.com", Href = customerHref },

            };

            //Act
            var actionResult = controller.Create(booking);

            //Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedResult));
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
            Assert.IsTrue(booking.GetValue().Guests == 4);
        }
    }
}
