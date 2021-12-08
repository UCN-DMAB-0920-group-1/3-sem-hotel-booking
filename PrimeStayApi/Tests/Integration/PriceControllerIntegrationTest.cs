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
                Value = 10000,
                Room_Type_Id = 1,
                Start_Date = DateTime.Now,
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
            Assert.AreEqual(1, price.Room_Type_Id);
            Assert.AreEqual(DateTime.Parse("2018-10-10"), price.Start_Date);
            Assert.AreEqual(50000, price.Value);
        }

        [TestMethod]
        public void ReadByRoomIdDaoTest()
        {
            //Arrange
            var dao = DaoFactory.Create<PriceEntity>(_dataContext);
            PriceEntity ValueToFind = new PriceEntity()
            {
                Room_Type_Id = 7,
            };
            IEnumerable<PriceEntity> ValueEntities = null;

            //Act
            ValueEntities = dao.ReadAll(ValueToFind);



            //Assert
            Assert.IsNotNull(ValueEntities);
            Assert.AreEqual(6, ValueEntities.Count());

            Assert.AreEqual(120000, ValueEntities.First().Value);
            Assert.AreEqual(DateTime.Parse("2016-10-10"), ValueEntities.First().Start_Date);

            Assert.AreEqual(140000, ValueEntities.ToList()[1].Value);
            Assert.AreEqual(DateTime.Parse("2017-10-10"), ValueEntities.ToList()[1].Start_Date);
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
                Value = 10000,
                RoomTypeId = 1,
                StartDate= DateTime.Now,
            };

            //Act
            var res = controller.Create(newPrice);

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
            var res = controller.Details(id);

            //Assert
            Assert.AreEqual(typeof(OkObjectResult), res.Result.GetType());
            var price = (res.Result as OkObjectResult).Value as PriceDto;

            Assert.AreEqual(2, price.RoomTypeId);
            Assert.AreEqual(DateTime.Parse("2016-10-10"), price.StartDate);
            Assert.AreEqual(50000, price.Value);
        }

        [TestMethod]
        public void ReadByRoomIdControllerTest()
        {
            //Arrange
            var dao = DaoFactory.Create<PriceEntity>(_dataContext);
            PriceController controller = new PriceController(dao);

            PriceDto ValueToFind = new PriceDto()
            {
                RoomTypeId = 7,
            };


            //Act
            var res = controller.Index(ValueToFind);

            //Assert
            Assert.AreEqual(typeof(OkObjectResult), res.Result.GetType());
            var prices = (res.Result as OkObjectResult).Value as IEnumerable<PriceDto>;

            Assert.IsNotNull(prices);
            Assert.AreEqual(6, prices.Count());

            Assert.AreEqual(120000, prices.First().Value);
            Assert.AreEqual(DateTime.Parse("2016-10-10"), prices.First().StartDate);

            Assert.AreEqual(140000, prices.ToList()[1].Value);
            Assert.AreEqual(DateTime.Parse("2017-10-10"), prices.ToList()[1].StartDate);
        }

        #endregion
    }
}
