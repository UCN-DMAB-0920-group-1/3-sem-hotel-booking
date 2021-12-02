using API.Controllers;
using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Integration.Common;

namespace Tests.Integration
{
    [TestClass]
    public class PriceControllerIntegrationTest : BaseDbSetup
    {
        #region DAO
        [TestMethod]
        public void CreatePriceDaoTest()
        {
            //Arrange
            var dao = DaoFactory.Create<PriceEntity>(_dataContext);
            PriceEntity newPrice = new PriceEntity()
            {
                price = 10000,
                room_type_id = 1,
                start_date = DateTime.Now,
            };

            //Act
            int id = dao.Create(newPrice);

            //Assert
            Assert.AreEqual(49, id);
        }

        [TestMethod]
        public void ReadByIdDaoTest()
        {
            //Arrange
            var dao = DaoFactory.Create<PriceEntity>(_dataContext);
            int id = 3;

            //Act
            PriceEntity price = dao.ReadById(id);


            //Assert
            Assert.AreEqual(1, price.room_type_id);
            Assert.AreEqual(DateTime.Parse("2018-10-10"), price.start_date);
            Assert.AreEqual(50000, price.price);
        }

        [TestMethod]
        public void ReadByRoomIdDaoTest()
        {
            //Arrange
            var dao = DaoFactory.Create<PriceEntity>(_dataContext);
            PriceEntity priceToFind = new PriceEntity()
            {
                room_type_id = 7,
            };
            IEnumerable<PriceEntity> priceEntities = null;

            //Act
            priceEntities = dao.ReadAll(priceToFind);



            //Assert
            Assert.IsNotNull(priceEntities);
            Assert.AreEqual(6, priceEntities.Count());

            Assert.AreEqual(120000, priceEntities.First().price);
            Assert.AreEqual(DateTime.Parse("2016-10-10"), priceEntities.First().start_date);

            Assert.AreEqual(140000, priceEntities.ToList()[1].price);
            Assert.AreEqual(DateTime.Parse("2017-10-10"), priceEntities.ToList()[1].start_date);
        }
        #endregion

        #region Controller
        [TestMethod]
        public void CreatePriceControllerTest()
        {
            //Arrange
            var dao = DaoFactory.Create<PriceEntity>(_dataContext);
            PriceController controller = new PriceController(dao);

            PriceDto newPrice = new PriceDto()
            {
                price = 10000,
                roomTypeId = 1,
                startDate = DateTime.Now,
            };

            //Act
            var res = controller.create(newPrice);

            //Assert
            Assert.AreEqual(typeof(OkObjectResult), res.Result.GetType());
            int? id = (res.Result as OkObjectResult).Value as int?;

            Assert.IsNotNull(id);
            Assert.AreEqual(49, id);
        }

        [TestMethod]
        public void ReadByIdControllerTest()
        {
            //Arrange
            var dao = DaoFactory.Create<PriceEntity>(_dataContext);
            PriceController controller = new PriceController(dao);
            int id = 7;

            //Act
            var res = controller.details(id);

            //Assert
            Assert.AreEqual(typeof(OkObjectResult), res.Result.GetType());
            var price = (res.Result as OkObjectResult).Value as PriceDto;

            Assert.AreEqual(2, price.roomTypeId);
            Assert.AreEqual(DateTime.Parse("2016-10-10"), price.startDate);
            Assert.AreEqual(50000, price.price);
        }

        [TestMethod]
        public void ReadByRoomIdControllerTest()
        {
            //Arrange
            var dao = DaoFactory.Create<PriceEntity>(_dataContext);
            PriceController controller = new PriceController(dao);

            PriceDto priceToFind = new PriceDto()
            {
                roomTypeId = 7,
            };


            //Act
            var res = controller.Index(priceToFind);

            //Assert
            Assert.AreEqual(typeof(OkObjectResult), res.Result.GetType());
            var prices = (res.Result as OkObjectResult).Value as IEnumerable<PriceDto>;

            Assert.IsNotNull(prices);
            Assert.AreEqual(6, prices.Count());

            Assert.AreEqual(120000, prices.First().price);
            Assert.AreEqual(DateTime.Parse("2016-10-10"), prices.First().startDate);

            Assert.AreEqual(140000, prices.ToList()[1].price);
            Assert.AreEqual(DateTime.Parse("2017-10-10"), prices.ToList()[1].startDate);
        }

        #endregion
    }
}
