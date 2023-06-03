using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public DbSet<RestoreFootball.Models.Player> Player { get; set; } = default!;
    }
}
