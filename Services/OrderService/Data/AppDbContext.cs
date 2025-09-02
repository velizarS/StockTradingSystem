﻿using Microsoft.EntityFrameworkCore;
using OrderService.Models;
using System.Collections.Generic;

namespace OrderService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
    }
}
