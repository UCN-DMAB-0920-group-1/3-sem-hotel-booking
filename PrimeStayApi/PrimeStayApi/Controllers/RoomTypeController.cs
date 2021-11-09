using Microsoft.AspNetCore.Mvc;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.Model;
using PrimeStayApi.Model.DTO;
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
        public IEnumerable<RoomDto> Index([FromQuery] RoomDto room)
            => _dao.ReadAll(room.Map()).Select(r => r.Map());

        [HttpGet]
        [Route("{id}")]
        public RoomDto Details(int id) => _dao.ReadById(id).Map();

        [HttpPost]
        public ActionResult Create(RoomDto room)
        {
            int id = _dao.Create(room.Map());
            return Created(id.ToString(), room);
        }


        [HttpPut]
        public ActionResult Edit(int id, RoomDto room)
        {
            return _dao.Update(room.Map()) == 1 ? Ok() : NotFound();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            RoomTypeEntity room = new()
            {
                Id = id,
            };

            return _dao.Delete(room) == 1 ? Ok() : NotFound();
        }
    }
}
