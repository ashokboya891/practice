
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string userName{set;get;}
        [Required]
        [StringLength(8,MinimumLength =4)]
        public string password{set;get;}
        
    }
}