using System.ComponentModel.DataAnnotations;

namespace ReservationApp.Models
{
    /// <summary>
    /// Represents a view model for the registration functionality in the application.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Gets or sets the email address used for registration.
        /// </summary>
        [Required]
        [EmailAddress] 
        public string Email { get; set;}

        /// <summary>
        /// Gets or sets the password used for registration.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set;}

        /// <summary>
        /// Gets or sets the confirmation password used for registration.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set;}
    }
}
