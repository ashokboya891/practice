
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LikesController:BaseapiController
    {
        public ILikesRepository _likesRepository;
        public IUserRepository _userRepository { get; }

        public LikesController(IUserRepository userRepository,ILikesRepository likesRepository)
        {
            _userRepository = userRepository;
            _likesRepository = likesRepository;      
        }
         [HttpPost("{username}")]
        public async Task<IActionResult> AddLike(string username)
        {
          
            var sourceUserId=User.GetUserId();
            var likedUser=await _userRepository.GetUserByUsernameAsync(username);
            var SourceUser=await _likesRepository.GetUserWithLike(sourceUserId);

            if(likedUser==null)return NotFound();

            if(SourceUser.UserName==username) return BadRequest("you cannot like yourself");
            
            var userLike=await _likesRepository.GetUserLike(sourceUserId,likedUser.Id);

            if(userLike!=null) return BadRequest("you already liked this user");
            
            userLike=new UserLike{
                SourceUserId=sourceUserId,
                TargetUserId=likedUser.Id
            };
            SourceUser.LikedUsers.Add(userLike);

            if(await _userRepository.SaveAllAsync())return Ok();

            return BadRequest("failed to like user");

        }
        [HttpGet]
        public async Task<ActionResult<PagedList<LikeDto>>> GetUserLikes([FromQuery]LikesParams likesParams)
        {
            likesParams.UserId=User.GetUserId();
              var users=await _likesRepository.GetUserLikes(likesParams);
              Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage,users.PageSize,users.TotalCount,users.TotalPages));
            return Ok(users);

        }

        
    }
}