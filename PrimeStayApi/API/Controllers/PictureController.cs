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
        #region setup
        private readonly IDao<PictureEntity> _dao;
        public PictureController(IDao<PictureEntity> dao)
        {
            _dao = dao;
        }
        #endregion

        [HttpGet]
        [Route("{type}/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<PictureDto>> getPictureByType(string type, int id) //TODO rename method
        {
            PictureEntity pictureEntity = new()
            {
                Type = type.ToLower(),
                Hotel_id = id,
                Room_id = id,
            };

            var pictures = _dao.ReadAll(pictureEntity).Select(p => p.Map());
            return pictures.Any() ? Ok(pictures) : NotFound();
        }
    }
}
