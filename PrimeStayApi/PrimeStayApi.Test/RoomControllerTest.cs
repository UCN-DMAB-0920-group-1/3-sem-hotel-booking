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
    public class RoomControllerTest
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

            //assert 
            Assert.AreEqual(res.Result.GetType(), typeof(OkResult));
            var room = (res.Result as OkObjectResult).Value as RoomDto;

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

        [TestMethod]
        public void GetRoomFakeDaoWithId()
        {
            //Arange
            RoomController controller = new RoomController(new MockRoomDao());
            int roomId = 2;

            //Act
            var res = controller.Details(roomId);

            //Assert
            Assert.AreEqual(res.Result.GetType(), typeof(OkResult));
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
            Assert.IsInstanceOfType(res, typeof(OkResult));

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
            Assert.IsInstanceOfType(res, typeof(CreatedResult));

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
            Assert.IsInstanceOfType(res, typeof(OkResult));
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
