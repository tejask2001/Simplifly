using Microsoft.EntityFrameworkCore;
using Simplifly.Context;
using Simplifly.Interfaces;
using Simplifly.Models;
using Simplifly.Repositories;
using Simplifly.Services;
using System.Drawing;
using System.Reflection.Emit;
using Route = Simplifly.Models.Route;

namespace Simplifly
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
            builder.Services.AddDbContext<RequestTrackerContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("requestTrackerConnection"));
            });

                                    builder.Services.AddScoped<IRepository<int,Airport>,AirportRepository>();
            builder.Services.AddScoped<IRepository<int,Booking>,BookingsRepository>();
            builder.Services.AddScoped<IRepository<string,Flight>,FlightRepository>();
            builder.Services.AddScoped<IRepository<int,FlightOwner>,FlightOwnerRepository>();
            builder.Services.AddScoped<IRepository<int, Admin>, AdminRepository>();
            builder.Services.AddScoped<IRepository<int, Customer>, CustomerRepository>();
            builder.Services.AddScoped<IRepository<string, User>, UserRepository>();
            builder.Services.AddScoped<IRepository<int,Route>,RouteRepository>();
            builder.Services.AddScoped<IRepository<int, Schedule>, ScheduleRepository>();
            builder.Services.AddScoped<IRepository<string, SeatDetail>, SeatDetailRepository>();

            builder.Services.AddScoped<IUserService, USerService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
