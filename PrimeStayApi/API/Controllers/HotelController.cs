using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
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
                    Stars = hotel.Stars,
                    Active = hotel.Active,
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
        [Authorize(Roles = "admin")]
        public ActionResult Create([FromBody] HotelDto hotel)
        {
            int id = _dao.Create(hotel.Map());
            if (id != -1)
            {
                hotel.Href = $"api/hotel/{id}";
                return Created(hotel.Href, hotel);
            }
            else return BadRequest("Bad Request: Could not create Hotel, check attributes");
        }

        [HttpPut]
        public ActionResult Edit([FromBody] HotelDto hotel)

        {
            int res = _dao.Update(hotel.Map());
            return res != -1 ? Ok("Number of rows affected: " + res) : NotFound("Bad data, could not update hotel, check attributes");
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] HotelDto hotel)
        {
            int res = _dao.Delete(hotel.Map());
            return res == 1 ? Ok("Hotel successfully deleted") : NotFound("Bad data: Hotel could not be deleted, check attributes");
        }



    }
}
