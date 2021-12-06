using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class LoginDto
    {
        //Validations as Username and Password is required
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}