using Domin.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options) {
            _configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User-Reservation relationship
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.ReservedBy)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.ReservedById);

            // Configure Trip-Reservation relationship
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Trip)
                .WithMany(t => t.Reservations)
                .HasForeignKey(r => r.TripId);

            modelBuilder.Entity<Trip>()
                .Property(p => p.Price).HasColumnName("decimal(18,2)");
            // Seed users and trips

            var users = _configuration.GetSection("SeedData:Users").Get<List<User>>();
            var trips = _configuration.GetSection("SeedData:Trips").Get<List<Trip>>();

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Trip>().HasData(trips);

            //     modelBuilder.Entity<User>().HasData(
            //    new User { Id = 1, Email = "user1@example.com", Password = "password1" },
            //    new User { Id = 2, Email = "user2@example.com", Password = "password2" }
            //);

            //     modelBuilder.Entity<Trip>().HasData(
            //         new Trip { Id = 1, Name = "Trip to Paris", CityName = "Paris", Price = 1000, ImageUrl = "image_url", Content = "<p>Beautiful trip to Paris</p>", CreationDate = DateTime.UtcNow },
            //         new Trip { Id = 2, Name = "Trip to Rome", CityName = "Rome", Price = 1200, ImageUrl = "image_url", Content = "<p>Amazing trip to Rome</p>", CreationDate = DateTime.UtcNow }
            //     );
        }
        }
}
