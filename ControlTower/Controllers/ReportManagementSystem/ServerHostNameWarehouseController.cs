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
    public class ServerHostNameWarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServerHostNameWarehouseController(ApplicationDbContext context)
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

        // GET: api/ServerHostNameWarehouse
        [HttpGet]
        public async Task<ActionResult<object>> GetServerHostNameWarehouses(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string search = "",
            [FromQuery] Guid? StationNameID = null)
        {
            var query = _context.ServerHostNameWarehouses
                .Where(s => !s.IsDeleted)
                .Include(s => s.StationNameWarehouse)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(s => s.Name.Contains(search) || 
                    (s.StationNameWarehouse != null && s.StationNameWarehouse.Name.Contains(search)));
            }

            if (StationNameID.HasValue)
            {
                query = query.Where(s => s.StationNameID == StationNameID.Value);
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var serverHostNames = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new ServerHostNameWarehouseDto
                {
                    ID = s.ID,
                    StationNameID = s.StationNameID,
                    StationNameWarehouseName = s.StationNameWarehouse != null ? s.StationNameWarehouse.Name : string.Empty,
                    Name = s.Name,
                    IsDeleted = s.IsDeleted,
                    CreatedDate = s.CreatedDate,
                    UpdatedDate = s.UpdatedDate
                })
                .ToListAsync();

            return Ok(new
            {
                data = serverHostNames,
                totalCount,
                totalPages,
                currentPage = page,
                pageSize
            });
        }

        // GET: api/ServerHostNameWarehouse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServerHostNameWarehouseDto>> GetServerHostNameWarehouse(Guid id)
        {
            var serverHostName = await _context.ServerHostNameWarehouses
                .Where(s => s.ID == id && !s.IsDeleted)
                .Include(s => s.StationNameWarehouse)
                .Select(s => new ServerHostNameWarehouseDto
                {
                    ID = s.ID,
                    StationNameID = s.StationNameID,
                    StationNameWarehouseName = s.StationNameWarehouse != null ? s.StationNameWarehouse.Name : string.Empty,
                    Name = s.Name,
                    IsDeleted = s.IsDeleted,
                    CreatedDate = s.CreatedDate,
                    UpdatedDate = s.UpdatedDate
                })
                .FirstOrDefaultAsync();

            if (serverHostName == null)
            {
                return NotFound(new { message = "ServerHostNameWarehouse not found" });
            }

            return Ok(serverHostName);
        }

        // GET: api/ServerHostNameWarehouse/ByStationName/5
        [HttpGet("ByStationName/{StationNameID}")]
        public async Task<ActionResult<IEnumerable<ServerHostNameWarehouseDto>>> GetServerHostNamesByStationName(Guid StationNameID)
        {
            var serverHostNames = await _context.ServerHostNameWarehouses
                .Where(s => s.StationNameID == StationNameID && !s.IsDeleted)
                .Include(s => s.StationNameWarehouse)
                .Select(s => new ServerHostNameWarehouseDto
                {
                    ID = s.ID,
                    StationNameID = s.StationNameID,
                    StationNameWarehouseName = s.StationNameWarehouse != null ? s.StationNameWarehouse.Name : string.Empty,
                    Name = s.Name,
                    IsDeleted = s.IsDeleted,
                    CreatedDate = s.CreatedDate,
                    UpdatedDate = s.UpdatedDate
                })
                .OrderBy(s => s.Name)
                .ToListAsync();

            return Ok(serverHostNames);
        }

        // POST: api/ServerHostNameWarehouse
        [HttpPost]
        public async Task<ActionResult<ServerHostNameWarehouseDto>> CreateServerHostNameWarehouse(CreateServerHostNameWarehouseDto createDto)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            // Verify StationNameWarehouse exists
            var stationNameExists = await _context.StationNameWarehouses
                .AnyAsync(s => s.ID == createDto.StationNameID && !s.IsDeleted);
            if (!stationNameExists)
            {
                return BadRequest(new { message = "StationNameWarehouse not found" });
            }

            var serverHostName = new ServerHostNameWarehouse
            {
                ID = Guid.NewGuid(),
                StationNameID = createDto.StationNameID,
                Name = createDto.Name,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            _context.ServerHostNameWarehouses.Add(serverHostName);
            await _context.SaveChangesAsync();

            var result = await _context.ServerHostNameWarehouses
                .Where(s => s.ID == serverHostName.ID)
                .Include(s => s.StationNameWarehouse)
                .Select(s => new ServerHostNameWarehouseDto
                {
                    ID = s.ID,
                    StationNameID = s.StationNameID,
                    StationNameWarehouseName = s.StationNameWarehouse != null ? s.StationNameWarehouse.Name : string.Empty,
                    Name = s.Name,
                    IsDeleted = s.IsDeleted,
                    CreatedDate = s.CreatedDate,
                    UpdatedDate = s.UpdatedDate
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction(nameof(GetServerHostNameWarehouse), new { id = serverHostName.ID }, result);
        }

        // PUT: api/ServerHostNameWarehouse/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServerHostNameWarehouse(Guid id, UpdateServerHostNameWarehouseDto updateDto)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var serverHostName = await _context.ServerHostNameWarehouses.FindAsync(id);
            if (serverHostName == null || serverHostName.IsDeleted)
            {
                return NotFound(new { message = "ServerHostNameWarehouse not found" });
            }

            // Verify StationNameWarehouse exists
            var stationNameExists = await _context.StationNameWarehouses
                .AnyAsync(s => s.ID == updateDto.StationNameID && !s.IsDeleted);
            if (!stationNameExists)
            {
                return BadRequest(new { message = "StationNameWarehouse not found" });
            }

            serverHostName.StationNameID = updateDto.StationNameID;
            serverHostName.Name = updateDto.Name;
            serverHostName.UpdatedDate = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServerHostNameWarehouseExists(id))
                {
                    return NotFound(new { message = "ServerHostNameWarehouse not found" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ServerHostNameWarehouse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServerHostNameWarehouse(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var serverHostName = await _context.ServerHostNameWarehouses.FindAsync(id);
            if (serverHostName == null || serverHostName.IsDeleted)
            {
                return NotFound(new { message = "ServerHostNameWarehouse not found" });
            }

            serverHostName.IsDeleted = true;
            serverHostName.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServerHostNameWarehouseExists(Guid id)
        {
            return _context.ServerHostNameWarehouses.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}

