using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.IO; // Add this import

namespace ControlTower.Controllers.ReportManagementSystem
{
    // Add the request class
    public class UploadReportFormImageRequest
    {
        public Guid ReportFormId { get; set; }
        public IFormFile ImageFile { get; set; } = default!;
        public Guid ReportFormImageTypeId { get; set; }
        public string? SectionName { get; set; } // Add optional section name for folder organization
    }

    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Add this to the class level
    public class ReportFormImageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        private bool ReportFormImageExists(Guid id)
        {
            return _context.ReportFormImages.Any(e => e.ID == id && !e.IsDeleted);
        }

        public ReportFormImageController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/ReportFormImage/image/{reportFormId}/{imageName}
        [HttpGet("image/{reportFormId}/{imageName}")]
        [AllowAnonymous] // Allow anonymous access for image serving
        public async Task<IActionResult> GetReportFormImage(Guid reportFormId, string imageName)
        {
            var basePath = _configuration["ReportManagementSystemFileStorage:BasePath"] ?? "C:\\Temp\\ReportFormImages";

            // Try to find the image in the report form directory or its subdirectories
            var reportFormDirectory = Path.Combine(basePath, reportFormId.ToString());

            if (!Directory.Exists(reportFormDirectory))
                return NotFound();

            // Search for the image file in the report form directory and subdirectories
            var imageFiles = Directory.GetFiles(reportFormDirectory, imageName, SearchOption.AllDirectories);

            if (imageFiles.Length == 0)
                return NotFound();

            var fullPath = imageFiles[0]; // Take the first match

            // Detect content type dynamically
            var extension = Path.GetExtension(imageName).ToLowerInvariant();
            var contentType = extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "image/jpeg"
            };

