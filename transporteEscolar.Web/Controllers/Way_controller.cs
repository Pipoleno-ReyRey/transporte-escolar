using Microsoft.AspNetCore.Mvc;

namespace DriversControllers
{
    public class WayController: Controller
    {
        public IActionResult Index_ways()
        {
            return View(); 
        }
        public IActionResult All()
        {
            return View(); 
        }

        public IActionResult Edit()
        {
            return View(); 
        }
    }
}