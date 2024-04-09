using Microsoft.AspNetCore.Mvc;

namespace ReservationApp.Models
{
    /// <summary>
    /// Represents a reservation service that uses an in-memory database.
    /// </summary>
    public class MemoryReservationService : IReservationService
    {
        private readonly AppDbContext _context;
        private readonly IRoomService _rooms;

        // <summary>
        /// Initializes a new instance of the <see cref="MemoryReservationService"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public MemoryReservationService(AppDbContext context, IRoomService rooms)
        {
            _context = context;
            _rooms = rooms;
        }

        /// <summary>
        /// Asynchronously creates a new reservation.
        /// </summary>
        /// <param name="reservation">The reservation to be created.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task<Reservation> CreateAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            var room = _rooms.FindRoomById(reservation.RoomId);
            room.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
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
        public async Task<Reservation> UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
            return reservation;
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

        /// <summary>
        /// Finds a page of rooms.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="size">The page size.</param>
        /// <returns>A paging list of rooms.</returns>
        public PagingList<Reservation> FindPage(int page, int size)
        {
            return PagingList<Reservation>.Create(
                    (p, s) => _context.Reservations
                            .OrderBy(b => b.Date)
                            .Skip((p - 1) * size)
                            .Take(s)
                            .ToList(),
                    _context.Reservations.Count(),
                    page,
                    size);
        }

    }
}
