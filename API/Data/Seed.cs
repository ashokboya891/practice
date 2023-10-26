

using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public static class Seed
    {

        // this got cmntd after adding aspnet roles,claims,tablesto seed our data into those tables we need
         // public static async Task seedUsers(DataContext  context)
        // {
        //     context.connections.RemoveRange(context.connections);
        //     await context.SaveChangesAsync();
        // }
        public static async Task SeedUsers(UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager)
        {
            if(await userManager.Users.AnyAsync())return ;

            var userData=await File.ReadAllTextAsync("Data/UserDataSeed.json");
            var options=new JsonSerializerOptions{PropertyNameCaseInsensitive=true};
            var users=JsonSerializer.Deserialize<List<AppUser>>(userData);
            var roles=new List<AppRole>
            {
                new AppRole{Name="Member"},
                new AppRole{Name="Admin"},
                new AppRole{Name="Moderator"},


            };
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
                
            }
            foreach(var user in users)
            {
                
                user.UserName=user.UserName.ToLower();
                await userManager.CreateAsync(user,"Pa$$w0rd");
                await userManager.AddToRoleAsync(user,"Member");

                // await .Users.Add(user);
                // cmntd after adding identityuser added
                // using var hmac=new HMACSHA512();
                // user.PassWordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                // user.PassWordSalt=hmac.Key;
                
            }
            var admin=new AppUser
            {
                UserName="Admin"
                
            };
            await userManager.CreateAsync(admin,"Pa$$w0rd");
            await userManager.AddToRolesAsync(admin,new[]{"Admin","Moderator"});
            // await context.SaveChangesAsync();

        }
        
    }
}