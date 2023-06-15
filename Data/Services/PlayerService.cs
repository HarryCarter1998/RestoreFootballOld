using Microsoft.EntityFrameworkCore;
using RestoreFootball.Models;
using System.Diagnostics;

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

            return player;
        }

        public async Task Create(Player player)
        {
            player.Rating = (int)Math.Round(_context.Player.Average(p => p.Rating));
            _context.Add(player);
            await _context.SaveChangesAsync();
        }

        public async Task<Player?> Edit(int id)
        {
            return await _context.Player.FindAsync(id);
        }

        public async Task Edit(Player player)
        {
            var dbPlayer = await Details(player.Id);
            dbPlayer.FirstName = player.FirstName;
            dbPlayer.LastName = player.LastName;

            _context.Update(dbPlayer);
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

        public void SignUp(int id)
        {
            Player player = _context.Player.FirstOrDefault(p => p.Id == id);

            Gameweek latestGameweek = _context.Gameweek.OrderByDescending(g => g.Date).FirstOrDefault();

            var gameweekPlayer = new GameweekPlayer { Player = player, Gameweek = latestGameweek };

            latestGameweek.GameweekPlayers.Add(gameweekPlayer);
            player.GameweekPlayers.Add(gameweekPlayer);

            _context.SaveChanges();
        }

        public void CancelSignUp(int id)
        {
            var gameweekPlayer = _context.GameweekPlayer.FirstOrDefault(gp => gp.Id == id);

            if (gameweekPlayer != null)
            {
                _context.GameweekPlayer.Remove(gameweekPlayer);
                _context.SaveChanges();
            }
        }

        public async Task<IEnumerable<Player>> GetRemainingPlayers()
        {
            var latestGameweek = await _context.Gameweek.OrderByDescending(g => g.Date).FirstOrDefaultAsync();
            var latestGameweekPlayers = latestGameweek.GameweekPlayers.Select(gp => gp.Player);
            var allPlayers = await _context.Player.ToListAsync();
            var remainingPlayers = allPlayers.Except(latestGameweekPlayers);
            return remainingPlayers;
        }
    }
}
