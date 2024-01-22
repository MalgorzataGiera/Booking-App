using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationApp.Models
{
	/// <summary>
	/// Represents a room in the application.
	/// </summary>
	public class Room
	{
		/// <summary>
		/// Gets or sets the unique identifier for the room.
		/// </summary>
		[HiddenInput]
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the room number.
		/// </summary>
		[Display(Name = "RoomNumber")]
		[Required(ErrorMessage = "You must provide the room number")]
		public int RoomNumber { get; set; }

		/// <summary>
		/// Gets or sets the price for the room.
		/// </summary>
		[Display(Name = "Price")]
		[Required(ErrorMessage = "You must provide the room price")]
		[Column(TypeName = "decimal(8, 2)")]
		public decimal Price { get; set; }

		/// <summary>
		/// Gets or sets the reservations associated with the room.
		/// </summary>
		public List<Reservation> Reservations { get; set; }
	}
}
