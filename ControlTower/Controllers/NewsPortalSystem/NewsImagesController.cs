using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.NewsPortalSystem;
using ControlTower.DTOs.NewsPortalSystem;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ControlTower.Controllers.NewsPortalSystem
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NewsImagesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _basePath;

        public NewsImagesController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _basePath = _configuration["FileStorage:BasePath"] ?? "C:\\Temp\\NewsImages";
        }

        // GET: api/NewsImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsImageDto>>> GetNewsImages([FromQuery] Guid? newsId = null)
        {
            var query = _context.NewsImages
                .Include(ni => ni.News)
                .Include(ni => ni.UploadedByUser)
                .Where(ni => !ni.IsDeleted);

            if (newsId.HasValue)
            {
                query = query.Where(ni => ni.NewsID == newsId.Value);
            }

            var images = await query
                .Select(ni => new NewsImageDto
                {
                    ID = ni.ID,
                    NewsID = ni.NewsID,
                    NewsTitle = ni.News.Title,
                    Name = ni.Name,
                    StoredDirectory = ni.StoredDirectory,
                    UploadedStatus = ni.UploadedStatus,
                    AltText = ni.AltText,
                    Caption = ni.Caption,
                    IsFeatured = ni.IsFeatured,
                    UploadedDate = ni.UploadedDate,
                    UploadedByUserName = ni.UploadedByUser != null ? ni.UploadedByUser.FirstName + " " + ni.UploadedByUser.LastName : null,
                    ImageUrl = ni.StoredDirectory + "/" + ni.Name
                })
                .ToListAsync();

            return Ok(images);
        }

        // GET: api/NewsImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsImageDto>> GetNewsImage(Guid id)
        {
            var newsImage = await _context.NewsImages
                .Include(ni => ni.News)
                .Include(ni => ni.UploadedByUser)
                .Where(ni => ni.ID == id && !ni.IsDeleted)
                .Select(ni => new NewsImageDto
                {
                    ID = ni.ID,
                    NewsID = ni.NewsID,
                    NewsTitle = ni.News.Title,
                    Name = ni.Name,
                    StoredDirectory = ni.StoredDirectory,
                    UploadedStatus = ni.UploadedStatus,
                    AltText = ni.AltText,
                    Caption = ni.Caption,
                    IsFeatured = ni.IsFeatured,
                    UploadedDate = ni.UploadedDate,
                    UploadedByUserName = ni.UploadedByUser != null ? ni.UploadedByUser.FirstName + " " + ni.UploadedByUser.LastName : null,
                    ImageUrl = ni.StoredDirectory + "/" + ni.Name
                })
                .FirstOrDefaultAsync();

            if (newsImage == null)
            {
                return NotFound();
            }

            return Ok(newsImage);
        }

        // POST: api/NewsImages/upload
        [HttpPost("upload")]
        public async Task<ActionResult<List<NewsImageDto>>> UploadImages([FromForm] Guid newsId, [FromForm] List<IFormFile> images, [FromForm] string? altText = null, [FromForm] string? caption = null, [FromForm] bool isFeatured = false)
        {
            if (images == null || !images.Any())
            {
                return BadRequest("No images provided");
            }

            // Validate news exists
            var newsExists = await _context.News.AnyAsync(n => n.ID == newsId && !n.IsDeleted);
            if (!newsExists)
            {
                return BadRequest("Invalid news ID");
            }

            // Get current user ID
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out Guid userId))
            {
                return Unauthorized("Invalid user");
            }

            var uploadedImages = new List<NewsImageDto>();

            // Create directory: BasePath/NewsImages/NewsId
            var newsDirectory = Path.Combine(_basePath, "NewsImages", newsId.ToString());
            if (!Directory.Exists(newsDirectory))
            {
                Directory.CreateDirectory(newsDirectory);
            }

            foreach (var image in images)
            {
                // Validate file type
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    continue; // Skip invalid files
                }

                // Generate unique filename
                var fileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid():N}{fileExtension}";
                var filePath = Path.Combine(newsDirectory, fileName);

                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // Create database record
                var newsImage = new NewsImages
                {
                    ID = Guid.NewGuid(),
                    NewsID = newsId,
                    Name = fileName,
                    StoredDirectory = newsDirectory,
                    UploadedStatus = "Uploaded",
                    AltText = altText,
                    Caption = caption,
                    IsFeatured = isFeatured && uploadedImages.Count == 0, // Only first image can be featured
                    IsDeleted = false,
                    UploadedDate = DateTime.UtcNow,
                    UploadedBy = userId
                };

                _context.NewsImages.Add(newsImage);
                await _context.SaveChangesAsync();

                uploadedImages.Add(new NewsImageDto
                {
                    ID = newsImage.ID,
                    NewsID = newsImage.NewsID,
                    Name = newsImage.Name,
                    StoredDirectory = newsImage.StoredDirectory,
                    UploadedStatus = newsImage.UploadedStatus,
                    AltText = newsImage.AltText,
                    Caption = newsImage.Caption,
                    IsFeatured = newsImage.IsFeatured,
                    UploadedDate = newsImage.UploadedDate,
                    ImageUrl = newsImage.StoredDirectory + "/" + newsImage.Name
                });
            }

            return Ok(uploadedImages);
        }

        // PUT: api/NewsImages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNewsImage(Guid id, UpdateNewsImageDto updateDto)
        {
            if (id != updateDto.ID)
            {
                return BadRequest();
            }

            var newsImage = await _context.NewsImages.FindAsync(id);
            if (newsImage == null || newsImage.IsDeleted)
            {
                return NotFound();
            }

            // If setting as featured, remove featured from other images of the same news
            if (updateDto.IsFeatured && !newsImage.IsFeatured)
            {
                var otherFeaturedImages = await _context.NewsImages
                    .Where(ni => ni.NewsID == newsImage.NewsID && ni.ID != id && ni.IsFeatured && !ni.IsDeleted)
                    .ToListAsync();

                foreach (var img in otherFeaturedImages)
                {
                    img.IsFeatured = false;
                }
            }

            newsImage.Name = updateDto.Name;
            newsImage.AltText = updateDto.AltText;
            newsImage.Caption = updateDto.Caption;
            newsImage.IsFeatured = updateDto.IsFeatured;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsImageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/NewsImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsImage(Guid id)
        {
            var newsImage = await _context.NewsImages.FindAsync(id);
            if (newsImage == null || newsImage.IsDeleted)
            {
                return NotFound();
            }

            // Soft delete
            newsImage.IsDeleted = true;

            // Delete physical file
            try
            {
                var filePath = Path.Combine(newsImage.StoredDirectory, newsImage.Name);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                // Log error but don't fail the operation
                Console.WriteLine($"Failed to delete file: {ex.Message}");
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NewsImageExists(Guid id)
        {
            return _context.NewsImages.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}