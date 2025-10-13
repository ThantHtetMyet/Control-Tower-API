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
    [Authorize]
    public class PMReportFormServerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PMReportFormServerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PMReportFormServer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PMReportFormServerDto>>> GetPMReportFormServer()
        {
            var PMReportFormServer = await _context.PMReportFormServer
                .Include(p => p.ReportForm)
                    .ThenInclude(r => r.ReportFormType)
                .Include(p => p.ReportForm)
                    .ThenInclude(r => r.SystemNameWarehouse)
                .Include(p => p.ReportForm)
                    .ThenInclude(r => r.StationNameWarehouse)
                .Include(p => p.PMReportFormType)
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Where(p => !p.IsDeleted)
                .Select(p => new PMReportFormServerDto
                {
                    ID = p.ID,
                    ReportFormID = p.ReportFormID,
                    PMReportFormTypeID = p.PMReportFormTypeID,
                    ProjectNo = p.ProjectNo,
                    Customer = p.Customer,
                    ReportTitle = p.ReportTitle,
                    AttendedBy = p.AttendedBy,
                    WitnessedBy = p.WitnessedBy,
                    StartDate = p.StartDate,
                    CompletionDate = p.CompletionDate,
                    Remarks = p.Remarks,
                    IsDeleted = p.IsDeleted,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    CreatedBy = p.CreatedBy,
                    UpdatedBy = p.UpdatedBy,
                    PMReportFormTypeName = p.PMReportFormType.Name,
                    CreatedByUserName = $"{p.CreatedByUser.FirstName} {p.CreatedByUser.LastName}",
                    UpdatedByUserName = p.UpdatedByUser != null ? $"{p.UpdatedByUser.FirstName} {p.UpdatedByUser.LastName}" : null,
                    JobNo = p.ReportForm.JobNo,
                    StationName = p.ReportForm.StationNameWarehouse.Name,
                    SystemDescription = p.ReportForm.SystemNameWarehouse.Name
                })
                .ToListAsync();

            return Ok(PMReportFormServer);
        }

        // GET: api/PMReportFormServer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PMReportFormServerDto>> GetPMReportFormServer(Guid id)
        {
            var pmReportFormServer = await _context.PMReportFormServer
                .Include(p => p.ReportForm)
                    .ThenInclude(r => r.ReportFormType)
                .Include(p => p.ReportForm)
                    .ThenInclude(r => r.SystemNameWarehouse)
                .Include(p => p.ReportForm)
                    .ThenInclude(r => r.StationNameWarehouse)
                .Include(p => p.PMReportFormType)
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Where(p => p.ID == id && !p.IsDeleted)
                .Select(p => new PMReportFormServerDto
                {
                    ID = p.ID,
                    ReportFormID = p.ReportFormID,
                    PMReportFormTypeID = p.PMReportFormTypeID,
                    ProjectNo = p.ProjectNo,
                    Customer = p.Customer,
                    ReportTitle = p.ReportTitle,
                    AttendedBy = p.AttendedBy,
                    WitnessedBy = p.WitnessedBy,
                    StartDate = p.StartDate,
                    CompletionDate = p.CompletionDate,
                    Remarks = p.Remarks,
                    IsDeleted = p.IsDeleted,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    CreatedBy = p.CreatedBy,
                    UpdatedBy = p.UpdatedBy,
                    PMReportFormTypeName = p.PMReportFormType.Name,
                    CreatedByUserName = $"{p.CreatedByUser.FirstName} {p.CreatedByUser.LastName}",
                    UpdatedByUserName = p.UpdatedByUser != null ? $"{p.UpdatedByUser.FirstName} {p.UpdatedByUser.LastName}" : null,
                    JobNo = p.ReportForm.JobNo,
                    StationName = p.ReportForm.StationNameWarehouse.Name,
                    SystemDescription = p.ReportForm.SystemNameWarehouse.Name
                })
                .FirstOrDefaultAsync();

            if (pmReportFormServer == null)
            {
                return NotFound();
            }

            return Ok(pmReportFormServer);
        }

        // POST: api/PMReportFormServer
        [HttpPost]
        public async Task<ActionResult<PMReportFormServerDto>> PostPMReportFormServer(CreatePMReportFormServerDto createDto)
        {
            // Validate that ReportForm exists
            var reportFormExists = await _context.ReportForms.AnyAsync(r => r.ID == createDto.ReportFormID && !r.IsDeleted);
            if (!reportFormExists)
            {
                return BadRequest("Invalid ReportFormID");
            }

            // Validate that PMReportFormType exists
            var pmReportFormTypeExists = await _context.PMReportFormTypes.AnyAsync(t => t.ID == createDto.PMReportFormTypeID && !t.IsDeleted);
            if (!pmReportFormTypeExists)
            {
                return BadRequest("Invalid PMReportFormTypeID");
            }

            // Validate that CreatedBy user exists
            var userExists = await _context.Users.AnyAsync(u => u.ID == createDto.CreatedBy && !u.IsDeleted);
            if (!userExists)
            {
                return BadRequest("Invalid CreatedBy user");
            }

            var pmReportFormServer = new PMReportFormServer
            {
                ID = Guid.NewGuid(),
                ReportFormID = createDto.ReportFormID,
                PMReportFormTypeID = createDto.PMReportFormTypeID,
                ProjectNo = createDto.ProjectNo,
                Customer = createDto.Customer,
                ReportTitle = createDto.ReportTitle,
                AttendedBy = createDto.AttendedBy,
                WitnessedBy = createDto.WitnessedBy,
                StartDate = createDto.StartDate,
                CompletionDate = createDto.CompletionDate,
                Remarks = createDto.Remarks,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = createDto.CreatedBy
            };

            _context.PMReportFormServer.Add(pmReportFormServer);
            await _context.SaveChangesAsync();

            var responseDto = new PMReportFormServerDto
            {
                ID = pmReportFormServer.ID,
                ReportFormID = pmReportFormServer.ReportFormID,
                PMReportFormTypeID = pmReportFormServer.PMReportFormTypeID,
                ProjectNo = pmReportFormServer.ProjectNo,
                Customer = pmReportFormServer.Customer,
                ReportTitle = pmReportFormServer.ReportTitle,
                AttendedBy = pmReportFormServer.AttendedBy,
                WitnessedBy = pmReportFormServer.WitnessedBy,
                StartDate = pmReportFormServer.StartDate,
                CompletionDate = pmReportFormServer.CompletionDate,
                Remarks = pmReportFormServer.Remarks,
                IsDeleted = pmReportFormServer.IsDeleted,
                CreatedDate = pmReportFormServer.CreatedDate,
                UpdatedDate = pmReportFormServer.UpdatedDate,
                CreatedBy = pmReportFormServer.CreatedBy,
                UpdatedBy = pmReportFormServer.UpdatedBy
            };

            return CreatedAtAction(nameof(GetPMReportFormServer), new { id = pmReportFormServer.ID }, responseDto);
        }

        // PUT: api/PMReportFormServer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPMReportFormServer(Guid id, UpdatePMReportFormServerDto updateDto)
        {
            var pmReportFormServer = await _context.PMReportFormServer.FindAsync(id);
            if (pmReportFormServer == null || pmReportFormServer.IsDeleted)
            {
                return NotFound();
            }

            // Validate that PMReportFormType exists
            var pmReportFormTypeExists = await _context.PMReportFormTypes.AnyAsync(t => t.ID == updateDto.PMReportFormTypeID && !t.IsDeleted);
            if (!pmReportFormTypeExists)
            {
                return BadRequest("Invalid PMReportFormTypeID");
            }

            // Validate that UpdatedBy user exists if provided
            if (updateDto.UpdatedBy.HasValue)
            {
                var userExists = await _context.Users.AnyAsync(u => u.ID == updateDto.UpdatedBy.Value && !u.IsDeleted);
                if (!userExists)
                {
                    return BadRequest("Invalid UpdatedBy user");
                }
            }

            pmReportFormServer.PMReportFormTypeID = updateDto.PMReportFormTypeID;
            pmReportFormServer.ProjectNo = updateDto.ProjectNo;
            pmReportFormServer.Customer = updateDto.Customer;
            pmReportFormServer.ReportTitle = updateDto.ReportTitle;
            pmReportFormServer.AttendedBy = updateDto.AttendedBy;
            pmReportFormServer.WitnessedBy = updateDto.WitnessedBy;
            pmReportFormServer.StartDate = updateDto.StartDate;
            pmReportFormServer.CompletionDate = updateDto.CompletionDate;
            pmReportFormServer.Remarks = updateDto.Remarks;
            pmReportFormServer.UpdatedDate = DateTime.UtcNow;
            pmReportFormServer.UpdatedBy = updateDto.UpdatedBy;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PMReportFormServerExists(id))
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

        // DELETE: api/PMReportFormServer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePMReportFormServer(Guid id)
        {
            var pmReportFormServer = await _context.PMReportFormServer.FindAsync(id);
            if (pmReportFormServer == null || pmReportFormServer.IsDeleted)
            {
                return NotFound();
            }

            // Soft delete
            pmReportFormServer.IsDeleted = true;
            pmReportFormServer.UpdatedDate = DateTime.UtcNow;

            // Get current user ID from claims if available
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(currentUserId, out Guid userId))
            {
                pmReportFormServer.UpdatedBy = userId;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PMReportFormServerExists(Guid id)
        {
            return _context.PMReportFormServer.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}