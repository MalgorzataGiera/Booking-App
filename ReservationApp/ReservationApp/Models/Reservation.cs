using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required(ErrorMessage = "You must enter a date.")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the city for the reservation.
        /// </summary>
        [Display(Name = "City")]
        [Required(ErrorMessage = "You must enter a city.")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the address for the reservation.
        /// </summary>
        [Display(Name = "Address")]
        [Required(ErrorMessage = "You must enter an address.")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the room number for the reservation.
        /// </summary>
        [Display(Name = "Room")]
        [Required(ErrorMessage = "You must enter a room number.")]
        public int Room { get; set; }

        /// <summary>
        /// Gets or sets the owner of the reservation.
        /// </summary>
        [Display(Name = "Owner")]
        [Required(ErrorMessage = "You must enter an owner.")]
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets the price for the reservation.
        /// </summary>
        [Display(Name = "Price")]
        [Required(ErrorMessage = "You must enter a price.")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the number of nights for the reservation.
        /// </summary>
        [Display(Name = "Number of Nights")]
        [Required(ErrorMessage = "You must enter the number of nights.")]
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
