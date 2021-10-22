using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrimeStay.DataAccessLayer;
using PrimeStay.Model;
using primestayMVC.Model;
using primestayMVC.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace primestayMVC.Controllers
{
    public class RoomController : Controller
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IDao<RoomDal> _dao;

        public RoomController(ILogger<RoomController> logger, IDao<RoomDal> dao)
        {
            _dao = dao;
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IEnumerable<Room> getAllHotelRoomsForHotel(int hotel_id)
        {
            return _dao.ReadAll(new RoomDal() { Hotel_Id = hotel_id }).Select(r => r.Map());
        }
    }
}
