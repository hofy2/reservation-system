using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domin.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define relationships for Reservation
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.ReservedBy)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.ReservedById);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Trip)
                .WithMany(t => t.Reservations)
                .HasForeignKey(r => r.TripId);

            modelBuilder.Entity<Trip>()
               .Property(p => p.Price).HasColumnName("decimal(18,2)");


            // Seed roles and users
            var trips = _configuration.GetSection("SeedData:Trips").Get<List<Trip>>();
            modelBuilder.Entity<Trip>().HasData(trips);

            modelBuilder.Entity<IdentityRole<int>>().HasData(
                new IdentityRole<int> { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole<int> { Id = 2, Name = "Customer", NormalizedName = "CUSTOMER" }
            );

            var adminUser = new User
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "Admin@123")
            };

            var customerUser = new User
            {
                Id = 2,
                UserName = "customer",
                NormalizedUserName = "CUSTOMER",
                Email = "customer@example.com",
                NormalizedEmail = "CUSTOMER@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "Customer@123")
            };

            modelBuilder.Entity<User>().HasData(adminUser, customerUser);

            // Assign roles
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int> { RoleId = 1, UserId = 1 }, // Admin role to Admin user
                new IdentityUserRole<int> { RoleId = 2, UserId = 2 }  // Customer role to Customer user
            );
        }
    }
}
