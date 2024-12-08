using Microsoft.AspNetCore.Mvc;

namespace transporteEscolar.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}