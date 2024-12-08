using Microsoft.AspNetCore.Mvc;

namespace StudentsControllers
{
    public class StudentController: Controller
    {
        public IActionResult Index_student()
        {
            return View(); 
        }

        public IActionResult Edit()
        {
            return View(); 
        }

        public IActionResult All()
        {
            return View(); 
        }

        public IActionResult Pays()
        {
            return View(); 
        }
    }
}
