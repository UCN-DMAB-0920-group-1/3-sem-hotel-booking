using API.Controllers;
using DataAccessLayer;
using DataAccessLayer.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Unit
{
    [TestClass]
    public class HotelControllerUnitTest
    {
        [TestMethod]
        public void GetHotelFakeDaoWithId()
        {
            //Arange
            HotelController controller = new HotelController(new MockHotelDao());
            int hotelId = 1;

            //Act
            var res = controller.Details(hotelId);

            //Assert
            Assert.AreEqual(res.Result.GetType(), typeof(OkObjectResult));
            var hotel = (res.Result as OkObjectResult).Value as HotelDto;

            //Assert.IsNotNull(hotel);
            //Assert.AreEqual(hotelId, hotel.ExtractId());
            //Assert.IsFalse(string.IsNullOrEmpty(hotel.Name));

            hotel.Should().NotBeNull().And.BeOfType<HotelDto>();
            hotel.ExtractId().Should().NotBeNull().And.Be(hotelId);
            hotel.Name.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void GetHotelsFakeDaoWithParameters()
        {
            //Arrange
            HotelController controller = new HotelController(new MockHotelDao());
            var hotel = new HotelEntity()
            {
                Name = "Test",
                Description = "Test",
                Staffed_hours = "Test",
                Stars = 1,
            };

            //Act
            var res = controller.Index(hotel.Map());
            var hotels = (res.Result as OkObjectResult).Value as IEnumerable<HotelDto>;

            //Assert
            Assert.AreEqual(res.Result.GetType(), typeof(OkObjectResult));

            Assert.IsNotNull(hotels);
            Assert.IsTrue(hotels.Any());
            Assert.IsFalse(hotels.Where(h => h is null).Any());
            Assert.AreEqual(2, hotels.Count());
        }

        [TestMethod]
        public void UpdateHotelFakeDao()
        {
            HotelController controller = new HotelController(new MockHotelDao());
            var hotel = new HotelDto()
            {
                Href = "api/Hotel/1",
                Name = "Test",
                Description = "Test",
                StaffedHours = "Test",
                Stars = 1,
            };

            //Act
            var res = controller.Edit(hotel);

            //Assert
            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(NoContentResult));

        }

        [TestMethod]
        public void CreateHotelFakeDao()
        {
            HotelController controller = new HotelController(new MockHotelDao());
            var hotel = new HotelDto()
            {
                Href = "api/Hotel/1",
                Name = "Test",
                Description = "Test",
                StaffedHours = "Test",
                Stars = 1,
                Active = true,
            };

            //Act
            var res = controller.Create(hotel);

            //Assert
            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res.Result, typeof(CreatedResult));

        }

        [TestMethod]
        public void DeleteHotelFakeDao()
        {
            HotelController controller = new HotelController(new MockHotelDao());
            var hotel = new HotelDto()
            {
                Href = "api/Hotel/1",
                Name = "Test",
                Description = "Test",
                StaffedHours = "Test",
                Stars = 1,
            };

            //Act
            var res = controller.Delete(hotel);

            //Assert
            Assert.IsNotNull(res);
            Assert.IsInstanceOfType(res, typeof(NoContentResult));

        }
    }

    #region mock implementations
    internal class MockHotelDao : IDao<HotelEntity>
    {
        private int count;
        public int Create(HotelEntity model)
        {
            return model is not null ? ++count : throw new Exception("Error, could not create");
        }

        public int Delete(HotelEntity model)
        {
            return model is not null && model.Id is not null ? model.Id!.Value : throw new Exception("Error, could not delete");
        }

        public int Update(HotelEntity model)
        {
            return model is not null && model.Id is not null ? model.Id!.Value : throw new Exception("Error, could not update");
        }

        public IEnumerable<HotelEntity> ReadAll(HotelEntity model)
        {
            return new List<HotelEntity>() {
                new HotelEntity()
                {
                    Id = 1,
                    Name = "Hotel Petrús",
                    Description = "Classic old fashioned hotel with a river of red wine.",
                    Stars = 3,
                    Staffed_hours = "24/7",
                },
                new HotelEntity()
                {
                    Id = 2,
                    Name = "Hotel Test",
                    Description = "Classic test hotel",
                    Stars = 3,
                    Staffed_hours = "24/7",
                }
            };
        }

        public HotelEntity ReadById(int id)
        {
            return new HotelEntity()
            {
                Id = 1,
                Name = "Hotel Petrús",
                Description = "Classic old fashioned hotel with a river of red wine.",
                Stars = 3,
                Staffed_hours = "24/7",
            };
        }
    }

    #endregion
}
