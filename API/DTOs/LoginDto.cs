using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    // needed for login
    public class LoginDto
    {
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
    }
}