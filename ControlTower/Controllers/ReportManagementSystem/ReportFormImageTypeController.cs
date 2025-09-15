using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportFormImageTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportFormImageTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReportFormImageType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportFormImageTypeDto>>> GetReportFormImageTypes()
        {
            var imageTypes = await _context.ReportFormImageTypes
                .Include(r => r.CreatedByUser)
                .Include(r => r.UpdatedByUser)
                .Where(r => !r.IsDeleted)
                .Select(r => new ReportFormImageTypeDto
                {
                    ID = r.ID,
                    ImageTypeName = r.ImageTypeName,
                    IsDeleted = r.IsDeleted,
                    CreatedDate = r.CreatedDate,
                    UpdatedDate = r.UpdatedDate,
                    CreatedBy = r.CreatedBy,
                    UpdatedBy = r.UpdatedBy,
                    CreatedByUserName = r.CreatedByUser != null ? $"{r.CreatedByUser.FirstName} {r.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = r.UpdatedByUser != null ? $"{r.UpdatedByUser.FirstName} {r.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            return Ok(imageTypes);
        }

        // GET: api/ReportFormImageType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportFormImageTypeDto>> GetReportFormImageType(Guid id)
        {
            var imageType = await _context.ReportFormImageTypes
                .Include(r => r.CreatedByUser)
                .Include(r => r.UpdatedByUser)
                .Where(r => r.ID == id && !r.IsDeleted)
                .Select(r => new ReportFormImageTypeDto
                {
                    ID = r.ID,
                    ImageTypeName = r.ImageTypeName,
                    IsDeleted = r.IsDeleted,
                    CreatedDate = r.CreatedDate,
                    UpdatedDate = r.UpdatedDate,
                    CreatedBy = r.CreatedBy,
                    UpdatedBy = r.UpdatedBy,
                    CreatedByUserName = r.CreatedByUser != null ? $"{r.CreatedByUser.FirstName} {r.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = r.UpdatedByUser != null ? $"{r.UpdatedByUser.FirstName} {r.UpdatedByUser.LastName}" : null
                })
                .FirstOrDefaultAsync();

            if (imageType == null)
            {
                return NotFound(new { message = "Report form image type not found." });
            }

            return Ok(imageType);
        }

        // POST: api/ReportFormImageType
        [HttpPost]
        public async Task<ActionResult<ReportFormImageTypeDto>> CreateReportFormImageType(CreateReportFormImageTypeDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check for duplicate name
            var existingImageType = await _context.ReportFormImageTypes
                .AnyAsync(r => r.ImageTypeName.ToLower() == createDto.ImageTypeName.ToLower() && !r.IsDeleted);

            if (existingImageType)
            {
                return BadRequest(new { message = "A report form image type with this name already exists." });
            }

            var imageType = new ReportFormImageType
            {
                ID = Guid.NewGuid(),
                ImageTypeName = createDto.ImageTypeName,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = null, // Set based on authentication context
                UpdatedBy = null  // Set based on authentication context
            };

            _context.ReportFormImageTypes.Add(imageType);
            await _context.SaveChangesAsync();

            // Return the created record with navigation properties
            var createdRecord = await GetReportFormImageType(imageType.ID);
            return CreatedAtAction(nameof(GetReportFormImageType), new { id = imageType.ID }, createdRecord.Value);
        }

        // PUT: api/ReportFormImageType/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReportFormImageType(Guid id, UpdateReportFormImageTypeDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imageType = await _context.ReportFormImageTypes
                .FirstOrDefaultAsync(r => r.ID == id && !r.IsDeleted);

            if (imageType == null)
            {
                return NotFound(new { message = "Report form image type not found." });
            }

            // Check for duplicate name (excluding current record)
            var existingImageType = await _context.ReportFormImageTypes
                .AnyAsync(r => r.ImageTypeName.ToLower() == updateDto.ImageTypeName.ToLower() && r.ID != id && !r.IsDeleted);

            if (existingImageType)
            {
                return BadRequest(new { message = "A report form image type with this name already exists." });
            }

            // Update properties
            imageType.ImageTypeName = updateDto.ImageTypeName;
            imageType.UpdatedDate = DateTime.UtcNow;
            imageType.UpdatedBy = null; // Set based on authentication context

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Report form image type updated successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The record was modified by another user. Please refresh and try again." });
            }
        }

        // DELETE: api/ReportFormImageType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportFormImageType(Guid id)
        {
            var imageType = await _context.ReportFormImageTypes
                .FirstOrDefaultAsync(r => r.ID == id && !r.IsDeleted);

            if (imageType == null)
            {
                return NotFound(new { message = "Report form image type not found." });
            }

            // Soft delete
            imageType.IsDeleted = true;
            imageType.UpdatedDate = DateTime.UtcNow;
            imageType.UpdatedBy = null; // Set based on authentication context

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Report form image type deleted successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The record was modified by another user. Please refresh and try again." });
            }
        }
    }
}