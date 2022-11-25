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
        public DbSet<SmartPhoneOS> SmartPhoneOSes { get; set; }
        public DbSet<Phone> Phones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("smartphones");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Phone>(phone => phone
            .HasOne<Company>(phone => phone.Company)
            .WithMany(company => company.Phones)
            .HasForeignKey(phone => phone.CompanyID)
            .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Phone>(phone => phone
            .HasOne<SmartPhoneOS>(phone => phone.SmartPhoneOS)
            .WithMany(os => os.Phones)
            .HasForeignKey(phone => phone.SmartPhoneOSID)
            .OnDelete(DeleteBehavior.Cascade));
        }
    }
}
