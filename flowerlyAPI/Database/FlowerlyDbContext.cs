using System;
using Domain;
using Microsoft.EntityFrameworkCore;


namespace Database
{
    public class FlowerlyDbContext: DbContext
    {
        public FlowerlyDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<MyFlowers> MyFlowers { get; set; }
        public DbSet<Flower> Flowers { get; set; }
        public DbSet<IrrigationDates> IrrigationDates { get; set; }
        public DbSet<IrrigationHistory> IrrigationHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyFlowers>()
                .HasOne(p => p.User)
                .WithMany(b => b.Flowers);

            modelBuilder.Entity<MyFlowers>()
               .HasOne(p => p.Flower);            

        }
    }
}
