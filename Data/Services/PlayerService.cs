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

            return player;
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

        public async Task<IEnumerable<Player>> RecalculateTeamsMiniMaxAlgorithm()
        {
            List<Player> players = (List<Player>) await GetSignedUpPlayers();
            // Sort the players based on their skill level
            players.Sort((p1, p2) => p1.Rating.CompareTo(p2.Rating));

            // Divide the players into teams using the minimax algorithm
            int numTeams = 2;
            int numPlayersPerTeam = players.Count / numTeams;

            for (int i = 0; i < numPlayersPerTeam; i++)
            {
                for (int j = 0; j < numTeams; j++)
                {
                    int index = i * numTeams + j;
                    players[index].Team = (Team) (j + 1);
                }
            }

            // Assign any remaining players to teams
            for (int i = numPlayersPerTeam * numTeams; i < players.Count; i++)
            {
                players[i].Team = (Team) (i % numTeams + 1);
            }

            await _context.SaveChangesAsync();

            // Log each team with the overall rating for that team
            for (int i = 1; i <= numTeams; i++)
            {
                var teamPlayers = players.Where(p => p.Team == (Team) i);
                var teamRating = teamPlayers.Sum(p => p.Rating);
                System.Diagnostics.Debug.WriteLine($"Team {i}: {teamRating}");
            }

            return players;
        }

        public async Task<IEnumerable<Player>> RecalculateTeams()
        {
            List<Player> players = (List<Player>)await GetSignedUpPlayers();
            // Sort the players based on their skill level
            players.Sort((p1, p2) => p2.Rating.CompareTo(p1.Rating));

            // Divide the players into teams using the greedy algorithm
            int numTeams = 2;
            int numPlayersPerTeam = players.Count / numTeams;

            for (int i = 0; i < numPlayersPerTeam; i++)
            {
                
                    for (int j = 0; j < numTeams; j++)
                    {
                    int index = i * numTeams + j;
                    if (i % 2 == 0)
                        {
                            players[index].Team = (Team)j;
                        }
                        else
                        {
                            players[index].Team = (Team)numTeams - j - 1;
                        }

                    }
                
            }

            // Assign any remaining players to teams
            for (int i = numPlayersPerTeam * numTeams; i < players.Count; i++)
            {
                var team1Rating = players.Where(p => p.Team == Team.Yellow).Sum(p => p.Rating);
                var team2Rating = players.Where(p => p.Team == Team.Green).Sum(p => p.Rating);

                if (team1Rating < team2Rating)
                {
                    players[i].Team = Team.Yellow;
                }
                else
                {
                    players[i].Team = Team.Green;
                }
            }

            await _context.SaveChangesAsync();

            // Log each team with the overall rating for that team
            for (int i = 0; i < numTeams; i++)
            {
                var teamPlayers = players.Where(p => p.Team == (Team)i);
                var teamRating = teamPlayers.Sum(p => p.Rating);
                System.Diagnostics.Debug.WriteLine($"Team {i}: {teamRating}");
            }

            return players;
        }

        public async Task<IEnumerable<Player>> RecalculateTeams(int numTeams)
        {
            List<Player> players = (List<Player>)await GetSignedUpPlayers();
            // Sort the players based on their skill level
            players.Sort((p1, p2) => p2.Rating.CompareTo(p1.Rating));

            // Divide the players into teams using the snake draft algorithm
            int numPlayersPerTeam = players.Count / numTeams;
            int direction = 1;
            int index = 0;

            for (int i = 0; i < numPlayersPerTeam; i++)
            {
                for (int j = 0; j < numTeams; j++)
                {
                    if (direction == 1)
                    {
                        index = i * numTeams + j;
                    }
                    else
                    {
                        index = (i + 1) * numTeams - j - 1;
                    }

                    players[index].Team = (Team)j;
                }

                direction *= -1;
            }

            // Assign any remaining players to teams
            for (int i = numPlayersPerTeam * numTeams; i < players.Count; i++)
            {
                var teamRatings = new Dictionary<Team, int>();

                foreach (var team in Enum.GetValues(typeof(Team)).Cast<Team>())
                {
                    teamRatings[team] = players.Where(p => p.Team == team).Sum(p => p.Rating);
                }

                var minRatingTeam = teamRatings.OrderBy(x => x.Value).First().Key;

                players[i].Team = minRatingTeam;
            }

            await _context.SaveChangesAsync();

            // Log each team with the overall rating for that team
            for (int i = 0; i < numTeams; i++)
            {
                var teamPlayers = players.Where(p => p.Team == (Team)i);
                var teamRating = teamPlayers.Sum(p => p.Rating);
                System.Diagnostics.Debug.WriteLine($"Team {i}: {teamRating}");
            }

            return players;
        }


    }
}
