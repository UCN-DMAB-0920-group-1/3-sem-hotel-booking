using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeStay.MVC.Model;

namespace PrimeStay.MVC.Test.Model
{
    [TestClass()]
    public class HotelHoteltests
    {
        [TestMethod]
        public void HotelMatchesTest()
        {
            //Arrange
            var nameTest = "name";
            var cityTest = "city";
            var countryTest = "country";
            var failTest = "error";
            var hotel = new Hotel()
            {
                Name = "nameTest",
                Location = new Location() { City = "cityTest", Country = "countryTest" }

            };
            //Act 
            var nameRes = hotel.Matches(nameTest);
            var cityRes = hotel.Matches(cityTest);
            var countryRes = hotel.Matches(countryTest);
            var failRes = hotel.Matches(failTest);
            //Assert
            Assert.IsTrue(nameRes);
            Assert.IsTrue(cityRes);
            Assert.IsTrue(countryRes);
            Assert.IsFalse(failRes);

        }
        [TestMethod]
        public void HotelMatchesNullTest()
        {
            //Arrange
            string nullTest = null;
            var hotel = new Hotel()
            {
                Name = "nameTest",
                Location = new Location() { City = "cityTest", Country = "countryTest" }

            };
            //Act 
            var nullRes = hotel.Matches(nullTest);
            //Assert
            Assert.IsFalse(nullRes);
        }
    }
}