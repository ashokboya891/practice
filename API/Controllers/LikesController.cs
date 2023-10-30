

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
        public ILikeRepository _likesRepository;
        public IUserRepository _userRepository { get; }

        public LikesController(IUserRepository userRepository,ILikeRepository likesRepository)
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
        public async Task<ActionResult<PagedList<LikeDto>>> GetUserLikes([FromQuery]LikeParams likeParams)
        {
            likeParams.UserId=User.GetUserId();
            var users=await _likesRepository.GetUserLikes(likeParams);
            Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage,users.PageSize,users.TotalCount,users.TotalPages));
            return Ok(users);

        }

    }
}

    //this filed removed after adding untiofwork and commentin saveall method 
//         private readonly IUserRepository _userRepository;
//         private readonly LikesRepository _likesRespository;
//         public LikesController(IUserRepository userRepository,LikesRepository likesRespository)
//          {
//              _userRepository = userRepository;
//             _likesRespository = likesRespository;
//         }
 

//         [HttpPost("{username}")]
//         public async Task<IActionResult> AddLike(string username)
//         {
          
//             var sourceUserId=int.Parse(User.GetUserId());
//             var likedUser=await _userRepository.GetUserByUsernameAsync(username);
//             var SourceUser=await _likesRespository.GetUserWithLike(sourceUserId);

//             if(likedUser==null)return NotFound();
//             if(SourceUser.UserName==username) return BadRequest("you cannot like yourself");
//             var userLike=await _likesRespository.GetUserLike(sourceUserId,likedUser.Id);
//             if(userLike!=null) return BadRequest("you already liked this user");
//             userLike=new UserLike{
//                 SourceUserId=sourceUserId,
//                 TargetUserId=likedUser.Id
//             };
//             SourceUser.LikedUsers.Add(userLike);
//             if(await _userRepository.SaveAllAsync())return Ok();
//             return BadRequest("failed to like user");

//         }

//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes(string predicate)
//         {
//             // var users=int.Parse(User.GetUserId());
//               var users=await _likesRespository.GetUserLikes(predicate,int.Parse(User.GetUserId()));
//             //   Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage,users.PageSize,users.TotalCount,users.TotalPages));
//             return Ok(users);

//         }
       
        
//     }
// }