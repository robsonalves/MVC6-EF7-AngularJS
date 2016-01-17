using Microsoft.AspNet.Mvc;
using POCProject.Services;
using POCProject.ViewModels;

namespace POCProject.Controllers.web
{
    public class HomeController : Controller
    {
        private IMailService _mailService;

        public HomeController(IMailService service)
        {
            _mailService = service;
        }

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
            if (ModelState.IsValid)
            {
                var email = Startup.configuration["AppSettings:SiteEmailAdress"];

                if (string.IsNullOrEmpty(email))
                {
                    ModelState.AddModelError("", "Could not send email, configuration is not found.");
                }

                if(_mailService.sendMail(email, email,
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
