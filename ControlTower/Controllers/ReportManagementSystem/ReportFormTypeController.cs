using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportFormTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportFormTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReportFormType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportFormTypeDto>>> GetReportFormTypes()
        {
            var reportFormTypes = await _context.ReportFormTypes
                .Include(r => r.CreatedByUser)
                .Include(r => r.UpdatedByUser)
                .Where(r => !r.IsDeleted)
                .Select(r => new ReportFormTypeDto
                {
                    ID = r.ID,
                    Name = r.Name,
                    IsDeleted = r.IsDeleted,
                    CreatedDate = r.CreatedDate,
                    UpdatedDate = r.UpdatedDate,
                    CreatedBy = r.CreatedBy,
                    UpdatedBy = r.UpdatedBy,
                    CreatedByUserName = r.CreatedByUser != null ? $"{r.CreatedByUser.FirstName} {r.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = r.UpdatedByUser != null ? $"{r.UpdatedByUser.FirstName} {r.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            return Ok(reportFormTypes);
        }

        // GET: api/ReportFormType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportFormTypeDto>> GetReportFormType(Guid id)
        {
            var reportFormType = await _context.ReportFormTypes
                .Include(r => r.CreatedByUser)
                .Include(r => r.UpdatedByUser)
                .Where(r => r.ID == id && !r.IsDeleted)
                .Select(r => new ReportFormTypeDto
                {
                    ID = r.ID,
                    Name = r.Name,
                    IsDeleted = r.IsDeleted,
                    CreatedDate = r.CreatedDate,
                    UpdatedDate = r.UpdatedDate,
                    CreatedBy = r.CreatedBy,
                    UpdatedBy = r.UpdatedBy,
                    CreatedByUserName = r.CreatedByUser != null ? $"{r.CreatedByUser.FirstName} {r.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = r.UpdatedByUser != null ? $"{r.UpdatedByUser.FirstName} {r.UpdatedByUser.LastName}" : null
                })
                .FirstOrDefaultAsync();

            if (reportFormType == null)
            {
                return NotFound(new { message = "Report form type not found." });
            }

            return Ok(reportFormType);
        }

        // POST: api/ReportFormType
        [HttpPost]
        public async Task<ActionResult<ReportFormTypeDto>> CreateReportFormType(CreateReportFormTypeDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check for duplicate name
            var existingReportFormType = await _context.ReportFormTypes
                .AnyAsync(r => r.Name.ToLower() == createDto.Name.ToLower() && !r.IsDeleted);

            if (existingReportFormType)
            {
                return BadRequest(new { message = "A report form type with this name already exists." });
            }

            var reportFormType = new ReportFormType
            {
                ID = Guid.NewGuid(),
                Name = createDto.Name,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = null, // Set based on authentication context
                UpdatedBy = null  // Set based on authentication context
            };

            _context.ReportFormTypes.Add(reportFormType);
            await _context.SaveChangesAsync();

            // Return the created record with navigation properties
            var createdRecord = await GetReportFormType(reportFormType.ID);
            return CreatedAtAction(nameof(GetReportFormType), new { id = reportFormType.ID }, createdRecord.Value);
        }

        // PUT: api/ReportFormType/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReportFormType(Guid id, UpdateReportFormTypeDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reportFormType = await _context.ReportFormTypes
                .FirstOrDefaultAsync(r => r.ID == id && !r.IsDeleted);

            if (reportFormType == null)
            {
                return NotFound(new { message = "Report form type not found." });
            }

            // Check for duplicate name (excluding current record)
            var existingReportFormType = await _context.ReportFormTypes
                .AnyAsync(r => r.Name.ToLower() == updateDto.Name.ToLower() && r.ID != id && !r.IsDeleted);

            if (existingReportFormType)
            {
                return BadRequest(new { message = "A report form type with this name already exists." });
            }

            // Update properties
            reportFormType.Name = updateDto.Name;
            reportFormType.UpdatedDate = DateTime.UtcNow;
            reportFormType.UpdatedBy = null; // Set based on authentication context

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Report form type updated successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The record was modified by another user. Please refresh and try again." });
            }
        }

        // DELETE: api/ReportFormType/5 (Soft Delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportFormType(Guid id)
        {
            var reportFormType = await _context.ReportFormTypes
                .FirstOrDefaultAsync(r => r.ID == id && !r.IsDeleted);

            if (reportFormType == null)
            {
                return NotFound(new { message = "Report form type not found." });
            }

            // Soft delete
            reportFormType.IsDeleted = true;
            reportFormType.UpdatedDate = DateTime.UtcNow;
            reportFormType.UpdatedBy = null; // Set based on authentication context

            await _context.SaveChangesAsync();

            return Ok(new { message = "Report form type deleted successfully." });
        }
    }
}