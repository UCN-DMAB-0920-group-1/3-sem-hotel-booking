using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.Model;
using PrimeStayApi.Model.DTO;
using PrimeStayApi.Services;
using System.Collections.Generic;
using System.Linq;

namespace PrimeStayApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : Controller
    {
        private readonly IDao<RoomEntity> _dao;
        public RoomController(IDao<RoomEntity> dao)
        {
            _dao = dao;
        }

        // GET: RoomController
        [HttpGet]
        public IEnumerable<RoomDto> Index([FromQuery] RoomDto room)
            => _dao.ReadAll(room.Map()).Select(r => r.Map());

        [HttpGet]
        [Route("{id}")]
        public RoomDto Details(int id) => _dao.ReadById(id).Map();

        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            IntParser parser = new();
            int availableRooms = parser.parseInt(collection["numOfAvailableRooms"]);
            int availableBeds = parser.parseInt(collection["numOfAvailableBeds"]);
            int hotelId = parser.parseInt(collection["hotelId"]);
            int rating = parser.parseInt(collection["rating"]);


            RoomEntity room = new()
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


            RoomEntity room = new()
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
        public ActionResult Delete(int id)
        {
            RoomEntity room = new()
            {
                Id = id,
            };

            return _dao.Delete(room) == 1 ? Ok() : NotFound();
        }
    }
}
