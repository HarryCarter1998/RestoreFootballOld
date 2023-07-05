using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestoreFootball.Data;
using RestoreFootball.Data.Services;
using RestoreFootball.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RestoreFootball.Controllers
{
    public class GameweeksController : Controller
    {
        private readonly RestoreFootballContext _context;
        private readonly IGameweekService _gameweekService;

        public GameweeksController(RestoreFootballContext context, IGameweekService gameweekService)
        {
            _context = context;
            _gameweekService = gameweekService;
        }

        // GET: Gameweeks
        public async Task<IActionResult> Index()
        {
            var gameweeks = await _context.Gameweek.OrderByDescending(g => g.Date).ToListAsync();
            return _context.Gameweek != null ? View(gameweeks) : Problem("Entity set 'RestoreFootballContext.Gameweek'  is null.");
        }


        // GET: Gameweeks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Gameweek == null)
            {
                return NotFound();
            }

            var gameweek = await _context.Gameweek
                .Include(g => g.Groupings)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameweek == null)
            {
                return NotFound();
            }

            return View(gameweek);
        }

        // GET: Gameweeks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gameweeks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GreenScore,NonBibsScore,YellowScore,OrangeScore")] Gameweek gameweek)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameweek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameweek);
        }

        // GET: Gameweeks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null || _context.Gameweek == null)
            {
                return NotFound();
            }

            var gameweek = await _context.Gameweek
                .Include(g => g.GameweekPlayers)
                    .ThenInclude(gp => gp.Player)
                .Include(g => g.Groupings)
                    .ThenInclude(gr => gr.GameweekPlayers)
                        .ThenInclude(gr => gr.Player)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gameweek == null)
            {
                return NotFound();
            }

            ViewBag.UngroupedGameweekPlayers = gameweek.GameweekPlayers
                .Where(gp => gp.GroupingId == null)
                .Select(gp => new {
                    GameweekPlayer = gp,
                    gp.Player
                })
                .ToList();

            return View(gameweek);
        }


        // POST: Gameweeks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,GreenScore,NonBibsScore,YellowScore,OrangeScore")] Gameweek gameweek)
        {
            //if (id != gameweek.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(gameweek);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!GameweekExists(gameweek.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}

            await _gameweekService.Edit(gameweek);

            return RedirectToAction(nameof(Index));
        }

        // GET: Gameweeks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Gameweek == null)
            {
                return NotFound();
            }

            var gameweek = await _context.Gameweek
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameweek == null)
            {
                return NotFound();
            }

            return View(gameweek);
        }

        // POST: Gameweeks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Gameweek == null)
            {
                return Problem("Entity set 'RestoreFootballContext.Gameweek'  is null.");
            }
            var gameweek = await _context.Gameweek.FindAsync(id);
            if (gameweek != null)
            {
                _context.Gameweek.Remove(gameweek);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameweekExists(int id)
        {
          return (_context.Gameweek?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //in the GameweeksController
        public IActionResult AddToGrouping(int groupingId, int gameweekPlayerId, int gameweekId)
        {            
            var gameweek = _context.Gameweek.Include(g => g.GameweekPlayers).Include(g => g.Groupings).SingleOrDefault(g => g.Id == gameweekId);

            var gameweekPlayer = gameweek?.GameweekPlayers.FirstOrDefault(gp => gp.Id == gameweekPlayerId);

            var grouping = gameweek?.Groupings.FirstOrDefault(g => g.Id == groupingId);

            if (gameweekPlayer == null || grouping == null)
            {
                return NotFound();
            }

            grouping.GameweekPlayers.Add(gameweekPlayer);

            _gameweekService.RecalculateTeams();
            return RedirectToAction(nameof(Edit), new { id = gameweekId });
        }

        //in the GameweeksController
        public IActionResult RemoveFromGrouping(int groupingId, int gameweekPlayerId, int gameweekId)
        {
            var gameweek = _context.Gameweek.Include(g => g.GameweekPlayers).Include(g => g.Groupings).SingleOrDefault(g => g.Id == gameweekId);

            var gameweekPlayer = gameweek?.GameweekPlayers.FirstOrDefault(gp => gp.Id == gameweekPlayerId);

            var grouping = gameweek?.Groupings.FirstOrDefault(g => g.Id == groupingId);

            if (gameweekPlayer == null || grouping == null)
            {
                return NotFound();
            }

            grouping.GameweekPlayers.Remove(gameweekPlayer);

            _gameweekService.RecalculateTeams();

            return RedirectToAction(nameof(Edit), new { id = gameweekId });
        }

        public IActionResult CreateGrouping(int gameweekId)
        {
            var gameweek = _context.Gameweek.Include(g => g.Groupings).SingleOrDefault(g => g.Id == gameweekId);
            if (gameweek == null)
            {
                return NotFound();
            }

            var grouping = new Grouping();
            gameweek?.Groupings.Add(grouping);
            _context.SaveChanges();

            return RedirectToAction(nameof(Edit), new { id = gameweekId });
        }

        public async Task<IActionResult> DeleteGrouping(int groupingId, int gameweekId)
        {
            var grouping = await _context.Grouping.Include(gw => gw.GameweekPlayers).FirstOrDefaultAsync(gw => gw.Id == groupingId);
            if (grouping == null)
            {
                return NotFound();
            }

            try
            {
                foreach (var gameweekPlayer in grouping.GameweekPlayers)
                {
                    gameweekPlayer.GroupingId = null;
                }
                _context.Grouping.Remove(grouping);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            await _gameweekService.RecalculateTeams();

            return RedirectToAction(nameof(Edit), new { id = gameweekId });
        }
    }
}
