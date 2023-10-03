using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id{set;get;}
        public string Url{set;get;}
        public bool IsMain{set;get;}
        public string  PublicId { get; set; }   
      
        public  int AppUserId{get;set;}

        public AppUser AppUser{set;get;}
        
    }
}