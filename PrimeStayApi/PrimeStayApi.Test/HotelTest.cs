using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeStayApi.Controllers;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Database;
using PrimeStayApi.Enviroment;
using PrimeStayApi.Model;
using PrimeStayApi.Model.DTO;

using System.Collections.Generic;
using System.Linq;

namespace PrimeStayApi.Test
{
    [TestClass]
    public class HotelControllerTest
    {
        private HotelController _controllerWithDB;
        private IDao<HotelEntity> _dao;
        private DataContext _dataContext;
        private HotelController _controllerNoDB;


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
            _dao = DaoFactory.Create<HotelEntity>(_dataContext);
            _controllerWithDB = new HotelController(_dao);
            int hotelId = 1;

            //act 
            var res = _controllerWithDB.Details(hotelId);

            //assert
            Assert.AreEqual(res.Result.GetType(), typeof(OkObjectResult));

            var hotel = (res.Result as OkObjectResult).Value as HotelDto;
            Assert.IsNotNull(hotel);
            Assert.AreEqual(hotel.Name, "Hotel Petrús");

        }

        [TestMethod]
        public void GetHotelFromTestDBWithHotel()
        {
            //arrange
            _dao = DaoFactory.Create<HotelEntity>(_dataContext);
            _controllerWithDB = new HotelController(_dao);


            var hotel = new HotelDto()
            {
                Name = "Hotel Petrús",
                Description = "Classic old fashioned hotel with a river of red wine.",
                Stars = 3,
                StaffedHours = "24/7",
            };


            //act 
            var res = _controllerWithDB.Index(hotel);

            //assert
            Assert.AreEqual(res.Result.GetType(), typeof(OkObjectResult));

            var hotels = (res.Result as OkObjectResult).Value as IEnumerable<HotelDto>;
            Assert.AreEqual(hotels.Count(), 1);
            Assert.AreEqual(hotels.First().Name, hotel.Name);
            Assert.AreEqual(hotels.First().Description, hotel.Description);
            Assert.AreEqual(hotels.First().StaffedHours, hotel.StaffedHours);
            Assert.AreEqual(hotels.First().Stars, hotel.Stars);
        }

        internal class FakeHotelDao : IDao<HotelEntity>
        {
            public int Create(HotelEntity model)
            {
                return -1;
            }

            public int Delete(HotelEntity model)
            {
                return -1;
            }

            public IEnumerable<HotelEntity> ReadAll(HotelEntity model)
            {
                return new List<HotelEntity>();
            }

            public HotelEntity ReadById(int id)
            {
                return new HotelEntity();
            }

            public int Update(HotelEntity model)
            {
                return -1;
            }
        }
        [TestMethod]
        public void GetHotelFakeDaoWithId()
        {
            //Arange
            _controllerNoDB = new HotelController(new FakeHotelDao());
            int id = 1;

            //Act
            var res = _controllerNoDB.Details(id);

            //Assert
            Assert.IsNull(res.Value);
        }

        [TestMethod]
        public void GetHotelsFakeDaoWithHotel()
        {
            //Arrange
            _controllerNoDB = new HotelController(new FakeHotelDao());
            var hotel = new HotelDto()
            {
                Name = "Test",
                Description = "Test",
                StaffedHours = "Test",
                Stars = 1,
            };

            //Act
            var res = _controllerNoDB.Index(hotel);
            //Assert
            Assert.IsNull(res.Value);
        }
    }
}
