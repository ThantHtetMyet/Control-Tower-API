using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.DTOs.EmployeeManagementSystem;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Controllers.EmployeeManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccupationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OccupationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Occupation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OccupationDto>>> GetOccupations()
        {
            var occupations = await _context.Occupations
                .Include(o => o.CreatedByEmployee)
                .Include(o => o.UpdatedByEmployee)
                .Where(o => !o.IsDeleted)
                .Select(o => new OccupationDto
                {
                    ID = o.ID,
                    OccupationName = o.OccupationName,
                    Description = o.Description,
                    Remark = o.Remark,
                    Rating = o.Rating,
                    CreatedDate = o.CreatedDate,
                    UpdatedDate = o.UpdatedDate,
                    CreatedByUserName = o.CreatedByEmployee != null ? $"{o.CreatedByEmployee.FirstName} {o.CreatedByEmployee.LastName}" : null,
                    UpdatedByUserName = o.UpdatedByEmployee != null ? $"{o.UpdatedByEmployee.FirstName} {o.UpdatedByEmployee.LastName}" : null
                })
                .ToListAsync();

            return Ok(occupations);
        }

        // GET: api/Occupation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OccupationDto>> GetOccupation(Guid id)
        {
            var occupation = await _context.Occupations
                .Include(o => o.CreatedByEmployee)
                .Include(o => o.UpdatedByEmployee)
                .Where(o => o.ID == id && !o.IsDeleted)
                .Select(o => new OccupationDto
                {
                    ID = o.ID,
                    OccupationName = o.OccupationName,
                    Description = o.Description,
                    Remark = o.Remark,
                    Rating = o.Rating,
                    CreatedDate = o.CreatedDate,
                    UpdatedDate = o.UpdatedDate,
                    CreatedByUserName = o.CreatedByEmployee != null ? $"{o.CreatedByEmployee.FirstName} {o.CreatedByEmployee.LastName}" : null,
                    UpdatedByUserName = o.UpdatedByEmployee != null ? $"{o.UpdatedByEmployee.FirstName} {o.UpdatedByEmployee.LastName}" : null
                })
                .FirstOrDefaultAsync();

            if (occupation == null)
            {
                return NotFound();
            }

            return occupation;
        }

        // POST: api/Occupation
        [HttpPost]
        public async Task<ActionResult<OccupationDto>> PostOccupation(CreateOccupationDto createOccupationDto)
        {
            var occupation = new Occupation
            {
                ID = Guid.NewGuid(),
                OccupationName = createOccupationDto.OccupationName,
                Description = createOccupationDto.Description,
                Remark = createOccupationDto.Remark,
                Rating = createOccupationDto.Rating,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            _context.Occupations.Add(occupation);
            await _context.SaveChangesAsync();

            var createdOccupation = await _context.Occupations
                .Include(o => o.CreatedByEmployee)
                .Include(o => o.UpdatedByEmployee)
                .Where(o => o.ID == occupation.ID)
                .Select(o => new OccupationDto
                {
                    ID = o.ID,
                    OccupationName = o.OccupationName,
                    Description = o.Description,
                    Remark = o.Remark,
                    Rating = o.Rating,
                    CreatedDate = o.CreatedDate,
                    UpdatedDate = o.UpdatedDate,
                    CreatedByUserName = o.CreatedByEmployee != null ? $"{o.CreatedByEmployee.FirstName} {o.CreatedByEmployee.LastName}" : null,
                    UpdatedByUserName = null
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction("GetOccupation", new { id = occupation.ID }, createdOccupation);
        }

        // PUT: api/Occupation/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOccupation(Guid id, Occupation occupation)
        {
            if (id != occupation.ID)
            {
                return BadRequest();
            }

            occupation.UpdatedDate = DateTime.UtcNow;
            _context.Entry(occupation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OccupationExists(id))
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

        // DELETE: api/Occupation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOccupation(Guid id)
        {
            var occupation = await _context.Occupations.FindAsync(id);
            if (occupation == null)
            {
                return NotFound();
            }

            // Check if occupation has active employees
            var hasActiveEmployees = await _context.Users
                .AnyAsync(e => e.OccupationID == id && !e.IsDeleted);

            if (hasActiveEmployees)
            {
                return BadRequest("Cannot delete occupation with active employees.");
            }

            occupation.IsDeleted = true;
            occupation.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OccupationExists(Guid id)
        {
            return _context.Occupations.Any(o => o.ID == id && !o.IsDeleted);
        }
    }
}