

using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]
    public class AccountController:BaseapiController
    {
         private readonly DataContext _context ;
        private   readonly ITokenServices _TokenServices;
        private readonly IMapper _mapper ;
        public AccountController(DataContext context,ITokenServices tokenServices,IMapper mapper)
        {
            _mapper = mapper;
            _TokenServices = tokenServices;
            _context = context;

        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await UserExist(registerDto.userName)) return  BadRequest("try different name its already existed");

             var user=_mapper.Map<AppUser>(registerDto);   
            using var  hmac=new HMACSHA512();
                user.UserName=registerDto.userName;
                user.PassWordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password));
                user.PassWordSalt=hmac.Key;
                // var user=new AppUser
                // { after adding reactive froms and regist
                //     UserName=registerDto.userName,
                //     PassWordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
                    
                //     PassWordSalt=hmac.Key

                // };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return new UserDto
                {
                    Username=user.UserName,
                    Token=_TokenServices.CreateToken(user),
                    KnownAs=user.KnownAs,
                    Gender=user.Gender

                };
        }
        public async Task<bool> UserExist(string  username)
        {
            return await _context.Users.AnyAsync(x=>x.UserName==username.ToLower());
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(loginDto login)
        {
            var user= await  _context.Users.Include(p=>p.Photos).SingleOrDefaultAsync(x => x.UserName==login.UserName);

            if(user==null) return Unauthorized("invalid username");

            using  var hmac=new HMACSHA512(user.PassWordSalt);
            var computedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if(computedHash[i]!=user.PassWordHash[i])
                return Unauthorized("wrong password");
                
            }
            return new UserDto
            {
                Username=user.UserName,
                Token=_TokenServices.CreateToken(user),
                PhotoUrl=user.Photos.FirstOrDefault(x=>x.IsMain)?.Url,
                KnownAs = user.KnownAs,
                Gender=user.Gender
            };
            
        }
        
    }
}