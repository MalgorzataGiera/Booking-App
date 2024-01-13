﻿using System.ComponentModel.DataAnnotations;

namespace ReservationApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress] 
        public string Email { get; set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set;}

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Hasła nie są takie same!")]
        public string ConfirmPassword { get; set;}
    }
}
