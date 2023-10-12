
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string userName{set;get;}
        [Required]
      public string KnownAs{set;get;}
      [Required]
      public string Gender{set;get;}
      [Required]
      public DateOnly? DateOfBirth{set;get;}  //optional to make required work
      [Required]
      public string City{set;get;}

      [Required]
      public string Country{set;get;}
      [Required]
      [StringLength(8,MinimumLength =4)]
       public string password{set;get;}
        
    }
}