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
    public class BookingDaoTest
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
        public void InsertBookingsTest()
        {
            //Arrange 
            IDao<BookingEntity> dao = DaoFactory.Create<BookingEntity>(_dataContext);
            var booking = new BookingEntity()
            {
                Start_date = DateTime.Parse("2020-11-11"),
                End_date = DateTime.Parse("2020-12-12"),
                Customer_id = 1,
                Guests = 1,
                Room_id = 8,
            };
            //Act 
            var test = dao.Create(booking);
            //Assert
            Assert.IsTrue(test > 0);
        }
        [TestMethod]
        public void InsertTooManyBookingsTest()
        {
            //Arrange 
            IDao<BookingEntity> dao = DaoFactory.Create<BookingEntity>(_dataContext);
            var booking = new BookingEntity()
            {
                Start_date = DateTime.Parse("2010-12-04"),
                End_date = DateTime.Parse("2010-12-16"),
                Customer_id = 1,
                Guests = 1,
                Room_id = 8,
            };
            //Act 
            dao.Create(booking);
            var fail = dao.Create(booking);
            //Assert
            Assert.AreEqual(-1, fail);
        }
    }
}