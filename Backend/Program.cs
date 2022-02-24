using Backend.Contexts;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options =>
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
