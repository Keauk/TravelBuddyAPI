
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TravelBuddyAPI.Data;
using TravelBuddyAPI.Services.Implementations;
using TravelBuddyAPI.Services.Interfaces;

namespace TravelBuddyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<TravelBuddyContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            // Register services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITripLogService, TripLogService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<ITripLogService, TripLogService>();
            builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
