
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string userName{set;get;}
        [Required]
        public string password{set;get;}
        
    }
}