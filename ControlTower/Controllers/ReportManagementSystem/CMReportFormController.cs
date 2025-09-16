using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMReportFormController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CMReportFormController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CMReportForm
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CMReportFormDto>>> GetCMReportForms()
        {
            var cmReportForms = await _context.CMReportForms
                .Include(c => c.ReportForm)
                    .ThenInclude(s => s.ReportFormType)
                .Include(c => c.FurtherActionTakenWarehouse)
                .Include(c => c.FormStatusWarehouse)
                .Include(c => c.CreatedByUser)
                .Include(c => c.UpdatedByUser)
                .Where(c => !c.IsDeleted)
                .Select(c => new CMReportFormDto
                {
                    ID = c.ID,
                    ReportFormID = c.ReportFormID,
                    FurtherActionTakenID = c.FurtherActionTakenID,
                    FormstatusID = c.FormstatusID,
                    Customer = c.Customer,
                    ProjectNo = c.ProjectNo,
                    IssueReportedDescription = c.IssueReportedDescription,
                    IssueFoundDescription = c.IssueFoundDescription,
                    ActionTakenDescription = c.ActionTakenDescription,
                    FailureDetectedDate = c.FailureDetectedDate,
                    ResponseDate = c.ResponseDate,
                    ArrivalDate = c.ArrivalDate,
                    CompletionDate = c.CompletionDate,
                    AttendedBy = c.AttendedBy,
                    ApprovedBy = c.ApprovedBy,
                    IsDeleted = c.IsDeleted,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate,
                    CreatedBy = c.CreatedBy,
                    UpdatedBy = c.UpdatedBy,
                    ReportFormTypeName = c.ReportForm.ReportFormType != null ? c.ReportForm.ReportFormType.Name : null,
                    FurtherActionTakenName = c.FurtherActionTakenWarehouse.Name,
                    FormStatusName = c.FormStatusWarehouse.Name,
                    CreatedByUserName = $"{c.CreatedByUser.FirstName} {c.CreatedByUser.LastName}",
                    UpdatedByUserName = c.UpdatedByUser != null ? $"{c.UpdatedByUser.FirstName} {c.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            return Ok(cmReportForms);
        }

        // GET: api/CMReportForm/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CMReportFormDto>> GetCMReportForm(Guid id)
        {
            var cmReportForm = await _context.CMReportForms
                .Include(c => c.ReportForm)
                    .ThenInclude(s => s.ReportFormType)
                .Include(c => c.FurtherActionTakenWarehouse)
                .Include(c => c.FormStatusWarehouse)
                .Include(c => c.CreatedByUser)
                .Include(c => c.UpdatedByUser)
                .Where(c => c.ID == id && !c.IsDeleted)
                .Select(c => new CMReportFormDto
                {
                    ID = c.ID,
                    ReportFormID = c.ReportFormID,
                    FurtherActionTakenID = c.FurtherActionTakenID,
                    FormstatusID = c.FormstatusID,
                    Customer = c.Customer,
                    ProjectNo = c.ProjectNo,
                    IssueReportedDescription = c.IssueReportedDescription,
                    IssueFoundDescription = c.IssueFoundDescription,
                    ActionTakenDescription = c.ActionTakenDescription,
                    FailureDetectedDate = c.FailureDetectedDate,
                    ResponseDate = c.ResponseDate,
                    ArrivalDate = c.ArrivalDate,
                    CompletionDate = c.CompletionDate,
                    AttendedBy = c.AttendedBy,
                    ApprovedBy = c.ApprovedBy,
                    IsDeleted = c.IsDeleted,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate,
                    CreatedBy = c.CreatedBy,
                    UpdatedBy = c.UpdatedBy,
                    ReportFormTypeName = c.ReportForm.ReportFormType != null ? c.ReportForm.ReportFormType.Name : null,
                    FurtherActionTakenName = c.FurtherActionTakenWarehouse.Name,
                    FormStatusName = c.FormStatusWarehouse.Name,
                    CreatedByUserName = $"{c.CreatedByUser.FirstName} {c.CreatedByUser.LastName}",
                    UpdatedByUserName = c.UpdatedByUser != null ? $"{c.UpdatedByUser.FirstName} {c.UpdatedByUser.LastName}" : null
                })
                .FirstOrDefaultAsync();

            if (cmReportForm == null)
            {
                return NotFound(new { message = "CM Report Form not found." });
            }

            return Ok(cmReportForm);
        }

        // GET: api/CMReportForm/ByReportForm/5
        [HttpGet("ByReportForm/{reportFormId}")]
        public async Task<ActionResult<IEnumerable<CMReportFormDto>>> GetCMReportFormsByReportForm(Guid reportFormId)
        {
            var cmReportForms = await _context.CMReportForms
                .Include(c => c.ReportForm)
                    .ThenInclude(s => s.ReportFormType)
                .Include(c => c.FurtherActionTakenWarehouse)
                .Include(c => c.FormStatusWarehouse)
                .Include(c => c.CreatedByUser)
                .Include(c => c.UpdatedByUser)
                .Where(c => c.ReportFormID == reportFormId && !c.IsDeleted)
                .Select(c => new CMReportFormDto
                {
                    ID = c.ID,
                    ReportFormID = c.ReportFormID,
                    FurtherActionTakenID = c.FurtherActionTakenID,
                    FormstatusID = c.FormstatusID,
                    Customer = c.Customer,
                    ProjectNo = c.ProjectNo,
                    // Removed: SystemDescription = c.SystemDescription,
                    IssueReportedDescription = c.IssueReportedDescription,
                    IssueFoundDescription = c.IssueFoundDescription,
                    ActionTakenDescription = c.ActionTakenDescription,
                    FailureDetectedDate = c.FailureDetectedDate,
                    ResponseDate = c.ResponseDate,
                    ArrivalDate = c.ArrivalDate,
                    CompletionDate = c.CompletionDate,
                    AttendedBy = c.AttendedBy,
                    ApprovedBy = c.ApprovedBy,
                    IsDeleted = c.IsDeleted,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate,
                    CreatedBy = c.CreatedBy,
                    UpdatedBy = c.UpdatedBy,
                    ReportFormTypeName = c.ReportForm.ReportFormType != null ? c.ReportForm.ReportFormType.Name : null,
                    FurtherActionTakenName = c.FurtherActionTakenWarehouse.Name,
                    FormStatusName = c.FormStatusWarehouse.Name,
                    CreatedByUserName = $"{c.CreatedByUser.FirstName} {c.CreatedByUser.LastName}",
                    UpdatedByUserName = c.UpdatedByUser != null ? $"{c.UpdatedByUser.FirstName} {c.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            return Ok(cmReportForms);
        }

        // POST: api/CMReportForm
        [HttpPost]
        public async Task<ActionResult<CMReportFormDto>> CreateCMReportForm(CreateCMReportFormDto createDto)
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

            var furtherActionExists = await _context.FurtherActionTakenWarehouses
                .AnyAsync(f => f.ID == createDto.FurtherActionTakenID && !f.IsDeleted);
            if (!furtherActionExists)
            {
                return BadRequest(new { message = "Invalid Further Action Taken ID." });
            }

            var formStatusExists = await _context.FormStatusWarehouses
                .AnyAsync(f => f.ID == createDto.FormstatusID && !f.IsDeleted);
            if (!formStatusExists)
            {
                return BadRequest(new { message = "Invalid Form Status ID." });
            } 

            var userExists = await _context.Users
                .AnyAsync(u => u.ID == createDto.CreatedBy && !u.IsDeleted);
            if (!userExists)
            {
                return BadRequest(new { message = "Invalid User ID." });
            }

            var cmReportForm = new CMReportForm
            {
                ID = Guid.NewGuid(),
                ReportFormID = createDto.ReportFormID,
                FurtherActionTakenID = createDto.FurtherActionTakenID,
                FormstatusID = createDto.FormstatusID,
                Customer = createDto.Customer,
                ProjectNo = createDto.ProjectNo,
                IssueReportedDescription = createDto.IssueReportedDescription,
                IssueFoundDescription = createDto.IssueFoundDescription,
                ActionTakenDescription = createDto.ActionTakenDescription,
                FailureDetectedDate = createDto.FailureDetectedDate,
                ResponseDate = createDto.ResponseDate,
                ArrivalDate = createDto.ArrivalDate,
                CompletionDate = createDto.CompletionDate,
                AttendedBy = createDto.AttendedBy,
                ApprovedBy = createDto.ApprovedBy,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = createDto.CreatedBy,
                Remark = createDto.Remark,
                UpdatedBy = null
            };

            _context.CMReportForms.Add(cmReportForm);
            await _context.SaveChangesAsync();

            // Return the created record with navigation properties
            var createdRecord = await GetCMReportForm(cmReportForm.ID);
            return CreatedAtAction(nameof(GetCMReportForm), new { id = cmReportForm.ID }, createdRecord);
        }

        // PUT: api/CMReportForm/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCMReportForm(Guid id, UpdateCMReportFormDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cmReportForm = await _context.CMReportForms
                .FirstOrDefaultAsync(c => c.ID == id && !c.IsDeleted);

            if (cmReportForm == null)
            {
                return NotFound(new { message = "CM Report Form not found." });
            }

            // Validate foreign key relationships
            var furtherActionExists = await _context.FurtherActionTakenWarehouses
                .AnyAsync(f => f.ID == updateDto.FurtherActionTakenID && !f.IsDeleted);
            if (!furtherActionExists)
            {
                return BadRequest(new { message = "Invalid Further Action Taken ID." });
            }

            var formStatusExists = await _context.FormStatusWarehouses
                .AnyAsync(f => f.ID == updateDto.FormstatusID && !f.IsDeleted);
            if (!formStatusExists)
            {
                return BadRequest(new { message = "Invalid Form Status ID." });
            }

            if (updateDto.UpdatedBy.HasValue)
            {
                var userExists = await _context.Users
                    .AnyAsync(u => u.ID == updateDto.UpdatedBy && !u.IsDeleted);
                if (!userExists)
                {
                    return BadRequest(new { message = "Invalid Updated By User ID." });
                }
            }

            // Update properties
            cmReportForm.FurtherActionTakenID = updateDto.FurtherActionTakenID;
            cmReportForm.FormstatusID = updateDto.FormstatusID;
            cmReportForm.Customer = updateDto.Customer;
            cmReportForm.ProjectNo = updateDto.ProjectNo;
            cmReportForm.IssueReportedDescription = updateDto.IssueReportedDescription;
            cmReportForm.IssueFoundDescription = updateDto.IssueFoundDescription;
            cmReportForm.ActionTakenDescription = updateDto.ActionTakenDescription;
            cmReportForm.FailureDetectedDate = updateDto.FailureDetectedDate;
            cmReportForm.ResponseDate = updateDto.ResponseDate;
            cmReportForm.ArrivalDate = updateDto.ArrivalDate;
            cmReportForm.CompletionDate = updateDto.CompletionDate;
            cmReportForm.AttendedBy = updateDto.AttendedBy;
            cmReportForm.ApprovedBy = updateDto.ApprovedBy;
            cmReportForm.UpdatedDate = DateTime.UtcNow;
            cmReportForm.UpdatedBy = updateDto.UpdatedBy;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "CM Report Form updated successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The record was modified by another user. Please refresh and try again." });
            }
        }

        // DELETE: api/CMReportForm/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCMReportForm(Guid id)
        {
            var cmReportForm = await _context.CMReportForms
                .FirstOrDefaultAsync(c => c.ID == id && !c.IsDeleted);

            if (cmReportForm == null)
            {
                return NotFound(new { message = "CM Report Form not found." });
            }

            // Soft delete
            cmReportForm.IsDeleted = true;
            cmReportForm.UpdatedDate = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "CM Report Form deleted successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The record was modified by another user. Please refresh and try again." });
            }
        }
    }
}