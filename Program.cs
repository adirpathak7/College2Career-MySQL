using System.Text;
using System.Text.Json;
using College2Career.Data;
using College2Career.Repository;
using College2Career.Service;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

// Load environment variables from .env
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Build connection string from .env
var connectionString = $"Server={Environment.GetEnvironmentVariable("DB_HOST")};" +
                       $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                       $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                       $"User={Environment.GetEnvironmentVariable("DB_USER")};" +
                       $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
                       "SslMode=Required;";
Console.WriteLine("connectionString:- " + connectionString);
// Register DbContext with MySQL Server
builder.Services.AddDbContext<C2CDBContext>(options =>
{
    options.UseMySql(connectionString,
        new MySqlServerVersion(new Version(8, 0, 35)),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null
        ));
});

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(7072);
});

// Registered Repositories
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<ICompaniesRepository, CompaniesRepository>();
builder.Services.AddScoped<ICollegesRepository, CollegesRepository>();
builder.Services.AddScoped<IStudentsRepository, StudentsRepository>();
builder.Services.AddScoped<IVacanciesRepository, VacanciesRepository>();
builder.Services.AddScoped<IApplicationsRepository, ApplicationsRepository>();
builder.Services.AddScoped<IInterviewsRepository, InterviewsRepository>();
builder.Services.AddScoped<IOffersRepository, OffersRepository>();

// Registered Services
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ICompaniesService, CompaniesService>();
builder.Services.AddScoped<ICollegesService, CollegesService>();
builder.Services.AddScoped<IStudentsService, StudentsService>();
builder.Services.AddScoped<IVacanciesService, VacanciesService>();
builder.Services.AddScoped<IApplicationsService, ApplicationsService>();
builder.Services.AddScoped<IInterviewsService, InterviewsService>();
builder.Services.AddScoped<IOffersService, OffersService>();

// Helper Services
builder.Services.AddSingleton<IJWTService, JWTService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.UseUrls("http://10.0.2.2:7072");

// Allow CORS for College2Career React App
builder.Services.AddCors(options =>
{
    options.AddPolicy("College2Career", policy =>
    {
        policy.WithOrigins(
                "https://college2career-frontend.vercel.app",
                "http://localhost:5173",
                "https://college2career-frontend-git-main-aaditya-pathas-projects.vercel.app")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// JWT Authentication Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET");
        var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
        var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
        };

        options.Events = new JwtBearerEvents
        {
            OnForbidden = async context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    message = "Unauthorized! Only authorized users can access this endpoint."
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("College2Career");

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
