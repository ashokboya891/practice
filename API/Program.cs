
using API.Data;
using API.Extensions;
using API.interfaces;
using API.Middleware;
using API.services;
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
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(builder=>builder.AllowAnyHeader().AllowAnyMethod()
.AllowCredentials()
.WithOrigins("https://localhost:4200"));



app.UseHttpsRedirection();



app.MapControllers();
using var scope=app.Services.CreateScope();
var services=scope.ServiceProvider;
try{
       var context = services.GetRequiredService<DataContext>();
        await context.Database.MigrateAsync();
        await Seed.SeedUsers(context);
    // var userManager=services.GetRequiredService<UserManager<AppUser>>();
    // var roleManager=services.GetRequiredService<RoleManager<AppRole>>();

   
    // await Seed.ClearConnections(context);
    // await context.Database.ExecuteSqlRawAsync("DELETE FROM [Connections]");  //after adding migration  problem will raise so added new method in seed.cs so it got commented
    // context.connections.RemoveRange(context.connections);
    // await Seed.SeedUsers(userManager,roleManager);
}
catch(Exception ex)
{
    var logger=services.GetService<ILogger<Program>>();
    logger.LogError(ex,"An Error occured druing migration");
}
app.Run();
