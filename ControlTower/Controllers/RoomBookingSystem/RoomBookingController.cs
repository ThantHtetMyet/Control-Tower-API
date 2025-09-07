using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.DTOs.RoomBookingSystem;
using ControlTower.Models.RoomBookingSystem;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ControlTower.Controllers.RoomBookingSystem
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomBookingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RoomBookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the current user ID from JWT token claims
        /// </summary>
        /// <returns>Current user ID or null if not found/invalid</returns>
        private Guid? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var currentUserId))
            {
                return null;
            }
            return currentUserId;
        }

        /// <summary>
        /// Validates if a room exists and is not deleted
        /// </summary>
        private async Task<bool> IsValidRoomAsync(Guid roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            return room != null && !room.IsDeleted;
        }

        /// <summary>
        /// Validates if a status exists and is not deleted
        /// </summary>
        private async Task<bool> IsValidStatusAsync(Guid statusId)
        {
            var status = await _context.RoomBookingStatus.FindAsync(statusId);
            return status != null && !status.IsDeleted;
        }

        /// <summary>
        /// Checks for booking conflicts in the specified time period
        /// </summary>
        private async Task<bool> HasBookingConflictAsync(Guid roomId, DateTime startTime, DateTime endTime, Guid? excludeBookingId = null)
        {
            // Get active status IDs (pending and approved)
            var pendingStatus = await GetBookingStatusByNameAsync("Pending");
            var approvedStatus = await GetBookingStatusByNameAsync("Approved");
            
            var activeStatusIds = new List<Guid>();
            if (pendingStatus != null) activeStatusIds.Add(pendingStatus.ID);
            if (approvedStatus != null) activeStatusIds.Add(approvedStatus.ID);
        
            var query = _context.RoomBookings
                .Where(b => b.RoomID == roomId && !b.IsDeleted &&
                       activeStatusIds.Contains(b.StatusID) &&  // Only check pending/approved bookings
                       ((startTime >= b.StartTime && startTime < b.EndTime) ||
                        (endTime > b.StartTime && endTime <= b.EndTime) ||
                        (startTime <= b.StartTime && endTime >= b.EndTime)));
        
            if (excludeBookingId.HasValue)
            {
                query = query.Where(b => b.ID != excludeBookingId.Value);
            }
        
            return await query.AnyAsync();
        }

        /// <summary>
        /// Gets booking status by name
        /// </summary>
        private async Task<RoomBookingStatus> GetBookingStatusByNameAsync(string statusName)
        {
            return await _context.RoomBookingStatus
                .Where(s => s.Name == statusName && !s.IsDeleted)
                .FirstOrDefaultAsync();
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings()
        {
            return await _context.RoomBookings
                .Where(b => !b.IsDeleted)
                .Include(b => b.Room)
                .Include(b => b.Status)
                .Include(b => b.RequestedByUser)
                .Include(b => b.ApprovedByUser)
                .Include(b => b.CancelledByUser)
                .Select(b => new BookingDto
                {
                    ID = b.ID,
                    RoomID = b.RoomID,
                    RoomName = b.Room.Name,
                    Title = b.Title,
                    Description = b.Description,
                    StartTime = b.StartTime,
                    EndTime = b.EndTime,
                    StatusID = b.StatusID,
                    StatusName = b.Status.Name,
                    RequestedBy = b.RequestedBy,
                    RequestedByName = b.RequestedByUser.FirstName + " " + b.RequestedByUser.LastName,
                    ApprovedBy = b.ApprovedBy,
                    ApprovedByName = b.ApprovedByUser != null ? b.ApprovedByUser.FirstName + " " + b.ApprovedByUser.LastName : null,
                    ApprovedDate = b.ApprovedDate,
                    RejectionReason = b.RejectionReason,
                    CancellationReason = b.CancellationReason,
                    CancelledBy = b.CancelledBy,
                    CancelledByName = b.CancelledByUser != null ? b.CancelledByUser.FirstName + " " + b.CancelledByUser.LastName : null,
                    RecurrenceRule = b.RecurrenceRule,
                    IsDeleted = b.IsDeleted,
                    CreatedDate = b.CreatedDate,
                    UpdatedDate = b.UpdatedDate,
                    RowVersion = b.RowVersion
                })
                .ToListAsync();
        }

        // Move this BEFORE the [HttpGet("{id}")] route
        // GET: api/RoomBooking/calendar
        [HttpGet("calendar")]
        public async Task<ActionResult<IEnumerable<CalendarBookingDto>>> GetCalendarBookings(
            [FromQuery] Guid? roomId = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            // Get the approved status
            var approvedStatus = await GetBookingStatusByNameAsync("Approved");
            if (approvedStatus == null)
            {
                return BadRequest("Approved status not found");
            }

            var query = _context.RoomBookings
                .Include(b => b.Room)
                .Include(b => b.Status)
                .Include(b => b.RequestedByUser)
                .Include(b => b.ApprovedByUser)
                .Where(b => !b.IsDeleted && b.StatusID == approvedStatus.ID); // Filter for approved status only

            // Filter by room if specified
            if (roomId.HasValue)
            {
                query = query.Where(b => b.RoomID == roomId.Value);
            }

            // Filter by date range if specified
            if (startDate.HasValue)
            {
                var startOfDay = startDate.Value.Date; // Start of the day (00:00:00)
                query = query.Where(b => b.EndTime >= startOfDay);
            }

            if (endDate.HasValue)
            {
                var endOfDay = endDate.Value.Date.AddDays(1).AddTicks(-1); // End of the day (23:59:59.999)
                query = query.Where(b => b.StartTime <= endOfDay);
            }

            var bookings = await query
                .OrderBy(b => b.StartTime) // Orders from morning to evening
                .Select(b => new CalendarBookingDto
                {
                    ID = b.ID,
                    RoomID = b.RoomID,
                    RoomName = b.Room.Name,
                    Title = b.Title,
                    Description = b.Description,
                    StartTime = b.StartTime,
                    EndTime = b.EndTime,
                    StatusName = b.Status.Name,
                    RequestedByName = b.RequestedByUser.FirstName + " " + b.RequestedByUser.LastName,
                    ApprovedByName = b.ApprovedByUser != null ? b.ApprovedByUser.FirstName + " " + b.ApprovedByUser.LastName : null,
                    IsDeleted = b.IsDeleted
                })
                .ToListAsync();

            return Ok(bookings);
        }

        // GET: api/Booking/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBooking(Guid id)
        {
            var booking = await _context.RoomBookings
                .Where(b => b.ID == id && !b.IsDeleted)
                .Include(b => b.Room)
                .Include(b => b.Status)
                .Include(b => b.RequestedByUser)
                .Include(b => b.ApprovedByUser)
                .Include(b => b.CancelledByUser)
                .FirstOrDefaultAsync();

            if (booking == null)
            {
                return NotFound();
            }

            return new BookingDto
            {
                ID = booking.ID,
                RoomID = booking.RoomID,
                RoomName = booking.Room.Name,
                Title = booking.Title,
                Description = booking.Description,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime,
                StatusID = booking.StatusID,
                StatusName = booking.Status.Name,
                RequestedBy = booking.RequestedBy,
                RequestedByName = booking.RequestedByUser.FirstName + " " + booking.RequestedByUser.LastName,
                ApprovedBy = booking.ApprovedBy,
                ApprovedByName = booking.ApprovedByUser != null ? booking.ApprovedByUser.FirstName + " " + booking.ApprovedByUser.LastName : null,
                ApprovedDate = booking.ApprovedDate,
                RejectionReason = booking.RejectionReason,
                CancellationReason = booking.CancellationReason,
                CancelledBy = booking.CancelledBy,
                CancelledByName = booking.CancelledByUser != null ? booking.CancelledByUser.FirstName + " " + booking.CancelledByUser.LastName : null,
                RecurrenceRule = booking.RecurrenceRule,
                IsDeleted = booking.IsDeleted,
                CreatedDate = booking.CreatedDate,
                UpdatedDate = booking.UpdatedDate,
                RowVersion = booking.RowVersion
            };
        }

        // POST: api/Booking
        [HttpPost]
        public async Task<ActionResult<BookingDto>> CreateBooking(CreateBookingDto createBookingDto)
        {
            // Get current user ID from JWT token claims
            var currentUserId = GetCurrentUserId();
            if (!currentUserId.HasValue)
            {
                return Unauthorized("User ID not found in token or invalid format");
            }

            // Validate room
            if (!await IsValidRoomAsync(createBookingDto.RoomID))
            {
                return BadRequest("Invalid room ID");
            }

            // Validate status
            if (!await IsValidStatusAsync(createBookingDto.StatusID))
            {
                return BadRequest("Invalid status ID");
            }

            // Check for booking conflicts
            if (await HasBookingConflictAsync(createBookingDto.RoomID, DateTime.SpecifyKind(createBookingDto.StartTime, DateTimeKind.Utc), DateTime.SpecifyKind(createBookingDto.EndTime, DateTimeKind.Utc)))
            {
                return BadRequest("The room is already booked during the requested time period");
            }

            // In the CreateBooking method (around line 208-209):
            var booking = new RoomBooking
            {
                ID = Guid.NewGuid(),
                RoomID = createBookingDto.RoomID,
                Title = createBookingDto.Title,
                Description = createBookingDto.Description,
                StartTime = DateTime.SpecifyKind(createBookingDto.StartTime, DateTimeKind.Utc),
                EndTime = DateTime.SpecifyKind(createBookingDto.EndTime, DateTimeKind.Utc),
                StatusID = createBookingDto.StatusID,
                RequestedBy = currentUserId.Value,
                CreatedBy = currentUserId.Value,
                UpdatedBy = currentUserId.Value,
                RecurrenceRule = createBookingDto.RecurrenceRule,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };
            
            // In the UpdateBooking method (around line 275-276):
            booking.RoomID = createBookingDto.RoomID;
            booking.Title = createBookingDto.Title;
            booking.Description = createBookingDto.Description;
            booking.StartTime = DateTime.SpecifyKind(createBookingDto.StartTime, DateTimeKind.Utc);
            booking.EndTime = DateTime.SpecifyKind(createBookingDto.EndTime, DateTimeKind.Utc);
            booking.StatusID = createBookingDto.StatusID;
            booking.RecurrenceRule = createBookingDto.RecurrenceRule;
            booking.UpdatedDate = DateTime.UtcNow;
            booking.UpdatedBy = currentUserId.Value;

            _context.RoomBookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBooking), new { id = booking.ID }, new { ID = booking.ID });
        }

        // PUT: api/Booking/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(Guid id, UpdateBookingDto updateBookingDto)
        {
            if (id != updateBookingDto.ID)
            {
                return BadRequest();
            }

            // Get current user ID from JWT token claims
            var currentUserId = GetCurrentUserId();
            if (!currentUserId.HasValue)
            {
                return Unauthorized("User ID not found in token or invalid format");
            }

            var booking = await _context.RoomBookings.FindAsync(id);
            if (booking == null || booking.IsDeleted)
            {
                return NotFound();
            }

            // Check if the RowVersion matches for concurrency control
            if (updateBookingDto.RowVersion != null && !booking.RowVersion.SequenceEqual(updateBookingDto.RowVersion))
            {
                return StatusCode(409, "The booking has been modified by another user. Please refresh and try again.");
            }

            // Validate room
            if (!await IsValidRoomAsync(updateBookingDto.RoomID))
            {
                return BadRequest("Invalid room ID");
            }

            // Validate status
            if (!await IsValidStatusAsync(updateBookingDto.StatusID))
            {
                return BadRequest("Invalid status ID");
            }

            // Check for booking conflicts (excluding this booking)
            if (await HasBookingConflictAsync(updateBookingDto.RoomID, DateTime.SpecifyKind(updateBookingDto.StartTime, DateTimeKind.Utc), DateTime.SpecifyKind(updateBookingDto.EndTime, DateTimeKind.Utc), id))
            {
                return BadRequest("The room is already booked during the requested time period");
            }

            booking.RoomID = updateBookingDto.RoomID;
            booking.Title = updateBookingDto.Title;
            booking.Description = updateBookingDto.Description;
            booking.StartTime = DateTime.SpecifyKind(updateBookingDto.StartTime, DateTimeKind.Utc); 
            booking.EndTime = DateTime.SpecifyKind(updateBookingDto.EndTime, DateTimeKind.Utc); 
            booking.StatusID = updateBookingDto.StatusID;
            booking.RecurrenceRule = updateBookingDto.RecurrenceRule;
            booking.UpdatedDate = DateTime.UtcNow;
            booking.UpdatedBy = currentUserId.Value;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(409, "The booking has been modified by another user. Please refresh and try again.");
                }
            }

            return NoContent();
        }

        // POST: api/Booking/5/approve
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> ApproveBooking(Guid id, ApproveBookingDto approveBookingDto)
        {
            if (id != approveBookingDto.ID)
            {
                return BadRequest();
            }

            // Get current user ID from JWT token claims
            var currentUserId = GetCurrentUserId();
            if (!currentUserId.HasValue)
            {
                return Unauthorized("User ID not found in token or invalid format");
            }

            var booking = await _context.RoomBookings.FindAsync(id);
            if (booking == null || booking.IsDeleted)
            {
                return NotFound();
            }

            // Check if the RowVersion matches for concurrency control
            if (approveBookingDto.RowVersion != null && !booking.RowVersion.SequenceEqual(approveBookingDto.RowVersion))
            {
                return StatusCode(409, "The booking has been modified by another user. Please refresh and try again.");
            }

            // Get the approved status
            var approvedStatus = await GetBookingStatusByNameAsync("Approved");
            if (approvedStatus == null)
            {
                return BadRequest("Approved status not found");
            }

            booking.StatusID = approvedStatus.ID;
            booking.ApprovedBy = currentUserId.Value;
            booking.ApprovedDate = DateTime.UtcNow;
            booking.UpdatedDate = DateTime.UtcNow;
            booking.UpdatedBy = currentUserId.Value;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(409, "The booking has been modified by another user. Please refresh and try again.");
            }

            return NoContent();
        }

        // POST: api/Booking/5/reject
        [HttpPost("{id}/reject")]
        public async Task<IActionResult> RejectBooking(Guid id, RejectBookingDto rejectBookingDto)
        {
            if (id != rejectBookingDto.ID)
            {
                return BadRequest();
            }

            // Get current user ID from JWT token claims
            var currentUserId = GetCurrentUserId();
            if (!currentUserId.HasValue)
            {
                return Unauthorized("User ID not found in token or invalid format");
            }

            var booking = await _context.RoomBookings.FindAsync(id);
            if (booking == null || booking.IsDeleted)
            {
                return NotFound();
            }

            // Check if the RowVersion matches for concurrency control
            if (rejectBookingDto.RowVersion != null && !booking.RowVersion.SequenceEqual(rejectBookingDto.RowVersion))
            {
                return StatusCode(409, "The booking has been modified by another user. Please refresh and try again.");
            }

            // Get the rejected status
            var rejectedStatus = await GetBookingStatusByNameAsync("Rejected");
            if (rejectedStatus == null)
            {
                return BadRequest("Rejected status not found");
            }

            booking.StatusID = rejectedStatus.ID;
            booking.RejectionReason = rejectBookingDto.RejectionReason;
            booking.UpdatedDate = DateTime.UtcNow;
            booking.UpdatedBy = currentUserId.Value;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(409, "The booking has been modified by another user. Please refresh and try again.");
            }

            return NoContent();
        }

        // POST: api/Booking/5/cancel
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelBooking(Guid id, CancelBookingDto cancelBookingDto)
        {
            if (id != cancelBookingDto.ID)
            {
                return BadRequest();
            }

            // Get current user ID from JWT token claims
            var currentUserId = GetCurrentUserId();
            if (!currentUserId.HasValue)
            {
                return Unauthorized("User ID not found in token or invalid format");
            }

            var booking = await _context.RoomBookings.FindAsync(id);
            if (booking == null || booking.IsDeleted)
            {
                return NotFound();
            }

            // Check if the RowVersion matches for concurrency control
            if (cancelBookingDto.RowVersion != null && !booking.RowVersion.SequenceEqual(cancelBookingDto.RowVersion))
            {
                return StatusCode(409, "The booking has been modified by another user. Please refresh and try again.");
            }

            // Get the cancelled status
            var cancelledStatus = await GetBookingStatusByNameAsync("Cancelled");
            if (cancelledStatus == null)
            {
                return BadRequest("Cancelled status not found");
            }

            booking.StatusID = cancelledStatus.ID;
            booking.CancellationReason = cancelBookingDto.CancellationReason;
            booking.CancelledBy = currentUserId.Value;
            booking.UpdatedDate = DateTime.UtcNow;
            booking.UpdatedBy = currentUserId.Value;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(409, "The booking has been modified by another user. Please refresh and try again.");
            }

            return NoContent();
        }

        // DELETE: api/Booking/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(Guid id)
        {
            // Get current user ID from JWT token claims
            var currentUserId = GetCurrentUserId();
            if (!currentUserId.HasValue)
            {
                return Unauthorized("User ID not found in token or invalid format");
            }

            var booking = await _context.RoomBookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            // Soft delete
            booking.IsDeleted = true;
            booking.UpdatedDate = DateTime.UtcNow;
            booking.UpdatedBy = currentUserId.Value;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(Guid id)
        {
            return _context.RoomBookings.Any(e => e.ID == id);
        }
    }
}