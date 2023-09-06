

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
using Microsoft.IdentityModel.Tokens;

namespace API.services
{
    public class TokenService : ITokenServices
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        }
        public string CreateToken(AppUser user)
        {
            var claims=new List<Claim>
           {
            new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName)
           };
            var creds=new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);

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

   
    }
}