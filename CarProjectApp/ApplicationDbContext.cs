using CarProjectApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarProjectApp
{
   public class ApplicationDbContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Emission> Emissions { get; set; }
        public DbSet<Extra> Extras { get; set; }
        public DbSet<CarExtra> CarExtras { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<VinNumber> VinNumbers { get; set; }

        
        public ApplicationDbContext() 
        {
            this.ChangeTracker.AutoDetectChangesEnabled = false;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public ApplicationDbContext(DbContextOptions option):
            base(option){
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            this.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-S5D5S14\\SQLEXPRESS; Initial Catalog=CarDB; Trusted_Connection = True;");
            }
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CarExtra>()
                .HasKey(k => new { k.CarId, k.ExtraId });

            modelBuilder.Entity<CarExtra>()
            .HasOne(cr => cr.Car)
            .WithMany(c => c.CarExtras)
            .HasForeignKey(f => f.CarId);

            modelBuilder.Entity<CarExtra>()
            .HasOne(cr => cr.Extra)
            .WithMany(c => c.CarExtras)
            .HasForeignKey(f => f.ExtraId);
        }
    }
}
