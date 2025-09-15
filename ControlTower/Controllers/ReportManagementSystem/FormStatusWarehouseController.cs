using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormStatusWarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FormStatusWarehouseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FormStatusWarehouse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormStatusWarehouseDto>>> GetFormStatusWarehouses()
        {
            var formStatusWarehouses = await _context.FormStatusWarehouses
                .Include(f => f.CreatedByUser)
                .Include(f => f.UpdatedByUser)
                .Where(f => !f.IsDeleted)
                .Select(f => new FormStatusWarehouseDto
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

            return Ok(formStatusWarehouses);
        }

        // GET: api/FormStatusWarehouse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FormStatusWarehouseDto>> GetFormStatusWarehouse(Guid id)
        {
            var formStatusWarehouse = await _context.FormStatusWarehouses
                .Include(f => f.CreatedByUser)
                .Include(f => f.UpdatedByUser)
                .Where(f => f.ID == id && !f.IsDeleted)
                .Select(f => new FormStatusWarehouseDto
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

            if (formStatusWarehouse == null)
            {
                return NotFound(new { message = "Form status warehouse not found." });
            }

            return Ok(formStatusWarehouse);
        }

        // POST: api/FormStatusWarehouse
        [HttpPost]
        public async Task<ActionResult<FormStatusWarehouseDto>> CreateFormStatusWarehouse(CreateFormStatusWarehouseDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check for duplicate name
            var existingFormStatusWarehouse = await _context.FormStatusWarehouses
                .AnyAsync(f => f.Name.ToLower() == createDto.Name.ToLower() && !f.IsDeleted);

            if (existingFormStatusWarehouse)
            {
                return BadRequest(new { message = "A form status warehouse with this name already exists." });
            }

            var formStatusWarehouse = new FormStatusWarehouse
            {
                ID = Guid.NewGuid(),
                Name = createDto.Name,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = null, // Set based on authentication context
                UpdatedBy = null  // Set based on authentication context
            };

            _context.FormStatusWarehouses.Add(formStatusWarehouse);
            await _context.SaveChangesAsync();

            // Return the created record with navigation properties
            var createdRecord = await GetFormStatusWarehouse(formStatusWarehouse.ID);
            return CreatedAtAction(nameof(GetFormStatusWarehouse), new { id = formStatusWarehouse.ID }, createdRecord.Value);
        }

        // PUT: api/FormStatusWarehouse/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFormStatusWarehouse(Guid id, UpdateFormStatusWarehouseDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var formStatusWarehouse = await _context.FormStatusWarehouses
                .FirstOrDefaultAsync(f => f.ID == id && !f.IsDeleted);

            if (formStatusWarehouse == null)
            {
                return NotFound(new { message = "Form status warehouse not found." });
            }

            // Check for duplicate name (excluding current record)
            var existingFormStatusWarehouse = await _context.FormStatusWarehouses
                .AnyAsync(f => f.Name.ToLower() == updateDto.Name.ToLower() && f.ID != id && !f.IsDeleted);

            if (existingFormStatusWarehouse)
            {
                return BadRequest(new { message = "A form status warehouse with this name already exists." });
            }

            // Update properties
            formStatusWarehouse.Name = updateDto.Name;
            formStatusWarehouse.UpdatedDate = DateTime.UtcNow;
            formStatusWarehouse.UpdatedBy = null; // Set based on authentication context

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Form status warehouse updated successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The record was modified by another user. Please refresh and try again." });
            }
        }

        // DELETE: api/FormStatusWarehouse/5 (Soft Delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormStatusWarehouse(Guid id)
        {
            var formStatusWarehouse = await _context.FormStatusWarehouses
                .FirstOrDefaultAsync(f => f.ID == id && !f.IsDeleted);

            if (formStatusWarehouse == null)
            {
                return NotFound(new { message = "Form status warehouse not found." });
            }

            // Soft delete
            formStatusWarehouse.IsDeleted = true;
            formStatusWarehouse.UpdatedDate = DateTime.UtcNow;
            formStatusWarehouse.UpdatedBy = null; // Set based on authentication context

            await _context.SaveChangesAsync();

            return Ok(new { message = "Form status warehouse deleted successfully." });
        }
    }
}