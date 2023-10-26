using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        // this 4 comtd after adding identityuser cause it consist of those properties
        // public int Id { get; set; }
        // public string UserName { get; set;}
        // public byte[] PasswordHash{set;get;}
        // public byte[] PassWordSalt{set;get;}
        public DateOnly DateOfBirth{set;get;}
        public string KnownAs{set;get;}
        public DateTime Created{set;get;}=DateTime.UtcNow;
        public DateTime LastActive{set;get;}=DateTime.UtcNow;
        public string Gender{set;get;}
        public string Introduction{set;get;}
        public string LookingFor{set;get;}
        public string Interests{set;get;}
        public string City{set;get;}
        public string Country{set;get;}
        public List<Photo> Photos{set;get;}=new();

        public List<UserLike> LikedByUsers{set;get;} 

        public List<UserLike> LikedUsers{set;get;}
        public List<Message> MessageSent{set;get;}
        public List<Message> MessageReceived{set;get;}
        public ICollection<AppUserRole> UserRoles{set;get;}

    
        // public int GetAge()
        // {
        //     return DateOfBirth.CalculateAge();
        // }

        
    }
}