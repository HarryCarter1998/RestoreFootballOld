using RestoreFootball.Models;

namespace RestoreFootball.Data.Services
{
    public interface IGameweekService
    {
        public Task CreateNextGameweek();

        public Task Edit(Gameweek gameweek);

        public Task<Gameweek> GetLatestGameweek();

        public ICollection<GameweekPlayer> GetLatestGameweekPlayers();

        public Task<IEnumerable<GameweekPlayer>> RecalculateTeams();

        public ICollection<GameweekPlayer> GetUngroupedGameweekPlayers(int id);
        
        public ICollection<GameweekPlayer> GetGameweekPlayers(int gameweekId);
    }
}
