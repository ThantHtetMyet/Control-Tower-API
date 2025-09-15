using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportFormTypesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ImportFormTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ImportFormTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImportFormTypesDto>>> GetImportFormTypes()
        {
            var importFormTypes = await _context.ImportFormTypes
                .Where(x => !x.IsDeleted)
                .Include(x => x.CreatedByUser)
                .Include(x => x.UpdatedByUser)
                .Select(x => new ImportFormTypesDto
                {
                    ID = x.ID,
                    Name = x.Name,
                    IsDeleted = x.IsDeleted,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    CreatedByUserName = x.CreatedByUser != null ? x.CreatedByUser.FirstName + " " + x.CreatedByUser.LastName : string.Empty,
                    UpdatedByUserName = x.UpdatedByUser != null ? x.UpdatedByUser.FirstName + " " + x.UpdatedByUser.LastName : string.Empty
                })
                .ToListAsync();

            return Ok(importFormTypes);
        }

        // GET: api/ImportFormTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImportFormTypesDto>> GetImportFormTypes(Guid id)
        {
            var importFormType = await _context.ImportFormTypes
                .Include(x => x.CreatedByUser)
                .Include(x => x.UpdatedByUser)
                .Where(x => x.ID == id && !x.IsDeleted)
                .Select(x => new ImportFormTypesDto
                {
                    ID = x.ID,
                    Name = x.Name,
                    IsDeleted = x.IsDeleted,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    CreatedByUserName = x.CreatedByUser != null ? x.CreatedByUser.FirstName + " " + x.CreatedByUser.LastName : string.Empty,
                    UpdatedByUserName = x.UpdatedByUser != null ? x.UpdatedByUser.FirstName + " " + x.UpdatedByUser.LastName : string.Empty
                })
                .FirstOrDefaultAsync();

            if (importFormType == null)
            {
                return NotFound(new { message = "Import form type not found" });
            }

            return Ok(importFormType);
        }

        // POST: api/ImportFormTypes
        [HttpPost]
        public async Task<ActionResult<ImportFormTypesDto>> CreateImportFormTypes(CreateImportFormTypesDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check for duplicate name
            var existingType = await _context.ImportFormTypes
                .Where(x => x.Name.ToLower() == createDto.Name.ToLower() && !x.IsDeleted)
                .FirstOrDefaultAsync();

            if (existingType != null)
            {
                return Conflict(new { message = "An import form type with this name already exists" });
            }

            var userId = Guid.Parse(createDto.CreatedBy);
            var importFormType = new ImportFormTypes
            {
                ID = Guid.NewGuid(),
                Name = createDto.Name,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = userId,
                UpdatedBy = userId
            };

            _context.ImportFormTypes.Add(importFormType);
            await _context.SaveChangesAsync();

            var result = await _context.ImportFormTypes
                .Include(x => x.CreatedByUser)
                .Include(x => x.UpdatedByUser)
                .Where(x => x.ID == importFormType.ID)
                .Select(x => new ImportFormTypesDto
                {
                    ID = x.ID,
                    Name = x.Name,
                    IsDeleted = x.IsDeleted,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    CreatedByUserName = x.CreatedByUser != null ? x.CreatedByUser.FirstName + " " + x.CreatedByUser.LastName : string.Empty,
                    UpdatedByUserName = x.UpdatedByUser != null ? x.UpdatedByUser.FirstName + " " + x.UpdatedByUser.LastName : string.Empty
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction(nameof(GetImportFormTypes), new { id = importFormType.ID }, result);
        }

        // PUT: api/ImportFormTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImportFormTypes(Guid id, UpdateImportFormTypesDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var importFormType = await _context.ImportFormTypes
                .Where(x => x.ID == id && !x.IsDeleted)
                .FirstOrDefaultAsync();

            if (importFormType == null)
            {
                return NotFound(new { message = "Import form type not found" });
            }

            // Check for duplicate name (excluding current record)
            var existingType = await _context.ImportFormTypes
                .Where(x => x.Name.ToLower() == updateDto.Name.ToLower() && x.ID != id && !x.IsDeleted)
                .FirstOrDefaultAsync();

            if (existingType != null)
            {
                return Conflict(new { message = "An import form type with this name already exists" });
            }

            importFormType.Name = updateDto.Name;
            importFormType.UpdatedDate = DateTime.UtcNow;
            importFormType.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImportFormTypesExists(id))
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

        // DELETE: api/ImportFormTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImportFormTypes(Guid id)
        {
            var importFormType = await _context.ImportFormTypes
                .Where(x => x.ID == id && !x.IsDeleted)
                .FirstOrDefaultAsync();

            if (importFormType == null)
            {
                return NotFound(new { message = "Import form type not found" });
            }

            // Soft delete
            importFormType.IsDeleted = true;
            importFormType.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImportFormTypesExists(Guid id)
        {
            return _context.ImportFormTypes.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}