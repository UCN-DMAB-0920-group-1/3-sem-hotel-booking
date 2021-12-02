using API.Controllers;
using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System.Collections.Generic;
using System.Linq;
using Tests.Integration.Common;

namespace Tests.Integration
{
    [TestClass]
    public class RoomControllerIntegrationTest : BaseDbSetup
    {
        [TestMethod]
        public void RoomDaoGetAllTest()
        {
            //Arrange
            var dao = DaoFactory.Create<RoomEntity>(_dataContext);
            var testRoom = new RoomEntity();
            //Act
            var list = dao.ReadAll(testRoom);
            //Assert
            Assert.IsTrue(list.Any());
        }

        [TestMethod]
        public void GetRoomFromTestDBWithId()
        {
            //arrange
            IDao<RoomEntity> dao = DaoFactory.Create<RoomEntity>(_dataContext);
            RoomController controller = new RoomController(dao);
            int roomId = 1;

            //act 
            var res = controller.Details(roomId);
            var room = (res.Result as OkObjectResult).Value as RoomDto;

            //assert 
            Assert.AreEqual(res.Result.GetType(), typeof(OkObjectResult));

            Assert.IsNotNull(room);
            Assert.AreEqual(roomId, room.ExtractId());
            Assert.AreEqual("Lugter af nutella", room.Notes);
            Assert.AreEqual("1", room.Room_number);
        }

        [TestMethod]
        public void GetRoomFromTestDBWithRoom()
        {
            //arrange
            IDao<RoomEntity> dao = DaoFactory.Create<RoomEntity>(_dataContext);
            RoomController controller = new RoomController(dao);

            var room = new RoomDto()
            {
                RoomTypeHref = "api/roomtype/1"
            };

            //act 
            var res = controller.Index(room);

            //assert
            Assert.AreEqual(typeof(OkObjectResult), res.Result.GetType());
            var rooms = (res.Result as OkObjectResult).Value as IEnumerable<RoomDto>;

            Assert.AreEqual(rooms.Count(), 4);
            Assert.AreEqual("Lugter af nutella", rooms.First().Notes);
            Assert.AreEqual("1", rooms.First().Room_number);
            Assert.AreEqual(true, rooms.First().Active);
        }
    }
}
