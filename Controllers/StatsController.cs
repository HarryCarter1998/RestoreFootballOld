using Microsoft.AspNetCore.Mvc;
using RestoreFootball.Data.Services;
using RestoreFootball.Models;
using RestoreFootball2.Models;
using System.Diagnostics;

namespace RestoreFootball.Controllers
{
    public class StatsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPlayerService _playerService;
        private readonly IGameweekService _gameweekService;

        public StatsController(ILogger<HomeController> logger, IPlayerService playerService, IGameweekService gameweekService)
        {
            _logger = logger;
            _playerService = playerService;
            _gameweekService = gameweekService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View();
        }
    }
}