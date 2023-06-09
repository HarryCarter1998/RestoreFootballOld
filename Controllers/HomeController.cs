using Microsoft.AspNetCore.Mvc;
using RestoreFootball.Data.Services;
using RestoreFootball2.Models;
using System.Diagnostics;

namespace RestoreFootball2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPlayerService _playerService;

        public HomeController(ILogger<HomeController> logger, IPlayerService playerService)
        {
            _logger = logger;
            _playerService = playerService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.RemainingPlayers = await _playerService.GetRemainingPlayers();
            ViewBag.NumTeams = _playerService.GetSignedUpPlayers().Result.Count() >= 20 ? 4 : 2;
            return View();
        }

        public IActionResult GetSignedUpPlayers()
        {
            var players = _playerService.GetSignedUpPlayers().Result;

            return Json(players);
        }

        public IActionResult RecalculateTeams()
        {
            var players = _playerService.RecalculateTeams().Result;

            return Json(players);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}