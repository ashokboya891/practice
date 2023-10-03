

namespace API.DTOs
{
    public class MemberDto
    {
         public int Id { get; set; }
        public string UserName { get; set;}
        // public byte[] PassWordHash{set;get;}
        // public byte[] PassWordSalt{set;get;}
        public string PhotoUrl{set;get;}
        public int Age{set;get;}
        public string KnownAs{set;get;}
        public DateTime Created{set;get;}
        public DateTime LastActive{set;get;}
        public string Gender{set;get;}
        public string Introduction{set;get;}
        public string LookingFor{set;get;}
        public string Interests{set;get;}
        public string City{set;get;}
        public string Country{set;get;}
        public List<PhotoDto> Photos{set;get;}
          // public int Getage()
        // {
        //     return DateOfBirth.CalculateAge();
        // }
    }
}