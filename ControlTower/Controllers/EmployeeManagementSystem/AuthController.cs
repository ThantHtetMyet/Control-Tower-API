using ControlTower.Models.EmployeeManagementSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ControlTower.Data;
using ControlTower.DTOs;
using ControlTower.Models.EmployeeManagementSystem;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ControlTower.DTOs.EmployeeManagementSystem;

namespace ControlTower.Controllers.EmployeeManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("signin")]
        public async Task<ActionResult<AuthResponseDto>> SignIn(SignInDto signInDto)
        {
            try
            {
                // Find employee by email
                var employee = await _context.Users
                    .Include(e => e.SubDepartment)
                        .ThenInclude(sd => sd.Department)
                    .Include(e => e.Occupation)
                    .FirstOrDefaultAsync(e => e.Email == signInDto.Email && !e.IsDeleted);

                if (employee == null)
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }

                // Verify password - handle both hashed and plain text
                bool isPasswordValid = false;
                
                if (employee.LoginPassword.StartsWith("$2"))
                {
                    // Password is already hashed with BCrypt
                    isPasswordValid = BCrypt.Net.BCrypt.Verify(signInDto.Password, employee.LoginPassword);
                }
                else
                {
                    // Password is plain text - compare directly and then hash it
                    isPasswordValid = employee.LoginPassword == signInDto.Password;
                    
                    if (isPasswordValid)
                    {
                        // Hash the password for future use
                        employee.LoginPassword = BCrypt.Net.BCrypt.HashPassword(signInDto.Password);
                    }
                }

                if (!isPasswordValid)
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }

                // Update last login
                employee.LastLogin = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                // Generate JWT token with all application access levels
                var token = await GenerateJwtTokenAsync(employee);
                var expiresAt = DateTime.UtcNow.AddHours(24);

                var response = new AuthResponseDto
                {
                    Token = token,
                    ExpiresAt = expiresAt,
                    Employee = new EmployeeAuthDto
                    {
                        ID = employee.ID,
                        Email = employee.Email,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        StaffCardID = employee.StaffCardID,
                        DepartmentName = employee.SubDepartment?.Department?.Name ?? "",
                        SubDepartmentName = employee.SubDepartment?.Name ?? "",
                        OccupationName = employee.Occupation?.OccupationName ?? "",
                        LastLogin = employee.LastLogin
                    }
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during sign in", error = ex.Message });
            }
        }

        /// <summary>
        /// Generates a comprehensive JWT token with user information and application access levels
        /// </summary>
        /// <param name="user">The user for whom to generate the token</param>
        /// <returns>JWT token string</returns>
        private async Task<string> GenerateJwtTokenAsync(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["Key"] ?? "YourDefaultSecretKeyThatIsAtLeast32CharactersLong!";
            var issuer = jwtSettings["Issuer"] ?? "ServiceReportSystem";
            var audience = jwtSettings["Audience"] ?? "ServiceReportSystemUsers";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Base claims for all users
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim("StaffCardID", user.StaffCardID ?? ""),
                new Claim("SubDepartmentID", user.SubDepartmentID.ToString() ?? ""),
                new Claim("SubDepartmentName", user.SubDepartment?.Name ?? ""),
                new Claim("DepartmentID", user.SubDepartment?.DepartmentID.ToString() ?? ""),
                new Claim("DepartmentName", user.SubDepartment?.Department?.Name ?? ""),
                new Claim("OccupationID", user.OccupationID.ToString())
            };

            // Add News Portal access level claim if user has access
            var applicationName = _configuration["NewsPortalSettings:ApplicationName"] ?? "News Portal System";
            var newsPortalApp = await _context.Applications
                .FirstOrDefaultAsync(app => app.ApplicationName == applicationName && !app.IsDeleted);

            if (newsPortalApp != null)
            {
                var newsPortalAccess = await _context.UserApplicationAccesses
                    .Include(uaa => uaa.AccessLevel)
                    .FirstOrDefaultAsync(uaa =>
                        uaa.UserID == user.ID &&
                        uaa.ApplicationID == newsPortalApp.ID &&
                        !uaa.IsDeleted &&
                        !uaa.IsRevoked);

                if (newsPortalAccess != null)
                {
                    claims.Add(new Claim("NewsPortalAccessLevel", newsPortalAccess.AccessLevel.LevelName));
                }
            }

            // You can add more application access levels here in the future
            // Example for other applications:
            // var serviceReportApp = await _context.Applications
            //     .FirstOrDefaultAsync(app => app.ApplicationName == "Service Report System" && !app.IsDeleted);
            // if (serviceReportApp != null) { ... }

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}