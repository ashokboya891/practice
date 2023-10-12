using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }

        public string Token{set; get; }

        public string PhotoUrl{set;get;}

        public string KnownAs{set;get;}
        public string Gender{set;get;}
    }
}