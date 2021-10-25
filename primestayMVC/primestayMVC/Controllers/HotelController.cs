using Microsoft.AspNetCore.Mvc;
using PrimeStay.MVC.DataAccessLayer;
using PrimeStay.MVC.DataAccessLayer.DTO;
using PrimeStay.MVC.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

namespace PrimeStay.MVC.Controllers
{
    public class HotelController : Controller
    {
        private readonly IDao<HotelDto> _dao;
        private readonly RoomController _RoomCTRL;
        private readonly LocationController _locationCTRL;

        public HotelController(IDao<HotelDto> dao, IDao<LocationDto> locationDao, IDao<RoomDto> roomDao)
        {
            _dao = dao;
            _locationCTRL = new LocationController(locationDao);
            _RoomCTRL = new RoomController(roomDao);
        }
        public IActionResult Index([FromQuery] Hotel hotel)
        {
            //if (hotels == null) 
            IEnumerable<HotelDto> hotels = _dao.ReadAll(hotel.Map());
            List<Hotel> hotelList = hotels.Select(h => h.Map()).ToList();
            hotelList.ForEach(h => h.Location = getHotelLocation(h));

            return View(hotelList);
        }


        //[Route("Details")]
        public IActionResult Details([FromQuery] string href)
        {
            int id = int.Parse(href[(href.LastIndexOf("/") + 1)..]);
            var hotel = GetHotel(id);
            hotel.Location = getHotelLocation(hotel);
            hotel.rooms = _RoomCTRL.getAllHotelRoomsForHotel(id);
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
            HotelDto emptyHotel = new HotelDto();
            return _dao.ReadAll(emptyHotel).Select(h => h.Map());
        }
        private Location getHotelLocation(Hotel h)
        {
            //TODO get controller from 
            return _locationCTRL.GetLocationById(h.Id ?? 0);
        }

    }
}
