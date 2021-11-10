using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Enviroment;
using PrimeStayApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Version = PrimeStayApi.Database.Version;

namespace PrimeStayApi.Test
{
    [TestClass]
    public class RoomDaoTest
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
        public void RoomDaoGetAllTest()
        {
            //Arrange
            var dao = DaoFactory.Create<RoomEntity>(_dataContext);
            var testRoom = new RoomEntity();
            //Act
            var list = dao.ReadAll(testRoom);
            //Assert
            Assert.IsTrue(list.Any());
        }

    }

}
