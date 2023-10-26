

using API.Entities;
// using API.Entity;

namespace API.interfaces
{
    public interface ITokenServices
    {
        Task<string> CreateToken(AppUser user);
        
    }
}