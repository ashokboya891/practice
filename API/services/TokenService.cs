

// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using API.Entity;
// using API.interfaces;
// using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.services
{
    public class TokenService : ITokenServices
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;
        public TokenService(IConfiguration configuration,UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        }

        public async Task<string> CreateToken(AppUser user)
        {
            
            var claims=new List<Claim>
           {
            //after adding last active unique added
            new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName)
            // new Claim(JwtRegisteredClaimNames)
           };
            var roles= await _userManager.GetRolesAsync(user);
           claims.AddRange(roles.Select(role=>new Claim(ClaimTypes.Role,role)));
           
            var creds=new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
            // below two lines added after adding roles.approles,appuserrole,approle 210 we added roles into jwt token

            // this line meaning is of user is having one role it will store if had more roles it stores as array of roles

            var tokenDescriptor=new SecurityTokenDescriptor
           {
                Subject =new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(7),
                SigningCredentials=creds
           };
           var tokenhandler=new JwtSecurityTokenHandler();
           var token = tokenhandler.CreateToken(tokenDescriptor);
           return tokenhandler.WriteToken(token);
        }


        // public  async Task<string> CreateTokenAsync(AppUser user)
        // {
        //     var claims=new List<Claim>
        //    {
        //     //after adding last active unique added
        //     new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
        //     new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName)
        //     // new Claim(JwtRegisteredClaimNames)
        //    };
        //     var creds=new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
        //     // below two lines added after adding roles.approles,appuserrole,approle 210 we added roles into jwt token

        //     var roles= await _userManager.GetRolesAsync(user);
        //     // this line meaning is of user is having one role it will store if had more roles it stores as array of roles
        //    claims.AddRange(roles.Select(role=>new Claim(ClaimTypes.Role,role)));

        //     var tokenDescriptor=new SecurityTokenDescriptor
        //    {
        //         Subject =new ClaimsIdentity(claims),
        //         Expires=DateTime.Now.AddDays(7),
        //         SigningCredentials=creds
        //    };
        //    var tokenhandler=new JwtSecurityTokenHandler();
        //    var token = tokenhandler.CreateToken(tokenDescriptor);
        //    return tokenhandler.WriteToken(token);
        // }

        // Task<string> ITokenServices.CreateToken(AppUser user)
        // {
        //     throw new NotImplementedException();
        // }

    }
}