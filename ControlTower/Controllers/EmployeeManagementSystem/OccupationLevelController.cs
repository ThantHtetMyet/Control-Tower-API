using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.EmployeeManagementSystem;
using ControlTower.DTOs.EmployeeManagementSystem;

namespace ControlTower.Controllers.EmployeeManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class OccupationLevelController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OccupationLevelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/OccupationLevel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OccupationLevelDto>>> GetOccupationLevels()
        {
            var occupationLevels = await _context.OccupationLevels
                .Include(ol => ol.CreatedByUser)
                .Include(ol => ol.UpdatedByUser)
                .Include(ol => ol.Users.Where(e => !e.IsDeleted))
                .Where(ol => !ol.IsDeleted)
                .Select(ol => new OccupationLevelDto
                {
                    ID = ol.ID,
                    LevelName = ol.LevelName,
                    Description = ol.Description,
                    Rank = ol.Rank,
                    CreatedDate = ol.CreatedDate,
                    UpdatedDate = ol.UpdatedDate,
                    CreatedByUserName = ol.CreatedByUser != null ? ol.CreatedByUser.FirstName + " " + ol.CreatedByUser.LastName : null,
                    UpdatedByUserName = ol.UpdatedByUser != null ? ol.UpdatedByUser.FirstName + " " + ol.UpdatedByUser.LastName : null,
                    EmployeeCount = ol.Users.Count(e => !e.IsDeleted)
                })
                .OrderBy(ol => ol.Rank)
                .ToListAsync();

            return occupationLevels;
        }

        // GET: api/OccupationLevel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OccupationLevelDto>> GetOccupationLevel(Guid id)
        {
            var occupationLevel = await _context.OccupationLevels
                .Include(ol => ol.CreatedByUser)
                .Include(ol => ol.UpdatedByUser)
                .Include(ol => ol.Users.Where(e => !e.IsDeleted))
                .Where(ol => ol.ID == id && !ol.IsDeleted)
                .Select(ol => new OccupationLevelDto
                {
                    ID = ol.ID,
                    LevelName = ol.LevelName,
                    Description = ol.Description,
                    Rank = ol.Rank,
                    CreatedDate = ol.CreatedDate,
                    UpdatedDate = ol.UpdatedDate,
                    CreatedByUserName = ol.CreatedByUser != null ? ol.CreatedByUser.FirstName + " " + ol.CreatedByUser.LastName : null,
                    UpdatedByUserName = ol.UpdatedByUser != null ? ol.UpdatedByUser.FirstName + " " + ol.UpdatedByUser.LastName : null,
                    EmployeeCount = ol.Users.Count(e => !e.IsDeleted)
                })
                .FirstOrDefaultAsync();

            if (occupationLevel == null)
            {
                return NotFound();
            }

            return occupationLevel;
        }

        // POST: api/OccupationLevel
        [HttpPost]
        public async Task<ActionResult<OccupationLevelDto>> PostOccupationLevel(CreateOccupationLevelDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var occupationLevel = new OccupationLevel
            {
                ID = Guid.NewGuid(),
                LevelName = createDto.LevelName,
                Description = createDto.Description,
                Rank = createDto.Rank,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                IsDeleted = false,
                CreatedBy = Guid.Parse(createDto.CreatedBy),
                UpdatedBy = Guid.Parse(createDto.CreatedBy)
            };

            _context.OccupationLevels.Add(occupationLevel);
            await _context.SaveChangesAsync();

            var occupationLevelDto = new OccupationLevelDto
            {
                ID = occupationLevel.ID,
                LevelName = occupationLevel.LevelName,
                Description = occupationLevel.Description,
                Rank = occupationLevel.Rank,
                CreatedDate = occupationLevel.CreatedDate,
                UpdatedDate = occupationLevel.UpdatedDate,
                EmployeeCount = 0
            };

            return CreatedAtAction("GetOccupationLevel", new { id = occupationLevel.ID }, occupationLevelDto);
        }

        // PUT: api/OccupationLevel/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOccupationLevel(Guid id, UpdateOccupationLevelDto updateDto)
        {
            if (id != updateDto.ID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var occupationLevel = await _context.OccupationLevels.FindAsync(id);
            if (occupationLevel == null || occupationLevel.IsDeleted)
            {
                return NotFound();
            }

            occupationLevel.LevelName = updateDto.LevelName;
            occupationLevel.Description = updateDto.Description;
            occupationLevel.Rank = updateDto.Rank;
            occupationLevel.UpdatedDate = DateTime.UtcNow;
            
            if (!string.IsNullOrEmpty(updateDto.UpdatedBy))
            {
                occupationLevel.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);
            }

            _context.Entry(occupationLevel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OccupationLevelExists(id))
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

        // DELETE: api/OccupationLevel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOccupationLevel(Guid id)
        {
            var occupationLevel = await _context.OccupationLevels.FindAsync(id);
            if (occupationLevel == null)
            {
                return NotFound();
            }

            // Check if occupation level has active Occupations
            var hasActiveOccupations = await _context.Occupations
                .AnyAsync(o => o.OccupationLevelID == id && !o.IsDeleted);

            if (hasActiveOccupations)
            {
                return BadRequest("Cannot delete occupation level with active Occupations.");
            }

            occupationLevel.IsDeleted = true;
            occupationLevel.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OccupationLevelExists(Guid id)
        {
            return _context.OccupationLevels.Any(ol => ol.ID == id && !ol.IsDeleted);
        }
    }
}