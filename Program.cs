using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Simplifly.Context;
using Simplifly.Interfaces;
using Simplifly.Models;
using Simplifly.Repositories;
using Simplifly.Services;
using System.Drawing;
using System.Reflection.Emit;
using System.Text;
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
            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            new string[]{}
                        }
                    });
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options =>
                  {
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuerSigningKey = true,
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"])),
                          ValidateIssuer = false,
                          ValidateAudience = false
                      };
                  });

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
            builder.Services.AddScoped<IRepository<int, PassengerBooking>, PassengerBookingRepository>();
            builder.Services.AddScoped<IRepository<int, Payment>,PaymentRepository>();
            builder.Services.AddScoped<IRepository<string, User>, UserRepository>();
            builder.Services.AddScoped<IRepository<int,Route>,RouteRepository>();
            builder.Services.AddScoped<IRepository<int, Schedule>, ScheduleRepository>();
            builder.Services.AddScoped<IRepository<string, SeatDetail>, SeatDetailRepository>();
            builder.Services.AddScoped<IPassengerBookingRepository,PassengerBookingRepository>();
            builder.Services.AddScoped<IBookingRepository ,BookingsRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<ISeatDeatilRepository, SeatDetailRepository>();



            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IFlightFlightOwnerService,FlightService>();
            builder.Services.AddScoped<IRouteFlightOwnerService, RouteService>();
            builder.Services.AddScoped<IScheduleFlightOwnerService,ScheduleServices>();
            builder.Services.AddScoped<IUserService, USerService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IFlightOwnerService, FlightOwnerService>();
            builder.Services.AddScoped<IBookingService, BookingService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
