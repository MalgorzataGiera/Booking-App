using Microsoft.AspNetCore.Mvc;

namespace ReservationApp.Models
{
    /// <summary>
    /// Represents a reservation service that uses an in-memory database.
    /// </summary>
    public class MemoryReservationService : IReservationService
    {
        private readonly AppDbContext _context;

        // <summary>
        /// Initializes a new instance of the <see cref="MemoryReservationService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public MemoryReservationService(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously creates a new reservation.
        /// </summary>
        /// <param name="reservation">The reservation to be created.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CreateAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously deletes a reservation.
        /// </summary>
        /// <param name="reservation">The reservation to be deleted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously updates an existing reservation.
        /// </summary>
        /// <param name="reservation">The reservation to be updated.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a list of all reservations.
        /// </summary>
        /// <returns>The list of reservations.</returns>
        public List<Reservation> FindAll()
        {
            return _context.Reservations.ToList();
        }

        /// <summary>
        /// Finds a reservation by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the reservation.</param>
        /// <returns>The found reservation, or null if not found.</returns>
        public Reservation? FindById(int id)
        {
            return _context.Reservations.Find(id);
        }


    }
}
