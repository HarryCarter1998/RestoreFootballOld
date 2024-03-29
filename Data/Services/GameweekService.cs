﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestoreFootball.Models;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RestoreFootball.Data.Services
{
    public class GameweekService : IGameweekService
    {
        private readonly RestoreFootballContext _context;
        private ICollection<Grouping> _groupings = new List<Grouping>();
        private readonly IConfiguration _configuration;

        public GameweekService(RestoreFootballContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IActionResult> Details(int? id)
        {
              if (id == null)
            {
                return new NotFoundResult();
            }

            var gameweek = await _context.Gameweek
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameweek == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(gameweek);
        }

        public async Task CreateNextGameweek()
        {
            var latestGameweek = await GetLatestGameweek();
            var newGameweek = new Gameweek { Date = latestGameweek.Date.AddDays(7) };
            _context.Update(newGameweek);
            await _context.SaveChangesAsync();
        }


        public async Task Edit(Gameweek gameweek)
        {
            var dbGameweek = await GetLatestGameweek();

            dbGameweek.GreenScore = gameweek.GreenScore;
            dbGameweek.NonBibsScore = gameweek.NonBibsScore;
            dbGameweek.YellowScore = gameweek.YellowScore;
            dbGameweek.OrangeScore = gameweek.OrangeScore;

            _context.Update(dbGameweek);

            var results = new List<(int? score, Team team)>
            {
                (gameweek.GreenScore, Team.Green),
                (gameweek.NonBibsScore, Team.NonBibs),
                (gameweek.YellowScore, Team.Yellow),
                (gameweek.OrangeScore, Team.Orange)
            };

            AdjustPlayerRatings(dbGameweek, results);
            AdjustWinStreaks(dbGameweek, results);

            await _context.SaveChangesAsync();

            await CreateNextGameweek();
        }

        private void AdjustWinStreaks(Gameweek gameweek, List<(int? score, Team team)> results)
        {
            var highestScoringTeams = results.Where(x => x.score == results.Max(r => r.score)).Select(x => x.team);
            if (highestScoringTeams.Count() > 1)
            {
                gameweek.GameweekPlayers
                    .Select(gp => gp.Player)
                    .ToList()
                    .ForEach(p => p.WinStreak = 0);
                return;
            }

            Team highestScoringTeam = highestScoringTeams.First();

            gameweek.GameweekPlayers
                .Where(gp => gp.Team == highestScoringTeam)
                .Select(gp => gp.Player)
                .ToList()
                .ForEach(p => p.WinStreak += 1);
            gameweek.GameweekPlayers
                .Where(gp => gp.Team != highestScoringTeam)
                .Select(gp => gp.Player)
                .ToList()
                .ForEach(p => p.WinStreak = 0);
            return;

        }

        public async Task<Gameweek> GetLatestGameweek()
        {
            return await _context.Gameweek
                .OrderByDescending(g => g.Date)
                .Include(g => g.GameweekPlayers)
                    .ThenInclude(gp => gp.Player)
                .Include(g => g.Groupings)
                    .ThenInclude(str => str.GameweekPlayers)
                .FirstOrDefaultAsync();
        }

        private void AdjustPlayerRatings(Gameweek gameweek, List<(int? score, Team team)> results)
        {
            foreach (var (score, team) in results)
            {
                int ratingChange = 0;

                int numTeams = results.Where(r => r.score != null).Count();

                if (numTeams == 2) ratingChange = score == 3 ? 25 : -25;
                else ratingChange = (int)Math.Round(100 * ((score ?? 0) / 9.0 - 0.5));

                gameweek.GameweekPlayers
                    .Where(gp => gp.Team == team)
                    .Select(gp => gp.Player)
                    .ToList()
                    .ForEach(p => p.Rating += ratingChange);
            }
        }

        public ICollection<GameweekPlayer> GetUngroupedGameweekPlayers(int id)
        {
            return _context.Gameweek
                .Include(g => g.GameweekPlayers)
                    .ThenInclude(gp => gp.Player)
                .Where(g => g.Id == id)
                .SelectMany(g => g.GameweekPlayers)
                .ToList();
        }


        public ICollection<GameweekPlayer> GetLatestGameweekPlayers()
        {
            var latestGameweek = GetLatestGameweek().Result;
            return latestGameweek.GameweekPlayers;
        }


        public async Task<IEnumerable<GameweekPlayer>> RecalculateTeams()
        {
            var gameweekPlayers = GetLatestGameweekPlayers();            

            if(AssigningFirstPlayer(gameweekPlayers)) return gameweekPlayers;

            _groupings = GetLatestGameweek().Result.Groupings;

            if (!gameweekPlayers.Any()) { return gameweekPlayers; }

            var players = gameweekPlayers.Select(gp => gp.Player).ToList();

            var playerInfos = gameweekPlayers.Select(gp => new PlayerInfo
            {
                GameweekPlayerId = gp.Id,
                Rating = players.FirstOrDefault(p => p.Id == gp.Player.Id)?.Rating ?? 0,
                Team = gp.Team
            }).ToArray();

            var teamsAndRatings = CreateTeamsAndRatings(playerInfos);

            var bestTeamsAndRatings = FindBestTeamsAndRatings(gameweekPlayers.Count, teamsAndRatings);

            await AssignTeams(gameweekPlayers, bestTeamsAndRatings);

            return gameweekPlayers;
        }

        private bool AssigningFirstPlayer(IEnumerable<GameweekPlayer> gameweekPlayers)
        {
            if (gameweekPlayers.Count() == 1)
            {
                gameweekPlayers.First().Team = Team.Green;
                return true;
            }
            return false;
        }

        private PlayerInfo[] CreateTeamsAndRatings(ICollection<PlayerInfo> players)
        {
            var numTeams = players.Count >= 20 ? 4 : 2;

            var teamsAndRatings = new PlayerInfo[players.Count()];

            for (int i = 0; i < teamsAndRatings.Count(); i++)
            {
                teamsAndRatings[i] = new PlayerInfo { GameweekPlayerId = players.ElementAt(i).GameweekPlayerId, Rating = players.ElementAt(i).Rating, Team = (Team)(i % numTeams) };
            }

            return teamsAndRatings;
        }

        private PlayerInfo[] FindBestTeamsAndRatings(int playerCount, PlayerInfo[] teamsAndRatings)
        {
            var numPlayersInEachTeam = teamsAndRatings.OrderBy(t => t.Team).GroupBy(t => t.Team).Select(g => g.Count()).ToArray();
            int numTeams = numPlayersInEachTeam.Length;
            int minPlayersPerTeam = numPlayersInEachTeam.Min();
            bool unevenTeams = (teamsAndRatings.Length % numTeams) > 0;
            int handicap = 0;
            if (unevenTeams && (minPlayersPerTeam is >= 4 and <= 9)) {
                var defaultHandicap = _configuration.GetValue<int>("DefaultHandicap");
                handicap = defaultHandicap - 75 * (minPlayersPerTeam - 4);
            }

            double bestDiff = 99;
            var bestTeamsAndRatings = new PlayerInfo[playerCount];
            var maxTries = playerCount < 6 ? 27 : Math.Pow((playerCount / 2), 3);
            int[] bestTeamRatings = new int[numTeams];

            for (int i = 0; i < maxTries; i++)
            {
                teamsAndRatings = ShuffleTeams(teamsAndRatings);

                var teamRatings = teamsAndRatings
                    .OrderBy(t => t.Team)
                    .GroupBy(t => t.Team)
                    .Select(g => g.Sum(t => t.Rating))
                    .ToArray();

                if (handicap > 0)
                    teamRatings = AdjustHandicap(teamRatings, numPlayersInEachTeam, handicap);

                var diff = (teamRatings.Max() - teamRatings.Min()) / teamRatings.Average();

                if (diff < bestDiff)
                {
                    bestDiff = diff;
                    bestTeamsAndRatings = CopyTeamsAndRatings(teamsAndRatings);
                    bestTeamRatings = teamRatings;
                }

                if (bestDiff < 0.01 || (bestDiff < 0.05 && i > 500)) { break; }
            }

            for (int i = 0; i < numTeams; i++)
                if (numPlayersInEachTeam[i] > minPlayersPerTeam && handicap > 0)
                {
                    Debug.WriteLine($"Team {((Team)i)}: {bestTeamRatings[i] - handicap} with handicap {handicap} = {bestTeamRatings[i]}");
                }
                else
                {
                    Debug.WriteLine($"Team {((Team)i)}: {bestTeamRatings[i]} with no handicap");
                }

            return bestTeamsAndRatings;
        }

        private PlayerInfo[] CopyTeamsAndRatings(PlayerInfo[] teamsAndRatings)
        {
            var bestTeamsAndRatings = new PlayerInfo[teamsAndRatings.Count()];
            for (int i = 0; i < teamsAndRatings.Count(); i++)
            {
                bestTeamsAndRatings[i] = new PlayerInfo();
                bestTeamsAndRatings[i].Team = teamsAndRatings[i].Team;
                bestTeamsAndRatings[i].Rating = teamsAndRatings[i].Rating;
            }
            return bestTeamsAndRatings;
        }

        private static int[] AdjustHandicap(int[] teamRatings, int[] numPlayersInEachTeam, int handicap)
        {
            for(int i = 0; i < numPlayersInEachTeam.Count(); i++)
                if (numPlayersInEachTeam[i] > numPlayersInEachTeam.Min())
                    teamRatings[i] += handicap;

            return teamRatings;
        }

        private async Task AssignTeams(ICollection<GameweekPlayer> gameweekPlayers, PlayerInfo[] bestTeamsAndRatings)
        {
            for (int l = 0; l < gameweekPlayers.Count; l++)
            {
                gameweekPlayers.ElementAt(l).Team = bestTeamsAndRatings[l].Team;
            }

            await _context.SaveChangesAsync();
        }

        private PlayerInfo[] ShuffleTeams(PlayerInfo[] teamsAndRatings)
        {
            int n = teamsAndRatings.Length;
            while (n > 1)
            {
                n--;
                int k = new Random().Next(n + 1);
                Team value = teamsAndRatings[k].Team;
                teamsAndRatings[k].Team = teamsAndRatings[n].Team;
                teamsAndRatings[n].Team = value;
            }
            if(_groupings.Any())
                if (!TeamsObeyGroupings(teamsAndRatings))
                    ShuffleTeams(teamsAndRatings); 

            return teamsAndRatings;
        }

        private bool TeamsObeyGroupings(PlayerInfo[] playerInfos)
        {

            foreach (Grouping grouping in _groupings)
            {
                List<int> groupedPlayers = grouping.GameweekPlayers.Select(gp => gp.Id).ToList();

                var numOfDiffTeams = playerInfos.Where(pi => groupedPlayers.Contains(pi.GameweekPlayerId)).Select(pi => pi.Team).Distinct().Count();

                if (numOfDiffTeams > 1) { return false; }
            }

            return true;
        }

        public ICollection<GameweekPlayer> GetGameweekPlayers(int gameweekId)
        {
            return _context.GameweekPlayer.Include(gp => gp.Player).Where(gp => gp.Gameweek.Id == gameweekId).ToList();
        }

        public async Task<IEnumerable<(string PlayerName, int score)>> GetBestLastTenGames()
        {
            List<(string PlayerName, int score)> players = new();

            var lastTenGameweeks = await _context.Gameweek
                .Include(g => g.GameweekPlayers)
                .Where(g => g.GameweekPlayers.Count() > 0)
                .OrderByDescending(g => g.Date)
                .Take(10)
                .ToListAsync();

            var lastTenGameweeksGameweekPlayers = lastTenGameweeks.SelectMany(g => g.GameweekPlayers).ToList();

            var lastTenGameweeksPlayers = lastTenGameweeksGameweekPlayers.Select(gp => gp.Player).Distinct().ToList();
            foreach (var player in lastTenGameweeksPlayers)
            {
                var totalPoints = 0;
                foreach (var results in lastTenGameweeks)
                {
                    var gameweekPlayer = results?.GameweekPlayers?.FirstOrDefault(gp => gp.Player == player);
                    if (gameweekPlayer == null) continue;

                    string property = gameweekPlayer.Team switch
                    {
                        Team.Green => "GreenScore",
                        Team.NonBibs => "NonBibsScore",
                        Team.Yellow => "YellowScore",
                        Team.Orange => "OrangeScore",
                        _ => throw new ArgumentException("Invalid team specified: " + gameweekPlayer.Team),
                    };
                    var gameweekProperty = results.GetType().GetProperty(property);
                    var points = gameweekProperty?.GetValue(results) as int? ?? 0;
                    totalPoints += points;
                }
                players.Add(($"{player.FirstName} {player.LastName}", totalPoints));
            }
            var leaderboard = players.OrderByDescending(p => p.score).Take(10).ToList();

            return await Task.FromResult(leaderboard);
        }
    }
}