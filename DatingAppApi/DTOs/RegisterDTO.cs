using System;
using System.ComponentModel.DataAnnotations;

namespace DatingAppApi.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength = 4)]
        public string Password { get; set; }

        [Required]
        public string KnownAs { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }
    }
}