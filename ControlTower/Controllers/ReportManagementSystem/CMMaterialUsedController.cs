using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMMaterialUsedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CMMaterialUsedController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CMMaterialUsed
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CMMaterialUsedDto>>> GetCMMaterialUsed()
        {
            var cmmaterialUsed = await _context.CMMaterialUsed
                .Include(cm => cm.CMReportForm)
                .Include(cm => cm.CreatedByUser)
                .Include(cm => cm.UpdatedByUser)
                .Where(cm => !cm.IsDeleted)
                .Select(cm => new CMMaterialUsedDto
                {
                    ID = cm.ID,
                    CMReportFormID = cm.CMReportFormID,
                    CMReportFormName = cm.CMReportForm.ProjectNo, // Using ProjectNo as identifier
                    ItemDescription = cm.ItemDescription,
                    NewSerialNo = cm.NewSerialNo,
                    OldSerialNo = cm.OldSerialNo,
                    Remark = cm.Remark,
                    IsDeleted = cm.IsDeleted,
                    CreatedDate = cm.CreatedDate,
                    UpdatedDate = cm.UpdatedDate,
                    CreatedBy = cm.CreatedBy,
                    CreatedByName = $"{cm.CreatedByUser.FirstName} {cm.CreatedByUser.LastName}",
                    UpdatedBy = cm.UpdatedBy,
                    UpdatedByName = cm.UpdatedByUser != null ? $"{cm.UpdatedByUser.FirstName} {cm.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            return Ok(cmmaterialUsed);
        }

        // GET: api/CMMaterialUsed/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CMMaterialUsedDto>> GetCMMaterialUsed(Guid id)
        {
            var cmmaterialUsed = await _context.CMMaterialUsed
                .Include(cm => cm.CMReportForm)
                .Include(cm => cm.CreatedByUser)
                .Include(cm => cm.UpdatedByUser)
                .Where(cm => cm.ID == id && !cm.IsDeleted)
                .Select(cm => new CMMaterialUsedDto
                {
                    ID = cm.ID,
                    CMReportFormID = cm.CMReportFormID,
                    CMReportFormName = cm.CMReportForm.ProjectNo,
                    ItemDescription = cm.ItemDescription,
                    NewSerialNo = cm.NewSerialNo,
                    OldSerialNo = cm.OldSerialNo,
                    Remark = cm.Remark,
                    IsDeleted = cm.IsDeleted,
                    CreatedDate = cm.CreatedDate,
                    UpdatedDate = cm.UpdatedDate,
                    CreatedBy = cm.CreatedBy,
                    CreatedByName = $"{cm.CreatedByUser.FirstName} {cm.CreatedByUser.LastName}",
                    UpdatedBy = cm.UpdatedBy,
                    UpdatedByName = cm.UpdatedByUser != null ? $"{cm.UpdatedByUser.FirstName} {cm.UpdatedByUser.LastName}" : null
                })
                .FirstOrDefaultAsync();

            if (cmmaterialUsed == null)
            {
                return NotFound(new { message = "CM Material Used record not found." });
            }

            return Ok(cmmaterialUsed);
        }

        // GET: api/CMMaterialUsed/bycmreportform/5
        [HttpGet("bycmreportform/{cmReportFormId}")]
        public async Task<ActionResult<IEnumerable<CMMaterialUsedDto>>> GetCMMaterialUsedByCMReportForm(Guid cmReportFormId)
        {
            var cmmaterialUsed = await _context.CMMaterialUsed
                .Include(cm => cm.CMReportForm)
                .Include(cm => cm.CreatedByUser)
                .Include(cm => cm.UpdatedByUser)
                .Where(cm => cm.CMReportFormID == cmReportFormId && !cm.IsDeleted)
                .Select(cm => new CMMaterialUsedDto
                {
                    ID = cm.ID,
                    CMReportFormID = cm.CMReportFormID,
                    CMReportFormName = cm.CMReportForm.ProjectNo,
                    ItemDescription = cm.ItemDescription,
                    NewSerialNo = cm.NewSerialNo,
                    OldSerialNo = cm.OldSerialNo,
                    Remark = cm.Remark,
                    IsDeleted = cm.IsDeleted,
                    CreatedDate = cm.CreatedDate,
                    UpdatedDate = cm.UpdatedDate,
                    CreatedBy = cm.CreatedBy,
                    CreatedByName = $"{cm.CreatedByUser.FirstName} {cm.CreatedByUser.LastName}",
                    UpdatedBy = cm.UpdatedBy,
                    UpdatedByName = cm.UpdatedByUser != null ? $"{cm.UpdatedByUser.FirstName} {cm.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            return Ok(cmmaterialUsed);
        }

        // POST: api/CMMaterialUsed
        [HttpPost]
        public async Task<ActionResult<CMMaterialUsedDto>> CreateCMMaterialUsed(CreateCMMaterialUsedDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate foreign key relationships
            var cmReportFormExists = await _context.CMReportForms
                .AnyAsync(cm => cm.ID == createDto.CMReportFormID && !cm.IsDeleted);
            if (!cmReportFormExists)
            {
                return BadRequest(new { message = "Invalid CM Report Form ID." });
            }

            var userExists = await _context.Users
                .AnyAsync(u => u.ID == createDto.CreatedBy && !u.IsDeleted);
            if (!userExists)
            {
                return BadRequest(new { message = "Invalid Created By User ID." });
            }

            var cmmaterialUsed = new CMMaterialUsed
            {
                ID = Guid.NewGuid(),
                CMReportFormID = createDto.CMReportFormID,
                ItemDescription = createDto.ItemDescription,
                NewSerialNo = createDto.NewSerialNo,
                OldSerialNo = createDto.OldSerialNo,
                Remark = createDto.Remark,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = createDto.CreatedBy
            };

            _context.CMMaterialUsed.Add(cmmaterialUsed);
            await _context.SaveChangesAsync();

            // Return the created record with navigation properties
            var createdRecord = await GetCMMaterialUsed(cmmaterialUsed.ID);
            return CreatedAtAction(nameof(GetCMMaterialUsed), new { id = cmmaterialUsed.ID }, createdRecord.Value);
        }

        // PUT: api/CMMaterialUsed/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCMMaterialUsed(Guid id, UpdateCMMaterialUsedDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cmmaterialUsed = await _context.CMMaterialUsed
                .FirstOrDefaultAsync(cm => cm.ID == id && !cm.IsDeleted);

            if (cmmaterialUsed == null)
            {
                return NotFound(new { message = "CM Material Used record not found." });
            }

            // Validate Updated By User
            var userExists = await _context.Users
                .AnyAsync(u => u.ID == updateDto.UpdatedBy && !u.IsDeleted);
            if (!userExists)
            {
                return BadRequest(new { message = "Invalid Updated By User ID." });
            }

            // Update properties
            cmmaterialUsed.ItemDescription = updateDto.ItemDescription;
            cmmaterialUsed.NewSerialNo = updateDto.NewSerialNo;
            cmmaterialUsed.OldSerialNo = updateDto.OldSerialNo;
            cmmaterialUsed.Remark = updateDto.Remark;
            cmmaterialUsed.UpdatedDate = DateTime.UtcNow;
            cmmaterialUsed.UpdatedBy = updateDto.UpdatedBy;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "CM Material Used record updated successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The record was modified by another user. Please refresh and try again." });
            }
        }

        // DELETE: api/CMMaterialUsed/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCMMaterialUsed(Guid id)
        {
            var cmmaterialUsed = await _context.CMMaterialUsed
                .FirstOrDefaultAsync(cm => cm.ID == id && !cm.IsDeleted);

            if (cmmaterialUsed == null)
            {
                return NotFound(new { message = "CM Material Used record not found." });
            }

            // Soft delete
            cmmaterialUsed.IsDeleted = true;
            cmmaterialUsed.UpdatedDate = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "CM Material Used record deleted successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The record was modified by another user. Please refresh and try again." });
            }
        }
    }
}