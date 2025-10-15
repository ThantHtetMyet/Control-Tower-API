using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class ASAFirewallStatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ASAFirewallStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ASAFirewallStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ASAFirewallStatusDto>>> GetASAFirewallStatuses()
        {
            var asaFirewallStatuses = await _context.ASAFirewallStatuses
                .Where(afs => !afs.IsDeleted)
                .OrderBy(afs => afs.SortOrder)
                .Select(afs => new ASAFirewallStatusDto
                {
                    ID = afs.ID,
                    Name = afs.Name,
                    Description = afs.Description,
                    CommandInput = afs.CommandInput,
                    SortOrder = afs.SortOrder,
                    IsDeleted = afs.IsDeleted
                })
                .ToListAsync();

            return Ok(asaFirewallStatuses);
        }

        // GET: api/ASAFirewallStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ASAFirewallStatusDto>> GetASAFirewallStatus(Guid id)
        {
            var asaFirewallStatus = await _context.ASAFirewallStatuses
                .Where(afs => afs.ID == id && !afs.IsDeleted)
                .Select(afs => new ASAFirewallStatusDto
                {
                    ID = afs.ID,
                    Name = afs.Name,
                    Description = afs.Description,
                    CommandInput = afs.CommandInput,
                    SortOrder = afs.SortOrder,
                    IsDeleted = afs.IsDeleted
                })
                .FirstOrDefaultAsync();

            if (asaFirewallStatus == null)
            {
                return NotFound(new { message = "ASA Firewall Status not found" });
            }

            return Ok(asaFirewallStatus);
        }

        // PUT: api/ASAFirewallStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutASAFirewallStatus(Guid id, UpdateASAFirewallStatusDto updateDto)
        {
            var asaFirewallStatus = await _context.ASAFirewallStatuses.FindAsync(id);

            if (asaFirewallStatus == null || asaFirewallStatus.IsDeleted)
            {
                return NotFound(new { message = "ASA Firewall Status not found" });
            }

            asaFirewallStatus.Name = updateDto.Name;
            asaFirewallStatus.Description = updateDto.Description;
            asaFirewallStatus.CommandInput = updateDto.CommandInput;
            asaFirewallStatus.SortOrder = updateDto.SortOrder;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ASAFirewallStatusExists(id))
                {
                    return NotFound(new { message = "ASA Firewall Status not found" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ASAFirewallStatus
        [HttpPost]
        public async Task<ActionResult<ASAFirewallStatusDto>> PostASAFirewallStatus(CreateASAFirewallStatusDto createDto)
        {
            var asaFirewallStatus = new ASAFirewallStatus
            {
                ID = Guid.NewGuid(),
                Name = createDto.Name,
                Description = createDto.Description,
                CommandInput = createDto.CommandInput,
                SortOrder = createDto.SortOrder,
                IsDeleted = false
            };

            _context.ASAFirewallStatuses.Add(asaFirewallStatus);
            await _context.SaveChangesAsync();

            var result = new ASAFirewallStatusDto
            {
                ID = asaFirewallStatus.ID,
                Name = asaFirewallStatus.Name,
                Description = asaFirewallStatus.Description,
                CommandInput = asaFirewallStatus.CommandInput,
                SortOrder = asaFirewallStatus.SortOrder,
                IsDeleted = asaFirewallStatus.IsDeleted
            };

            return CreatedAtAction(nameof(GetASAFirewallStatus), new { id = asaFirewallStatus.ID }, result);
        }

        // DELETE: api/ASAFirewallStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteASAFirewallStatus(Guid id)
        {
            var asaFirewallStatus = await _context.ASAFirewallStatuses.FindAsync(id);

            if (asaFirewallStatus == null || asaFirewallStatus.IsDeleted)
            {
                return NotFound(new { message = "ASA Firewall Status not found" });
            }

            // Soft delete
            asaFirewallStatus.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ASAFirewallStatusExists(Guid id)
        {
            return _context.ASAFirewallStatuses.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}