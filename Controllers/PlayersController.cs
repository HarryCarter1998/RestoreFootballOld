using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestoreFootball.Data;
using RestoreFootball.Data.Services;
using RestoreFootball.Migrations;
using RestoreFootball.Models;

namespace RestoreFootball.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        // GET: Player
        public async Task<IActionResult> Index()
        {
            var players = await _playerService.Index();
            return View(players);
        }

        // GET: Player/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var player = await _playerService.Details(id);
            return View(player);
        }

        // GET: Player/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Player/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Create([Bind("FirstName,LastName,SignedUp")] Player player, bool redirect = true)
        {
            if (ModelState.IsValid)
            {
                await _playerService.Create(player);
                if (redirect) return Redirect("~/Players/Index");
            }
            return Ok();
        }

        // GET: Player/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var player = await _playerService.Edit(id);
            if (player is null) return NotFound();

            return View(player);
        }

        // POST: Player/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(Player player)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _playerService.Edit(player);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_playerService.PlayerExists(player.Id))
                    {
                        return NotFound();
                    }
                    else
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(player);
        }

        // GET: Player/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var player = await _playerService.Delete(id);
            if (player is null) return NotFound();

            return View(player);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _playerService.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public void UpdateSignedUp(int id, bool signUp)
        {
            _playerService.UpdateSignedUp(id, signUp);
        }
    }
}
