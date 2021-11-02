using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeStay.MVC.DataAccessLayer;
using PrimeStay.MVC.DataAccessLayer.DTO;
using PrimeStay.MVC.Model;
using System.Collections.Generic;

namespace PrimeStay.MVC.Controllers.Tests
{
    [TestClass()]
    public class HotelControllerUnitTest
    {
        private IDao<HotelDto> _hotelDao;
        private IDao<LocationDto> _locationDao;
        private IDao<RoomDto> _roomDao;
        private HotelController _controller;

        [TestInitialize]
        public void SetUp()
        {
            _hotelDao = new mockHotelDao();
            _locationDao = new mockLocationDao();
            _roomDao = new mockRoomDao();
        }

        [TestCleanup]
        public void TearDown()
        {

        }

        [TestMethod()]
        public void IndexTest()
        {
            //Arrange
            _controller = new HotelController(_hotelDao, _locationDao, _roomDao);
            var testHotel = new Hotel();
            //Act 
            var testView = _controller.Index(testHotel);
            //Assert 
            Assert.IsNotNull(testView);
        }
        [TestMethod()]
        public void ResultTest()
        {
            //Arrange
            _controller = new HotelController(_hotelDao, _locationDao, _roomDao);
            var collection = new FormCollection(new Dictionary<string, StringValues>()
            {
                {"Location", "" },
                {"checkIn", "10/10/2021" },
                {"checkOut", "10/10/2021" },
                {"guests", "1" },
                {"minPrice", "1" },
                {"maxPrice", "1" },

            }
            );
            //Act 
            var testView = _controller.Result(collection);
            //Assert 
            Assert.IsNotNull(testView);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            //Arrange
            _controller = new HotelController(_hotelDao, _locationDao, _roomDao);
            var testHotelHref = "hotel/1";
            //Act 
            var testView = _controller.Details(testHotelHref);
            //Assert 
            Assert.IsNotNull(testView);
        }



        internal class mockHotelDao : IDao<HotelDto>
        {
            public string Create(HotelDto model) => "test";
            public int Delete(HotelDto model) => -1;
            public IEnumerable<HotelDto> ReadAll(HotelDto model) => new List<HotelDto>() { new HotelDto() { Name = "test1" }, new HotelDto() { Name = "test2" } };
            public HotelDto ReadByHref(string href) => new HotelDto() { Name = "test" };
            public int Update(HotelDto model) => -1;
        }
        internal class mockLocationDao : IDao<LocationDto>
        {
            public string Create(LocationDto model) => "test";
            public int Delete(LocationDto model) => -1;
            public IEnumerable<LocationDto> ReadAll(LocationDto model) => new List<LocationDto>() { new LocationDto() { City = "test1" }, new LocationDto() { City = "test2" } };
            public LocationDto ReadByHref(string href) => new LocationDto() { City = "test" };
            public int Update(LocationDto model) => -1;
        }
        internal class mockRoomDao : IDao<RoomDto>
        {
            public string Create(RoomDto model) => "test";
            public int Delete(RoomDto model) => -1;

            public IEnumerable<RoomDto> ReadAll(RoomDto model) => new List<RoomDto>() { new RoomDto() { Type = "test1" }, new RoomDto() { Type = "test2" } };
            public RoomDto ReadByHref(string href) => new RoomDto() { Type = "test" };

            public int Update(RoomDto model) => -1;
        }

    }
}