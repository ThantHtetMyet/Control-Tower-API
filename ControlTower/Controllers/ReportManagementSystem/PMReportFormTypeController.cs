using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class PMReportFormTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PMReportFormTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PMReportFormType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PMReportFormTypeDto>>> GetPMReportFormTypes()
        {
            var pmReportFormTypes = await _context.PMReportFormTypes
                .Include(r => r.CreatedByUser)
                .Include(r => r.UpdatedByUser)
                .Where(r => !r.IsDeleted)
                .Select(r => new PMReportFormTypeDto
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

            return Ok(pmReportFormTypes);
        }

        // GET: api/PMReportFormType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PMReportFormTypeDto>> GetPMReportFormType(Guid id)
        {
            var pmReportFormType = await _context.PMReportFormTypes
                .Include(r => r.CreatedByUser)
                .Include(r => r.UpdatedByUser)
                .Where(r => r.ID == id && !r.IsDeleted)
                .Select(r => new PMReportFormTypeDto
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

            if (pmReportFormType == null)
            {
                return NotFound(new { message = "PM report form type not found." });
            }

            return Ok(pmReportFormType);
        }

        // POST: api/PMReportFormType
        [HttpPost]
        public async Task<ActionResult<PMReportFormTypeDto>> CreatePMReportFormType(CreatePMReportFormTypeDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check for duplicate name
            var existingPMReportFormType = await _context.PMReportFormTypes
                .AnyAsync(r => r.Name.ToLower() == createDto.Name.ToLower() && !r.IsDeleted);

            if (existingPMReportFormType)
            {
                return BadRequest(new { message = "A PM report form type with this name already exists." });
            }

            var pmReportFormType = new PMReportFormType
            {
                ID = Guid.NewGuid(),
                Name = createDto.Name,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = null, // Set based on authentication context
                UpdatedBy = null  // Set based on authentication context
            };

            _context.PMReportFormTypes.Add(pmReportFormType);
            await _context.SaveChangesAsync();

            // Return the created record with navigation properties
            var createdRecord = await GetPMReportFormType(pmReportFormType.ID);
            return CreatedAtAction(nameof(GetPMReportFormType), new { id = pmReportFormType.ID }, createdRecord.Value);
        }

        // PUT: api/PMReportFormType/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePMReportFormType(Guid id, UpdatePMReportFormTypeDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pmReportFormType = await _context.PMReportFormTypes
                .FirstOrDefaultAsync(r => r.ID == id && !r.IsDeleted);

            if (pmReportFormType == null)
            {
                return NotFound(new { message = "PM report form type not found." });
            }

            // Check for duplicate name (excluding current record)
            var existingPMReportFormType = await _context.PMReportFormTypes
                .AnyAsync(r => r.Name.ToLower() == updateDto.Name.ToLower() && r.ID != id && !r.IsDeleted);

            if (existingPMReportFormType)
            {
                return BadRequest(new { message = "A PM report form type with this name already exists." });
            }

            // Update properties
            pmReportFormType.Name = updateDto.Name;
            pmReportFormType.UpdatedDate = DateTime.UtcNow;
            pmReportFormType.UpdatedBy = null; // Set based on authentication context

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "PM report form type updated successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The record was modified by another user. Please refresh and try again." });
            }
        }

        // DELETE: api/PMReportFormType/5 (Soft Delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePMReportFormType(Guid id)
        {
            var pmReportFormType = await _context.PMReportFormTypes
                .FirstOrDefaultAsync(r => r.ID == id && !r.IsDeleted);

            if (pmReportFormType == null)
            {
                return NotFound(new { message = "PM report form type not found." });
            }

            // Soft delete
            pmReportFormType.IsDeleted = true;
            pmReportFormType.UpdatedDate = DateTime.UtcNow;
            pmReportFormType.UpdatedBy = null; // Set based on authentication context

            await _context.SaveChangesAsync();

            return Ok(new { message = "PM report form type deleted successfully." });
        }
    }
}