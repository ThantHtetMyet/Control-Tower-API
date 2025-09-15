using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class FurtherActionTakenWarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FurtherActionTakenWarehouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FurtherActionTakenWarehouse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FurtherActionTakenWarehouseDto>>> GetFurtherActionTakenWarehouses()
        {
            var furtherActionTakenWarehouses = await _context.FurtherActionTakenWarehouses
                .Include(f => f.CreatedByUser)
                .Include(f => f.UpdatedByUser)
                .Where(f => !f.IsDeleted)
                .Select(f => new FurtherActionTakenWarehouseDto
                {
                    ID = f.ID,
                    Name = f.Name,
                    IsDeleted = f.IsDeleted,
                    CreatedDate = f.CreatedDate,
                    UpdatedDate = f.UpdatedDate,
                    CreatedBy = f.CreatedBy,
                    UpdatedBy = f.UpdatedBy,
                    CreatedByUserName = f.CreatedByUser != null ? $"{f.CreatedByUser.FirstName} {f.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = f.UpdatedByUser != null ? $"{f.UpdatedByUser.FirstName} {f.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            return Ok(furtherActionTakenWarehouses);
        }

        // GET: api/FurtherActionTakenWarehouse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FurtherActionTakenWarehouseDto>> GetFurtherActionTakenWarehouse(Guid id)
        {
            var furtherActionTakenWarehouse = await _context.FurtherActionTakenWarehouses
                .Include(f => f.CreatedByUser)
                .Include(f => f.UpdatedByUser)
                .Where(f => f.ID == id && !f.IsDeleted)
                .Select(f => new FurtherActionTakenWarehouseDto
                {
                    ID = f.ID,
                    Name = f.Name,
                    IsDeleted = f.IsDeleted,
                    CreatedDate = f.CreatedDate,
                    UpdatedDate = f.UpdatedDate,
                    CreatedBy = f.CreatedBy,
                    UpdatedBy = f.UpdatedBy,
                    CreatedByUserName = f.CreatedByUser != null ? $"{f.CreatedByUser.FirstName} {f.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = f.UpdatedByUser != null ? $"{f.UpdatedByUser.FirstName} {f.UpdatedByUser.LastName}" : null
                })
                .FirstOrDefaultAsync();

            if (furtherActionTakenWarehouse == null)
            {
                return NotFound(new { message = "Further action taken warehouse not found." });
            }

            return Ok(furtherActionTakenWarehouse);
        }

        // POST: api/FurtherActionTakenWarehouse
        [HttpPost]
        public async Task<ActionResult<FurtherActionTakenWarehouseDto>> CreateFurtherActionTakenWarehouse(CreateFurtherActionTakenWarehouseDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check for duplicate name
            var existingFurtherActionTakenWarehouse = await _context.FurtherActionTakenWarehouses
                .AnyAsync(f => f.Name.ToLower() == createDto.Name.ToLower() && !f.IsDeleted);

            if (existingFurtherActionTakenWarehouse)
            {
                return BadRequest(new { message = "A further action taken warehouse with this name already exists." });
            }

            var furtherActionTakenWarehouse = new FurtherActionTakenWarehouse
            {
                ID = Guid.NewGuid(),
                Name = createDto.Name,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = null, // Set based on authentication context
                UpdatedBy = null  // Set based on authentication context
            };

            _context.FurtherActionTakenWarehouses.Add(furtherActionTakenWarehouse);
            await _context.SaveChangesAsync();

            // Return the created record with navigation properties
            var createdRecord = await GetFurtherActionTakenWarehouse(furtherActionTakenWarehouse.ID);
            return CreatedAtAction(nameof(GetFurtherActionTakenWarehouse), new { id = furtherActionTakenWarehouse.ID }, createdRecord.Value);
        }

        // PUT: api/FurtherActionTakenWarehouse/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFurtherActionTakenWarehouse(Guid id, UpdateFurtherActionTakenWarehouseDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var furtherActionTakenWarehouse = await _context.FurtherActionTakenWarehouses
                .FirstOrDefaultAsync(f => f.ID == id && !f.IsDeleted);

            if (furtherActionTakenWarehouse == null)
            {
                return NotFound(new { message = "Further action taken warehouse not found." });
            }

            // Check for duplicate name (excluding current record)
            var existingFurtherActionTakenWarehouse = await _context.FurtherActionTakenWarehouses
                .AnyAsync(f => f.Name.ToLower() == updateDto.Name.ToLower() && f.ID != id && !f.IsDeleted);

            if (existingFurtherActionTakenWarehouse)
            {
                return BadRequest(new { message = "A further action taken warehouse with this name already exists." });
            }

            // Update properties
            furtherActionTakenWarehouse.Name = updateDto.Name;
            furtherActionTakenWarehouse.UpdatedDate = DateTime.UtcNow;
            furtherActionTakenWarehouse.UpdatedBy = null; // Set based on authentication context

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Further action taken warehouse updated successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The record was modified by another user. Please refresh and try again." });
            }
        }

        // DELETE: api/FurtherActionTakenWarehouse/5 (Soft Delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFurtherActionTakenWarehouse(Guid id)
        {
            var furtherActionTakenWarehouse = await _context.FurtherActionTakenWarehouses
                .FirstOrDefaultAsync(f => f.ID == id && !f.IsDeleted);

            if (furtherActionTakenWarehouse == null)
            {
                return NotFound(new { message = "Further action taken warehouse not found." });
            }

            // Soft delete
            furtherActionTakenWarehouse.IsDeleted = true;
            furtherActionTakenWarehouse.UpdatedDate = DateTime.UtcNow;
            furtherActionTakenWarehouse.UpdatedBy = null; // Set based on authentication context

            await _context.SaveChangesAsync();

            return Ok(new { message = "Further action taken warehouse deleted successfully." });
        }
    }
}