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
    public class PictureController : ControllerBase
    {
        private readonly IDao<PictureEntity> _dao;
        public PictureController(IDao<PictureEntity> dao)
        {
            _dao = dao;
        }

        // GET: PictureController/{type}/{id}
        [HttpGet]
        [Route("{type}/{id}")]
        public IEnumerable<PictureDto> getPictureByType(string type, int id)
        {
            PictureEntity pictureEntity = new()
            {
                Type = type.ToLower(),
                Hotel_id = id,
                Room_id = id,
            };

            return _dao.ReadAll(pictureEntity).Select(p => p.Map());
        }
    }
}
