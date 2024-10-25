using Microsoft.EntityFrameworkCore;
using CM.API.Data;
using CM.API.Interfaces;
using CM.API.Repositories;
using CM.API.Services;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; 
var builder = WebApplication.CreateBuilder(args);


// Disable HTTPS Redirection Middleware (for local dev)
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = null;  // Disable HTTPS redirection
});


builder.Services.AddSingleton<IMovieService, MovieService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddSingleton<IShowtimeService, ShowtimeService>();
builder.Services.AddSingleton<GenreRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 28))));



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
