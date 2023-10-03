using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class PhotoDto
    {
        public int Id{set;get;}
        public string Url{set;get;}
        public bool IsMain{set;get;}
    }
}