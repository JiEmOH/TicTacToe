using Microsoft.EntityFrameworkCore;
using TicTacToe.Models;

namespace TicTacToe.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Пример настройки правил для PostgreSQL
            modelBuilder.Entity<Game>()
                .Property(g => g.BoardState)
                .HasMaxLength(9)
                .IsFixedLength(); // Для поля "_________" (3x3)
        }
    }
}
