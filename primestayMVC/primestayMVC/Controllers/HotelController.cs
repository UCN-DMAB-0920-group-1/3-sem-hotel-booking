using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeStay.MVC.DataAccessLayer;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PrimeStay.MVC.Model;
using PrimeStay.MVC.DataAccessLayer.DTO;

namespace PrimeStay.MVC.Controllers
{
    public class HotelController : Controller
    {
        private readonly IDao<HotelDto> _HotelDao;
        private readonly IDao<RoomTypeDto> _RoomDao;
        private readonly IDao<LocationDto> _LocationDao;

        public HotelController(IDao<HotelDto> dao, IDao<LocationDto> locationDao, IDao<RoomTypeDto> roomDao)
        {
            _HotelDao = dao;
            _LocationDao = locationDao;
            _RoomDao = roomDao;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Result(string location)
        {

            if (location is null) location = " ";
            IEnumerable<HotelDto> hotels = _HotelDao.ReadAll(new HotelDto());
            List<Hotel> hotelMatches = hotels.Select(h => h.Map()).ToList();
            hotelMatches.ForEach(h => h.Location = GetHotelLocation(h));
            hotelMatches = hotelMatches.Where(h => h.Matches(location)).ToList();

            hotelMatches = hotelMatches.Where(h => h.Matches(location)).ToList();

            return View(hotelMatches);
        }


        //[Route("Details")]
        public IActionResult Details(string hotelHref)
        {
            var hotel = GetHotel(hotelHref);
            hotel.Location = GetHotelLocation(hotel);
            hotel.rooms = GetAllHotelRoomsForHotel(hotelHref);

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

        #region ServiceMethods
        private Hotel GetHotel(string href)
        {
            return _HotelDao.ReadByHref(href).Map();
        }
        public IEnumerable<Hotel> GetAllHotels()
        {
            HotelDto emptyHotel = new();
            return _HotelDao.ReadAll(emptyHotel).Select(h => h.Map());
        }
        private Location GetHotelLocation(Hotel h)
        {
            return _LocationDao.ReadByHref($"/api/location/{h.Location_Id}").Map();
        }

        private IEnumerable<Room> GetAllHotelRoomsForHotel(string href)
        {
            return _RoomDao.ReadAll(new RoomTypeDto() { HotelId = int.Parse(href[(href.LastIndexOf("/") + 1)..]) }).Select(r => r.Map());
        }
        #endregion

    }
}
