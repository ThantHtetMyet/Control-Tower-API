using ControlTower.Data;
using ControlTower.DTOs.ReportManagementSystem;
using ControlTower.Models.ReportManagementSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.IO;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportFormFinalReportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public ReportFormFinalReportController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private static string? BuildUserName(Models.EmployeeManagementSystem.User? user)
        {
            if (user == null)
            {
                return null;
            }

            return $"{user.FirstName} {user.LastName}".Trim();
        }

        private IQueryable<ReportFormFinalReportDto> ProjectFinalReports()
        {
            return _context.ReportFormFinalReports
                .Include(r => r.ReportForm)
                    .ThenInclude(rf => rf.ReportFormType)
                .Include(r => r.UploadedByUser)
                .Include(r => r.CreatedByUser)
                .Include(r => r.UpdatedByUser)
                .Where(r => !r.IsDeleted)
                .Select(r => new ReportFormFinalReportDto
                {
                    ID = r.ID,
                    ReportFormID = r.ReportFormID,
                    AttachmentName = r.AttachmentName,
                    AttachmentPath = r.AttachmentPath,
                    IsDeleted = r.IsDeleted,
                    UploadedDate = r.UploadedDate,
                    UploadedBy = r.UploadedBy,
                    CreatedDate = r.CreatedDate,
                    CreatedBy = r.CreatedBy,
                    UpdatedDate = r.UpdatedDate,
                    UpdatedBy = r.UpdatedBy,
                    ReportFormTypeName = r.ReportForm.ReportFormType != null ? r.ReportForm.ReportFormType.Name : null,
                    UploadedByUserName = BuildUserName(r.UploadedByUser),
                    CreatedByUserName = BuildUserName(r.CreatedByUser),
                    UpdatedByUserName = BuildUserName(r.UpdatedByUser)
                });
        }

        public class UploadFinalReportRequest
        {
            public Guid ReportFormId { get; set; }
            public IFormFile FinalReportFile { get; set; } = default!;
        }

        // POST: api/ReportFormFinalReport/upload
        [HttpPost("upload")]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ReportFormFinalReportDto>> UploadFinalReport([FromForm] UploadFinalReportRequest request)
        {
            if (request.FinalReportFile == null || request.FinalReportFile.Length == 0)
            {
                return BadRequest(new { message = "Final report file is required." });
            }

            var reportForm = await _context.ReportForms
                .FirstOrDefaultAsync(r => r.ID == request.ReportFormId && !r.IsDeleted);
            if (reportForm == null)
            {
                return BadRequest(new { message = "Invalid ReportFormID." });
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out Guid userId))
            {
                return Unauthorized(new { message = "User context is not available." });
            }

            var basePath = _configuration["ReportManagementSystemFileStorage:BasePath"] ??
                           "C:\\Temp\\ReportFormFiles";
            var reportFolder = Path.Combine(basePath, request.ReportFormId.ToString());
            var finalReportFolder = Path.Combine(reportFolder, "ReportForm_FinalReport");
            Directory.CreateDirectory(finalReportFolder);

            var originalName = Path.GetFileName(request.FinalReportFile.FileName);
            var safeName = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}_{originalName}";
            var savedPath = Path.Combine(finalReportFolder, safeName);
            var relativePath = Path.Combine(request.ReportFormId.ToString(), "ReportForm_FinalReport", safeName);

            using (var stream = new FileStream(savedPath, FileMode.Create))
            {
                await request.FinalReportFile.CopyToAsync(stream);
            }

            var entity = new ReportFormFinalReport
            {
                ID = Guid.NewGuid(),
                ReportFormID = request.ReportFormId,
                AttachmentName = originalName,
                AttachmentPath = relativePath,
                IsDeleted = false,
                UploadedDate = DateTime.UtcNow,
                UploadedBy = userId,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = userId
            };

            _context.ReportFormFinalReports.Add(entity);
            await _context.SaveChangesAsync();

            var created = await ProjectFinalReports()
                .FirstOrDefaultAsync(r => r.ID == entity.ID);

            return CreatedAtAction(nameof(GetReportFormFinalReport), new { id = entity.ID }, created);
        }

        // GET: api/ReportFormFinalReport
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportFormFinalReportDto>>> GetReportFormFinalReports()
        {
            var items = await ProjectFinalReports()
                .OrderByDescending(r => r.UploadedDate)
                .ToListAsync();

            return Ok(items);
        }

        // GET: api/ReportFormFinalReport/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportFormFinalReportDto>> GetReportFormFinalReport(Guid id)
        {
            var item = await ProjectFinalReports()
                .FirstOrDefaultAsync(r => r.ID == id);

            if (item == null)
            {
                return NotFound(new { message = "Final report not found." });
            }

            return Ok(item);
        }

        // GET: api/ReportFormFinalReport/ByReportForm/5
        [HttpGet("ByReportForm/{reportFormId}")]
        public async Task<ActionResult<IEnumerable<ReportFormFinalReportDto>>> GetByReportForm(Guid reportFormId)
        {
            var items = await ProjectFinalReports()
                .Where(r => r.ReportFormID == reportFormId)
                .OrderByDescending(r => r.UploadedDate)
                .ToListAsync();

            return Ok(items);
        }

        // GET: api/ReportFormFinalReport/download/5
        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadFinalReport(Guid id)
        {
            var entity = await _context.ReportFormFinalReports
                .FirstOrDefaultAsync(r => r.ID == id && !r.IsDeleted);

            if (entity == null)
            {
                return NotFound(new { message = "Final report not found." });
            }

            var basePath = _configuration["ReportManagementSystemFileStorage:BasePath"] ??
                           "C:\\Temp\\ReportFormFiles";

            var storedPath = entity.AttachmentPath ?? string.Empty;
            var physicalPath = Path.IsPathRooted(storedPath)
                ? storedPath
                : Path.Combine(basePath, storedPath);

            if (string.IsNullOrWhiteSpace(physicalPath) || !System.IO.File.Exists(physicalPath))
            {
                return NotFound(new { message = "Final report file is missing on the server." });
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(physicalPath);
            var fileName = string.IsNullOrWhiteSpace(entity.AttachmentName)
                ? Path.GetFileName(physicalPath)
                : entity.AttachmentName;

            return File(fileBytes, "application/pdf", fileName);
        }

        // POST: api/ReportFormFinalReport
        [HttpPost]
        public async Task<ActionResult<ReportFormFinalReportDto>> CreateReportFormFinalReport(CreateReportFormFinalReportDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reportForm = await _context.ReportForms
                .FirstOrDefaultAsync(r => r.ID == createDto.ReportFormID && !r.IsDeleted);
            if (reportForm == null)
            {
                return BadRequest(new { message = "Invalid ReportFormID." });
            }

            var uploaderExists = await _context.Users
                .AnyAsync(u => u.ID == createDto.UploadedBy && !u.IsDeleted);
            if (!uploaderExists)
            {
                return BadRequest(new { message = "Invalid UploadedBy user ID." });
            }

            var creatorExists = await _context.Users
                .AnyAsync(u => u.ID == createDto.CreatedBy && !u.IsDeleted);
            if (!creatorExists)
            {
                return BadRequest(new { message = "Invalid CreatedBy user ID." });
            }

            var entity = new ReportFormFinalReport
            {
                ID = Guid.NewGuid(),
                ReportFormID = createDto.ReportFormID,
                AttachmentName = createDto.AttachmentName,
                AttachmentPath = createDto.AttachmentPath,
                UploadedBy = createDto.UploadedBy,
                CreatedBy = createDto.CreatedBy,
                UploadedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.ReportFormFinalReports.Add(entity);
            await _context.SaveChangesAsync();

            var created = await ProjectFinalReports()
                .FirstOrDefaultAsync(r => r.ID == entity.ID);

            return CreatedAtAction(nameof(GetReportFormFinalReport), new { id = entity.ID }, created);
        }

        // PUT: api/ReportFormFinalReport/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReportFormFinalReport(Guid id, UpdateReportFormFinalReportDto updateDto)
        {
            if (id != updateDto.ID)
            {
                return BadRequest(new { message = "ID mismatch." });
            }

            var entity = await _context.ReportFormFinalReports
                .FirstOrDefaultAsync(r => r.ID == id && !r.IsDeleted);

            if (entity == null)
            {
                return NotFound(new { message = "Final report not found." });
            }

            var uploaderExists = await _context.Users
                .AnyAsync(u => u.ID == updateDto.UploadedBy && !u.IsDeleted);
            if (!uploaderExists)
            {
                return BadRequest(new { message = "Invalid UploadedBy user ID." });
            }

            var updaterExists = await _context.Users
                .AnyAsync(u => u.ID == updateDto.UpdatedBy && !u.IsDeleted);
            if (!updaterExists)
            {
                return BadRequest(new { message = "Invalid UpdatedBy user ID." });
            }

            entity.AttachmentName = updateDto.AttachmentName;
            entity.AttachmentPath = updateDto.AttachmentPath;
            entity.UploadedBy = updateDto.UploadedBy;
            entity.UploadedDate = updateDto.UploadedDate ?? entity.UploadedDate;
            entity.UpdatedBy = updateDto.UpdatedBy;
            entity.UpdatedDate = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportFormFinalReportExists(id))
                {
                    return NotFound(new { message = "Final report not found." });
                }

                throw;
            }
        }

        // DELETE: api/ReportFormFinalReport/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportFormFinalReport(Guid id)
        {
            var entity = await _context.ReportFormFinalReports
                .FirstOrDefaultAsync(r => r.ID == id && !r.IsDeleted);

            if (entity == null)
            {
                return NotFound(new { message = "Final report not found." });
            }

            entity.IsDeleted = true;
            entity.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Final report deleted successfully." });
        }

        private bool ReportFormFinalReportExists(Guid id)
        {
            return _context.ReportFormFinalReports.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}
