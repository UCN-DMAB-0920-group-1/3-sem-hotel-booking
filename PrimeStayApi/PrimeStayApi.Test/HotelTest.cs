using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeStayApi.Controllers;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.Database;
using PrimeStayApi.Enviroment;
using PrimeStayApi.Model;

namespace PrimeStayApi.Test
{
    [TestClass]
    public class HotelTest
    {
        private HotelController _controller;
        private IDao<Hotel> _dao;
        private DataContext _dataContext;


        [TestInitialize]
        private void SetUp()
        {
            _dataContext = new DataContext(ENV.ConnectionStringTest);
            _dao = DaoFactory.Create<Hotel>(_dataContext);
            _controller = new HotelController(_dao);
            Version.Upgrade(ENV.ConnectionStringTest);
        }
        [TestCleanup]
        private void CleanUp()
        {
            Version.Drop(ENV.ConnectionStringTest);
        }


        [TestMethod]
        public void GetHotelWithId1()
        {
            //arrange
            int hotelId = 1;
            //act 
            var hotel = _controller.Details(hotelId);
            //assert 
            Assert.IsTrue(hotel.Name.Equals("Hotel Petrús"));

        }
    }
}