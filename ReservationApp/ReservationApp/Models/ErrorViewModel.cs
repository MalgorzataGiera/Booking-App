namespace ReservationApp.Models
{
    /// <summary>
    /// Represents a model for handling and displaying errors in the application.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Gets or sets the unique identifier for the request associated with the error.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Gets a value indicating whether to show the request identifier in the error view.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}