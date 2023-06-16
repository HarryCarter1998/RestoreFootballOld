using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model.Tree;
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

        void SignUp(int id);

        void CancelSignUp(int id);

        Task<IEnumerable<Player>> GetRemainingPlayers();

        Player GetPlayerFromGameweekPlayerId(int gameweekPlayerId);
    }
}
