using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
