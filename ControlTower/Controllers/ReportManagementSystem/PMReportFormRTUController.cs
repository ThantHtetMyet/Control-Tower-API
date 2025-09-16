using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class PMReportFormRTUController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PMReportFormRTUController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PMReportForm
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PMReportFormRTUDto>>> GetPMReportFormRTU()
        {
            var PMReportFormRTU = await _context.PMReportFormRTU
                .Include(p => p.PMReportFormType)
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Where(p => !p.IsDeleted)
                .Select(p => new PMReportFormRTUDto
                {
                    ID = p.ID,
                    ReportFormID = p.ReportFormID,
                    PMReportFormTypeID = p.PMReportFormTypeID,
                    ProjectNo = p.ProjectNo,
                    Customer = p.Customer,
                    DateOfService = p.DateOfService,
                    CleaningOfCabinet = p.CleaningOfCabinet,
                    Remarks = p.Remarks,
                    AttendedBy = p.AttendedBy,
                    ApprovedBy = p.ApprovedBy,
                    IsDeleted = p.IsDeleted,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    CreatedBy = p.CreatedBy,
                    UpdatedBy = p.UpdatedBy,
                    PMReportFormTypeName = p.PMReportFormType != null ? p.PMReportFormType.Name : null,
                    CreatedByUserName = p.CreatedByUser != null ? $"{p.CreatedByUser.FirstName} {p.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = p.UpdatedByUser != null ? $"{p.UpdatedByUser.FirstName} {p.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            return Ok(PMReportFormRTU);
        }

        // GET: api/PMReportForm/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PMReportFormRTUDto>> GetPMReportForm(Guid id)
        {
            var pmReportForm = await _context.PMReportFormRTU
                .Include(p => p.PMReportFormType)
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Where(p => p.ID == id && !p.IsDeleted)
                .Select(p => new PMReportFormRTUDto
                {
                    ID = p.ID,
                    ReportFormID = p.ReportFormID,
                    PMReportFormTypeID = p.PMReportFormTypeID,
                    ProjectNo = p.ProjectNo,
                    Customer = p.Customer,
                    DateOfService = p.DateOfService,
                    CleaningOfCabinet = p.CleaningOfCabinet,
                    Remarks = p.Remarks,
                    AttendedBy = p.AttendedBy,
                    ApprovedBy = p.ApprovedBy,
                    IsDeleted = p.IsDeleted,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    CreatedBy = p.CreatedBy,
                    UpdatedBy = p.UpdatedBy,
                    PMReportFormTypeName = p.PMReportFormType != null ? p.PMReportFormType.Name : null,
                    CreatedByUserName = p.CreatedByUser != null ? $"{p.CreatedByUser.FirstName} {p.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = p.UpdatedByUser != null ? $"{p.UpdatedByUser.FirstName} {p.UpdatedByUser.LastName}" : null
                })
                .FirstOrDefaultAsync();

            if (pmReportForm == null)
            {
                return NotFound(new { message = "PM report form not found." });
            }

            return Ok(pmReportForm);
        }

        // POST: api/PMReportForm
        [HttpPost]
        public async Task<ActionResult<PMReportFormRTUDto>> CreatePMReportForm(CreatePMReportFormRTUDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pmReportForm = new PMReportFormRTU
            {
                ID = Guid.NewGuid(),
                ReportFormID = createDto.ReportFormID,
                PMReportFormTypeID = createDto.PMReportFormTypeID,
                ProjectNo = createDto.ProjectNo,
                Customer = createDto.Customer,
                DateOfService = createDto.DateOfService,
                CleaningOfCabinet = createDto.CleaningOfCabinet,
                Remarks = createDto.Remarks,
                AttendedBy = createDto.AttendedBy,
                ApprovedBy = createDto.ApprovedBy,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = null, // Set based on authentication context
                UpdatedBy = null  // Set based on authentication context
            };

            _context.PMReportFormRTU.Add(pmReportForm);
            await _context.SaveChangesAsync();

            var createdRecord = await GetPMReportForm(pmReportForm.ID);
            return CreatedAtAction(nameof(GetPMReportForm), new { id = pmReportForm.ID }, createdRecord.Value);
        }

        // PUT: api/PMReportForm/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePMReportForm(Guid id, UpdatePMReportFormRTUDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pmReportForm = await _context.PMReportFormRTU
                .FirstOrDefaultAsync(p => p.ID == id && !p.IsDeleted);

            if (pmReportForm == null)
            {
                return NotFound(new { message = "PM report form not found." });
            }
            pmReportForm.ProjectNo = updateDto.ProjectNo;
            pmReportForm.Customer = updateDto.Customer;
            pmReportForm.DateOfService = updateDto.DateOfService;
            pmReportForm.CleaningOfCabinet = updateDto.CleaningOfCabinet;
            pmReportForm.Remarks = updateDto.Remarks;
            pmReportForm.AttendedBy = updateDto.AttendedBy;
            pmReportForm.ApprovedBy = updateDto.ApprovedBy;
            pmReportForm.UpdatedDate = DateTime.UtcNow;
            pmReportForm.UpdatedBy = null; // Set based on authentication context

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "PM report form updated successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The record was modified by another user. Please refresh and try again." });
            }
        }

        // DELETE: api/PMReportForm/5 (Soft Delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePMReportForm(Guid id)
        {
            var pmReportForm = await _context.PMReportFormRTU
                .FirstOrDefaultAsync(p => p.ID == id && !p.IsDeleted);

            if (pmReportForm == null)
            {
                return NotFound(new { message = "PM report form not found." });
            }

            // Soft delete
            pmReportForm.IsDeleted = true;
            pmReportForm.UpdatedDate = DateTime.UtcNow;
            pmReportForm.UpdatedBy = null; // Set based on authentication context

            await _context.SaveChangesAsync();

            return Ok(new { message = "PM report form deleted successfully." });
        }
    }
}