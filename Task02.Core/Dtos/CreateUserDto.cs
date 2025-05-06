using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02.Core.Dtos
{
    public class CreateUserDto
    {
        [Required]
        public string? Name { get; set; }
        public string? UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Please Enter A Valid Email !!")]
        public string? Email { get; set; }
        public int? Age { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password don't match with ConfirmPassword")]
        public string? ConfirmPassword { get; set; }
    }
}
