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
    public class RoomController : Controller
    {
        #region setup
        private readonly IDao<RoomEntity> _dao;
        public RoomController(IDao<RoomEntity> dao)
        {
            _dao = dao;
        }
        #endregion

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<RoomDto>> Index([FromQuery] RoomDto room)
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
        public ActionResult<RoomDto> Details(int id)
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
        public ActionResult<RoomDto> Create([FromBody] RoomDto room)
        {
            int id = _dao.Create(room.Map());
            if (id <= 0) return BadRequest();
            return Created(id.ToString(), room);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Edit([FromBody] RoomDto room)
        {
            return _dao.Update(room.Map()) == 1 ? NoContent() : NotFound();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete([FromBody] RoomDto room)
        {
            return _dao.Delete(room.Map()) == 1 ? NoContent() : NotFound();
        }
    }


}
