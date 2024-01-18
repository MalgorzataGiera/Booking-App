using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ReservationApp.Models
{
    /// <summary>
    /// Represents a reservation in the application.
    /// </summary>
    public class Reservation
    {
        /// <summary>
        /// Gets or sets the unique identifier for the reservation.
        /// </summary>
        [HiddenInput]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date of the reservation.
        /// </summary>
        [Display(Name = "Date")]
        [Required(ErrorMessage = "Musisz wspiać datę")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the city for the reservation.
        /// </summary>
        [Display(Name = "City")]
        [Required(ErrorMessage = "Musisz wpisać miasto")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the address for the reservation.
        /// </summary>
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Musisz podać adres")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the room number for the reservation.
        /// </summary>
        [Display(Name = "Room")]
        [Required(ErrorMessage = "Musisz podać numer pokoju")]
        public int Room { get; set; }

        /// <summary>
        /// Gets or sets the owner of the reservation.
        /// </summary>
        [Display(Name = "Owner")]
        [Required(ErrorMessage = "Musisz wpisać właściela")]
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets the price for the reservation.
        /// </summary>
        [Display(Name = "Price")]
        [Required(ErrorMessage = "Musisz wpisać cenę")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the number of nights for the reservation.
        /// </summary>
        [Display(Name = "NumberOfNights")]
        [Required(ErrorMessage = "Musisz podać liczbę nocy hotelowych")]
        public int NumberOfNights { get; set; }

        /// <summary>
        /// Gets or sets the user who received the reservation.
        /// </summary>
        [ValidateNever]
        public IdentityUser ReceivedBy { get; set; }

        /// <summary>
        /// Gets or sets the ID of the user who received the reservation.
        /// </summary>
        [ValidateNever]
        public string ReceivedById { get; set; }
    }
}
