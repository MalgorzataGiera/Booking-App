using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationApp.Models
{
    public class Room
    {
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "RoomNumber")]
        [Required(ErrorMessage = "You must provide the room numer")]
        public int RoomNumber { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "You must provide the room price")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

    }
}
