using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebClient.Models;
using DataAccessLayer.DTO;
using DataAccessLayer;
using Models;
using System;

namespace WebClient.Controllers
{
    public class HotelController : Controller
    {
        private readonly IDao<HotelDto> _HotelDao;
        private readonly IDao<RoomTypeDto> _RoomDao;
        private readonly IDao<LocationDto> _LocationDao;
        private readonly IDao<PriceDto> _priceDao;

        public HotelController(IDao<HotelDto> dao, IDao<LocationDto> locationDao, IDao<RoomTypeDto> roomDao, IDao<PriceDto> priceDao)
        {
            _HotelDao = dao;
            _LocationDao = locationDao;
            _RoomDao = roomDao;
            _priceDao = priceDao;
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



            return View(hotelMatches);
        }


        //[Route("Details")]
        public IActionResult Details(string hotelHref)
        {
            Hotel hotel = GetHotel(hotelHref);
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
            return _LocationDao.ReadByHref($"/api/location/{h.LocationId}").Map();
        }

        private IEnumerable<Room> GetAllHotelRoomsForHotel(string href)
        {
            //Get to the last number of the href (last index of '/' + 1), then take the rest
            string idAsString = href[(href.LastIndexOf("/") + 1)..];
            int hotelId = int.Parse(idAsString);

            var rooms = _RoomDao.ReadAll(new RoomTypeDto() { HotelId = hotelId }).Select(r => r.Map()).ToList();

            for (int i = 0; i < rooms.Count(); i++)
            {
                rooms[i].price = GetPriceOnRoom(rooms[i]);
            }
 
            


            return rooms;
        }

        private int GetPriceOnRoom(Room room)
        {
            DateTime now = DateTime.Now;
            var res = _priceDao
                .ReadAll(new PriceDto() { RoomTypeId = room.Id ?? -1 })
                .Where((p) => p.StartDate <= now)
                .Last()
                .Value;

            return res;
        }
        #endregion

    }
}
