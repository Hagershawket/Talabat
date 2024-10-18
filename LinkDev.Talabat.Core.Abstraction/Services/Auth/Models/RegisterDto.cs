using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Abstraction.Services.Auth.Models
{
    public class RegisterDto
    {
        [Required]
        public required string DisplayName { get; set; }

        [Required]
        public required string UserName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Phone { get; set; }

        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&()_+}{\":;'?/<>.,])(?!.*\\s).*",
            ErrorMessage = "Password Must Have At Least 1 Uppercase, 1 Lowercase, 1 Number, 1 Non Alphnumeric And At Least 6 Characters.")]
        public required string Password { get; set; }

    }
}
