using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        #region setup
        private readonly IDao<HotelEntity> _dao;
        public HotelController(IDao<HotelEntity> dao)
        {
            _dao = dao;
        }
        #endregion

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<HotelDto> Create([FromBody] HotelDto hotel)
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Edit([FromBody] HotelDto hotel)

        {
            int res = _dao.Update(hotel.Map());
            return res != -1 ? NoContent() : NotFound("Bad data, could not update hotel, check attributes");
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete([FromBody] HotelDto hotel)
        {
            int res = _dao.Delete(hotel.Map());
            return res == 1 ? NoContent() : NotFound("Bad data: Hotel could not be deleted, check attributes");
        }
    }
}
