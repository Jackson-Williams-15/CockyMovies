using Microsoft.EntityFrameworkCore;
using CM.API.Data;
using CM.API.Interfaces;
using CM.API.Repositories;
using CM.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using DotNetEnv;

/// <summary>
/// Entry point for the application.
/// </summary>
var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Clears all logging providers and adds console logging.
/// </summary>
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Load environment variables from .env file
Env.Load();

// Disable HTTPS Redirection Middleware (for local dev)
builder.Services.AddHttpsRedirection(options =>
{
    // Disable HTTPS redirection
    options.HttpsPort = null;
});

// Configures CORS to allow all origins, methods, and headers.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Adds scoped services for dependency injection.
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IShowtimeService, ShowtimeService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

builder.Services.AddScoped<GenreRepository>();
builder.Services.AddScoped<ContentModerationService>();
builder.Services.AddControllers();

// Configures JSON serialization options.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adds the database context to the service container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 28))));

// Configure email settings
builder.Services.Configure<EmailSettings>(options =>
{
    options.SmtpServer = builder.Configuration["EmailSettings:SmtpServer"];
    options.SmtpPort = int.Parse(builder.Configuration["EmailSettings:SmtpPort"]);
    options.SenderName = builder.Configuration["EmailSettings:SenderName"];
    options.SenderEmail = Environment.GetEnvironmentVariable("EMAIL_SENDER");
    options.SenderPassword = Environment.GetEnvironmentVariable("EMAIL_PASSWORD");
});

builder.Services.AddTransient<IEmailService, EmailService>();

// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Configures authorization policies.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManagerOnly", policy => policy.RequireRole("Manager"));
});

var app = builder.Build();

/// <summary>
/// Middleware to log request paths, methods, and bodies for debugging purposes.
/// </summary>
/// <remarks>
/// This middleware should only be uncommented for debugging purposes as it can log sensitive information.
/// </remarks>
/*app.Use(async (context, next) =>
{
    // Log the request path and method
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Handling request: {Path} {Method}", context.Request.Path, context.Request.Method);

    // Log the request body
    context.Request.EnableBuffering();
    var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
    context.Request.Body.Position = 0;
    logger.LogInformation("Request body: {Body}", body);

    await next.Invoke();
});*/

// Configure cors and frontend URL
app.UseCors(x => x
            .WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithExposedHeaders("X-total-count"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();