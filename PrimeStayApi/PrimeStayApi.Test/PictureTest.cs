using Microsoft.VisualStudio.TestTools.UnitTesting;
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
using System.Threading.Tasks;
using Version = PrimeStayApi.Database.Version;

namespace PrimeStayApi.Test
{

    [TestClass]
    public class PictureTest
    {
        private string connectionString = new ENV().ConnectionStringTest;
        private static DataContext _dataContext;
        private static List<Action> _dropDatabaseActions = new();

        [TestInitialize]
        public void SetUp()
        {
            _dataContext = new DataContext(connectionString);
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
        public void GetRoomPictureTest()
        {
            //arrange
            IDao<PictureEntity> dao = DaoFactory.Create<PictureEntity>(_dataContext);
            PictureController controller = new PictureController(dao);
            IEnumerable<PictureDto> pictureDtos = null;
            string type = "room";
            int id = 1;


            //act 
            pictureDtos = controller.getPictureByType(type, id);

            //assert 
            Assert.IsNotNull(pictureDtos);
            Assert.AreEqual(pictureDtos.Count(), 3);
            Assert.AreEqual(pictureDtos.ElementAt(0).Path, "https://juto.dk/semester/room/1.png");
            Assert.AreEqual(pictureDtos.ElementAt(1).Path, "https://juto.dk/semester/room/2.png");
            Assert.AreEqual(pictureDtos.ElementAt(2).Path, "https://juto.dk/semester/room/3.png");
        }

        [TestMethod]
        public void GetHotelPictureTest()
        {
            //arrange
            IDao<PictureEntity> dao = DaoFactory.Create<PictureEntity>(_dataContext);
            PictureController controller = new PictureController(dao);
            IEnumerable<PictureDto> pictureDtos = null;
            string type = "hotel";
            int id = 1;


            //act 
            pictureDtos = controller.getPictureByType(type, id);

            //assert 
            Assert.IsNotNull(pictureDtos);
            Assert.AreEqual(pictureDtos.Count(), 3);
            Assert.AreEqual(pictureDtos.ElementAt(0).Path, "https://juto.dk/semester/hotel/1.png");
            Assert.AreEqual(pictureDtos.ElementAt(1).Path, "https://juto.dk/semester/hotel/2.png");
            Assert.AreEqual(pictureDtos.ElementAt(2).Path, "https://juto.dk/semester/hotel/3.png");
        }
    }
}
