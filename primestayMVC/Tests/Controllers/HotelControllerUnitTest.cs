using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebClient.Controllers;
using System.Collections.Generic;

namespace Tests.Controllers
{
    [TestClass()]
    public class HotelControllerUnitTest
    {
        private IDao<HotelDto> _hotelDao;
        private IDao<LocationDto> _locationDao;
        private IDao<RoomTypeDto> _roomDao;
        private HotelController _controller;

        [TestInitialize]
        public void SetUp()
        {
            _hotelDao = new MockHotelDao();
            _locationDao = new MockLocationDao();
            _roomDao = new MockRoomDao();
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
            //Act 
            var testView = _controller.Index();
            //Assert 
            Assert.IsNotNull(testView);
        }
        [TestMethod()]
        public void ResultTest()
        {
            //Arrange
            _controller = new HotelController(_hotelDao, _locationDao, _roomDao);
            var collection = "api/hotel/1";
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



        internal class MockHotelDao : IDao<HotelDto>
        {
            public string Create(HotelDto model)
            {
                return "test";
            }

            public int Delete(HotelDto model)
            {
                return -1;
            }

            public IEnumerable<HotelDto> ReadAll(HotelDto model)
            {
                return new List<HotelDto>() { new HotelDto() { Name = "test1" }, new HotelDto() { Name = "test2" } };
            }

            public HotelDto ReadByHref(string href)
            {
                return new HotelDto() { Name = "test" };
            }

            public int Update(HotelDto model)
            {
                return -1;
            }
        }
        internal class MockLocationDao : IDao<LocationDto>
        {
            public string Create(LocationDto model)
            {
                return "test";
            }

            public int Delete(LocationDto model)
            {
                return -1;
            }

            public IEnumerable<LocationDto> ReadAll(LocationDto model)
            {
                return new List<LocationDto>() { new LocationDto() { City = "test1" }, new LocationDto() { City = "test2" } };
            }

            public LocationDto ReadByHref(string href)
            {
                return new LocationDto() { City = "test" };
            }

            public int Update(LocationDto model)
            {
                return -1;
            }
        }
        internal class MockRoomDao : IDao<RoomTypeDto>
        {
            public string Create(RoomTypeDto model)
            {
                return "test";
            }

            public int Delete(RoomTypeDto model)
            {
                return -1;
            }

            public IEnumerable<RoomTypeDto> ReadAll(RoomTypeDto model)
            {
                return new List<RoomTypeDto>() { new RoomTypeDto() { Type = "test1" }, new RoomTypeDto() { Type = "test2" } };
            }

            public RoomTypeDto ReadByHref(string href)
            {
                return new RoomTypeDto() { Type = "test" };
            }

            public int Update(RoomTypeDto model)
            {
                return -1;
            }
        }

    }
}