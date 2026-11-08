using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WLdemo.Models;

namespace WLdemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // temporary test
        [HttpGet("test-hello")]
        public IActionResult Hello()
        {
            return Content("Running");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
