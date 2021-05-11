using Ludo_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ludo_API.Database
{
    public class LudoContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Gameboard> Gameboards { get; set; }
        public DbSet<Square> Squares { get; set; }

        public LudoContext(DbContextOptions<LudoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Creates a composite primary key constraint
            builder.Entity<Square>().HasKey(square => new
            {
                square.ID,
                square.GameboardId
            });
        }
    }
}