            var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            return File(fileBytes, contentType);
        }

        // GET: api/ReportFormImage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportFormImageDto>>> GetReportFormImages()
        {
            var images = await _context.ReportFormImages
                .Include(r => r.ServiceReportForm)
                    .ThenInclude(s => s.ReportFormType)
                .Include(r => r.ReportFormImageType)
                .Include(r => r.UploadedByUser)
                .Where(r => !r.IsDeleted)
                .Select(r => new ReportFormImageDto
                {
                    ID = r.ID,
                    ReportFormID = r.ReportFormID,
                    ReportImageTypeID = r.ReportImageTypeID,
                    ImageName = r.ImageName,
                    StoredDirectory = r.StoredDirectory,
                    UploadedStatus = r.UploadedStatus,
                    IsDeleted = r.IsDeleted,
                    UploadedDate = r.UploadedDate,
                    UploadedBy = r.UploadedBy,
                    ReportFormTypeName = r.ServiceReportForm.ReportFormType != null ? r.ServiceReportForm.ReportFormType.Name : null,
                    ImageTypeName = r.ReportFormImageType.ImageTypeName,
                    UploadedByUserName = $"{r.UploadedByUser.FirstName} {r.UploadedByUser.LastName}"
                })
                .ToListAsync();

            return Ok(images);
        }

        // GET: api/ReportFormImage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportFormImageDto>> GetReportFormImage(Guid id)
        {
            var image = await _context.ReportFormImages
                .Include(r => r.ServiceReportForm)
                    .ThenInclude(s => s.ReportFormType)
                .Include(r => r.ReportFormImageType)
                .Include(r => r.UploadedByUser)
                .Where(r => r.ID == id && !r.IsDeleted)
                .Select(r => new ReportFormImageDto
                {
                    ID = r.ID,
                    ReportFormID = r.ReportFormID,
                    ReportImageTypeID = r.ReportImageTypeID,
                    ImageName = r.ImageName,
                    StoredDirectory = r.StoredDirectory,
                    UploadedStatus = r.UploadedStatus,
                    IsDeleted = r.IsDeleted,
                    UploadedDate = r.UploadedDate,
                    UploadedBy = r.UploadedBy,
                    ReportFormTypeName = r.ServiceReportForm.ReportFormType != null ? r.ServiceReportForm.ReportFormType.Name : null,
                    ImageTypeName = r.ReportFormImageType.ImageTypeName,
                    UploadedByUserName = $"{r.UploadedByUser.FirstName} {r.UploadedByUser.LastName}"
                })
                .FirstOrDefaultAsync();

            if (image == null)
            {
                return NotFound(new { message = "Report form image not found." });
            }

            return Ok(image);
        }

        // GET: api/ReportFormImage/ByReportForm/5
        [HttpGet("ByReportForm/{reportFormId}")]
        public async Task<ActionResult<IEnumerable<ReportFormImageDto>>> GetImagesByReportForm(Guid reportFormId)
        {
            var images = await _context.ReportFormImages
                .Include(r => r.ServiceReportForm)
                    .ThenInclude(s => s.ReportFormType)
                .Include(r => r.ReportFormImageType)
                .Include(r => r.UploadedByUser)
                .Where(r => r.ReportFormID == reportFormId && !r.IsDeleted)
                .Select(r => new ReportFormImageDto
                {
                    ID = r.ID,
                    ReportFormID = r.ReportFormID,
                    ReportImageTypeID = r.ReportImageTypeID,
                    ImageName = r.ImageName,
                    StoredDirectory = r.StoredDirectory,
                    UploadedStatus = r.UploadedStatus,
                    IsDeleted = r.IsDeleted,
                    UploadedDate = r.UploadedDate,
                    UploadedBy = r.UploadedBy,
                    ReportFormTypeName = r.ServiceReportForm.ReportFormType != null ? r.ServiceReportForm.ReportFormType.Name : null,
                    ImageTypeName = r.ReportFormImageType.ImageTypeName,
                    UploadedByUserName = $"{r.UploadedByUser.FirstName} {r.UploadedByUser.LastName}"
                })
                .ToListAsync();

            return Ok(images);
        }

        // POST: api/ReportFormImage
        [HttpPost]
        public async Task<ActionResult<ReportFormImageDto>> CreateReportFormImage(CreateReportFormImageDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate foreign key relationships
            var reportFormExists = await _context.ReportForms
                .AnyAsync(r => r.ID == createDto.ReportFormID && !r.IsDeleted);
            if (!reportFormExists)
            {
                return BadRequest(new { message = "Invalid Report Form ID." });
            }

            var imageTypeExists = await _context.ReportFormImageTypes
                .AnyAsync(r => r.ID == createDto.ReportImageTypeID && !r.IsDeleted);
            if (!imageTypeExists)
            {
                return BadRequest(new { message = "Invalid Report Image Type ID." });
            }

            var userExists = await _context.Users
                .AnyAsync(u => u.ID == createDto.UploadedBy && !u.IsDeleted);
            if (!userExists)
            {
                return BadRequest(new { message = "Invalid User ID." });
            }

            var image = new ReportFormImage
            {
                ID = Guid.NewGuid(),
                ReportFormID = createDto.ReportFormID,
                ReportImageTypeID = createDto.ReportImageTypeID,
                ImageName = createDto.ImageName,
                StoredDirectory = createDto.StoredDirectory,
                UploadedStatus = createDto.UploadedStatus,
                IsDeleted = false,
                UploadedDate = DateTime.UtcNow,
                UploadedBy = createDto.UploadedBy
            };

            _context.ReportFormImages.Add(image);
            await _context.SaveChangesAsync();

            // Return the created record with navigation properties
            var createdRecord = await GetReportFormImage(image.ID);
            return CreatedAtAction(nameof(GetReportFormImage), new { id = image.ID }, createdRecord.Value);
        }

        // PUT: api/ReportFormImage/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReportFormImage(Guid id, UpdateReportFormImageDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var image = await _context.ReportFormImages
                .FirstOrDefaultAsync(r => r.ID == id && !r.IsDeleted);

            if (image == null)
            {
                return NotFound(new { message = "Report form image not found." });
            }

            // Validate foreign key relationships
            var imageTypeExists = await _context.ReportFormImageTypes
                .AnyAsync(r => r.ID == updateDto.ReportImageTypeID && !r.IsDeleted);
            if (!imageTypeExists)
            {
                return BadRequest(new { message = "Invalid Report Image Type ID." });
            }

            // Update properties
            image.ReportImageTypeID = updateDto.ReportImageTypeID;
            image.ImageName = updateDto.ImageName;
            image.StoredDirectory = updateDto.StoredDirectory;
            image.UploadedStatus = updateDto.UploadedStatus;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Report form image updated successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The record was modified by another user. Please refresh and try again." });
            }
        }

        // DELETE: api/ReportFormImage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportFormImage(Guid id)
        {
            var image = await _context.ReportFormImages
                .FirstOrDefaultAsync(r => r.ID == id && !r.IsDeleted);

            if (image == null)
            {
                return NotFound(new { message = "Report form image not found." });
            }

            // Soft delete
            image.IsDeleted = true;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Report form image deleted successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The record was modified by another user. Please refresh and try again." });
            }
        }
        
        // Modified upload endpoint to support section-based folder organization
        // POST: api/ReportFormImage/upload
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        [Produces("application/json")]
        public async Task<ActionResult<ReportFormImageDto>> UploadReportFormImage(
            [FromForm] UploadReportFormImageRequest req)
        {
            if (req.ImageFile is null || req.ImageFile.Length == 0)
                return BadRequest("Image file is required");

            // Validate report form exists
            var reportFormExists = await _context.ReportForms
                .AnyAsync(r => r.ID == req.ReportFormId && !r.IsDeleted);
            if (!reportFormExists)
                return BadRequest("Invalid Report Form ID");

            // Get current user ID (ensure this claim is actually a GUID in your auth setup)
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out Guid userId))
                return Unauthorized("Invalid user");

            var basePath = _configuration["ReportManagementSystemFileStorage:BasePath"]
                           ?? "C:\\Temp\\ReportFormImages";

            Directory.CreateDirectory(basePath);
            
            // Create ReportFormID folder
            var reportFormDirectory = Path.Combine(basePath, req.ReportFormId.ToString());
            Directory.CreateDirectory(reportFormDirectory);

            // Create section-specific subfolder if SectionName is provided
            string finalDirectory = reportFormDirectory;
            if (!string.IsNullOrWhiteSpace(req.SectionName))
            {
                finalDirectory = Path.Combine(reportFormDirectory, req.SectionName);
                Directory.CreateDirectory(finalDirectory);
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var fileExtension = Path.GetExtension(req.ImageFile.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
                return BadRequest("Invalid file type. Only JPG, PNG, GIF, and WebP are allowed.");

            var fileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid():N}{fileExtension}";
            var filePath = Path.Combine(finalDirectory, fileName);

            try
            {
                await using (var stream = System.IO.File.Create(filePath))
                {
                    await req.ImageFile.CopyToAsync(stream);
                }

                var reportFormImage = new ReportFormImage
                {
                    ID = Guid.NewGuid(),
                    ReportFormID = req.ReportFormId,
                    ReportImageTypeID = req.ReportFormImageTypeId,
                    ImageName = fileName,
                    StoredDirectory = finalDirectory, // Store the full path including section folder
                    UploadedStatus = "Uploaded",
                    IsDeleted = false,
                    UploadedDate = DateTime.UtcNow,
                    UploadedBy = userId
                };

                _context.ReportFormImages.Add(reportFormImage);
                await _context.SaveChangesAsync();

                // Assuming this returns ActionResult<ReportFormImageDto>
                var createdRecord = await GetReportFormImage(reportFormImage.ID);
                return Ok(createdRecord.Value);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error uploading file: {ex.Message}");
            }
        }
    }
}
