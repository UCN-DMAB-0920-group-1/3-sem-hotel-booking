using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomTypeController : Controller
    {
        private readonly IDao<RoomTypeEntity> _dao;
        public RoomTypeController(IDao<RoomTypeEntity> dao)
        {
            _dao = dao;
        }

        // GET: RoomController
        [HttpGet]
        public ActionResult<IEnumerable<RoomTypeDto>> Index([FromQuery] RoomTypeDto room)
        {
            try
            {
                return Ok(_dao.ReadAll(room.Map()).Select(roomType => roomType.Map()));
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
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
        public ActionResult Create(RoomTypeDto room)
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
        public ActionResult Edit(int id, RoomTypeDto room)
        {
            return _dao.Update(room.Map()) == 1 ? Ok() : NotFound();
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] RoomTypeDto room)
        {
            return _dao.Delete(room.Map()) == 1 ? Ok() : NotFound();
        }

        // GET: api/roomType/available?roomTypeId={id}&startDate={startDate}&endDate={endDate}
        [HttpGet]
        [Route("available")]
        public IActionResult roomAvailibility(int roomTypeId, DateTime startDate, DateTime endDate)
        {
            try
            {
                if(_dao is IDaoDateExtension<RoomTypeEntity> extendedDao)
                {
                    return Ok(extendedDao.CheckAvailability(roomTypeId, startDate, endDate).Map());
                } else
                {
                    throw new DaoExtensionException($"{_dao.GetType().Name} could not be cast to {typeof(IDaoDateExtension<RoomTypeEntity>).Name}");
                }
            }
            catch (SqlException ex)
            {
                return NotFound(ex);
            }
        }
    }
}
