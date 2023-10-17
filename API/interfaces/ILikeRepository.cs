

using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.interfaces
{
    public interface ILikeRepository
    {

         
        Task<UserLike> GetUserLike(int sourceUserId,int targetUserId);
        Task<AppUser> GetUserWithLike(int userId);
        Task<PagedList<LikeDto>> GetUserLikes(LikeParams likeParams);

        
    }
}