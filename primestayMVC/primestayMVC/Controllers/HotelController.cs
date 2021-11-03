using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeStay.MVC.DataAccessLayer;
using PrimeStay.MVC.DataAccessLayer.DTO;
using PrimeStay.MVC.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PrimeStay.MVC.Controllers
{
    public class HotelController : Controller
    {
        private readonly IDao<HotelDto> _HotelDao;
        private readonly IDao<RoomDto> _RoomDao;
        private readonly IDao<LocationDto> _LocationDao;

        public HotelController(IDao<HotelDto> dao, IDao<LocationDto> locationDao, IDao<RoomDto> roomDao)
        {
            _HotelDao = dao;
            _LocationDao = locationDao;
            _RoomDao = roomDao;
        }
        public IActionResult Index([FromQuery] Hotel hotel)
        {
            //if (hotels == null) 
            IEnumerable<HotelDto> hotels = _HotelDao.ReadAll(hotel.Map());
            List<Hotel> hotelList = hotels.Select(h => h.Map()).ToList();
            hotelList.ForEach(h => h.Location = GetHotelLocation(h));
            return View(hotelList);
        }

        public IActionResult Result(IFormCollection collection)
        {
            IEnumerable<HotelDto> hotels = _HotelDao.ReadAll(new HotelDto());
            List<Hotel> hotelMatches = hotels.Select(h => h.Map()).ToList();
            hotelMatches.ForEach(h => h.Location = GetHotelLocation(h));
            hotelMatches = hotelMatches.Where(h => h.Matches(collection["Location"])).ToList();

            SetSessionSearchBar(collection["location"], collection["startDate"], collection["endDate"], int.Parse(collection["guests"]), int.Parse(collection["minPrice"]), int.Parse(collection["maxPrice"]));

            return View((collection, hotelMatches));
        }


        //[Route("Details")]
        public IActionResult Details([FromQuery] string href)
        {
            var hotel = GetHotel(href);
            hotel.Location = GetHotelLocation(hotel);
            hotel.rooms = GetAllHotelRoomsForHotel(href);

            if (HttpContext is not null) HttpContext.Session.SetString("selectedHotel", href);
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
        private void SetSessionSearchBar(string location, string startDate, string endDate, int guests, int minPrice, int maxPrice)
        {
            if (HttpContext is not null)
            {
                HttpContext.Session.SetString("hotelLocation", location);
                HttpContext.Session.SetString("checkIn", startDate);
                HttpContext.Session.SetString("checkOut", endDate);
                HttpContext.Session.SetInt32("guests", guests);
                HttpContext.Session.SetInt32("minPrice", minPrice);
                HttpContext.Session.SetInt32("maxPrice", maxPrice);
            }
        }
        private IEnumerable<Room> GetAllHotelRoomsForHotel(string href)
        {
            return _RoomDao.ReadAll(new RoomDto() { HotelId = int.Parse(href[(href.LastIndexOf("/") + 1)..]) }).Select(r => r.Map());
        }
        #endregion

    }
}
