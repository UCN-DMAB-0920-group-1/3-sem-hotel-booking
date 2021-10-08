using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PrimeStayApi.Services;

namespace PrimeStayApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : Controller
    {
        private readonly IDao<Room> _dao;
        public RoomController(IDao<Room> dao)
        {
            _dao = dao;
        }

        // GET: RoomController
        [HttpGet]
        public IEnumerable<Room> Index() => _dao.ReadAll();


        [HttpGet]
        public Room Details(int id) => _dao.ReadById(id);

        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            IntParser parser = new();
            int availableRooms = parser.parseInt(collection["numOfAvailableRooms"]);
            int availableBeds = parser.parseInt(collection["numOfAvailableBeds"]);
            int hotelId = parser.parseInt(collection["hotelId"]);
            int rating = parser.parseInt(collection["rating"]);


            Room room = new()
            {
                Type = collection["type"],
                Description = collection["description"],
                Num_of_avaliable = availableRooms,
                Num_of_beds = availableBeds,
                HotelId = hotelId,
                Rating =rating
            };

            int id = _dao.Create(room);

            return Created(id.ToString(), room);
        }


        [HttpPut]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            IntParser parser = new();
            int availableRooms = parser.parseInt(collection["numOfAvailableRooms"]);
            int availableBeds = parser.parseInt(collection["numOfAvailableBeds"]);
            int hotelId = parser.parseInt(collection["hotelId"]);
            int rating = parser.parseInt(collection["rating"]);


            Room room = new()
            {
                Id = id,
                Type = collection["type"],
                Description = collection["description"],
                Num_of_avaliable = availableRooms,
                Num_of_beds = availableBeds,
                HotelId = hotelId,
                Rating = rating
            };

            return _dao.Update(room) == 1 ? Ok() : NotFound();
        }

        [HttpDelete]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            Room room = new()
            {
                Id = id,
            }; 

            return _dao.Delete(room) == 1 ? Ok() : NotFound();
        }
    }
}
