using Microsoft.EntityFrameworkCore;
using Todo.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Run();
