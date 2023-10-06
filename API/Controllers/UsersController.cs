

using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController:ControllerBase
    {
        // AFTER ADDING REPOSITORY PATTERN AM GOING TO USER iUSERREPOSITORY WHICH IS IMPLEMTED BY USERREPOSITORY IN DATA,INTERFACES
        // private readonly DataContext _context;
        // public UsersController(DataContext context)
        // {
        //     _context = context;

        // }
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository,IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;

        }
        [AllowAnonymous]
        [HttpGet]
        public  async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            // after adding iuserrepo and userrepo 
            // var users= await _context.Users.ToListAsync();
            // var user= await _userRepository.GetUsersAsync();
            // return users;
            var users= await  _userRepository.GetMembersAsync();
            // var userToreturn=_mapper.Map<IEnumerable<MemberDto>>(users);
            return Ok(users);
       
        }
        // [Authorize]
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);
            // return _mapper.Map<MemberDto>(user);
            // return await sqlite3_context.Users.findallasync(id);
           
        }
        [HttpPut]
         public async Task<ActionResult>UpdateUser(MemberUpdateDto memberUpdateDto)
        {
                // var username=User.FindFirst(ClaimTypes.NameIdentifier)?.Value;  --this line removed and added inside cliamsprincipal extension
                // var user=await _uow.userRepository.GetUserByUserNameAsync(User.GetUsername());
                // if(user==null)return NotFound();
                // _Mapper.Map(memberUpdateDto,user);
                // if(await _uow.Complete())return NoContent();
                // return BadRequest("Failed to update user");

                var username=User.FindFirst(ClaimTypes.Name)?.Value;
                var user=await _userRepository.GetUserByUsernameAsync(username);
                if(user==null)return NotFound();
                _mapper.Map(memberUpdateDto,user);
                if(await _userRepository.SaveAllAsync()) return NoContent ();
                return BadRequest("failed to update user");
        }
    }
}