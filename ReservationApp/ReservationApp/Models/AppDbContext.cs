using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ReservationApp.Models
{
    /// <summary>
    /// Represents the database context for the ReservationApp application, extending IdentityDbContext for user authentication.
    /// </summary>
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet for reservations in the database.
        /// </summary>
        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Room> Room { get; set; }

        /// <summary>
        /// Configures the database context during model creation.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to define the database model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Seed();
        }
    }
}
