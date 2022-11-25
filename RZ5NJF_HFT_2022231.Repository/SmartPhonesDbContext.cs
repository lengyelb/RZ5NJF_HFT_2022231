using Microsoft.EntityFrameworkCore;
using System;
using RZ5NJF_HFT_2022231.Models;

namespace RZ5NJF_HFT_2022231.Repository
{
    public class SmartPhonesDbContext : DbContext
    {
        protected SmartPhonesDbContext()
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<System.OperatingSystem> OperatingSystems { get; set; }
        public DbSet<Phone> Phones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("smartphones");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
