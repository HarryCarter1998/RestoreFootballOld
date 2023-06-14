using RestoreFootball.Models;

namespace RestoreFootball.Data.Services
{
    public interface IGameweekService
    {
        public Task CreateNextGameweek();

        public Task Edit(Gameweek gameweek);

        public Task<Gameweek> GetLatestGameweek();

        public ICollection<GameweekPlayer> GetGameweekPlayers();

        public Task<IEnumerable<GameweekPlayer>> RecalculateTeams();
    }
}
