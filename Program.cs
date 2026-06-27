using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.DTO;
using Todo.Entities;
using Todo.Interface;
using Todo.Service;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

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
