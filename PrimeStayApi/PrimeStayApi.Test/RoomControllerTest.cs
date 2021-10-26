using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrimeStayApi.Controllers;
using PrimeStayApi.DataAccessLayer.DAO;
using PrimeStayApi.Enviroment;
using PrimeStayApi.Model;

namespace PrimeStayApi.Test
{
    [TestClass]
    public class RoomControllerTest
    {

        private static RoomController _controller;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _controller = new RoomController(DaoFactory.Create<RoomEntity>(new DataContext(ENV.ConnectionStringTest)));
        }

        [TestInitialize]
        public void SetupDatabase()
        {
            Database.Version.Upgrade(Enviroment.ENV.ConnectionStringTest);
        }

        [TestCleanup]
        public void TearDown()
        {
            Database.Version.Drop(Enviroment.ENV.ConnectionStringTest);
        }

        //[TestMethod]
        //public void TestIndex()
        //{
        //    //Arrange
        //    RoomEntity[] rooms;

        //    //Act
        //    rooms = _controller.Index(null, null, null, null, null, null, null).ToArray();

        //    //Assert
        //    Assert.IsNotNull(rooms);
        //    Assert.IsTrue(rooms.Length > 0);
        //}

        //[TestMethod]
        //public void TestGetRoomsByHotelId()
        //{

        //    //Arrange
        //    RoomEntity[] rooms;

        //    //Act
        //    rooms = _controller.Index(null, null, null, null, null, null, 1).ToArray();

        //    //Assert
        //    Assert.IsNotNull(rooms);
        //    Assert.IsTrue(rooms.Length > 0);



    }
    /*
    [TestMethod]
    public void TestGetRoomById()
    {
        //Arrange
        Room room = null;

        //Act
        room = _controller.Details(1);

        //Assert
        Assert.IsNotNull(room);
    }

    [TestMethod]
    public void TestCreateRoom()
    {
        //Arrange
        var collection = new FormCollection(new Dictionary<string, StringValues>
        {
            { "numOfAvailableRooms", "321" },
            { "numOfAvailableBeds", "123" },
            { "hotelId", "1" },
            { "rating", "5" },
            { "type", "Mælkerummet" },
            { "description", "Ikke for folk der er lactose introlerent. Beklager guys" }
        });

        ActionResult room = null;


        //Act
        room = _controller.Create(collection);

        //Assert
        Assert.IsNotNull(room);
    }


    [TestMethod]
    public void TestUpdateRoom()
    {
        //Arrange
        var collection = new FormCollection(new Dictionary<string, StringValues>
        {
            { "numOfAvailableRooms", "35" },
            { "numOfAvailableBeds", "3" },
            { "hotelId", "1" },
            { "rating", "3" },
            { "type", "Test Suite" },
            { "description", "Kun de helt modige tør være her!" }
        });

        ActionResult room = null;


        //Act
        room = _controller.Edit(1, collection);

        //Assert

        Assert.IsNotNull(room);
    }



    [TestMethod]
    public void TestDeleteRoom()
    {
        //Arrange


        //Act
        var result = _controller.Delete(1);
        Room room = _controller.Details(1);


        //Assert
        Assert.IsNull(room);
    }
}
    */
}
