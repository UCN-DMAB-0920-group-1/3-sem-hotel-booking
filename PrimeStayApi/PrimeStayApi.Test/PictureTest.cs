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
    public class PictureTest
    {
        private PictureController _controllerWithDB;
        private IDao<PictureEntity> _dao;
        private DataContext _dataContext;
        private PictureController _controllerNoDB;
       


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
        public void GetRoomPictureTest()
        {
            //arrange
            _dao = DaoFactory.Create<PictureEntity>(_dataContext);
            _controllerWithDB = new PictureController(_dao);
            IEnumerable<PictureDto> pictureDtos = null;
            string type = "room";
            int id = 1;


            //act 
            pictureDtos = _controllerWithDB.getPictureByType(type, id);

            //assert 
            Assert.IsNotNull(pictureDtos);
            Assert.IsTrue(pictureDtos.Count() == 3);
            Assert.IsTrue(pictureDtos.ElementAt(0).Path == "https://juto.dk/semester/room/1.png");
            Assert.IsTrue(pictureDtos.ElementAt(1).Path == "https://juto.dk/semester/room/2.png");
            Assert.IsTrue(pictureDtos.ElementAt(2).Path == "https://juto.dk/semester/room/3.png");
        }
        [TestMethod]
        public void GetHotelPictureTest()
        {
            //arrange
            _dao = DaoFactory.Create<PictureEntity>(_dataContext);
            _controllerWithDB = new PictureController(_dao);
            IEnumerable<PictureDto> pictureDtos = null;
            string type = "hotel";
            int id = 1;


            //act 
            pictureDtos = _controllerWithDB.getPictureByType(type, id);

            //assert 
            Assert.IsNotNull(pictureDtos);
            Assert.IsTrue(pictureDtos.Count() == 3);
            Assert.IsTrue(pictureDtos.ElementAt(0).Path == "https://juto.dk/semester/hotel/1.png");
            Assert.IsTrue(pictureDtos.ElementAt(1).Path == "https://juto.dk/semester/hotel/2.png");
            Assert.IsTrue(pictureDtos.ElementAt(2).Path == "https://juto.dk/semester/hotel/3.png");
        }
    }
}
