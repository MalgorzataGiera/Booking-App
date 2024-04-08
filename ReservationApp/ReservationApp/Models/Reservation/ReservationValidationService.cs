using Microsoft.EntityFrameworkCore;

namespace ReservationApp.Models
{
    /// <summary>
    /// Service for validating reservations.
    /// </summary>
    public class ReservationValidationService
    {
        private readonly AppDbContext _context;

        public ReservationValidationService(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Validates whether a reservation date is valid.
        /// </summary>
        /// <param name="reservation">The reservation to validate.</param>
        /// <returns>True if the reservation is valid; otherwise, false.</returns>
        public async Task<bool> IsReservationDateValid(Reservation reservation)
        {
            // Fetch existing reservations for the selected room
            var existingReservations = await _context.Reservations
                .Where(r => r.RoomId == reservation.RoomId)
                .ToListAsync();

            // Calculate end date for the new reservation
            var newResEndDate = reservation.Date.AddDays(reservation.NumberOfNights).Date;

            // Check if the reservation date is in the past
            if (reservation.Date.Date < DateTime.Now.Date)
            {
                return false;
            }

            // Check for conflicts with existing reservations
            foreach (var existingReservation in existingReservations)
            {
                var existingResEndDate = existingReservation.Date.AddDays(existingReservation.NumberOfNights).Date;

                if ((reservation.Date.Date >= existingReservation.Date.Date && reservation.Date.Date <= existingResEndDate)
                    || (newResEndDate >= existingReservation.Date.Date && newResEndDate <= existingResEndDate))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
