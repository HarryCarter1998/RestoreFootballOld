using Microsoft.AspNetCore.Mvc;
using RestoreFootball.Data.Services;
using RestoreFootball.Models;
using RestoreFootball2.Models;
using System.Diagnostics;

namespace RestoreFootball.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPlayerService _playerService;
        private readonly IGameweekService _gameweekService;

        public HomeController(ILogger<HomeController> logger, IPlayerService playerService, IGameweekService gameweekService)
        {
            _logger = logger;
            _playerService = playerService;
            _gameweekService = gameweekService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            ViewBag.RemainingPlayers = await _playerService.GetRemainingPlayers();
            ViewBag.NumTeams = _gameweekService.GetLatestGameweekPlayers().Count() >= 20 ? 4 : 2;
            return View();
        }

        public IActionResult GetLatestGameweekPlayers()
        {
            return Json(_gameweekService.GetLatestGameweekPlayers());
        }

        public IActionResult RecalculateTeams()
        {
            var players = _gameweekService.RecalculateTeams().Result;

            return Json(players);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public int GetNumTeams()
        {
            return _gameweekService.GetLatestGameweekPlayers().Count() >= 20 ? 4 : 2;
        }
    }
}