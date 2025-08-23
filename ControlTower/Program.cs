using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ControlTower.Data;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Features;
using System.Text.Json;
using Microsoft.Extensions.FileProviders;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ================= Add services =================
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                // Handle JSON serialization cycles if needed
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                // Add camelCase property naming policy
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

        // ===== CORS (allow all) =====
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowReactApp", policy =>
            {
                policy
                    .WithOrigins(
                        "http://192.3.71.120:5002", // React app URL
                        "http://192.3.71.120:5001", // API URL (for same-origin requests)
                        "http://localhost:3000",    // Development React
                        "https://localhost:7145"    // Development API
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials(); // required for cookies/auth headers
            });
        });

        // ===== Database =====
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // ===== Swagger =====
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // ===== JWT Authentication =====
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"] ?? "")),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"]
                };
            });

        // ===== File upload limit =====
        builder.Services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 100 * 1024 * 1024; // 100MB
        });

        var app = builder.Build();

        // ================= Middleware =================
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Configure static file serving for news images
        var newsFileStoragePath = builder.Configuration["NewsFileStorage:BasePath"];
        if (!string.IsNullOrEmpty(newsFileStoragePath) && Directory.Exists(newsFileStoragePath))
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(newsFileStoragePath),
                RequestPath = "/news-uploads"
            });
        }

        // If IIS isn't using HTTPS, comment this line
        // app.UseHttpsRedirection();

        // Apply CORS (must be before auth)
        app.UseCors("AllowReactApp"); // Fix: use the correct policy name

        // Authentication & Authorization
        app.UseAuthentication();
        app.UseAuthorization();

        // Map controllers
        app.MapControllers();

        app.Run();
    }
}
