using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using PrimeStayApi.Model.DTO;
using System.Linq;
using PrimeStayApi.Controllers;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Enviroment;
using PrimeStayApi.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Version = PrimeStayApi.Database.Version;



namespace PrimeStayApi.Test
{
    [TestClass]
    public class RoomTypeTest
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
            Assert.AreEqual("Junior Suite",roomType.Type);
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
