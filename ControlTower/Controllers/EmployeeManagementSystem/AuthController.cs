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
                    .Include(e => e.Department)
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

                // Generate JWT token
                var token = GenerateJwtToken(employee);
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
                        DepartmentName = employee.Department?.Name ?? "",
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

        private string GenerateJwtToken(User employee)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            // ✅ Fix: Change "SecretKey" to "Key"
            var secretKey = jwtSettings["Key"] ?? "YourDefaultSecretKeyThatIsAtLeast32CharactersLong!";
            // ✅ Fix: Update default values to match appsettings.json
            var issuer = jwtSettings["Issuer"] ?? "ServiceReportSystem";
            var audience = jwtSettings["Audience"] ?? "ServiceReportSystemUsers";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, employee.ID.ToString()),
                new Claim(ClaimTypes.Email, employee.Email),
                new Claim(ClaimTypes.Name, $"{employee.FirstName} {employee.LastName}"),
                new Claim("StaffCardID", employee.StaffCardID),
                new Claim("DepartmentID", employee.DepartmentID.ToString()),
                new Claim("OccupationID", employee.OccupationID.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(24),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        /*
        [HttpPost("signup")]
        public async Task<ActionResult<AuthResponseDto>> SignUp(SignUpDto signUpDto)
        {
            if (signUpDto.Password != signUpDto.ConfirmPassword)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Message = "Passwords do not match"
                });
            }

            var userExists = await _context.Users.AnyAsync(u => u.Email == signUpDto.Email);
            if (userExists)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Message = "Email already registered"
                });
            }

            var user = new User
            {
                FirstName = signUpDto.FirstName,
                LastName = signUpDto.LastName,
                Email = signUpDto.Email,
                MobileNo = signUpDto.MobileNo,
                Gender = signUpDto.Gender,
                LoginPassword = BCrypt.Net.BCrypt.HashPassword(signUpDto.Password),
                IsDeleted = false,
                LastLogin = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(user);

            return Ok(new AuthResponseDto
            {
                Success = true,
                Message = "User registered successfully",
                Token = token,
                User = MapToUserDto(user)
            });
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<AuthResponseDto>> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == forgotPasswordDto.Email && !u.IsDeleted);

            if (user == null)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Message = "User not found"
                });
            }

            // Generate password reset token
            var resetToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            // TODO: Implement email sending logic here
            // For now, we'll just return the token in the response
            return Ok(new AuthResponseDto
            {
                Success = true,
                Message = "Password reset instructions sent to your email",
                Token = resetToken
            });
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<AuthResponseDto>> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Message = "Passwords do not match"
                });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == resetPasswordDto.Email && !u.IsDeleted);

            if (user == null)
            {
                return BadRequest(new AuthResponseDto
                {
                    Success = false,
                    Message = "User not found"
                });
            }

            // TODO: Validate reset token
            // For now, we'll just update the password
            user.LoginPassword = BCrypt.Net.BCrypt.HashPassword(resetPasswordDto.NewPassword);
            await _context.SaveChangesAsync();

            return Ok(new AuthResponseDto
            {
                Success = true,
                Message = "Password reset successful"
            });
        }
        private UserDto MapToUserDto(User user)
        {
            return new UserDto
            {
                ID = user.ID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                MobileNo = user.MobileNo,
                Gender = user.Gender
            };
        }
        */
    }
}