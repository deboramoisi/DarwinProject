using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required] public string Name { get; set; }
        
        [Required] 
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [StringLength(8, MinimumLength = 4)]
        
        public string Password { get; set; }
        
        [Required] public string Location { get; set; }
    }
}