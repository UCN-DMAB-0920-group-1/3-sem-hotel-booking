using DataAccessLayer;
using DataAccessLayer.DTO;
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
    public class RoomTypeController : Controller
    {
        #region setup
        private readonly IDao<RoomTypeEntity> _dao;
        public RoomTypeController(IDao<RoomTypeEntity> dao)
        {
            _dao = dao;
        }
        #endregion

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<RoomTypeDto>> Index([FromQuery] RoomTypeDto room)
        {
            try
            {
                return Ok(_dao.ReadAll(room.Map()).Select(h => h.Map()));
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
        public ActionResult<RoomTypeDto> Details(int id)
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<RoomTypeDto> Create(RoomTypeDto room)
        {
            int id = _dao.Create(room.Map());

            if (id != -1)
            {
                return Created(id.ToString(), room);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Edit(RoomTypeDto room)
        {
            return _dao.Update(room.Map()) == 1 ? NoContent() : NotFound();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete([FromBody] RoomTypeDto room)
        {
            return _dao.Delete(room.Map()) == 1 ? NoContent() : NotFound();
        }

        [HttpGet]
        [Route("date")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RoomDto> RoomAvailibility(int roomTypeId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var res = (_dao as IDaoDateExtension<RoomTypeEntity>).CheckAvailability(roomTypeId, startDate, endDate);
                return Ok(res.Map());
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}
