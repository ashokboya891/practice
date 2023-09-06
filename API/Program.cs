using System.Text;
using API.Data;
using API.Extensions;
using API.interfaces;
using API.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.


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
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200", "https://localhost:58005"));
// app.UseCors(builder=>builder.AllowAnyHeader().AllowAnyMethod()
// .AllowCredentials()
// .WithOrigins("https://localhost:4200"));


app.UseHttpsRedirection();



app.MapControllers();

app.Run();
