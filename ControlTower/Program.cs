using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using ControlTower.Data;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(
                path: "Logs/controltower-api-.log",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 7,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {SourceContext} - {Message:lj}{NewLine}{Exception}"
            )
            .CreateLogger();

        try
        {
            Log.Information("Starting ControlTower API");
            
            var builder = WebApplication.CreateBuilder(args);
            
            // Use Serilog for logging
            builder.Host.UseSerilog();

        // ================= Services =================

        // Controllers + JSON options
        builder.Services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

        // CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowReactApp", policy =>
            {
                policy.WithOrigins(
                        "http://192.3.62.144:5002", // React app URL
                        "http://192.3.62.144:5001", // API URL (same-origin)
                        "http://localhost:5001",
                        "http://localhost:8080",
                        "http://localhost:3000",
                        "https://localhost:7145"
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        // Database
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // JWT Authentication
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var key = builder.Configuration["JwtSettings:Key"] ?? string.Empty;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"]
                };
            });

        // File upload limit
        builder.Services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 100 * 1024 * 1024; // 100MB
        });

        builder.Services.AddAuthorization();

        var app = builder.Build();

        // ================= Middleware =================

        // Swagger (enabled for all environments)
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "ControlTower API v1");
            c.RoutePrefix = "swagger";
        });

        // Static files for news images
        var newsFileStoragePath = app.Configuration["NewsFileStorage:BasePath"];
        if (!string.IsNullOrEmpty(newsFileStoragePath) && Directory.Exists(newsFileStoragePath))
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(newsFileStoragePath),
                RequestPath = "/news-uploads"
            });
        }

        // If IIS isn't using HTTPS, keep this commented
        // app.UseHttpsRedirection();

        // CORS
        app.UseCors("AllowReactApp");

        // Auth
        app.UseAuthentication();
        app.UseAuthorization();

        // Endpoints
        app.MapControllers();

        app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
