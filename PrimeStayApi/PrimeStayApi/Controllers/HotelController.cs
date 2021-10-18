
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.Model;
using System;
using System.Collections.Generic;
using PrimeStayApi.Services;
using PrimeStayApi.Model.DTO;
using System.Linq;

namespace PrimeStayApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IDao<Hotel> _dao;
        public HotelController(IDao<Hotel> dao)
        {
            _dao = dao;
        }

        // GET: HotelController
        [HttpGet]
        public IEnumerable<HotelDTO> Index(int? id, string name, string description, string staffed_hours, int? stars)
            => _dao.ReadAll(new Hotel()
            {
                Id = id,
                Name = name,
                Description = description,
                Staffed_hours = staffed_hours,
                Stars = stars

            }).Select(x => x.MapToDTO());

        // GET: HotelController/Details/5
        [Route("{id}")]
        [HttpGet]
        public HotelDTO Details(int id) => _dao.ReadById(id).MapToDTO();



        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            int stars = new IntParser().parseInt(collection["star"]);

            Hotel hotel = new()
            {
                Name = collection["name"],
                Description = collection["description"],
                Staffed_hours = collection["staffedHours"],
                Stars = stars,
            };

            int id = _dao.Create(hotel);

            return Created(id.ToString(), hotel);

        }

        [HttpPut]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            int stars = new IntParser().parseInt(collection["star"]);

            Hotel hotel = new()
            {
                Name = collection["name"],
                Description = collection["description"],
                Staffed_hours = collection["staffedHours"],
                Stars = stars,
                Id = id,
            };

            return _dao.Update(hotel) == 1 ? Ok() : NotFound();


        }

        [HttpDelete]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            Hotel hotel = new()
            {
                Id = id,
            };

            return _dao.Delete(hotel) == 1 ? Ok() : NotFound();
        }
    }
}
