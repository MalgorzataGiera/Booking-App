using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ReservationApp.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
            );

            // Seed a test user
            var UserId = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = UserId,
                    UserName = "user@example.com",
                    NormalizedUserName = "USER@EXAMPLE.COM",
                    Email = "user@example.com",
                    NormalizedEmail = "USER@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "User@123")
                }
            );

            // Seed a test admin
            var adminId = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = adminId,
                    UserName = "admi",
                    NormalizedUserName = "ADMIN",
                    Email = "admin",
                    NormalizedEmail = "ADMIN",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Admin123")
                }
            );

            // Assign roles to users
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "1", UserId = adminId },
                new IdentityUserRole<string> { RoleId = "2", UserId = UserId }
            );

            // Save changes to the database
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 1,
                    Date = DateTime.Now,
                    City = "Sample City",
                    Address = "Sample Address",
                    Room = 101,
                    Owner = "Sample Owner",
                    Price = 100.00m
                }
            );
        }
    }
}