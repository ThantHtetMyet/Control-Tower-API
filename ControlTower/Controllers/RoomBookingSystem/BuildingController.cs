using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.RoomBookingSystem;
using ControlTower.DTOs.RoomBookingSystem;

namespace ControlTower.Controllers.RoomBookingSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BuildingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Building
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuildingDto>>> GetBuildings()
        {
            var buildings = await _context.Buildings
                .Include(b => b.CreatedByUser)
                .Include(b => b.UpdatedByUser)
                .Where(b => !b.IsDeleted)
                .Select(b => new BuildingDto
                {
                    ID = b.ID,
                    Name = b.Name,
                    Code = b.Code,
                    Address = b.Address,
                    CreatedDate = b.CreatedDate,
                    UpdatedDate = b.UpdatedDate,
                    CreatedByUserName = b.CreatedByUser != null ? b.CreatedByUser.FirstName + " " + b.CreatedByUser.LastName : null,
                    UpdatedByUserName = b.UpdatedByUser != null ? b.UpdatedByUser.FirstName + " " + b.UpdatedByUser.LastName : null
                })
                .ToListAsync();

            return buildings;
        }

        // GET: api/Building/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BuildingDto>> GetBuilding(Guid id)
        {
            var building = await _context.Buildings
                .Include(b => b.CreatedByUser)
                .Include(b => b.UpdatedByUser)
                .Where(b => b.ID == id && !b.IsDeleted)
                .Select(b => new BuildingDto
                {
                    ID = b.ID,
                    Name = b.Name,
                    Code = b.Code,
                    Address = b.Address,
                    CreatedDate = b.CreatedDate,
                    UpdatedDate = b.UpdatedDate,
                    CreatedByUserName = b.CreatedByUser != null ? b.CreatedByUser.FirstName + " " + b.CreatedByUser.LastName : null,
                    UpdatedByUserName = b.UpdatedByUser != null ? b.UpdatedByUser.FirstName + " " + b.UpdatedByUser.LastName : null
                })
                .FirstOrDefaultAsync();

            if (building == null)
            {
                return NotFound();
            }

            return building;
        }

        // POST: api/Building
        [HttpPost]
        public async Task<ActionResult<BuildingDto>> PostBuilding(CreateBuildingDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var building = new Building
            {
                ID = Guid.NewGuid(),
                Name = createDto.Name,
                Code = createDto.Code,
                Address = createDto.Address,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                IsDeleted = false,
                CreatedBy = Guid.Parse(createDto.CreatedBy),
                UpdatedBy = Guid.Parse(createDto.CreatedBy)
            };

            _context.Buildings.Add(building);
            await _context.SaveChangesAsync();

            var buildingDto = new BuildingDto
            {
                ID = building.ID,
                Name = building.Name,
                Code = building.Code,
                Address = building.Address,
                CreatedDate = building.CreatedDate,
                UpdatedDate = building.UpdatedDate
            };

            return CreatedAtAction(nameof(GetBuilding), new { id = building.ID }, buildingDto);
        }

        // PUT: api/Building/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuilding(Guid id, UpdateBuildingDto updateDto)
        {
            if (id != updateDto.ID)
            {
                return BadRequest();
            }

            var building = await _context.Buildings.FindAsync(id);
            if (building == null || building.IsDeleted)
            {
                return NotFound();
            }

            building.Name = updateDto.Name;
            building.Code = updateDto.Code;
            building.Address = updateDto.Address;
            building.UpdatedDate = DateTime.UtcNow;
            
            if (!string.IsNullOrEmpty(updateDto.UpdatedBy))
            {
                building.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(id))
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

        // DELETE: api/Building/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilding(Guid id)
        {
            var building = await _context.Buildings.FindAsync(id);
            if (building == null || building.IsDeleted)
            {
                return NotFound();
            }

            building.IsDeleted = true;
            building.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuildingExists(Guid id)
        {
            return _context.Buildings.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}