using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Todo.Data;
using Todo.DTO;
using Todo.Entities;
using Todo.Interface;
using Todo.Jwt;
using Todo.Service;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    
    options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,

      ValidIssuer = jwtSettings!.Issuer,
      ValidAudience = jwtSettings!.Audience,

      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
      
      NameClaimType = ClaimTypes.NameIdentifier

    };


});


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));


builder.Services.AddAuthorization();
var app = builder.Build();

app.MapPost("/register", (AppDbContext db, RegisterRequestDto request, IAuthService service) =>
{
    if(string.IsNullOrWhiteSpace(request.Name) ||
        string.IsNullOrWhiteSpace(request.Mail) ||
        string.IsNullOrWhiteSpace(request.Password) || request.Age == 0)
        return Results.BadRequest("All fields must be filled in");

    bool mailExists = db.Users.Any(u => u.Mail == request.Mail);
    if(mailExists)
        return Results.BadRequest("Email already exists");
    
    string doHashPassw = service.HashGeneration(request.Password);

    User user = new User
    {
        Id = Guid.NewGuid(),
        Name = request.Name,
        Age = request.Age,
        Mail = request.Mail,
        PasswordHash = doHashPassw,
        IsActive = true
    };
    db.Users.Add(user);
    db.SaveChanges();
    return Results.Created();
});

app.Run();
