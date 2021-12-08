using API.Controllers;
using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Unit
{
    [TestClass]
    public class RoomTypeControllerUnitTest
    {
        [TestMethod]
        public void GetroomTypeFakeDaoWithId()
        {
            //Arange
            RoomTypeController controller = new RoomTypeController(new MockRoomTypeDao());
            int roomTypeId = 1;

            //Act
            var res = controller.Details(roomTypeId);

            //Assert
            Assert.AreEqual(res.Result.GetType(), typeof(OkObjectResult));
            var roomType = (res.Result as OkObjectResult).Value as RoomTypeDto;

            Assert.IsNotNull(res);
            Assert.AreEqual(roomTypeId, roomType.ExtractId());
            Assert.IsFalse(string.IsNullOrEmpty(roomType.Type));
        }

        [TestMethod]
        public void GetRoomTypesFakeDao()
        {
            //Arrange
            RoomTypeController controller = new RoomTypeController(new MockRoomTypeDao());
            var roomType = new RoomTypeEntity()
            {
                Id = 1,
                Type = "Junior suite",
                beds = 4,
                Description = "Junior suite smaller room but space for 4",
                Rating = 2,
                Hotel_Id = 1,
                Active = true,
            };

            //Act
            var res = controller.Index(roomType.Map());

            //Assert
            Assert.AreEqual(res.Result.GetType(), typeof(OkObjectResult));
            var roomTypes = (res.Result as OkObjectResult).Value as IEnumerable<RoomTypeDto>;

            Assert.IsNotNull(roomTypes);
            Assert.IsTrue(roomTypes.Any());
            Assert.IsFalse(roomTypes.Where(h => h is null).Any());
            Assert.AreEqual(2, roomTypes.Count());
        }
    }

    #region mock implementations
    internal class MockRoomTypeDao : IDao<RoomTypeEntity>
    {
        private int count;
        public int Create(RoomTypeEntity model)
        {
            return model is not null ? ++count : throw new Exception("Error, could not create");
        }

        public int Delete(RoomTypeEntity model)
        {
            return model is not null && model.Id is not null ? model.Id!.Value : throw new Exception("Error, could not delete");
        }

        public int Update(RoomTypeEntity model)
        {
            return model is not null && model.Id is not null ? model.Id!.Value : throw new Exception("Error, could not update");
        }

        public IEnumerable<RoomTypeEntity> ReadAll(RoomTypeEntity model)
        {
            return new List<RoomTypeEntity>() {
                new RoomTypeEntity()
                {
                    Id = 1,
                    Type = "Junior suite",
                    beds = 4,
                    Description = "Junior suite smaller room but space for 4",
                    Rating = 2,
                    Hotel_Id = 1,
                    Active = true,
                },
                new RoomTypeEntity()
                {
                    Id = 2,
                    Type = "Economy Suite",
                    beds = 2,
                    Description = "Economy suite with 2 bunk beds",
                    Rating = 3,
                    Hotel_Id = 1,
                    Active = true,
                }
            };
        }

        public RoomTypeEntity ReadById(int id)
        {
            return new RoomTypeEntity()
            {
                Id = 1,
                Type = "Junior suite",
                beds = 4,
                Description = "Junior suite smaller room but space for 4",
                Rating = 2,
                Hotel_Id = 1,
                Active = true,
            };
        }
    }

    #endregion
}
