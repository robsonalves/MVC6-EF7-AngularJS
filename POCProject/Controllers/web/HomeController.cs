using Microsoft.AspNet.Mvc;
using POCProject.ViewModels;

namespace POCProject.Controllers.web
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel contact)
        {
            return View();
        }
    }
}
