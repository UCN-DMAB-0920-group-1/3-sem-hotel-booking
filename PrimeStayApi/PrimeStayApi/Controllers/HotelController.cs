
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
    public class HotelController : ControllerBase
    {
        private readonly IDao<HotelEntity> _dao;
        public HotelController(IDao<HotelEntity> dao)
        {
            _dao = dao;
        }

        // GET: HotelController
        [HttpGet]
        public IEnumerable<HotelDto> Index(string name, string description, string staffed_hours, int? stars)
            => _dao.ReadAll(new HotelEntity()
            {
                Name = name,
                Description = description,
                Staffed_hours = staffed_hours,
                Stars = stars

            }).Select(h => h.Map());

        // GET: HotelController/Details/5
        [HttpGet]
        [Route("{id}")]
        public HotelDto Details(int id) => _dao.ReadById(id).Map();



        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            int stars = new IntParser().parseInt(collection["stars"]);

            HotelEntity hotel = new()
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

            HotelEntity hotel = new()
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
            HotelEntity hotel = new()
            {
                Id = id,
            };

            return _dao.Delete(hotel) == 1 ? Ok() : NotFound();
        }


    }
}
