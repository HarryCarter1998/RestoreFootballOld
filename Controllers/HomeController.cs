using Microsoft.AspNetCore.Mvc;
using RestoreFootball.Controllers;
using RestoreFootball.Models;
using RestoreFootball2.Models;
using System.Diagnostics;

namespace RestoreFootball2.Controllers
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
            //make a viewbag to hold a list of players
            //make a new empty list of players
            List<Player> players = new List<Player>();

            ViewBag.players = players;

            //make a viewdata to hold a list of players
            ViewData["Players"] = players;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}