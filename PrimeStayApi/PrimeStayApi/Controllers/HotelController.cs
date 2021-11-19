
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.Model;
using PrimeStayApi.Model.DTO;
using PrimeStayApi.Services;
using System;
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
        public ActionResult<IEnumerable<HotelDto>> Index([FromQuery] HotelDto hotel)
        {
            try
            {
                return Ok(_dao.ReadAll(new HotelEntity()
                {
                    Name = hotel.Name,
                    Description = hotel.Description,
                    Staffed_hours = hotel.StaffedHours,
                    Stars = hotel.Stars
                }).Select(h => h.Map()));

            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        // GET: HotelController/5
        [HttpGet]
        [Route("{id}")]
        public ActionResult<HotelDto> Details(int id)
        {
            try
            {
                return Ok(_dao.ReadById(id).Map());
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost]
        //[Route("create")]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([FromBody] HotelDto hotel)
        {
            var tesmp = hotel.Map();
            tesmp.Id = 100;
            hotel = tesmp.Map();
            return Created(hotel.Href, hotel);
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
