using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeStayApi.Controllers;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.Model;
using System.Linq;
using System.Collections.Generic;

namespace PrimeStayApi.Test
{
    [TestClass]
    public class RoomControllerTest
    {

        private static RoomController _controller;

        [ClassInitialize]
        public static void setUp(TestContext context)
        {
            IDataContext dataContext = new DataContext();
            _controller = new RoomController(DaoFactory.Create<Room>(dataContext));
        }
         
        [TestMethod]
        public  void TestIndex()
        {
            Room[] rooms = _controller.Index(null, null, null, null, null, null, null).ToArray();
            Assert.IsNotNull(rooms);
            Assert.IsTrue(rooms.Length > 0);
        }

        [TestMethod]
        public void TestGetRoomsByHotelId()
        {
            Room[] rooms = _controller.Index(null, null, null, null, null, null, 1).ToArray();
            Assert.IsNotNull(rooms);
            Assert.IsTrue(rooms.Length > 0);
        }

        [TestMethod]
        public void TestGetRoomById()
        {
            Room room = _controller.Details(1);
            Assert.IsNotNull(room);
        }

        [TestMethod]
        public void TestCreateRoom()
        {
            //Arrange


            var form = new FormCollection {
    {"WeekList", weekfilter},
    {"PracticeList", practicefitler}
}


            //Act
            Room room = _controller.Create(collection);

            //Assert

            Assert.IsNotNull(room);
        }
    }
}
