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
        [ValidateNever]
        public int RoomId { get; set; }
        [ValidateNever]
        public Room Room { get; set; }

        /// <summary>
        /// Gets or sets the number of people for the reservation.
        /// </summary>
        [Required(ErrorMessage = "You must enter the number of people.")]
        [Range(1,10, ErrorMessage = "Number of people for one reservation must be beetwen 1 and 10")]
        public int NumberOfPeople { get; set; }

        /// <summary>
        /// Gets or sets the owner of the reservation.
        /// </summary>
        [Display(Name = "Owner")]
        [Required(ErrorMessage = "You must enter an owner.")]
        public string Owner { get; set; }

        /// <summary>
        /// Gets or sets the price for the reservation.
        /// </summary>
        [ValidateNever]
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the number of nights for the reservation.
        /// </summary>
        [Display(Name = "Number of Nights")]
        [Required(ErrorMessage = "You must enter the number of nights.")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of nights must be set to a positive value")]
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
