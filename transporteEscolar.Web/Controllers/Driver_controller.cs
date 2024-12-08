using Microsoft.AspNetCore.Mvc;

namespace DriversControllers
{
    public class DriverController: Controller
    {
        public IActionResult Index_driver()
        {
            return View(); 
        }
        public IActionResult Drivers()
        {
            return View(); 
        }

        public IActionResult Edit()
        {
            return View(); 
        }

        public IActionResult Driver_ways()
        {
            return View(); 
        }
    }
}
