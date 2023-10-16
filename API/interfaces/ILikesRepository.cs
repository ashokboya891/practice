

using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.interfaces
{
    public interface ILikesRepository
    {
        
        Task<UserLike> GetUserLike(int sourceUserId,int targetUserId);

        Task<AppUser> GetUserWithLike(int userId);

        // Task<IEnumerable<LikeDto>> GetUserLikes(string predicate,int userId);

        
        Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
    }
}