using Microsoft.AspNetCore.Mvc;
using PrimeStay.MVC.DataAccessLayer;
using PrimeStay.MVC.DataAccessLayer.DTO;
using PrimeStay.MVC.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PrimeStay.MVC.Controllers
{
    public class RoomController : Controller
    {
        private readonly IDao<RoomDto> _dao;

        public RoomController(IDao<RoomDto> dao)
        {
            _dao = dao;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IEnumerable<Room> getAllHotelRoomsForHotel(int hotel_id)
        {
            return _dao.ReadAll(new RoomDto() { Hotel_Id = hotel_id }).Select(r => r.Map());
        }

        public Room GetRoom(string Href)
        {
            //TODO Make Pretty
            int id;
            if (ModelMapper.GetIdFromHref(Href) == null)
            {
                return null;
            }
            else
            {
                id = (int)ModelMapper.GetIdFromHref(Href);
                return _dao.ReadById(id).Map();
            }
        }
    }
}
