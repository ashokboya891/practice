

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
    }
}