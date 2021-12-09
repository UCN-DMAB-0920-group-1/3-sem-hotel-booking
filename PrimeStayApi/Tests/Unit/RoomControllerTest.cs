using API.Controllers;
using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;

namespace Tests.Unit
{
    [TestClass]
    public class RoomControllerTest
    {
        [TestMethod]
        public void GetRoomFakeDaoWithId()
        {
            //Arange
            RoomController controller = new RoomController(new MockRoomDao());
            int roomId = 2;

            //Act
            var res = controller.Details(roomId);

            //Assert
            Assert.AreEqual(res.Result.GetType(), typeof(OkObjectResult));
            var room = (res.Result as OkObjectResult).Value as RoomDto;

            Assert.IsNotNull(room);
            Assert.AreEqual(roomId, room.ExtractId());
            Assert.AreEqual("test notes", room.Notes);
            Assert.AreEqual("989", room.Room_number);
        }


        [TestMethod]
        public void UpdateRoomFakeDao()
        {
            RoomController controller = new RoomController(new MockRoomDao());
            var room = new RoomDto()
            {
                Room_number = "989",
                Active = true,
                Notes = "test notes",
                RoomTypeHref = "api/roomType/3",
                Href = "api/room/1"
            };

            //Act
            var res = controller.Edit(room);

            //Assert
            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(NoContentResult));
        }

        [TestMethod]
        public void CreateRoomFakeDao()
        {
            RoomController controller = new RoomController(new MockRoomDao());
            var room = new RoomDto()
            {
                Room_number = "989",
                Active = true,
                Notes = "test notes",
                RoomTypeHref = "api/roomType/3",
                Href = "api/room/1"
            };

            //Act
            var res = controller.Create(room);

            //Assert
            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res.Result, typeof(CreatedResult));

        }
        [TestMethod]
        public void DeleteRoomFakeDao()
        {
            RoomController controller = new RoomController(new MockRoomDao());
            var room = new RoomDto()
            {
                Room_number = "989",
                Active = true,
                Notes = "test notes",
                RoomTypeHref = "api/roomType/3",
                Href = "api/room/1"
            };

            //Act
            var res = controller.Delete(room);

            //Assert
            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(NoContentResult));
        }


    }

    #region mock implementations
    internal class MockRoomDao : IDao<RoomEntity>
    {
        private int count;
        public int Create(RoomEntity model)
        {
            return model is not null ? ++count : throw new Exception("Error, could not create");
        }

        public int Delete(RoomEntity model)
        {
            return model is not null && model.Id is not null ? model.Id!.Value : throw new Exception("Error, could not delete");
        }

        public int Update(RoomEntity model)
        {
            return model is not null && model.Id is not null ? model.Id!.Value : throw new Exception("Error, could not update");
        }

        public IEnumerable<RoomEntity> ReadAll(RoomEntity model)
        {
            return new List<RoomEntity>() {
                new RoomEntity()
                {
                    Room_number = "989",
                    Active = true,
                    Notes = "test notes",
                    Id = 1,
                    Room_type_id = 1,
                },
                new RoomEntity()
                {
                    Room_number = "989",
                    Active = true,
                    Notes = "test notes",
                    Id = 2,
                    Room_type_id = 3,
                }
            };
        }

        public RoomEntity ReadById(int id)
        {
            return new RoomEntity()
            {
                Room_number = "989",
                Active = true,
                Notes = "test notes",
                Id = 2,
                Room_type_id = 3,
            };
        }
    }

    #endregion

}
