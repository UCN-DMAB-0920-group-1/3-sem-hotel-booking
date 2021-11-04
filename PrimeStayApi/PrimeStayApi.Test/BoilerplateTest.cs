using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeStayApi.Controllers;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Database;
using PrimeStayApi.Enviroment;
using PrimeStayApi.Model;
using System.Collections.Generic;
using System.Linq;


/* description:
 * Use find-and-replace to replace {NAME} with the actual class you're testing.
 * HotelTest should be: find and replace "{NAME}" and replace with "test"
 * You may have to look though the tests, in order to have proper tests
 */

/*
namespace PrimeStayApi.Test
{
    [TestClass]
    public class BoilerplateTest
    {
        private {NAME}Controller _controllerWithDB;
        private IDao<{NAME}Entity> _dao;
        private DataContext _dataContext;
        private {NAME}Controller _controllerNoDB;


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
        public void Get{NAME}FromTestDBWithId()
        {
            //arrange
            _dao = DaoFactory.Create<{NAME}Entity>(_dataContext);
            _controllerWithDB = new {NAME}Controller(_dao);
            int {NAME}Id = 1;

            //act 
            var {NAME} = _controllerWithDB.Details({NAME}Id);

            //assert      //TODO: BELOW SHOULD BE UPDATED!
            Assert.IsTrue({NAME}.Name.Equals("{NAME} Petrús"));
        }

        [TestMethod]
        public void Get{NAME}FromTestDBWith{NAME}()
        {
            //arrange
            _dao = DaoFactory.Create<{NAME}Entity>(_dataContext);
            _controllerWithDB = new {NAME}Controller(_dao);

            //TODO: BELOW SHOULD BE UPDATED!
            var {NAME} = new {NAME}Entity()
            {
                Name = "{NAME} Petrús",
                Description = "Classic old fashioned {NAME} with a river of red wine.",
                Stars = 3,
                Staffed_hours = "24/7",
            };
            //act 
            var {NAME}s = _controllerWithDB.Index({NAME}.Name, {NAME}.Description, {NAME}.Staffed_hours, {NAME}.Stars);
            //assert 
            Assert.IsTrue({NAME}s.Count() == 1);
            Assert.IsTrue({NAME}s.First().Name == {NAME}.Name);
            Assert.IsTrue({NAME}s.First().Description == {NAME}.Description);
            Assert.IsTrue({NAME}s.First().StaffedHours == {NAME}.Staffed_hours);
            Assert.IsTrue({NAME}s.First().Stars == {NAME}.Stars);
        }

        internal class Fake{NAME}Dao : IDao<{NAME}Entity>
        {
            public int Create({NAME}Entity model)
            {
                return -1;
            }

            public int Delete({NAME}Entity model)
            {
                return -1;
            }

            public IEnumerable<{NAME}Entity> ReadAll({NAME}Entity model)
            {
                return new List<{NAME}Entity>();
            }

            public {NAME}Entity ReadById(int id)
            {
                return new {NAME}Entity();
            }

            public int Update({NAME}Entity model)
            {
                return -1;
            }
        }

        [TestMethod]
        public void Get{NAME}FakeDaoWithId()
        {
            //Arange
            _controllerNoDB = new {NAME}Controller(new Fake{NAME}Dao());
            int id = 1;

            //Act
            var res = _controllerNoDB.Details(id);

            //Assert
            Assert.IsTrue(string.IsNullOrEmpty(res.Name));
        }

        [TestMethod]
        public void Get{NAME}sFakeDaoWith{NAME}()
        {
            //Arrange
            _controllerNoDB = new {NAME}Controller(new Fake{NAME}Dao());
            var {NAME} = new {NAME}Entity()
            {
                Name = "Test",
                Description = "Test",
                Staffed_hours = "Test",
                Stars = 1,
            };

            //Act
            var res = _controllerNoDB.Index({NAME}.Name, {NAME}.Description, {NAME}.Staffed_hours, {NAME}.Stars);
            //Assert
            Assert.AreEqual(res.Count(), 0);
        }
    }
}
*/