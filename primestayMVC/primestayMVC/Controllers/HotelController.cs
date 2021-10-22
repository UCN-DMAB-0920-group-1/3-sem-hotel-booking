using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrimeStay.DataAccessLayer;
using PrimeStay.Model;
using primestayMVC.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace primestayMVC.Controllers
{
    public class HotelController : Controller
    {
        private readonly ILogger<HotelController> _logger;
        private readonly IDao<HotelDal> _dao;
        private readonly RoomController _roomController;

        public HotelController(ILogger<HotelController> logger, IDao<HotelDal> dao, RoomController roomController)
        {
            _logger = logger;
            _dao = dao;
            _roomController = roomController;
        }
        public IActionResult Index([FromQuery] Hotel hotel)
        {
            //if (hotels == null) 
            IEnumerable<HotelDal> hotels = _dao.ReadAll(hotel.Map());
            hotels.Select(h => h.Map()).ToList().ForEach(h => h.Location = getHotelLocation(h));

            return View(hotels);
        }


        //[Route("Details")]
        public IActionResult Details(int id)
        {
            var hotel = GetHotel(id);
            hotel.Location = getHotelLocation(hotel);
            hotel.rooms = _roomController.getAllHotelRoomsForHotel(id);
            return View(hotel);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private Hotel GetHotel(int id)
        {
            return _dao.ReadById(id).Map();

        }
        public IEnumerable<Hotel> GetAllHotels()
        {
            HotelDal emptyHotel = new HotelDal();
            return _dao.ReadAll(emptyHotel).Select(h => h.Map());
        }
        private Location getHotelLocation(Hotel h) => LocationController.GetLocation(h.LocationHref);

    }
}
