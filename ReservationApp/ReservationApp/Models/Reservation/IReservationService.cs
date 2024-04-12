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

        /// <summary>
        /// Retrieves a paged list of reservations.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="size">The number of items per page.</param>
        /// <returns>A paged list of reservations.</returns>
        PagingList<Reservation> FindPage(int page, int size);

        /// <summary>
        /// Retrieves reservations for a specified user.
        /// </summary>
        /// <param name="userId">The user's identifier.</param>
        /// <returns>A list of reservations for the specified user.</returns>
        List<Reservation> FindReservationsByUserId(string userId);
    }
}
