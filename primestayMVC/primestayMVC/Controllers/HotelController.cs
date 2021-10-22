using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrimeStay.DataAccessLayer;
using PrimeStay.Model;
using primestayMVC.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace primestayMVC.Controllers
{
    public class HotelController : Controller
    {
        private readonly ILogger<HotelController> _logger;
        private readonly IDao<HotelDal> _dao;

        public HotelController(ILogger<HotelController> logger, IDao<HotelDal> dao)
        {
            _logger = logger;
            _dao = dao;
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
            var controller = DependencyResolver.Current.GetService<RoomController>();
            var hotel = GetHotel(id);
            hotel.Location = getHotelLocation(hotel);
            hotel.rooms = controller.getAllHotelRoomsForHotel(id);
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
        private Location getHotelLocation(Hotel h)
        {
            LocationController controller = DependencyResolver.Current.GetService<LocationController>();
            return controller.GetLocationById(h.Id ?? 0);
        }

    }
}
