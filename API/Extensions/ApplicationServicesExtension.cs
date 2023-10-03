using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.interfaces;
using API.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddapplicationServices(this IServiceCollection services,IConfiguration configuration) 
        {

            services.AddScoped<ITokenServices,TokenService>();
            services.AddCors();
            services.AddDbContext<DataContext>(opt=>{
            opt.UseSqlite(configuration.GetConnectionString("Defaultconnection"));
            });
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
        }
        
    }
}