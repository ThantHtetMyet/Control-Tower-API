using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using Microsoft.Extensions.Configuration;

namespace ControlTower.Controllers.EmployeeManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeImageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public EmployeeImageController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProfileImage(Guid userId)
        {
            var userImage = await _context.UserImages
                .Include(ui => ui.ImageType)
                .FirstOrDefaultAsync(ui => ui.UserID == userId && 
                                          ui.ImageType.ImageTypeName == "User Profile Image" && 
                                          !ui.IsDeleted);

            if (userImage == null)
            {
                return NotFound("Profile image not found");
            }

            var basePath = _configuration["EmployeeManagementSystemFileStorage:BasePath"];
            var filePath = Path.Combine(basePath, userImage.StoredDirectory, userImage.ImageName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("Image file not found");
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var contentType = GetContentType(userImage.ImageName);

            return File(fileBytes, contentType);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteProfileImage(Guid userId, [FromQuery] Guid deletedBy)
        {
            var userImage = await _context.UserImages
                .Include(ui => ui.ImageType)
                .FirstOrDefaultAsync(ui => ui.UserID == userId && 
                                          ui.ImageType.ImageTypeName == "User Profile Image" && 
                                          !ui.IsDeleted);

            if (userImage == null)
            {
                return NotFound("Profile image not found");
            }

            // Delete physical file
            var basePath = _configuration["EmployeeManagementSystemFileStorage:BasePath"];
            var filePath = Path.Combine(basePath, userImage.StoredDirectory, userImage.ImageName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            // Mark as deleted in database
            userImage.IsDeleted = true;
            userImage.UpdatedDate = DateTime.UtcNow;
            userImage.UpdatedBy = deletedBy;

            await _context.SaveChangesAsync();

            return Ok("Profile image deleted successfully");
        }

        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };
        }
    }
}