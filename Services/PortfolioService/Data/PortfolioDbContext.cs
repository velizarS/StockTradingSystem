using Microsoft.EntityFrameworkCore;
using PortfolioService.Models;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using System.Reflection.Emit;
>>>>>>> 16e64bac1335152d90dae2ae354a9ba993cfd07d

namespace PortfolioService.Data
{
    public class PortfolioDbContext : DbContext
    {
<<<<<<< HEAD
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
        : base(options) { }

        public DbSet<Portfolio> Portfolios => Set<Portfolio>();
=======
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options) { }
        public DbSet<Portfolio> Portfolios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Portfolio>()
                .HasKey(p => p.Id);

            base.OnModelCreating(modelBuilder);
        }
>>>>>>> 16e64bac1335152d90dae2ae354a9ba993cfd07d
    }
}
