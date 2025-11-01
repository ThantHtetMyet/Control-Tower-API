using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class PMServerReportFormPDFRequestLogController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PMServerReportFormPDFRequestLogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PMServerReportFormPDFRequestLog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PMServerReportFormPDFRequestLogDto>>> GetPMServerReportFormPDFRequestLogs()
        {
            var logs = await _context.PMServerReportFormPDFRequestLogs
                .Include(l => l.RequestedByUser)
                .Include(l => l.PMReportFormServer)
                .Select(l => new PMServerReportFormPDFRequestLogDto
                {
                    ID = l.ID,
                    PMReportFormServerID = l.PMReportFormServerID,
                    RequestedBy = l.RequestedBy,
                    RequestedDate = l.RequestedDate,
                    CreatedDate = l.CreatedDate,
                    RequestedByUserName = l.RequestedByUser.FirstName + " " + l.RequestedByUser.LastName,
                    PMReportFormServerTitle = l.PMReportFormServer.ReportTitle
                })
                .ToListAsync();

            return Ok(logs);
        }

        // GET: api/PMServerReportFormPDFRequestLog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PMServerReportFormPDFRequestLogDto>> GetPMServerReportFormPDFRequestLog(Guid id)
        {
            var log = await _context.PMServerReportFormPDFRequestLogs
                .Include(l => l.RequestedByUser)
                .Include(l => l.PMReportFormServer)
                .Where(l => l.ID == id)
                .Select(l => new PMServerReportFormPDFRequestLogDto
                {
                    ID = l.ID,
                    PMReportFormServerID = l.PMReportFormServerID,
                    RequestedBy = l.RequestedBy,
                    RequestedDate = l.RequestedDate,
                    CreatedDate = l.CreatedDate,
                    RequestedByUserName = l.RequestedByUser.FirstName + " " + l.RequestedByUser.LastName,
                    PMReportFormServerTitle = l.PMReportFormServer.ReportTitle
                })
                .FirstOrDefaultAsync();

            if (log == null)
            {
                return NotFound();
            }

            return Ok(log);
        }

        // GET: api/PMServerReportFormPDFRequestLog/ByPMReportFormServer/5
        [HttpGet("ByPMReportFormServer/{pmReportFormServerId}")]
        public async Task<ActionResult<IEnumerable<PMServerReportFormPDFRequestLogDto>>> GetPMServerReportFormPDFRequestLogsByPMReportFormServer(Guid pmReportFormServerId)
        {
            var logs = await _context.PMServerReportFormPDFRequestLogs
                .Include(l => l.RequestedByUser)
                .Include(l => l.PMReportFormServer)
                .Where(l => l.PMReportFormServerID == pmReportFormServerId)
                .Select(l => new PMServerReportFormPDFRequestLogDto
                {
                    ID = l.ID,
                    PMReportFormServerID = l.PMReportFormServerID,
                    RequestedBy = l.RequestedBy,
                    RequestedDate = l.RequestedDate,
                    CreatedDate = l.CreatedDate,
                    RequestedByUserName = l.RequestedByUser.FirstName + " " + l.RequestedByUser.LastName,
                    PMReportFormServerTitle = l.PMReportFormServer.ReportTitle
                })
                .OrderByDescending(l => l.RequestedDate)
                .ToListAsync();

            return Ok(logs);
        }

        // POST: api/PMServerReportFormPDFRequestLog
        [HttpPost]
        public async Task<ActionResult<PMServerReportFormPDFRequestLogDto>> PostPMServerReportFormPDFRequestLog(CreatePMServerReportFormPDFRequestLogDto createDto)
        {
            // Validate that ReportForm exists
            var reportForm = await _context.ReportForms
                .FirstOrDefaultAsync(r => r.ID == createDto.ReportFormID);
            if (reportForm == null)
            {
                return BadRequest("ReportForm not found.");
            }

            // Find the corresponding PMReportFormServer using ReportFormID
            var pmReportFormServer = await _context.PMReportFormServer
                .FirstOrDefaultAsync(p => p.ReportFormID == createDto.ReportFormID);
            if (pmReportFormServer == null)
            {
                return BadRequest("PMReportFormServer not found for the given ReportForm.");
            }

            // Validate that User exists
            var userExists = await _context.Users
                .AnyAsync(u => u.ID == createDto.RequestedBy);
            if (!userExists)
            {
                return BadRequest("User not found.");
            }

            var log = new PMServerReportFormPDFRequestLog
            {
                ID = Guid.NewGuid(),
                PMReportFormServerID = pmReportFormServer.ID, // Use the found PMReportFormServerID
                RequestedBy = createDto.RequestedBy,
                RequestedDate = createDto.RequestedDate,
                CreatedDate = DateTime.UtcNow
            };

            _context.PMServerReportFormPDFRequestLogs.Add(log);
            await _context.SaveChangesAsync();

            // Return the created log with navigation properties
            var createdLog = await _context.PMServerReportFormPDFRequestLogs
                .Include(l => l.RequestedByUser)
                .Include(l => l.PMReportFormServer)
                .Where(l => l.ID == log.ID)
                .Select(l => new PMServerReportFormPDFRequestLogDto
                {
                    ID = l.ID,
                    PMReportFormServerID = l.PMReportFormServerID,
                    RequestedBy = l.RequestedBy,
                    RequestedDate = l.RequestedDate,
                    CreatedDate = l.CreatedDate,
                    RequestedByUserName = l.RequestedByUser.FirstName + " " + l.RequestedByUser.LastName,
                    PMReportFormServerTitle = l.PMReportFormServer.ReportTitle
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction("GetPMServerReportFormPDFRequestLog", new { id = log.ID }, createdLog);
        }

        // PUT: api/PMServerReportFormPDFRequestLog/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPMServerReportFormPDFRequestLog(Guid id, UpdatePMServerReportFormPDFRequestLogDto updateDto)
        {
            if (id != updateDto.ID)
            {
                return BadRequest();
            }

            var log = await _context.PMServerReportFormPDFRequestLogs.FindAsync(id);
            if (log == null)
            {
                return NotFound();
            }

            // Validate that PMReportFormServer exists
            var pmReportFormServerExists = await _context.PMReportFormServer
                .AnyAsync(p => p.ID == updateDto.PMReportFormServerID);
            if (!pmReportFormServerExists)
            {
                return BadRequest("PMReportFormServer not found.");
            }

            // Validate that User exists
            var userExists = await _context.Users
                .AnyAsync(u => u.ID == updateDto.RequestedBy);
            if (!userExists)
            {
                return BadRequest("User not found.");
            }

            log.PMReportFormServerID = updateDto.PMReportFormServerID;
            log.RequestedBy = updateDto.RequestedBy;
            log.RequestedDate = updateDto.RequestedDate;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PMServerReportFormPDFRequestLogExists(id))
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

        // DELETE: api/PMServerReportFormPDFRequestLog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePMServerReportFormPDFRequestLog(Guid id)
        {
            var log = await _context.PMServerReportFormPDFRequestLogs.FindAsync(id);
            if (log == null)
            {
                return NotFound();
            }

            _context.PMServerReportFormPDFRequestLogs.Remove(log);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PMServerReportFormPDFRequestLogExists(Guid id)
        {
            return _context.PMServerReportFormPDFRequestLogs.Any(e => e.ID == id);
        }
    }
}