using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeStay.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }
        public IActionResult Confirm()
        {
            return View();
        }

        public IActionResult Create(IFormCollection collection)
        {
            return View();
        }

    }
}
