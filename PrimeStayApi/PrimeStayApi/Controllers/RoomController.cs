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
        public IEnumerable<Room> Index(int? id, string type, int? num_of_available, int? num_of_beds, string description, int? rating, int? hotel_id) => _dao.ReadAll(new Room()
        {
            Id = id,
            Type = type,
            Num_of_avaliable = num_of_available,
            Num_of_beds = num_of_beds,
            Description = description,
            Rating = rating,
            Hotel_Id = hotel_id
        });

        [HttpGet]
        [Route("{id}")]
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
                Hotel_Id = hotelId,
                Rating = rating
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
                Hotel_Id = hotelId,
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
