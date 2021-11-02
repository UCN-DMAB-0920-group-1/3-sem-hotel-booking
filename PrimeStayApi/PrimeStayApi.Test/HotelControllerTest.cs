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

using Version = PrimeStayApi.Database.Version;

namespace PrimeStayApi.Test
{
    [TestClass]
    public class HotelControllerTest
    {
        private DataContext _dataContext;


        [TestInitialize]
        public void SetUp()
        {
            Version.Upgrade(ENV.ConnectionStringTest);
            _dataContext = new DataContext(ENV.ConnectionStringTest);

        }
        [TestCleanup]
        public void CleanUp()
        {
            Version.Drop(ENV.ConnectionStringTest);
        }


        [TestMethod]
        public void GetHotelFromTestDBWithId()
        {
            //arrange
            IDao<HotelEntity> dao = DaoFactory.Create<HotelEntity>(_dataContext);
            HotelController controller = new HotelController(dao);
            int hotelId = 1;

            //act 
            var hotel = controller.Details(hotelId);

            //assert 
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
            var hotel = new HotelEntity()
            {
                Name = "Hotel Petrús",
                Description = "Classic old fashioned hotel with a river of red wine.",
                Stars = 3,
                Staffed_hours = "24/7",
            };

            //act 
            var res = controller.Index(hotel.Map());

            //assert 
            Assert.IsNotNull(res);
            Assert.IsFalse(res.Where(h => h is null).Any());
            Assert.IsTrue(res.First().Name == hotel.Name);
            Assert.IsTrue(res.First().Description == hotel.Description);
            Assert.IsTrue(res.First().StaffedHours == hotel.Staffed_hours);
            Assert.IsTrue(res.First().Stars == hotel.Stars);
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
            Assert.IsNotNull(res);
            Assert.AreEqual(hotelId, res.ExtractId());
            Assert.IsFalse(string.IsNullOrEmpty(res.Name));
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
            Assert.IsNotNull(res);
            Assert.IsTrue(res.Any());
            Assert.IsFalse(res.Where(h => h is null).Any());
            Assert.AreEqual(2, res.Count());
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
