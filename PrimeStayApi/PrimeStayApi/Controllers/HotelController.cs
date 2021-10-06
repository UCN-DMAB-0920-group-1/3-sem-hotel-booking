using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        private readonly IDbConnection conn = new SqlConnection(ENV.ConnectionString());
        private const string GET_ALL_QUERY = "Select * FROM Hotel";
        // GET: HotelController
        [HttpGet]
        public IEnumerable<Hotel> Index()
        {
            return conn.Query<Hotel>(GET_ALL_QUERY);
        }

        // GET: HotelController/Details/5
        public ActionResult Details(int id)
        {
            throw new NotImplementedException();
        }

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
