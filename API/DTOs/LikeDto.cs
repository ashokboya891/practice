using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class LikeDto
    {
        public int Id{set;get;}
        public string UserName{set;get;}
        public int Age{set;get;}
        public string KnownAs{set;get;}
        public string PhotoUrl{set;get;}
        public string City{set;get;}
    }
}