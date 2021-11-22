﻿using Microsoft.AspNetCore.Mvc;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.Model;
using PrimeStayApi.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimeStayApi.Controllers
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
        public IEnumerable<RoomTypeDto> Index([FromQuery] RoomTypeDto room)
            => _dao.ReadAll(room.Map()).Select(r => r.Map());

        [HttpGet]
        [Route("{id}")]
        public RoomTypeDto Details(int id) => _dao.ReadById(id).Map();

        [HttpPost]
        public ActionResult Create(RoomTypeDto room)
        {
            int id = _dao.Create(room.Map());

            if(id != -1)
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
        public ActionResult<RoomDto> roomAvailibility(int roomTypeId, DateTime startDate, DateTime endDate)
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
