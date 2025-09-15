using ControlTower.Data;
using ControlTower.DTOs;
using ControlTower.DTOs.ReportManagementSystem;
using ControlTower.Models;
using ControlTower.Models.ReportManagementSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims; // Add this using statement
using Microsoft.AspNetCore.Authorization; // Add this using statement

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Add authorization attribute
    public class ReportFormController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportFormController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the current user ID from JWT token claims
        /// </summary>
        /// <returns>Current user ID or null if not found/invalid</returns>
        private Guid? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var currentUserId))
            {
                return null;
            }
            return currentUserId;
        }

        // GET: api/ReportForm
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportFormDto>>> GetReportForms()
        {
            var reportForms = await _context.ReportForms
                .Where(rf => !rf.IsDeleted)
                .Include(rf => rf.ReportFormType)
                .Include(rf => rf.CreatedByUser)
                .Include(rf => rf.UpdatedByUser)
                .Select(rf => new ReportFormDto
                {
                    ID = rf.ID,
                    ReportFormTypeID = rf.ReportFormTypeID,
                    ReportFormTypeName = rf.ReportFormType.Name,
                    IsDeleted = rf.IsDeleted,
                    CreatedDate = rf.CreatedDate,
                    UpdatedDate = rf.UpdatedDate,
                    CreatedBy = rf.CreatedBy,
                    CreatedByUserName = rf.CreatedByUser.FirstName + " " + rf.CreatedByUser.LastName,
                    UpdatedBy = rf.UpdatedBy,
                    UpdatedByUserName = rf.UpdatedByUser != null ? rf.UpdatedByUser.FirstName + " " + rf.UpdatedByUser.LastName : null,
                    UploadStatus = rf.UploadStatus,
                    UploadHostname = rf.UploadHostname,
                    UploadIPAddress = rf.UploadIPAddress,
                    FormStatus = rf.FormStatus
                })
                .ToListAsync();

            return Ok(reportForms);
        }

        // GET: api/ReportForm/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportFormDto>> GetReportForm(Guid id)
        {
            var reportForm = await _context.ReportForms
                .Where(rf => rf.ID == id && !rf.IsDeleted)
                .Include(rf => rf.ReportFormType)
                .Include(rf => rf.CreatedByUser)
                .Include(rf => rf.UpdatedByUser)
                .Select(rf => new ReportFormDto
                {
                    ID = rf.ID,
                    ReportFormTypeID = rf.ReportFormTypeID,
                    ReportFormTypeName = rf.ReportFormType.Name,
                    IsDeleted = rf.IsDeleted,
                    CreatedDate = rf.CreatedDate,
                    UpdatedDate = rf.UpdatedDate,
                    CreatedBy = rf.CreatedBy,
                    CreatedByUserName = rf.CreatedByUser.FirstName + " " + rf.CreatedByUser.LastName,
                    UpdatedBy = rf.UpdatedBy,
                    UpdatedByUserName = rf.UpdatedByUser != null ? rf.UpdatedByUser.FirstName + " " + rf.UpdatedByUser.LastName : null,
                    UploadStatus = rf.UploadStatus,
                    UploadHostname = rf.UploadHostname,
                    UploadIPAddress = rf.UploadIPAddress,
                    FormStatus = rf.FormStatus
                })
                .FirstOrDefaultAsync();

            if (reportForm == null)
            {
                return NotFound(new { message = "ReportForm not found" });
            }

            return Ok(reportForm);
        }

        // GET: api/ReportForm/ByReportFormType/5
        [HttpGet("ByReportFormType/{reportFormTypeId}")]
        public async Task<ActionResult<IEnumerable<ReportFormDto>>> GetReportFormsByType(Guid reportFormTypeId)
        {
            var reportForms = await _context.ReportForms
                .Where(rf => rf.ReportFormTypeID == reportFormTypeId && !rf.IsDeleted)
                .Include(rf => rf.ReportFormType)
                .Include(rf => rf.CreatedByUser)
                .Include(rf => rf.UpdatedByUser)
                .Select(rf => new ReportFormDto
                {
                    ID = rf.ID,
                    ReportFormTypeID = rf.ReportFormTypeID,
                    ReportFormTypeName = rf.ReportFormType.Name,
                    IsDeleted = rf.IsDeleted,
                    CreatedDate = rf.CreatedDate,
                    UpdatedDate = rf.UpdatedDate,
                    CreatedBy = rf.CreatedBy,
                    CreatedByUserName = rf.CreatedByUser.FirstName + " " + rf.CreatedByUser.LastName,
                    UpdatedBy = rf.UpdatedBy,
                    UpdatedByUserName = rf.UpdatedByUser != null ? rf.UpdatedByUser.FirstName + " " + rf.UpdatedByUser.LastName : null,
                    UploadStatus = rf.UploadStatus,
                    UploadHostname = rf.UploadHostname,
                    UploadIPAddress = rf.UploadIPAddress,
                    FormStatus = rf.FormStatus
                })
                .ToListAsync();

            return Ok(reportForms);
        }

        // POST: api/ReportForm
        [HttpPost]
        public async Task<ActionResult<ReportFormDto>> PostReportForm(CreateReportFormDto createDto)
        {
            // Get current user ID from JWT token claims
            var currentUserId = GetCurrentUserId();
            if (!currentUserId.HasValue)
            {
                return Unauthorized("User ID not found in token or invalid format");
            }

            // Validate ReportFormType exists and get the type details
            var reportFormType = await _context.ReportFormTypes
                .FirstOrDefaultAsync(rft => rft.ID == createDto.ReportFormTypeID && !rft.IsDeleted);

            if (reportFormType == null)
            {
                return BadRequest(new { message = "Invalid ReportFormTypeID" });
            }

            var reportForm = new ReportForm
            {
                ID = Guid.NewGuid(),
                ReportFormTypeID = createDto.ReportFormTypeID,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = currentUserId.Value,
                UploadStatus = createDto.UploadStatus,
                UploadHostname = createDto.UploadHostname,
                UploadIPAddress = createDto.UploadIPAddress,
                FormStatus = createDto.FormStatus
            };

            _context.ReportForms.Add(reportForm);
            await _context.SaveChangesAsync();

            // Remove this entire block that auto-creates CMReportForm
            // if (reportFormType.Name.Equals("Corrective Maintenance", StringComparison.OrdinalIgnoreCase))
            // {
            //     ... entire auto-creation logic ...
            // }
            
            // Return the created report form with related data
            var createdReportForm = await _context.ReportForms
                .Where(rf => rf.ID == reportForm.ID)
                .Include(rf => rf.ReportFormType)
                .Include(rf => rf.CreatedByUser)
                .Select(rf => new ReportFormDto
                {
                    ID = rf.ID,
                    ReportFormTypeID = rf.ReportFormTypeID,
                    ReportFormTypeName = rf.ReportFormType.Name,
                    IsDeleted = rf.IsDeleted,
                    CreatedDate = rf.CreatedDate,
                    UpdatedDate = rf.UpdatedDate,
                    CreatedBy = rf.CreatedBy,
                    CreatedByUserName = rf.CreatedByUser.FirstName + " " + rf.CreatedByUser.LastName,
                    UpdatedBy = rf.UpdatedBy,
                    UpdatedByUserName = null,
                    UploadStatus = rf.UploadStatus,
                    UploadHostname = rf.UploadHostname,
                    UploadIPAddress = rf.UploadIPAddress,
                    FormStatus = rf.FormStatus
                })
                .FirstAsync();

            return CreatedAtAction("GetReportForm", new { id = reportForm.ID }, createdReportForm);
        }

        // PUT: api/ReportForm/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportForm(Guid id, UpdateReportFormDto updateDto)
        {
            // Get current user ID from JWT token claims
            var currentUserId = GetCurrentUserId();
            if (!currentUserId.HasValue)
            {
                return Unauthorized("User ID not found in token or invalid format");
            }

            var reportForm = await _context.ReportForms
                .FirstOrDefaultAsync(rf => rf.ID == id && !rf.IsDeleted);

            if (reportForm == null)
            {
                return NotFound(new { message = "ReportForm not found" });
            }

            // Validate ReportFormType exists
            var reportFormTypeExists = await _context.ReportFormTypes
                .AnyAsync(rft => rft.ID == updateDto.ReportFormTypeID && !rft.IsDeleted);

            if (!reportFormTypeExists)
            {
                return BadRequest(new { message = "Invalid ReportFormTypeID" });
            }

            reportForm.ReportFormTypeID = updateDto.ReportFormTypeID;
            reportForm.UploadStatus = updateDto.UploadStatus;
            reportForm.UploadHostname = updateDto.UploadHostname;
            reportForm.UploadIPAddress = updateDto.UploadIPAddress;
            reportForm.FormStatus = updateDto.FormStatus;
            reportForm.UpdatedDate = DateTime.UtcNow;
            reportForm.UpdatedBy = currentUserId.Value; // Use actual current user ID

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportFormExists(id))
                {
                    return NotFound(new { message = "ReportForm not found" });
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "ReportForm updated successfully" });
        }

        // DELETE: api/ReportForm/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportForm(Guid id)
        {
            // Get current user ID from JWT token claims
            var currentUserId = GetCurrentUserId();
            if (!currentUserId.HasValue)
            {
                return Unauthorized("User ID not found in token or invalid format");
            }

            var reportForm = await _context.ReportForms
                .FirstOrDefaultAsync(rf => rf.ID == id && !rf.IsDeleted);

            if (reportForm == null)
            {
                return NotFound(new { message = "ReportForm not found" });
            }

            // Soft delete
            reportForm.IsDeleted = true;
            reportForm.UpdatedDate = DateTime.UtcNow;
            reportForm.UpdatedBy = currentUserId.Value; // Use actual current user ID

            await _context.SaveChangesAsync();

            return Ok(new { message = "ReportForm deleted successfully" });
        }

        private bool ReportFormExists(Guid id)
        {
            return _context.ReportForms.Any(rf => rf.ID == id && !rf.IsDeleted);
        }
    }
}