
using API.Data;
using API.Entities;
using API.Extensions;
using API.interfaces;
using API.Middleware;
using API.services;
using API.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ITokenServices,TokenService>();

builder.Services.AddControllers();
builder.Services.AddapplicationServices(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
//   builder.Services.AddCors(options =>
//     {
//         options.AddPolicy("AllowSpecificOrigin", builder =>
//         {
//             builder.WithOrigins("https://localhost:4200") // Replace with the origin(s) you want to allow
//                    .AllowAnyHeader()
//                    .AllowAnyMethod();
//         });
//     });
var app = builder.Build();
app.UseMiddleware<ExceptionMiddleWare>();
// if(builder.Environment.IsDevelopment())
// {
//     app.UseDeveloperExceptionPage();
// }

app.UseCors(builder=>builder
.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials()  
.WithOrigins("https://localhost:4200"));  // allow credentials added this line added after adding presence hub,applicserex and identitserex ,programcs
app.UseAuthentication();
app.UseAuthorization();



app.UseHttpsRedirection();



app.MapControllers();
app.MapHub<PresenceHub>("hubs/presence");
using var scope=app.Services.CreateScope();
var services=scope.ServiceProvider;
try{
    var context = services.GetRequiredService<DataContext>();
    //    this below lines added after adding apsnet table and seeding our data into those tables we modifed seed class with userMnager 
    //we added userman and roles mamnger after addiing roles in seed class so that seed line got roleman,userman
    var userManager=services.GetRequiredService<UserManager<AppUser>>();
    var roleManager=services.GetRequiredService<RoleManager<AppRole>>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(userManager,roleManager);
}
catch(Exception ex)
{
    var logger=services.GetService<ILogger<Program>>();
    logger.LogError(ex,"An Error occured druing migration");
}
app.Run();
