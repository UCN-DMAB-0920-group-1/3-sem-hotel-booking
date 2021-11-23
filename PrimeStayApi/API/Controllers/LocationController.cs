using DataAccessLayer;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly IDao<LocationEntity> _dao;
        public LocationController(IDao<LocationEntity> dao)
        {
            _dao = dao;
        }
        // GET: LocationController
        [HttpGet]
        public ActionResult Index()
        {
            throw new NotImplementedException();
        }

        // GET: LocationController/Details/5
        [HttpGet]
        [Route("{id}")]
        public LocationDto Details(int id) => _dao.ReadById(id).Map();

        // POST: LocationController/Create
        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        // POST: LocationController/Edit/5
        [HttpPut]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        // POST: LocationController/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
    }


}
