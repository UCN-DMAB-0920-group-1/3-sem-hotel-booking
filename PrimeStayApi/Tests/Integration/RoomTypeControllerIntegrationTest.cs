using API.Controllers;
using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Integration.Common;

namespace Tests.Integration
{
    [TestClass]
    public class RoomTypeControllerIntegrationTest : BaseDbSetup
    {
        [TestMethod]
        public void GetAvaliableforRoom()
        {
            //arrange
            var dao = DaoFactory.Create<RoomTypeEntity>(_dataContext) as IDaoDateExtension<RoomTypeEntity>;
            int roomTypeId = 1;
            DateTime startDate = DateTime.Parse("2010-11-10");
            DateTime endDate = DateTime.Parse("2010-11-15");
            //act
            var testRes = dao.CheckAvailability(roomTypeId, startDate, endDate);
            //assert
            Assert.IsNotNull(testRes.Avaliable);
            Assert.AreEqual(1, testRes.Avaliable);
        }


        [TestMethod]
        public void GetRoomTypeFromTestDBWithId()
        {
            //arrange
            IDao<RoomTypeEntity> dao = DaoFactory.Create<RoomTypeEntity>(_dataContext);
            RoomTypeController controller = new RoomTypeController(dao);
            int roomTypeId = 1;

            //act 
            var res = controller.Details(roomTypeId);

            //assert 
            Assert.AreEqual(res.Result.GetType(), typeof(OkObjectResult));
            var roomType = (res.Result as OkObjectResult).Value as RoomTypeDto;

            Assert.IsNotNull(roomType);
            Assert.AreEqual(roomTypeId, roomType.ExtractId());
            Assert.AreEqual("Junior Suite", roomType.Type);
            Assert.AreEqual("Junior suite smaller room but space for 4", roomType.Description);
            Assert.AreEqual(4, roomType.Beds);
            Assert.AreEqual(2, roomType.Rating);
            Assert.AreEqual(true, roomType.Active);
        }

        [TestMethod]
        public void getRoomTypeFromTestDB()
        {
            //arrange
            IDao<RoomTypeEntity> dao = DaoFactory.Create<RoomTypeEntity>(_dataContext);
            RoomTypeController controller = new RoomTypeController(dao);

            var roomType = new RoomTypeDto()
            {
                Beds = 2,
            };

            //act 
            var res = controller.Index(roomType);

            //assert
            Assert.AreEqual(res.Result.GetType(), typeof(OkObjectResult));
            var roomTypes = (res.Result as OkObjectResult).Value as IEnumerable<RoomTypeDto>;

            Assert.AreEqual(roomTypes.Count(), 6);
            Assert.AreEqual("Economy Suite", roomTypes.First().Type);
            Assert.AreEqual("Economy suite with 2 bunk beds", roomTypes.First().Description);
            Assert.AreEqual(3, roomTypes.First().Rating);
            Assert.AreEqual(true, roomTypes.First().Active);
        }
    }
}
