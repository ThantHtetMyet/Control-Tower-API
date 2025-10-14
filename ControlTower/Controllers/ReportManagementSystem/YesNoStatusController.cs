using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class YesNoStatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public YesNoStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/YesNoStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<YesNoStatusDto>>> GetYesNoStatuses()
        {
            var yesNoStatuses = await _context.YesNoStatuses
                .Where(yns => !yns.IsDeleted)
                .Select(yns => new YesNoStatusDto
                {
                    ID = yns.ID,
                    Name = yns.Name,
                    IsDeleted = yns.IsDeleted
                })
                .ToListAsync();

            return Ok(yesNoStatuses);
        }

        // GET: api/YesNoStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<YesNoStatusDto>> GetYesNoStatus(Guid id)
        {
            var yesNoStatus = await _context.YesNoStatuses
                .Where(yns => yns.ID == id && !yns.IsDeleted)
                .Select(yns => new YesNoStatusDto
                {
                    ID = yns.ID,
                    Name = yns.Name,
                    IsDeleted = yns.IsDeleted
                })
                .FirstOrDefaultAsync();

            if (yesNoStatus == null)
            {
                return NotFound(new { message = "Yes/No status not found" });
            }

            return Ok(yesNoStatus);
        }

        // PUT: api/YesNoStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYesNoStatus(Guid id, UpdateYesNoStatusDto updateDto)
        {
            var yesNoStatus = await _context.YesNoStatuses.FindAsync(id);

            if (yesNoStatus == null || yesNoStatus.IsDeleted)
            {
                return NotFound(new { message = "Yes/No status not found" });
            }

            yesNoStatus.Name = updateDto.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!YesNoStatusExists(id))
                {
                    return NotFound(new { message = "Yes/No status not found" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/YesNoStatus
        [HttpPost]
        public async Task<ActionResult<YesNoStatusDto>> PostYesNoStatus(CreateYesNoStatusDto createDto)
        {
            var yesNoStatus = new YesNoStatus
            {
                ID = Guid.NewGuid(),
                Name = createDto.Name,
                IsDeleted = false
            };

            _context.YesNoStatuses.Add(yesNoStatus);
            await _context.SaveChangesAsync();

            var result = new YesNoStatusDto
            {
                ID = yesNoStatus.ID,
                Name = yesNoStatus.Name,
                IsDeleted = yesNoStatus.IsDeleted
            };

            return CreatedAtAction(nameof(GetYesNoStatus), new { id = yesNoStatus.ID }, result);
        }

        // DELETE: api/YesNoStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYesNoStatus(Guid id)
        {
            var yesNoStatus = await _context.YesNoStatuses.FindAsync(id);

            if (yesNoStatus == null || yesNoStatus.IsDeleted)
            {
                return NotFound(new { message = "Yes/No status not found" });
            }

            // Soft delete
            yesNoStatus.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool YesNoStatusExists(Guid id)
        {
            return _context.YesNoStatuses.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}