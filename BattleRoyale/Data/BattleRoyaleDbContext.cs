
using BattleRoyale.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace BattleRoyale.Data
{
    public class BattleRoyaleDbContext : IdentityDbContext<User>
    {
        public BattleRoyaleDbContext(DbContextOptions<BattleRoyaleDbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<AuctionItem> AuctionItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Player>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Player>(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Item>()
                .HasOne(i => i.Hero)
                .WithMany(h => h.Items)
                .HasForeignKey(i => i.HeroId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
