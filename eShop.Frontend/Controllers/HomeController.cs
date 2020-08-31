using System.Diagnostics;
using System.Net.Http;
using eShop.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eShop.Frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // https://stackoverflow.com/questions/32618759/viewcomponent-alternative-for-ajax-refresh/32628302
            // COmponent refresh 

            return View();
        }

        public IActionResult Shop()
        {
            return View();
        }

        public IActionResult ProductDetail()
        {
            return View();
        }

        public IActionResult Cart()
        {
            return View();
        }

        public IActionResult IndexVC()
        {
            return ViewComponent("Product");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}