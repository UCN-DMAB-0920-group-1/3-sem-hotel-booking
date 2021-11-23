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

        public RoomController(IDao<RoomTypeDto> dao)
        {
            _dao = dao;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IEnumerable<Room> GetAllHotelRoomsForHotel(string href)
        {
            return _dao.ReadAll(new RoomTypeDto() { HotelId = int.Parse(href[(href.LastIndexOf("/") + 1)..]) }).Select(r => r.Map());
        }

        public Room GetRoom(string href)
        {
            //TODO Make Pretty
            return _dao.ReadByHref(href).Map();
        }


    }
}
