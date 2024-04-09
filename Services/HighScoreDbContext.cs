using cadflash.Models;
using Microsoft.EntityFrameworkCore;

namespace cadflash.Services
{
    public class HighScoreDbContext : DbContext
    {
        public DbSet<HighScoreStreak> HighScoreStreaks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=highscore.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HighScoreStreak>().HasData(new HighScoreStreak { Id = 1, HighestStreak = 0 });
        }
    }
}
