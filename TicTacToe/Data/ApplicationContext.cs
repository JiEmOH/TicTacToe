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
            modelBuilder.Entity<Game>()
                .Property(g => g.BoardState)
                .HasMaxLength(9) // максимум 9 символов для 3x3
                .HasColumnType("char(9)"); // жёсткая длина 9 символов в PostgreSQL
        }

    }
}
