using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Enviroment;
using PrimeStayApi.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Version = PrimeStayApi.Database.Version;

namespace PrimeStayApi.Test
{
    [TestClass]
    public class RoomTypeTest
    {
        private string connectionString = new ENV().ConnectionStringTest;
        private static SqlDataContext _dataContext;
        private static List<Action> _dropDatabaseActions = new();

        [TestInitialize]
        public void SetUp()
        {
            _dataContext = new SqlDataContext(connectionString);
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
        public void GetAvaliableforRoom()
        {
            //arrange
            var dao = DaoFactory.Create<RoomTypeEntity>(_dataContext) as IDaoDateExtension<RoomTypeEntity>;
            int roomTypeId = 1;
            DateTime startDate = DateTime.Parse("2010-11-10");
            DateTime endDate = DateTime.Parse("2010-11-15");
            //act
            var testRes = dao.CheckAvailability(roomTypeId, startDate, endDate);
            //assert
            Assert.IsNotNull(testRes.Avaliable);
            Assert.AreEqual(1, testRes.Avaliable);
        }
    }
}
