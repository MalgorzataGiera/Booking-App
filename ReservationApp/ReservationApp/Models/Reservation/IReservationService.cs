namespace ReservationApp.Models
{
    /// <summary>
    /// Interface defining the contract for a reservation service.
    /// </summary>
    public interface IReservationService
    {
        /// <summary>
        /// Asynchronously creates a new reservation.
        /// </summary>
        /// <param name="reservation">The reservation to be created.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<Reservation> CreateAsync(Reservation reservation);

        /// <summary>
        /// Asynchronously updates an existing reservation.
        /// </summary>
        /// <param name="reservation">The reservation to be updated.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<Reservation> UpdateAsync(Reservation reservation);

        /// <summary>
        /// Asynchronously deletes a reservation.
        /// </summary>
        /// <param name="reservation">The reservation to be deleted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(Reservation reservation);

        /// <summary>
        /// Retrieves a list of all reservations.
        /// </summary>
        /// <returns>The list of reservations.</returns>
        List<Reservation> FindAll();

        /// <summary>
        /// Finds a reservation by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the reservation.</param>
        /// <returns>The found reservation, or null if not found.</returns>
        Reservation? FindById(int id);

        PagingList<Reservation> FindPage(int page, int size);

        List<Reservation> FindReservationsByUserId(string userId);
    }
}
