
using BattleRoyale.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BattleRoyale.Data
{
    public class BattleRoyaleDbContext : IdentityDbContext
    {
        public BattleRoyaleDbContext(DbContextOptions<BattleRoyaleDbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Shop> Shops { get; set; }
    }
}
