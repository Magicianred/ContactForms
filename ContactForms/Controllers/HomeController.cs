using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ContactForms.Models;

namespace ContactForms.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly Database database;

        [TempData] 
        public string SuccessMessage { get; set; }

        public HomeController(ILogger<HomeController> logger, Database database)
        {
            this.logger = logger;
            this.database = database;
        }

        [HttpGet, Route("", Name = "root")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet, Route("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet, Route("mvc/contact")]
        public IActionResult Contact()
        {
            return View(new ContactModel {
                SuccessMessage = SuccessMessage
            });
        }

        [HttpPost, Route("mvc/contact")]
        public IActionResult Contact([FromForm] ContactModel model)
        {
            if (ModelState.IsValid)
            {
                database.Save(model);
                SuccessMessage = "Thank you for contacting us!";
                
                logger.LogInformation("Contact message submitted: {Message}", model);
                
                return RedirectToAction(nameof(Contact));
            }

            return View(model);
        }

        [HttpGet, Route("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}