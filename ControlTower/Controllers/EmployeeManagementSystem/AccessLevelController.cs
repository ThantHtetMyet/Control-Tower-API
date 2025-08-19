using ControlTower.Data;
using ControlTower.DTOs.EmployeeManagementSystem;
using ControlTower.Models.EmployeeManagementSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControlTower.Controllers.EmployeeManagementSystem
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccessLevelController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AccessLevelController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AccessLevel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccessLevelDto>>> GetAccessLevels()
        {
            var accessLevels = await _context.AccessLevels
                .Where(al => !al.IsDeleted)
                .Include(al => al.CreatedByUser)
                .Include(al => al.UpdatedByUser)
                .Select(al => new AccessLevelDto
                {
                    ID = al.ID,
                    LevelName = al.LevelName,
                    Description = al.Description,
                    CreatedDate = al.CreatedDate,
                    UpdatedDate = al.UpdatedDate,
                    CreatedByUserName = al.CreatedByUser != null ?
                        al.CreatedByUser.FirstName + " " + al.CreatedByUser.LastName : null,
                    UpdatedByUserName = al.UpdatedByUser != null ?
                        al.UpdatedByUser.FirstName + " " + al.UpdatedByUser.LastName : null
                })
                .ToListAsync();

            return Ok(accessLevels);
        }

        // GET: api/AccessLevel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccessLevelDto>> GetAccessLevel(Guid id)
        {
            var accessLevel = await _context.AccessLevels
                .Where(al => al.ID == id && !al.IsDeleted)
                .Include(al => al.CreatedByUser)
                .Include(al => al.UpdatedByUser)
                .Select(al => new AccessLevelDto
                {
                    ID = al.ID,
                    LevelName = al.LevelName,
                    Description = al.Description,
                    CreatedDate = al.CreatedDate,
                    UpdatedDate = al.UpdatedDate,
                    CreatedByUserName = al.CreatedByUser != null ?
                        al.CreatedByUser.FirstName + " " + al.CreatedByUser.LastName : null,
                    UpdatedByUserName = al.UpdatedByUser != null ?
                        al.UpdatedByUser.FirstName + " " + al.UpdatedByUser.LastName : null
                })
                .FirstOrDefaultAsync();

            if (accessLevel == null)
            {
                return NotFound();
            }

            return Ok(accessLevel);
        }

        // PUT: api/AccessLevel/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAccessLevel(Guid id, UpdateAccessLevelDto updateDto)
        {
            if (id != updateDto.ID)
            {
                return BadRequest();
            }

            var accessLevel = await _context.AccessLevels
                .FirstOrDefaultAsync(al => al.ID == id && !al.IsDeleted);

            if (accessLevel == null)
            {
                return NotFound();
            }

            // Check if LevelName already exists (excluding current record)
            var existingAccessLevel = await _context.AccessLevels
                .FirstOrDefaultAsync(al => al.LevelName == updateDto.LevelName && al.ID != id && !al.IsDeleted);

            if (existingAccessLevel != null)
            {
                return BadRequest("Access level name already exists.");
            }

            accessLevel.LevelName = updateDto.LevelName;
            accessLevel.Description = updateDto.Description;
            accessLevel.UpdatedDate = DateTime.UtcNow;
            accessLevel.UpdatedBy = updateDto.UpdatedBy;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccessLevelExists(id))
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

        // POST: api/AccessLevel
        [HttpPost]
        public async Task<ActionResult<AccessLevel>> PostAccessLevel(CreateAccessLevelDto createDto)
        {
            // Check if LevelName already exists
            var existingAccessLevel = await _context.AccessLevels
                .FirstOrDefaultAsync(al => al.LevelName == createDto.LevelName && !al.IsDeleted);

            if (existingAccessLevel != null)
            {
                return BadRequest("Access level name already exists.");
            }

            var accessLevel = new AccessLevel
            {
                ID = Guid.NewGuid(),
                LevelName = createDto.LevelName,
                Description = createDto.Description,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = createDto.CreatedBy
            };

            _context.AccessLevels.Add(accessLevel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAccessLevel", new { id = accessLevel.ID }, accessLevel);
        }

        // DELETE: api/AccessLevel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccessLevel(Guid id)
        {
            var accessLevel = await _context.AccessLevels
                .FirstOrDefaultAsync(al => al.ID == id && !al.IsDeleted);

            if (accessLevel == null)
            {
                return NotFound();
            }

            // Check if this access level is being used by any employee application access
            var isInUse = await _context.UserApplicationAccesses
                .AnyAsync(eaa => eaa.AccessLevelID == id && !eaa.IsDeleted);

            if (isInUse)
            {
                return BadRequest("Cannot delete access level as it is currently being used by employee application accesses.");
            }

            // Soft delete
            accessLevel.IsDeleted = true;
            accessLevel.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccessLevelExists(Guid id)
        {
            return _context.AccessLevels.Any(al => al.ID == id && !al.IsDeleted);
        }
    }
}
