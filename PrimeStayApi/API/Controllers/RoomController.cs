using DataAccessLayer;
using DataAccessLayer.DTO;
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
        private readonly IDao<RoomEntity> _dao;
        public RoomController(IDao<RoomEntity> dao)
        {
            _dao = dao;
        }

        // GET: RoomController
        [HttpGet]
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
        public ActionResult Create([FromBody] RoomDto room)
        {
            int id = _dao.Create(room.Map());
            return Created(id.ToString(), room);
        }


        [HttpPut]
        public ActionResult Edit([FromBody] RoomDto room)
        {
            return _dao.Update(room.Map()) == 1 ? Ok() : NotFound();
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] RoomDto room)
        {
            return _dao.Delete(room.Map()) == 1 ? Ok() : NotFound();
        }
    }


}
