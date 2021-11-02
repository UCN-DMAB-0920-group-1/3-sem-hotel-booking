using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PrimeStayApi.Controllers;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Database;
using PrimeStayApi.Enviroment;
using PrimeStayApi.Model;
using PrimeStayApi.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeStayApi.Test
{
    [TestClass]
    public class HotelControllerTest
    {
        private HotelController _controllerWithDB;
        private IDao<HotelEntity> _dao;
        private IDataContext _dataContext;
        private HotelController _controllerNoDB;


        [TestInitialize]
        public void SetUp()
        {
            Database.Version.Upgrade(ENV.ConnectionStringTest);
            _dataContext = new DataContext(ENV.ConnectionStringTest);

        }
        [TestCleanup]
        public void CleanUp()
        {
            Database.Version.Drop(ENV.ConnectionStringTest);
        }


        [TestMethod]
        public void GetHotelFromTestDBWithId()
        {
            //arrange
            _dao = DaoFactory.Create<HotelEntity>(_dataContext);
            _controllerWithDB = new HotelController(_dao);
            int hotelId = 1;
            //act 
            var hotel = _controllerWithDB.Details(hotelId);
            //assert 
            Assert.IsNotNull(hotel);
            Assert.IsInstanceOfType(hotel, typeof(HotelDto));
            Assert.IsTrue(hotel.Name.Equals("Hotel Petrús"));
        }

        [TestMethod]
        public void GetHotelFromTestDBWithHotel()
        {
            //arrange
            _dao = DaoFactory.Create<HotelEntity>(_dataContext);
            _controllerWithDB = new HotelController(_dao);

            var hotel = new HotelEntity()
            {
                Name = "Hotel Petrús",
                Description = "Classic old fashioned hotel with a river of red wine.",
                Stars = 3,
                Staffed_hours = "24/7",
            };
            //act 
            var hotels = _controllerWithDB.Index(hotel.Map());
            //assert 
            Assert.IsTrue(hotels.Count() == 1);
            Assert.IsTrue(hotels.First().Name == hotel.Name);
            Assert.IsTrue(hotels.First().Description == hotel.Description);
            Assert.IsTrue(hotels.First().StaffedHours == hotel.Staffed_hours);
            Assert.IsTrue(hotels.First().Stars == hotel.Stars);
        }

        [TestMethod]
        public void GetHotelFakeDaoWithId()
        {
            //Arange
            int id = 1;
            var mockHotelDao = Mock.Of<IDao<HotelEntity>>(m => m.ReadById(id) == new HotelEntity());
            //_controllerNoDB = new HotelController(new FakeHotelDao());
            _controllerNoDB = new HotelController(mockHotelDao);

            //Act
            var hotel = _controllerNoDB.Details(id);

            //Assert
            Assert.IsNotNull(hotel);
            Assert.IsInstanceOfType(hotel, typeof(HotelDto));
            Assert.IsTrue(string.IsNullOrEmpty(hotel.Name));
        }

        [TestMethod]
        public void GetHotelsFakeDaoWithHotel()
        {
            //Arrange
            var hotel = new HotelEntity()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Staffed_hours = "Test",
                Stars = 1,
            };
            IEnumerable<HotelEntity> hotelEntities = new List<HotelEntity>() { hotel };
            var mockHotelDao = new Mock<IDao<HotelEntity>>();
            mockHotelDao.Setup(m => m.ReadAll(hotel)).Returns(hotelEntities);
            _controllerNoDB = new HotelController(mockHotelDao.Object);

            //Act
            var hotels = _controllerNoDB.Index(hotel.Map());

            var test2 = mockHotelDao.Object.ReadAll(hotel.Map().Map());
            var test = mockHotelDao.Object.ReadAll(hotel);

            //Assert
            Assert.IsTrue(test.Any());


            Assert.IsTrue(mockHotelDao.Object.ReadAll(new HotelEntity()).Any());

            Assert.IsNotNull(hotels);
            Assert.IsNotNull(hotels.First());
            Assert.IsInstanceOfType(hotels.First(), typeof(HotelDto));
            Assert.AreEqual(hotels.Count(), 0);
        }
    }
}
