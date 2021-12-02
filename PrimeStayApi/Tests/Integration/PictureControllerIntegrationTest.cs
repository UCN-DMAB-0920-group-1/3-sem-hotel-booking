using API.Controllers;
using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System.Collections.Generic;
using System.Linq;
using Tests.Integration.Common;

namespace Tests.Integration
{

    [TestClass]
    public class PictureControllerIntegrationTest : BaseDbSetup
    {
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
            Assert.IsTrue(pictureDtos.Any());
            Assert.AreEqual("https://juto.dk/semester/hotel/1.png", pictureDtos.ElementAt(0).Path);
            Assert.AreEqual("https://juto.dk/semester/hotel/2.png", pictureDtos.ElementAt(1).Path);
            Assert.AreEqual("https://juto.dk/semester/hotel/3.png", pictureDtos.ElementAt(2).Path);
        }
    }
}
