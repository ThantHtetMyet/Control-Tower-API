using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;
using System.Security.Claims;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMReportFormTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CMReportFormTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CMReportFormType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CMReportFormTypeDto>>> GetCMReportFormTypes()
        {
            var cmReportFormTypes = await _context.CMReportFormTypes
                .Include(c => c.CreatedByUser)
                .Include(c => c.UpdatedByUser)
                .Where(c => !c.IsDeleted)
                .Select(c => new CMReportFormTypeDto
                {
                    ID = c.ID,
                    Name = c.Name,
                    IsDeleted = c.IsDeleted,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate,
                    CreatedBy = c.CreatedBy,
                    UpdatedBy = c.UpdatedBy,
                    CreatedByUserName = c.CreatedByUser != null ? $"{c.CreatedByUser.FirstName} {c.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = c.UpdatedByUser != null ? $"{c.UpdatedByUser.FirstName} {c.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            return Ok(cmReportFormTypes);
        }

        // GET: api/CMReportFormType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CMReportFormTypeDto>> GetCMReportFormType(Guid id)
        {
            var cmReportFormType = await _context.CMReportFormTypes
                .Include(c => c.CreatedByUser)
                .Include(c => c.UpdatedByUser)
                .Where(c => c.ID == id && !c.IsDeleted)
                .Select(c => new CMReportFormTypeDto
                {
                    ID = c.ID,
                    Name = c.Name,
                    IsDeleted = c.IsDeleted,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate,
                    CreatedBy = c.CreatedBy,
                    UpdatedBy = c.UpdatedBy,
                    CreatedByUserName = c.CreatedByUser != null ? $"{c.CreatedByUser.FirstName} {c.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = c.UpdatedByUser != null ? $"{c.UpdatedByUser.FirstName} {c.UpdatedByUser.LastName}" : null
                })
                .FirstOrDefaultAsync();

            if (cmReportFormType == null)
            {
                return NotFound(new { message = "CM Report Form Type not found." });
            }

            return Ok(cmReportFormType);
        }

        // POST: api/CMReportFormType
        [HttpPost]
        public async Task<ActionResult<CMReportFormTypeDto>> CreateCMReportFormType(CreateCMReportFormTypeDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check for duplicate name
            var existingCMReportFormType = await _context.CMReportFormTypes
                .AnyAsync(c => c.Name.ToLower() == createDto.Name.ToLower() && !c.IsDeleted);

            if (existingCMReportFormType)
            {
                return BadRequest(new { message = "A CM Report Form Type with this name already exists." });
            }

            // Get current user ID from claims (if authentication is implemented)
            var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            Guid? currentUserId = currentUserIdClaim != null ? Guid.Parse(currentUserIdClaim.Value) : null;

            var cmReportFormType = new CMReportFormType
            {
                ID = Guid.NewGuid(),
                Name = createDto.Name,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = currentUserId,
                UpdatedBy = currentUserId
            };

            _context.CMReportFormTypes.Add(cmReportFormType);
            await _context.SaveChangesAsync();

            // Return the created record with navigation properties
            var createdRecord = await GetCMReportFormType(cmReportFormType.ID);
            return CreatedAtAction(nameof(GetCMReportFormType), new { id = cmReportFormType.ID }, createdRecord.Value);
        }

        // PUT: api/CMReportFormType/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCMReportFormType(Guid id, UpdateCMReportFormTypeDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cmReportFormType = await _context.CMReportFormTypes
                .FirstOrDefaultAsync(c => c.ID == id && !c.IsDeleted);

            if (cmReportFormType == null)
            {
                return NotFound(new { message = "CM Report Form Type not found." });
            }

            // Check for duplicate name (excluding current record)
            var existingCMReportFormType = await _context.CMReportFormTypes
                .AnyAsync(c => c.Name.ToLower() == updateDto.Name.ToLower() && c.ID != id && !c.IsDeleted);

            if (existingCMReportFormType)
            {
                return BadRequest(new { message = "A CM Report Form Type with this name already exists." });
            }

            // Get current user ID from claims (if authentication is implemented)
            var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            Guid? currentUserId = currentUserIdClaim != null ? Guid.Parse(currentUserIdClaim.Value) : null;

            // Update properties
            cmReportFormType.Name = updateDto.Name;
            cmReportFormType.UpdatedDate = DateTime.UtcNow;
            cmReportFormType.UpdatedBy = currentUserId;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "CM Report Form Type updated successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The record was modified by another user. Please refresh and try again." });
            }
        }

        // DELETE: api/CMReportFormType/5 (Soft Delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCMReportFormType(Guid id)
        {
            var cmReportFormType = await _context.CMReportFormTypes
                .FirstOrDefaultAsync(c => c.ID == id && !c.IsDeleted);

            if (cmReportFormType == null)
            {
                return NotFound(new { message = "CM Report Form Type not found." });
            }

            // Get current user ID from claims (if authentication is implemented)
            var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            Guid? currentUserId = currentUserIdClaim != null ? Guid.Parse(currentUserIdClaim.Value) : null;

            // Soft delete
            cmReportFormType.IsDeleted = true;
            cmReportFormType.UpdatedDate = DateTime.UtcNow;
            cmReportFormType.UpdatedBy = currentUserId;

            await _context.SaveChangesAsync();

            return Ok(new { message = "CM Report Form Type deleted successfully." });
        }
    }
}