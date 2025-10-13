using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultStatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ResultStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ResultStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultStatusDto>>> GetResultStatuses()
        {
            var resultStatuses = await _context.ResultStatuses
                .Where(rs => !rs.IsDeleted)
                .Select(rs => new ResultStatusDto
                {
                    ID = rs.ID,
                    Name = rs.Name,
                    IsDeleted = rs.IsDeleted
                })
                .ToListAsync();

            return Ok(resultStatuses);
        }

        // GET: api/ResultStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultStatusDto>> GetResultStatus(Guid id)
        {
            var resultStatus = await _context.ResultStatuses
                .Where(rs => rs.ID == id && !rs.IsDeleted)
                .Select(rs => new ResultStatusDto
                {
                    ID = rs.ID,
                    Name = rs.Name,
                    IsDeleted = rs.IsDeleted
                })
                .FirstOrDefaultAsync();

            if (resultStatus == null)
            {
                return NotFound(new { message = "Result status not found" });
            }

            return Ok(resultStatus);
        }

        // PUT: api/ResultStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResultStatus(Guid id, UpdateResultStatusDto updateDto)
        {
            var resultStatus = await _context.ResultStatuses.FindAsync(id);

            if (resultStatus == null || resultStatus.IsDeleted)
            {
                return NotFound(new { message = "Result status not found" });
            }

            resultStatus.Name = updateDto.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultStatusExists(id))
                {
                    return NotFound(new { message = "Result status not found" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ResultStatus
        [HttpPost]
        public async Task<ActionResult<ResultStatusDto>> PostResultStatus(CreateResultStatusDto createDto)
        {
            var resultStatus = new ResultStatus
            {
                ID = Guid.NewGuid(),
                Name = createDto.Name,
                IsDeleted = false
            };

            _context.ResultStatuses.Add(resultStatus);
            await _context.SaveChangesAsync();

            var result = new ResultStatusDto
            {
                ID = resultStatus.ID,
                Name = resultStatus.Name,
                IsDeleted = resultStatus.IsDeleted
            };

            return CreatedAtAction(nameof(GetResultStatus), new { id = resultStatus.ID }, result);
        }

        // DELETE: api/ResultStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResultStatus(Guid id)
        {
            var resultStatus = await _context.ResultStatuses.FindAsync(id);

            if (resultStatus == null || resultStatus.IsDeleted)
            {
                return NotFound(new { message = "Result status not found" });
            }

            // Soft delete
            resultStatus.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResultStatusExists(Guid id)
        {
            return _context.ResultStatuses.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}