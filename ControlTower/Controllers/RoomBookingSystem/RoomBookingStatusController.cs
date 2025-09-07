using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.RoomBookingSystem;
using ControlTower.DTOs.RoomBookingSystem;

namespace ControlTower.Controllers.RoomBookingSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomBookingStatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoomBookingStatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BookingStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingStatusDto>>> GetBookingStatuses()
        {
            var statuses = await _context.RoomBookingStatus
                .Where(s => !s.IsDeleted)
                .Select(s => new BookingStatusDto
                {
                    ID = s.ID,
                    Name = s.Name
                })
                .ToListAsync();

            return statuses;
        }

        // GET: api/BookingStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingStatusDto>> GetBookingStatus(Guid id) // Changed from int to Guid
        {
            var status = await _context.RoomBookingStatus
                .Where(s => s.ID == id && !s.IsDeleted)
                .Select(s => new BookingStatusDto
                {
                    ID = s.ID,
                    Name = s.Name
                })
                .FirstOrDefaultAsync();

            if (status == null)
            {
                return NotFound();
            }

            return status;
        }

        // POST: api/BookingStatus
        [HttpPost]
        public async Task<ActionResult<BookingStatusDto>> PostBookingStatus(CreateBookingStatusDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if Name already exists
            if (await _context.RoomBookingStatus.AnyAsync(s => s.Name == createDto.Name && !s.IsDeleted))
            {
                return Conflict("A booking status with this name already exists.");
            }

            var bookingStatus = new RoomBookingStatus
            {
                ID = Guid.NewGuid(), // Generate a new GUID
                Name = createDto.Name,
                IsDeleted = false
            };

            _context.RoomBookingStatus.Add(bookingStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookingStatus), new { id = bookingStatus.ID }, 
                new BookingStatusDto { ID = bookingStatus.ID, Name = bookingStatus.Name });
        }

        // PUT: api/BookingStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingStatus(Guid id, UpdateBookingStatusDto updateDto) // Changed from int to Guid
        {
            if (id != updateDto.ID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if Name already exists for a different ID
            if (await _context.RoomBookingStatus.AnyAsync(s => s.Name == updateDto.Name && s.ID != id && !s.IsDeleted))
            {
                return Conflict("A booking status with this name already exists.");
            }

            var bookingStatus = await _context.RoomBookingStatus.FindAsync(id);
            if (bookingStatus == null || bookingStatus.IsDeleted)
            {
                return NotFound();
            }

            bookingStatus.Name = updateDto.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingStatusExists(id))
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

        // DELETE: api/BookingStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingStatus(Guid id) // Changed from int to Guid
        {
            var bookingStatus = await _context.RoomBookingStatus.FindAsync(id);
            if (bookingStatus == null || bookingStatus.IsDeleted)
            {
                return NotFound();
            }

            bookingStatus.IsDeleted = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingStatusExists(Guid id) // Changed from int to Guid
        {
            return _context.RoomBookingStatus.Any(e => e.ID == id);
        }
    }
}