using ControlTower.Data;
using ControlTower.DTOs.ReportManagementSystem;
using ControlTower.Models.ReportManagementSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SystemNameWarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SystemNameWarehouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        private Guid? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var currentUserId))
            {
                return null;
            }
            return currentUserId;
        }

        // GET: api/SystemNameWarehouse
        [HttpGet]
        public async Task<ActionResult<object>> GetSystemNameWarehouses(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string search = "")
        {
            var query = _context.SystemNameWarehouses
                .Where(s => !s.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.Name.Contains(search));
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var systemNames = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new SystemNameWarehouseDto
                {
                    ID = s.ID,
                    Name = s.Name,
                    IsDeleted = s.IsDeleted,
                    
                })
                .ToListAsync();

            return Ok(new
            {
                data = systemNames,
                totalCount,
                totalPages,
                currentPage = page,
                pageSize
            });
        }

        // GET: api/SystemNameWarehouse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SystemNameWarehouseDto>> GetSystemNameWarehouse(Guid id)
        {
            var systemName = await _context.SystemNameWarehouses
                .Where(s => s.ID == id && !s.IsDeleted)
                .Select(s => new SystemNameWarehouseDto
                {
                    ID = s.ID,
                    Name = s.Name,
                    IsDeleted = s.IsDeleted,
                   
                })
                .FirstOrDefaultAsync();

            if (systemName == null)
            {
                return NotFound(new { message = "SystemNameWarehouse not found" });
            }

            return Ok(systemName);
        }

        // POST: api/SystemNameWarehouse
        [HttpPost]
        public async Task<ActionResult<SystemNameWarehouseDto>> CreateSystemNameWarehouse(CreateSystemNameWarehouseDto createDto)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var systemName = new SystemNameWarehouse
            {
                ID = Guid.NewGuid(),
                Name = createDto.Name,
                IsDeleted = false,
            };

            _context.SystemNameWarehouses.Add(systemName);
            await _context.SaveChangesAsync();

            var result = await _context.SystemNameWarehouses
                .Where(s => s.ID == systemName.ID)
                .Select(s => new SystemNameWarehouseDto
                {
                    ID = s.ID,
                    Name = s.Name,
                    IsDeleted = s.IsDeleted,
                    UpdatedByUserName = null
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction(nameof(GetSystemNameWarehouse), new { id = systemName.ID }, result);
        }

        // PUT: api/SystemNameWarehouse/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSystemNameWarehouse(Guid id, UpdateSystemNameWarehouseDto updateDto)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var systemName = await _context.SystemNameWarehouses.FindAsync(id);
            if (systemName == null || systemName.IsDeleted)
            {
                return NotFound(new { message = "SystemNameWarehouse not found" });
            }

            systemName.Name = updateDto.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SystemNameWarehouseExists(id))
                {
                    return NotFound(new { message = "SystemNameWarehouse not found" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/SystemNameWarehouse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSystemNameWarehouse(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var systemName = await _context.SystemNameWarehouses.FindAsync(id);
            if (systemName == null || systemName.IsDeleted)
            {
                return NotFound(new { message = "SystemNameWarehouse not found" });
            }

            systemName.IsDeleted = true;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SystemNameWarehouseExists(Guid id)
        {
            return _context.SystemNameWarehouses.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}