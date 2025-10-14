using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerDiskStatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServerDiskStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ServerDiskStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServerDiskStatusDto>>> GetServerDiskStatuses()
        {
            var serverDiskStatuses = await _context.ServerDiskStatuses
                .Where(sds => !sds.IsDeleted)
                .Select(sds => new ServerDiskStatusDto
                {
                    ID = sds.ID,
                    Name = sds.Name,
                    IsDeleted = sds.IsDeleted
                })
                .ToListAsync();

            return Ok(serverDiskStatuses);
        }

        // GET: api/ServerDiskStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServerDiskStatusDto>> GetServerDiskStatus(Guid id)
        {
            var serverDiskStatus = await _context.ServerDiskStatuses
                .Where(sds => sds.ID == id && !sds.IsDeleted)
                .Select(sds => new ServerDiskStatusDto
                {
                    ID = sds.ID,
                    Name = sds.Name,
                    IsDeleted = sds.IsDeleted
                })
                .FirstOrDefaultAsync();

            if (serverDiskStatus == null)
            {
                return NotFound(new { message = "Server disk status not found" });
            }

            return Ok(serverDiskStatus);
        }

        // PUT: api/ServerDiskStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServerDiskStatus(Guid id, UpdateServerDiskStatusDto updateDto)
        {
            var serverDiskStatus = await _context.ServerDiskStatuses.FindAsync(id);

            if (serverDiskStatus == null || serverDiskStatus.IsDeleted)
            {
                return NotFound(new { message = "Server disk status not found" });
            }

            serverDiskStatus.Name = updateDto.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServerDiskStatusExists(id))
                {
                    return NotFound(new { message = "Server disk status not found" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ServerDiskStatus
        [HttpPost]
        public async Task<ActionResult<ServerDiskStatusDto>> PostServerDiskStatus(CreateServerDiskStatusDto createDto)
        {
            var serverDiskStatus = new ServerDiskStatus
            {
                ID = Guid.NewGuid(),
                Name = createDto.Name,
                IsDeleted = false
            };

            _context.ServerDiskStatuses.Add(serverDiskStatus);
            await _context.SaveChangesAsync();

            var result = new ServerDiskStatusDto
            {
                ID = serverDiskStatus.ID,
                Name = serverDiskStatus.Name,
                IsDeleted = serverDiskStatus.IsDeleted
            };

            return CreatedAtAction(nameof(GetServerDiskStatus), new { id = serverDiskStatus.ID }, result);
        }

        // DELETE: api/ServerDiskStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServerDiskStatus(Guid id)
        {
            var serverDiskStatus = await _context.ServerDiskStatuses.FindAsync(id);

            if (serverDiskStatus == null || serverDiskStatus.IsDeleted)
            {
                return NotFound(new { message = "Server disk status not found" });
            }

            // Soft delete
            serverDiskStatus.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServerDiskStatusExists(Guid id)
        {
            return _context.ServerDiskStatuses.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}