using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class RoomController : Controller
    {
        private readonly IDao<RoomTypeDto> _dao;
        private readonly IDao<PriceDto> _priceDao;

        public RoomController(IDao<RoomTypeDto> dao, IDao<PriceDto> priceDao)
        {
            _dao = dao;
            _priceDao = priceDao;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IEnumerable<RoomType> GetAllHotelRoomsForHotel(string href)
        {
            var rooms = _dao.ReadAll(new RoomTypeDto() { HotelHref = href }).Select(r => r.Map());

            var prices = _priceDao.ReadAll(new PriceDto() { RoomTypeId = rooms.First().Id ?? 0 });

            return rooms;
        }

        public RoomType GetRoom(string href)
        {
            //TODO Make Pretty
            return _dao.ReadByHref(href).Map();
        }


    }
}
