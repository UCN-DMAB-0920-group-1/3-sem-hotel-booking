using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrimeStayApi.DataAccessLayer;
using PrimeStayApi.Enviroment;
using PrimeStayApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeStayApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly IDao<Hotel> _dao;
        public HotelController(IDao<Hotel> dao)
        {
            _dao = dao;
        }
        // GET: HotelController
        [HttpGet]
        public IEnumerable<Hotel> Index(int? id, string name, string description, string staffed_hours, int? stars)
            => _dao.ReadAll(new Dictionary<string, object>()
            {
                {"id", id },
                {"name", name},
                {"description",description },
                {"staffed_hours", staffed_hours },
                {"stars", stars }

            });

        // GET: HotelController/Details/5
        [Route("id={id}")]
        public Hotel Details(int id) => _dao.ReadById(id);


        // GET: HotelController/Create
        public ActionResult Create()
        {
            throw new NotImplementedException();
        }

        // POST: HotelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: HotelController/Edit/5
        public ActionResult Edit(int id)
        {
            throw new NotImplementedException();
        }

        // POST: HotelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: HotelController/Delete/5
        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        // POST: HotelController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
