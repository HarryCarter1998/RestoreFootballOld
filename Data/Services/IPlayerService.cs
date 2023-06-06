using Microsoft.AspNetCore.Mvc;
using RestoreFootball.Models;

namespace RestoreFootball.Data.Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<Player>> Index();

        Task<Player> Details(int id);

        Task Create(Player player);

        Task<Player?> Edit(int id);

        Task Edit(Player player);

        Task<Player?> Delete(int id);

        Task DeleteConfirmed(int id);

        bool PlayerExists(int id);

        void AddExistingPlayer(Player player);

        void AddNewPlayer(Player player);

        void UpdateSignedUp(int id, bool signUp);

        Task<IEnumerable<Player>> GetRemainingPlayers();
    }
}
