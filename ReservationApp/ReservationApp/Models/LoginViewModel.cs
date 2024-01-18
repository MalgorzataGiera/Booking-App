using System.ComponentModel.DataAnnotations;

namespace ReservationApp.Models
{
    /// <summary>
    /// Represents a view model for the login functionality in the application.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the email address used for login.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password used for login.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to remember the user's login state.
        /// </summary>
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; } = false;
    }
}
