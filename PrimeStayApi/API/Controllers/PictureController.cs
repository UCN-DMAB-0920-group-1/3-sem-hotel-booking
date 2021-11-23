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
    public class PictureController : Controller
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

        // GET: PictureController/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PictureController
        [HttpPost]
        public ActionResult Create()
        {
            return View();
        }

        // GET: PictureController/5
        [HttpPut]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // GET: PictureController/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
