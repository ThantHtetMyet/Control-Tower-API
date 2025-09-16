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
    public class StationNameWarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StationNameWarehouseController(ApplicationDbContext context)
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

        // GET: api/StationNameWarehouse
        [HttpGet]
        public async Task<ActionResult<object>> GetStationNameWarehouses(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string search = "",
            [FromQuery] Guid? SystemNameWarehouseID = null)
        {
            var query = _context.StationNameWarehouses
                .Where(s => !s.IsDeleted)
                .Include(s => s.SystemNameWarehouse)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.Name.Contains(search) || s.SystemNameWarehouse.Name.Contains(search));
            }

            if (SystemNameWarehouseID.HasValue)
            {
                query = query.Where(s => s.SystemNameWarehouseID == SystemNameWarehouseID.Value);
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var stationNames = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new StationNameWarehouseDto
                {
                    ID = s.ID,
                    SystemNameWarehouseID = s.SystemNameWarehouseID,
                    SystemNameWarehouseName = s.SystemNameWarehouse.Name,
                    Name = s.Name,
                    IsDeleted = s.IsDeleted,
                })
                .ToListAsync();

            return Ok(new
            {
                data = stationNames,
                totalCount,
                totalPages,
                currentPage = page,
                pageSize
            });
        }

        // GET: api/StationNameWarehouse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StationNameWarehouseDto>> GetStationNameWarehouse(Guid id)
        {
            var stationName = await _context.StationNameWarehouses
                .Where(s => s.ID == id && !s.IsDeleted)
                .Include(s => s.SystemNameWarehouse)
                .Select(s => new StationNameWarehouseDto
                {
                    ID = s.ID,
                    SystemNameWarehouseID = s.SystemNameWarehouseID,
                    SystemNameWarehouseName = s.SystemNameWarehouse.Name,
                    Name = s.Name,
                    IsDeleted = s.IsDeleted,
                })
                .FirstOrDefaultAsync();

            if (stationName == null)
            {
                return NotFound(new { message = "StationNameWarehouse not found" });
            }

            return Ok(stationName);
        }

        // GET: api/StationNameWarehouse/BySystemName/5
        [HttpGet("BySystemName/{SystemNameWarehouseID}")]
        public async Task<ActionResult<IEnumerable<StationNameWarehouseDto>>> GetStationNamesBySystemName(Guid SystemNameWarehouseID)
        {
            var stationNames = await _context.StationNameWarehouses
                .Where(s => s.SystemNameWarehouseID == SystemNameWarehouseID && !s.IsDeleted)
                .Include(s => s.SystemNameWarehouse)
                .Select(s => new StationNameWarehouseDto
                {
                    ID = s.ID,
                    SystemNameWarehouseID = s.SystemNameWarehouseID,
                    SystemNameWarehouseName = s.SystemNameWarehouse.Name,
                    Name = s.Name,
                    IsDeleted = s.IsDeleted,
                })
                .OrderBy(s => s.Name)
                .ToListAsync();

            return Ok(stationNames);
        }

        // POST: api/StationNameWarehouse
        [HttpPost]
        public async Task<ActionResult<StationNameWarehouseDto>> CreateStationNameWarehouse(CreateStationNameWarehouseDto createDto)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            // Verify SystemNameWarehouse exists
            var systemNameExists = await _context.SystemNameWarehouses
                .AnyAsync(s => s.ID == createDto.SystemNameWarehouseID && !s.IsDeleted);
            if (!systemNameExists)
            {
                return BadRequest(new { message = "SystemNameWarehouse not found" });
            }

            var stationName = new StationNameWarehouse
            {
                ID = Guid.NewGuid(),
                SystemNameWarehouseID = createDto.SystemNameWarehouseID,
                Name = createDto.Name,
                IsDeleted = false,
            };

            _context.StationNameWarehouses.Add(stationName);
            await _context.SaveChangesAsync();

            var result = await _context.StationNameWarehouses
                .Where(s => s.ID == stationName.ID)
                .Include(s => s.SystemNameWarehouse)
                .Select(s => new StationNameWarehouseDto
                {
                    ID = s.ID,
                    SystemNameWarehouseID = s.SystemNameWarehouseID,
                    SystemNameWarehouseName = s.SystemNameWarehouse.Name,
                    Name = s.Name,
                    IsDeleted = s.IsDeleted,
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction(nameof(GetStationNameWarehouse), new { id = stationName.ID }, result);
        }

        // PUT: api/StationNameWarehouse/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStationNameWarehouse(Guid id, UpdateStationNameWarehouseDto updateDto)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var stationName = await _context.StationNameWarehouses.FindAsync(id);
            if (stationName == null || stationName.IsDeleted)
            {
                return NotFound(new { message = "StationNameWarehouse not found" });
            }

            // Verify SystemNameWarehouse exists
            var systemNameExists = await _context.SystemNameWarehouses
                .AnyAsync(s => s.ID == updateDto.SystemNameWarehouseID && !s.IsDeleted);
            if (!systemNameExists)
            {
                return BadRequest(new { message = "SystemNameWarehouse not found" });
            }

            stationName.SystemNameWarehouseID = updateDto.SystemNameWarehouseID;
            stationName.Name = updateDto.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StationNameWarehouseExists(id))
                {
                    return NotFound(new { message = "StationNameWarehouse not found" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/StationNameWarehouse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStationNameWarehouse(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var stationName = await _context.StationNameWarehouses.FindAsync(id);
            if (stationName == null || stationName.IsDeleted)
            {
                return NotFound(new { message = "StationNameWarehouse not found" });
            }

            stationName.IsDeleted = true;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StationNameWarehouseExists(Guid id)
        {
            return _context.StationNameWarehouses.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}