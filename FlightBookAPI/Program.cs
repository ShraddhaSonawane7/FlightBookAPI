using FlightBookAPI.Interfaces;
using FlightBookAPI.Repositories;
using FlightBookAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using JWTAuthentication.Authentication;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure Entity Framework
builder.Services.AddDbContext<FlightDemoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlightBookingDbConnection")));

// Configure Identity (if using Identity)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<FlightDemoContext>()
    .AddDefaultTokenProviders();

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],  // ? Ensure this is correct
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],  // ? Ensure this is correct
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
        RoleClaimType = ClaimTypes.Role  // ? Ensure roles are validated correctly
    };
});


// Add repositories to Dependency Injection (DI)
builder.Services.AddTransient<IUser, UserRepository>();
builder.Services.AddTransient<IFlight, FlightRepository>();
builder.Services.AddTransient<IBooking, BookingRepository>();
builder.Services.AddTransient<IPayment, PaymentRepository>();
builder.Services.AddTransient<ICheckIn, CheckInRepository>();

// Enable API documentation with Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular frontend
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();  // Ensure this is placed before UseAuthorization
app.UseAuthorization();

app.MapControllers();
app.Run();
