using System.ComponentModel.DataAnnotations;

namespace LinkDev.Talabat.Core.Abstraction.Services.Auth.Models
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
