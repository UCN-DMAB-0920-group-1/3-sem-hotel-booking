using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult login()
        {
            return View();
        }
    }
}
