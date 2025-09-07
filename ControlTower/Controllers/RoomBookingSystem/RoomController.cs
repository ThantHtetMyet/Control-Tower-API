using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.RoomBookingSystem;
using ControlTower.DTOs.RoomBookingSystem;

namespace ControlTower.Controllers.RoomBookingSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoomController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Room
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetRooms()
        {
            var rooms = await _context.Rooms
                .Include(r => r.Building)
                .Include(r => r.CreatedByUser)
                .Include(r => r.UpdatedByUser)
                .Where(r => !r.IsDeleted)
                .Select(r => new RoomDto
                {
                    ID = r.ID,
                    BuildingID = r.BuildingID,
                    BuildingName = r.Building.Name,
                    Name = r.Name,
                    Code = r.Code,
                    // Floor removed
                    Capacity = r.Capacity,
                    Description = r.Description,
                    CreatedDate = r.CreatedDate,
                    UpdatedDate = r.UpdatedDate,
                    CreatedByUserName = r.CreatedByUser != null ? r.CreatedByUser.FirstName + " " + r.CreatedByUser.LastName : null,
                    UpdatedByUserName = r.UpdatedByUser != null ? r.UpdatedByUser.FirstName + " " + r.UpdatedByUser.LastName : null
                })
                .ToListAsync();

            return rooms;
        }

        // GET: api/Room/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDto>> GetRoom(Guid id)
        {
            var room = await _context.Rooms
                .Include(r => r.Building)
                .Include(r => r.CreatedByUser)
                .Include(r => r.UpdatedByUser)
                .Where(r => r.ID == id && !r.IsDeleted)
                .Select(r => new RoomDto
                {
                    ID = r.ID,
                    BuildingID = r.BuildingID,
                    BuildingName = r.Building.Name,
                    Name = r.Name,
                    Code = r.Code,
                    // Floor removed
                    Capacity = r.Capacity,
                    Description = r.Description,
                    CreatedDate = r.CreatedDate,
                    UpdatedDate = r.UpdatedDate,
                    CreatedByUserName = r.CreatedByUser != null ? r.CreatedByUser.FirstName + " " + r.CreatedByUser.LastName : null,
                    UpdatedByUserName = r.UpdatedByUser != null ? r.UpdatedByUser.FirstName + " " + r.UpdatedByUser.LastName : null
                })
                .FirstOrDefaultAsync();

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        // POST: api/Room
        [HttpPost]
        public async Task<ActionResult<RoomDto>> PostRoom(CreateRoomDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var room = new Room
            {
                ID = Guid.NewGuid(),
                BuildingID = createDto.BuildingID,
                Name = createDto.Name,
                Code = createDto.Code,
                // Floor removed
                Capacity = createDto.Capacity,
                Description = createDto.Description,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                IsDeleted = false,
                CreatedBy = Guid.Parse(createDto.CreatedBy),
                UpdatedBy = Guid.Parse(createDto.CreatedBy)
            };

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            var roomDto = new RoomDto
            {
                ID = room.ID,
                BuildingID = room.BuildingID,
                Name = room.Name,
                Code = room.Code,
                // Floor removed
                Capacity = room.Capacity,
                Description = room.Description,
                CreatedDate = room.CreatedDate,
                UpdatedDate = room.UpdatedDate
            };

            return CreatedAtAction(nameof(GetRoom), new { id = room.ID }, roomDto);
        }

        // PUT: api/Room/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(Guid id, UpdateRoomDto updateDto)
        {
            if (id != updateDto.ID)
            {
                return BadRequest();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null || room.IsDeleted)
            {
                return NotFound();
            }

            room.BuildingID = updateDto.BuildingID;
            room.Name = updateDto.Name;
            room.Code = updateDto.Code;
            // Floor removed
            room.Capacity = updateDto.Capacity;
            room.Description = updateDto.Description;
            room.UpdatedDate = DateTime.UtcNow;
            
            if (!string.IsNullOrEmpty(updateDto.UpdatedBy))
            {
                room.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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

        // DELETE: api/Room/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null || room.IsDeleted)
            {
                return NotFound();
            }

            room.IsDeleted = true;
            room.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomExists(Guid id)
        {
            return _context.Rooms.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}