using Microsoft.AspNet.Mvc;
using POCProject.Model;
using POCProject.Services;
using POCProject.ViewModels;
using System.Linq;

namespace POCProject.Controllers.web
{
    public class HomeController : Controller
    {
        private IMailService _mailService;
        private WorldContext _context;

        public HomeController(IMailService service, WorldContext context)
        {
            _mailService = service;
            _context = context;
        }

        public IActionResult Index()
        {
            var trips = _context.Trips.OrderBy(t => t.Name).ToList();

            return View(trips);
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
            if (ModelState.IsValid)
            {
                var email = Startup.configuration["AppSettings:SiteEmailAdress"];

                if (string.IsNullOrEmpty(email))
                {
                    ModelState.AddModelError("", "Could not send email, configuration is not found.");
                }

                if (_mailService.sendMail(email, email,
                    $"Contact Page From {contact.Name} ({contact.Email})",
                    contact.Message))
                {
                    ModelState.Clear();

                    ViewBag.Message = "Mail Sent. Thanks!";
                }
            }

            return View();
        }
    }
}
