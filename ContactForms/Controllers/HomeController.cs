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

        public IActionResult Index()
        {
            return View();
        }

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
                SuccessMessage = "Thank you for contact us!";
                
                logger.LogInformation(model.ToString());
                
                return RedirectToAction("Contact");
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}