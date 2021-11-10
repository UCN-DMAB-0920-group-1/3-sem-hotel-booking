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
    public class HotelControllerTest
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
        public void GetHotelFromTestDBWithId()
        {
            //arrange
            IDao<HotelEntity> dao = DaoFactory.Create<HotelEntity>(_dataContext);
            HotelController controller = new HotelController(dao);
            int hotelId = 1;

            //act 
            var res = controller.Details(hotelId);

            //assert 
            Assert.AreEqual(res.Result.GetType(), typeof(OkObjectResult));
            var hotel = (res.Result as OkObjectResult).Value as HotelDto;

            Assert.IsNotNull(hotel);
            Assert.AreEqual(hotelId, hotel.ExtractId());
            Assert.IsFalse(string.IsNullOrEmpty(hotel.Name));
        }

        [TestMethod]
        public void GetHotelFromTestDBWithHotel()
        {
            //arrange
            IDao<HotelEntity> dao = DaoFactory.Create<HotelEntity>(_dataContext);
            HotelController controller = new HotelController(dao);

            var hotel = new HotelDto()
            {
                Name = "Hotel Petrús",
                Description = "Classic old fashioned hotel with a river of red wine.",
                Stars = 3,
                StaffedHours = "24/7",
            };

            //act 
            var res = controller.Index(hotel);

            //assert
            Assert.AreEqual(res.Result.GetType(), typeof(OkObjectResult));
            var hotels = (res.Result as OkObjectResult).Value as IEnumerable<HotelDto>;

            Assert.AreEqual(hotels.Count(), 1);
            Assert.AreEqual(hotels.First().Name, hotel.Name);
            Assert.AreEqual(hotels.First().Description, hotel.Description);
            Assert.AreEqual(hotels.First().StaffedHours, hotel.StaffedHours);
            Assert.AreEqual(hotels.First().Stars, hotel.Stars);
        }

        [TestMethod]
        public void GetHotelFakeDaoWithId()
        {
            //Arange
            HotelController controller = new HotelController(new MockHotelDao());
            int hotelId = 1;

            //Act
            var res = controller.Details(hotelId);

            //Assert
            Assert.AreEqual(res.Result.GetType(), typeof(OkObjectResult));
            var hotel = (res.Result as OkObjectResult).Value as HotelDto;

            Assert.IsNotNull(res);
            Assert.AreEqual(hotelId, hotel.ExtractId());
            Assert.IsFalse(string.IsNullOrEmpty(hotel.Name));
        }

        [TestMethod]
        public void GetHotelsFakeDaoWithHotel()
        {
            //Arrange
            HotelController controller = new HotelController(new MockHotelDao());
            var hotel = new HotelEntity()
            {
                Name = "Test",
                Description = "Test",
                Staffed_hours = "Test",
                Stars = 1,
            };

            //Act
            var res = controller.Index(hotel.Map());

            //Assert
            Assert.AreEqual(res.Result.GetType(), typeof(OkObjectResult));
            var hotels = (res.Result as OkObjectResult).Value as IEnumerable<HotelDto>;

            Assert.IsNotNull(hotels);
            Assert.IsTrue(hotels.Any());
            Assert.IsFalse(hotels.Where(h => h is null).Any());
            Assert.AreEqual(2, hotels.Count());
        }
    }

    #region mock implementations
    internal class MockHotelDao : IDao<HotelEntity>
    {
        private int count;
        public int Create(HotelEntity model)
        {
            return model is not null ? ++count : throw new Exception("Error, could not create");
        }

        public int Delete(HotelEntity model)
        {
            return model is not null && model.Id is not null ? model.Id!.Value : throw new Exception("Error, could not delete");
        }

        public int Update(HotelEntity model)
        {
            return model is not null && model.Id is not null ? model.Id!.Value : throw new Exception("Error, could not update");
        }

        public IEnumerable<HotelEntity> ReadAll(HotelEntity model)
        {
            return new List<HotelEntity>() {
                new HotelEntity()
                {
                    Id = 1,
                    Name = "Hotel Petrús",
                    Description = "Classic old fashioned hotel with a river of red wine.",
                    Stars = 3,
                    Staffed_hours = "24/7",
                },
                new HotelEntity()
                {
                    Id = 2,
                    Name = "Hotel Test",
                    Description = "Classic test hotel",
                    Stars = 3,
                    Staffed_hours = "24/7",
                }
            };
        }

        public HotelEntity ReadById(int id)
        {
            return new HotelEntity()
            {
                Id = 1,
                Name = "Hotel Petrús",
                Description = "Classic old fashioned hotel with a river of red wine.",
                Stars = 3,
                Staffed_hours = "24/7",
            };
        }
    }

    #endregion
}
