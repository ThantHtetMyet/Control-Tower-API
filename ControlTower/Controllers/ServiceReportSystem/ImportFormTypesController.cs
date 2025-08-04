using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.DTOs.ServiceReportSystem;
using ControlTower.Models.ServiceReportSystem;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Controllers.ServiceReportSystem
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
                .Include(i => i.CreatedByUser)
                .Include(i => i.UpdatedByUser)
                .Include(i => i.ImportFileRecords)
                    .ThenInclude(r => r.CreatedByUser)
                .Where(i => !i.IsDeleted)
                .Select(i => new ImportFormTypesDto
                {
                    ID = i.ID,
                    Name = i.Name,
                    IsDeleted = i.IsDeleted,
                    CreatedDate = i.CreatedDate,
                    UpdatedDate = i.UpdatedDate,
                    CreatedByUserName = i.CreatedByUser != null ? i.CreatedByUser.FirstName + " " + i.CreatedByUser.LastName : null,
                    UpdatedByUserName = i.UpdatedByUser != null ? i.UpdatedByUser.FirstName + " " + i.UpdatedByUser.LastName : null,
                    ImportFileRecords = i.ImportFileRecords != null ? i.ImportFileRecords
                        .Where(r => !r.IsDeleted)
                        .Select(r => new ImportFileRecordsDto
                        {
                            ID = r.ID,
                            ImportFormTypeID = r.ImportFormTypeID,
                            ImportFormTypeName = i.Name,
                            Name = r.Name,
                            StoredDirectory = r.StoredDirectory,
                            ImportedStatus = r.ImportedStatus,
                            UploadedStatus = r.UploadedStatus,
                            IsDeleted = r.IsDeleted,
                            CreatedDate = r.CreatedDate,
                            UpdatedDate = r.UpdatedDate,
                            UploadedDate = r.UploadedDate,
                            UploadedByUserName = r.UploadedByUser != null ? r.UploadedByUser.FirstName + " " + r.UploadedByUser.LastName : null,
                            CreatedByUserName = r.CreatedByUser != null ? r.CreatedByUser.FirstName + " " + r.CreatedByUser.LastName : null,
                            UpdatedByUserName = r.UpdatedByUser != null ? r.UpdatedByUser.FirstName + " " + r.UpdatedByUser.LastName : null
                        }).ToList() : new List<ImportFileRecordsDto>()
                })
                .ToListAsync();

            return Ok(importFormTypes);
        }

        // GET: api/ImportFormTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImportFormTypesDto>> GetImportFormType(Guid id)
        {
            var importFormType = await _context.ImportFormTypes
                .Include(i => i.CreatedByUser)
                .Include(i => i.UpdatedByUser)
                .Include(i => i.ImportFileRecords)
                    .ThenInclude(r => r.CreatedByUser)
                .Include(i => i.ImportFileRecords)
                    .ThenInclude(r => r.UpdatedByUser)
                .Include(i => i.ImportFileRecords)
                    .ThenInclude(r => r.UploadedByUser)
                .Where(i => i.ID == id && !i.IsDeleted)
                .Select(i => new ImportFormTypesDto
                {
                    ID = i.ID,
                    Name = i.Name,
                    IsDeleted = i.IsDeleted,
                    CreatedDate = i.CreatedDate,
                    UpdatedDate = i.UpdatedDate,
                    CreatedByUserName = i.CreatedByUser != null ? i.CreatedByUser.FirstName + " " + i.CreatedByUser.LastName : null,
                    UpdatedByUserName = i.UpdatedByUser != null ? i.UpdatedByUser.FirstName + " " + i.UpdatedByUser.LastName : null,
                    ImportFileRecords = i.ImportFileRecords != null ? i.ImportFileRecords
                        .Where(r => !r.IsDeleted)
                        .Select(r => new ImportFileRecordsDto
                        {
                            ID = r.ID,
                            ImportFormTypeID = r.ImportFormTypeID,
                            ImportFormTypeName = i.Name,
                            Name = r.Name,
                            StoredDirectory = r.StoredDirectory,
                            ImportedStatus = r.ImportedStatus,
                            UploadedStatus = r.UploadedStatus,
                            IsDeleted = r.IsDeleted,
                            CreatedDate = r.CreatedDate,
                            UpdatedDate = r.UpdatedDate,
                            UploadedDate = r.UploadedDate,
                            UploadedByUserName = r.UploadedByUser != null ? r.UploadedByUser.FirstName + " " + r.UploadedByUser.LastName : null,
                            CreatedByUserName = r.CreatedByUser != null ? r.CreatedByUser.FirstName + " " + r.CreatedByUser.LastName : null,
                            UpdatedByUserName = r.UpdatedByUser != null ? r.UpdatedByUser.FirstName + " " + r.UpdatedByUser.LastName : null
                        }).ToList() : new List<ImportFileRecordsDto>()
                })
                .FirstOrDefaultAsync();

            if (importFormType == null)
            {
                return NotFound();
            }

            return Ok(importFormType);
        }

        // POST: api/ImportFormTypes
        [HttpPost]
        public async Task<ActionResult<ImportFormTypesDto>> CreateImportFormType([FromBody] CreateImportFormTypesDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var importFormType = new ImportFormTypes
            {
                ID = Guid.NewGuid(),
                Name = createDto.Name,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = Guid.Parse(createDto.CreatedBy),
                UpdatedBy = Guid.Parse(createDto.CreatedBy)
            };

            _context.ImportFormTypes.Add(importFormType);
            await _context.SaveChangesAsync();

            // Return the created item with user names
            var createdImportFormType = await _context.ImportFormTypes
                .Include(i => i.CreatedByUser)
                .Include(i => i.UpdatedByUser)
                .Where(i => i.ID == importFormType.ID)
                .Select(i => new ImportFormTypesDto
                {
                    ID = i.ID,
                    Name = i.Name,
                    IsDeleted = i.IsDeleted,
                    CreatedDate = i.CreatedDate,
                    UpdatedDate = i.UpdatedDate,
                    CreatedByUserName = i.CreatedByUser != null ? i.CreatedByUser.FirstName + " " + i.CreatedByUser.LastName : null,
                    UpdatedByUserName = i.UpdatedByUser != null ? i.UpdatedByUser.FirstName + " " + i.UpdatedByUser.LastName : null,
                    ImportFileRecords = new List<ImportFileRecordsDto>()
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction(nameof(GetImportFormType), new { id = importFormType.ID }, createdImportFormType);
        }

        // PUT: api/ImportFormTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImportFormType(Guid id, [FromBody] UpdateImportFormTypesDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var importFormType = await _context.ImportFormTypes.FindAsync(id);
            if (importFormType == null || importFormType.IsDeleted)
            {
                return NotFound();
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
                if (!ImportFormTypeExists(id))
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
        public async Task<IActionResult> DeleteImportFormType(Guid id, [FromQuery] string deletedBy)
        {
            if (string.IsNullOrEmpty(deletedBy))
            {
                return BadRequest("DeletedBy parameter is required");
            }

            var importFormType = await _context.ImportFormTypes.FindAsync(id);
            if (importFormType == null || importFormType.IsDeleted)
            {
                return NotFound();
            }

            // Soft delete
            importFormType.IsDeleted = true;
            importFormType.UpdatedDate = DateTime.UtcNow;
            importFormType.UpdatedBy = Guid.Parse(deletedBy);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImportFormTypeExists(Guid id)
        {
            return _context.ImportFormTypes.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}