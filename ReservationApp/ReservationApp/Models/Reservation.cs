using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReservationApp.Models
{
    public class Reservation
    {

        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Musisz wspiać datę")]
        public DateTime Date { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Musisz wpisać miasto")]
        public string City { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Musisz podać adres")]
        public string Address { get; set; }

        [Display(Name = "Room")]
        [Required(ErrorMessage = "Musisz podać numer pokoju")]
        public int Room { get; set; }

        [Display(Name = "Owner")]
        [Required(ErrorMessage = "Musisz wpisać właściela")]
        public string Owner { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Musisz wpisać cenę")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }
    }
}
