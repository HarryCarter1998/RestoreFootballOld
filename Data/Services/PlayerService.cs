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

        public void UpdateSignedUp(int id, bool signUp)
        {
            Player playerToUpdate = _context.Player.FirstOrDefault(p => p.Id == id);

            if (playerToUpdate != null) playerToUpdate.SignedUp = signUp;

            _context.SaveChanges();
        }

        public async Task<IEnumerable<Player>> GetRemainingPlayers()
        { 
            return await _context.Player.Where(p => p.SignedUp == false).ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetSignedUpPlayers()
        {
            return await _context.Player.Where(p => p.SignedUp == true).ToListAsync();
        }

        public async Task<IEnumerable<Player>> RecalculateTeams()
        {

            List<Player> players = (List<Player>)await GetSignedUpPlayers();

            if (!players.Any()) { return players; }

            var numTeams = players.Count >= 20 ? 4 : 2;

            var teamsAndRatings = new (int TeamNumber, int Rating)[players.Count];

            for (int i = 0; i < teamsAndRatings.Length; i++)
            {
                teamsAndRatings[i] = (i % numTeams, players[i].Rating);
            }

            double bestDiff = 99;
            var bestTeamsAndRatings = new (int TeamNumber, int Rating)[players.Count];

            for (int i = 0; i < 10000; i++)
            {
                int n = teamsAndRatings.Length;
                while (n > 1)
                {
                    n--;
                    int k = new Random().Next(n + 1);
                    int value = teamsAndRatings[k].TeamNumber;
                    teamsAndRatings[k].TeamNumber = teamsAndRatings[n].TeamNumber;
                    teamsAndRatings[n].TeamNumber = value;
                }

                var teamRatings = teamsAndRatings.GroupBy(t => t.TeamNumber)
                  .Select(g => g.Sum(t => t.Rating))
                  .ToArray();

                var diff = (teamRatings.Max() - teamRatings.Min()) / teamRatings.Average();

                if (diff < bestDiff)
                {
                    bestDiff = diff;
                    bestTeamsAndRatings = teamsAndRatings.ToArray();
                }
            }

            for (int l = 0; l < players.Count; l++)
            {
                players[l].Team = (Team)bestTeamsAndRatings[l].TeamNumber;
            }

            await _context.SaveChangesAsync();

            Debug.WriteLine(string.Join(", ", bestTeamsAndRatings));
            Debug.WriteLine($"{Math.Round(bestDiff*100, 2)}% difference");

            return players;
        }


    }
}
