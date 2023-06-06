using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestoreFootball.Models;

namespace RestoreFootball.Data.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly RestoreFootballContext _context;

        public PlayerService(RestoreFootballContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> Index()
        {
            var players = await _context.Player.ToListAsync();
            return players;
        }

        public async Task<Player> Details(int id)
        {
            var player = await _context.Player
                .FirstOrDefaultAsync(m => m.Id == id);

            return player is null ? throw new Exception() : player;
        }

        public async Task Create(Player player)
        {
            _context.Add(player);
            await _context.SaveChangesAsync();
        }

        public async Task<Player?> Edit(int id)
        {
            return await _context.Player.FindAsync(id);
        }

        public async Task Edit(Player player)
        {
            _context.Update(player);
            await _context.SaveChangesAsync();
        }

        public async Task<Player?> Delete(int id)
        {
            var player = await _context.Player.FirstOrDefaultAsync(p => p.Id == id);
            return player;
        }

        public async Task DeleteConfirmed(int id)
        {
            var player = await _context.Player.FindAsync(id);
            if (player != null)
            {
                _context.Player.Remove(player);
            }

            await _context.SaveChangesAsync();
        }

        public void AddExistingPlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public void AddNewPlayer(Player player)
        {
            throw new NotImplementedException();
        }

        public bool PlayerExists(int id)
        {
            return (_context.Player?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
