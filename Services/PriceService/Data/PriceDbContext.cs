using Microsoft.EntityFrameworkCore;
using PriceService.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PriceService.Data
{
    public class PriceDbContext : DbContext
    {
        public PriceDbContext(DbContextOptions<PriceDbContext> options)
        : base(options) { }
        public DbSet<Price> Prices { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Price>().HasKey(p => p.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
