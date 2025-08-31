using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.EmployeeManagementSystem;
using ControlTower.DTOs.EmployeeManagementSystem;

namespace ControlTower.Controllers.EmployeeManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserImageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserImageController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/UserImage/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserImageDto>>> GetUserImages(Guid userId)
        {
            var userImages = await _context.UserImages
                .Include(ui => ui.User)
                .Include(ui => ui.ImageType)
                .Include(ui => ui.UploadedByUser)
                .Where(ui => ui.UserID == userId && !ui.IsDeleted)
                .Select(ui => new UserImageDto
                {
                    ID = ui.ID,
                    UserID = ui.UserID,
                    ImageTypeID = ui.ImageTypeID,
                    ImageName = ui.ImageName,
                    StoredDirectory = ui.StoredDirectory,
                    UploadedStatus = ui.UploadedStatus,
                    UploadedDate = ui.UploadedDate,
                    UploadedBy = ui.UploadedBy,
                    UserFullName = ui.User.FirstName + " " + ui.User.LastName,
                    ImageTypeName = ui.ImageType.ImageTypeName,
                    UploadedByName = ui.UploadedByUser.FirstName + " " + ui.UploadedByUser.LastName
                })
                .ToListAsync();

            return Ok(userImages);
        }

        // GET: api/UserImage/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserImageDto>> GetUserImage(Guid id)
        {
            var userImage = await _context.UserImages
                .Include(ui => ui.User)
                .Include(ui => ui.ImageType)
                .Include(ui => ui.UploadedByUser)
                .Where(ui => ui.ID == id && !ui.IsDeleted)
                .Select(ui => new UserImageDto
                {
                    ID = ui.ID,
                    UserID = ui.UserID,
                    ImageTypeID = ui.ImageTypeID,
                    ImageName = ui.ImageName,
                    StoredDirectory = ui.StoredDirectory,
                    UploadedStatus = ui.UploadedStatus,
                    UploadedDate = ui.UploadedDate,
                    UploadedBy = ui.UploadedBy,
                    UserFullName = ui.User.FirstName + " " + ui.User.LastName,
                    ImageTypeName = ui.ImageType.ImageTypeName,
                    UploadedByName = ui.UploadedByUser.FirstName + " " + ui.UploadedByUser.LastName
                })
                .FirstOrDefaultAsync();

            if (userImage == null)
            {
                return NotFound();
            }

            return Ok(userImage);
        }

        // DELETE: api/UserImage/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserImage(Guid id, [FromQuery] Guid deletedBy)
        {
            var userImage = await _context.UserImages.FindAsync(id);
            if (userImage == null || userImage.IsDeleted)
            {
                return NotFound();
            }

            userImage.IsDeleted = true;
            userImage.UpdatedDate = DateTime.UtcNow;
            userImage.UpdatedBy = deletedBy;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}