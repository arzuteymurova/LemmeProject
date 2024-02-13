using LemmeProject.API.Middlewares;
using LemmeProject.Application;
using LemmeProject.Application.Helpers;
using LemmeProject.Application.Identity.Concrete;
using LemmeProject.Domain.Entities.Identity;
using LemmeProject.Infrastructure;
using LemmeProject.Persistence;
using LemmeProject.Persistence.AppDbContext;
using Microsoft.AspNetCore.Identity;

namespace LemmeProject.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<JWTOptions>(builder.Configuration.GetSection("JWTOptions"));
            JWTOptions jwtSettings = builder.Configuration.GetSection("JWTOptions").Get<JWTOptions>();

            builder.Services.Configure<FileServerPath>(builder.Configuration.GetSection("FileServerPath"));
            FileServerPath filePath = builder.Configuration.GetSection("FileServerPath").Get<FileServerPath>();

            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddApplication(builder.Configuration);
            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddSwaggerSetting();
            builder.Services.AuthenticationJwtSettings(jwtSettings);

            

            builder.Services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 5; 
                options.Password.RequireNonAlphanumeric = false; 
                options.Password.RequireLowercase = false; 
                options.Password.RequireUppercase = false;
                                                          
                options.Password.RequireDigit = false; 
                options.Lockout.MaxFailedAccessAttempts = 5; 
            }).AddEntityFrameworkStores<LemmeAppContext>().AddDefaultTokenProviders();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();

            app.UseAuthorization();

         //   app.UseMiddleware<ErrorHandlerMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
