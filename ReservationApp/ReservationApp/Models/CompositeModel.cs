namespace ReservationApp.Models
{
    /// <summary>
    /// Represents a composite model containing information about a reservation and the associated user name.
    /// </summary>
    public class CompositeModel
    {
        /// <summary>
        /// Gets or sets the reservation information.
        /// </summary>
        public Reservation? reservation { get; set; }

        /// <summary>
        /// Gets or sets the user name associated with the reservation.
        /// </summary>
        public string? userName { get; set; }
    }
}
