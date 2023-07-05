using Microsoft.EntityFrameworkCore;
using RestoreFootball.Models;

namespace RestoreFootball.Data
{
    public class RestoreFootballContext : DbContext
    {
        public RestoreFootballContext (DbContextOptions<RestoreFootballContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Player { get; set; } = default!;

        public DbSet<Gameweek> Gameweek { get; set; } = default!;

        public DbSet<GameweekPlayer> GameweekPlayer { get; set; } = default!;

        public DbSet<Grouping> Grouping { get; set; } = default!;
    }
}
