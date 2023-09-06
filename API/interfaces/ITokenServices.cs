

using API.Entities;
// using API.Entity;

namespace API.interfaces
{
    public interface ITokenServices
    {
         string CreateToken(AppUser user);
        
    }
}