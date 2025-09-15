using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportFileRecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ImportFileRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ImportFileRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImportFileRecordsDto>>> GetImportFileRecords()
        {
            var importFileRecords = await _context.ImportFileRecords
                .Include(i => i.ImportFormType)
                .Include(i => i.UploadedByUser)
                .Include(i => i.CreatedByUser)
                .Include(i => i.UpdatedByUser)
                .Where(i => !i.IsDeleted)
                .Select(i => new ImportFileRecordsDto
                {
                    ID = i.ID,
                    ImportFormTypeID = i.ImportFormTypeID,
                    Name = i.Name,
                    StoredDirectory = i.StoredDirectory,
                    ImportedStatus = i.ImportedStatus,
                    UploadedStatus = i.UploadedStatus,
                    IsDeleted = i.IsDeleted,
                    UploadedDate = i.UploadedDate,
                    UploadedBy = i.UploadedBy,
                    CreatedDate = i.CreatedDate,
                    UpdatedDate = i.UpdatedDate,
                    CreatedBy = i.CreatedBy,
                    UpdatedBy = i.UpdatedBy,
                    ImportFormTypeName = i.ImportFormType != null ? i.ImportFormType.Name : null,
                    UploadedByUserName = i.UploadedByUser != null ? $"{i.UploadedByUser.FirstName} {i.UploadedByUser.LastName}" : null,
                    CreatedByUserName = i.CreatedByUser != null ? $"{i.CreatedByUser.FirstName} {i.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = i.UpdatedByUser != null ? $"{i.UpdatedByUser.FirstName} {i.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            return Ok(importFileRecords);
        }

        // GET: api/ImportFileRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImportFileRecordsDto>> GetImportFileRecord(Guid id)
        {
            var importFileRecord = await _context.ImportFileRecords
                .Include(i => i.ImportFormType)
                .Include(i => i.UploadedByUser)
                .Include(i => i.CreatedByUser)
                .Include(i => i.UpdatedByUser)
                .Where(i => i.ID == id && !i.IsDeleted)
                .Select(i => new ImportFileRecordsDto
                {
                    ID = i.ID,
                    ImportFormTypeID = i.ImportFormTypeID,
                    Name = i.Name,
                    StoredDirectory = i.StoredDirectory,
                    ImportedStatus = i.ImportedStatus,
                    UploadedStatus = i.UploadedStatus,
                    IsDeleted = i.IsDeleted,
                    UploadedDate = i.UploadedDate,
                    UploadedBy = i.UploadedBy,
                    CreatedDate = i.CreatedDate,
                    UpdatedDate = i.UpdatedDate,
                    CreatedBy = i.CreatedBy,
                    UpdatedBy = i.UpdatedBy,
                    ImportFormTypeName = i.ImportFormType != null ? i.ImportFormType.Name : null,
                    UploadedByUserName = i.UploadedByUser != null ? $"{i.UploadedByUser.FirstName} {i.UploadedByUser.LastName}" : null,
                    CreatedByUserName = i.CreatedByUser != null ? $"{i.CreatedByUser.FirstName} {i.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = i.UpdatedByUser != null ? $"{i.UpdatedByUser.FirstName} {i.UpdatedByUser.LastName}" : null
                })
                .FirstOrDefaultAsync();

            if (importFileRecord == null)
            {
                return NotFound(new { message = "Import file record not found." });
            }

            return Ok(importFileRecord);
        }

        // POST: api/ImportFileRecords
        [HttpPost]
        public async Task<ActionResult<ImportFileRecordsDto>> CreateImportFileRecord(CreateImportFileRecordsDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if ImportFormType exists
            var importFormTypeExists = await _context.ImportFormTypes
                .AnyAsync(ift => ift.ID == createDto.ImportFormTypeID && !ift.IsDeleted);
            if (!importFormTypeExists)
            {
                return BadRequest(new { message = "Invalid ImportFormType ID." });
            }

            // Check if UploadedBy user exists (if provided)
            if (createDto.UploadedBy.HasValue)
            {
                var userExists = await _context.Users
                    .AnyAsync(u => u.ID == createDto.UploadedBy.Value && !u.IsDeleted);
                if (!userExists)
                {
                    return BadRequest(new { message = "Invalid UploadedBy user ID." });
                }
            }

            var importFileRecord = new ImportFileRecords
            {
                ID = Guid.NewGuid(),
                ImportFormTypeID = createDto.ImportFormTypeID,
                Name = createDto.Name,
                StoredDirectory = createDto.StoredDirectory,
                ImportedStatus = createDto.ImportedStatus,
                UploadedStatus = createDto.UploadedStatus,
                UploadedDate = createDto.UploadedDate,
                UploadedBy = createDto.UploadedBy,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = null, // Set based on authentication context
                UpdatedBy = null  // Set based on authentication context
            };

            _context.ImportFileRecords.Add(importFileRecord);
            await _context.SaveChangesAsync();

            // Return the created record with navigation properties
            var createdRecord = await GetImportFileRecord(importFileRecord.ID);
            return CreatedAtAction(nameof(GetImportFileRecord), new { id = importFileRecord.ID }, createdRecord.Value);
        }

        // PUT: api/ImportFileRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImportFileRecord(Guid id, UpdateImportFileRecordsDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var importFileRecord = await _context.ImportFileRecords
                .FirstOrDefaultAsync(i => i.ID == id && !i.IsDeleted);

            if (importFileRecord == null)
            {
                return NotFound(new { message = "Import file record not found." });
            }

            // Check if ImportFormType exists
            var importFormTypeExists = await _context.ImportFormTypes
                .AnyAsync(ift => ift.ID == updateDto.ImportFormTypeID && !ift.IsDeleted);
            if (!importFormTypeExists)
            {
                return BadRequest(new { message = "Invalid ImportFormType ID." });
            }

            // Check if UploadedBy user exists (if provided)
            if (updateDto.UploadedBy.HasValue)
            {
                var userExists = await _context.Users
                    .AnyAsync(u => u.ID == updateDto.UploadedBy.Value && !u.IsDeleted);
                if (!userExists)
                {
                    return BadRequest(new { message = "Invalid UploadedBy user ID." });
                }
            }

            // Update properties
            importFileRecord.ImportFormTypeID = updateDto.ImportFormTypeID;
            importFileRecord.Name = updateDto.Name;
            importFileRecord.StoredDirectory = updateDto.StoredDirectory;
            importFileRecord.ImportedStatus = updateDto.ImportedStatus;
            importFileRecord.UploadedStatus = updateDto.UploadedStatus;
            importFileRecord.UploadedDate = updateDto.UploadedDate;
            importFileRecord.UploadedBy = updateDto.UploadedBy;
            importFileRecord.UpdatedDate = DateTime.UtcNow;
            importFileRecord.UpdatedBy = null; // Set based on authentication context

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Import file record updated successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The record was modified by another user. Please refresh and try again." });
            }
        }

        // DELETE: api/ImportFileRecords/5 (Soft Delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImportFileRecord(Guid id)
        {
            var importFileRecord = await _context.ImportFileRecords
                .FirstOrDefaultAsync(i => i.ID == id && !i.IsDeleted);

            if (importFileRecord == null)
            {
                return NotFound(new { message = "Import file record not found." });
            }

            // Soft delete
            importFileRecord.IsDeleted = true;
            importFileRecord.UpdatedDate = DateTime.UtcNow;
            importFileRecord.UpdatedBy = null; // Set based on authentication context

            await _context.SaveChangesAsync();

            return Ok(new { message = "Import file record deleted successfully." });
        }

        // GET: api/ImportFileRecords/by-form-type/{formTypeId}
        [HttpGet("by-form-type/{formTypeId}")]
        public async Task<ActionResult<IEnumerable<ImportFileRecordsDto>>> GetImportFileRecordsByFormType(Guid formTypeId)
        {
            var importFileRecords = await _context.ImportFileRecords
                .Include(i => i.ImportFormType)
                .Include(i => i.UploadedByUser)
                .Include(i => i.CreatedByUser)
                .Include(i => i.UpdatedByUser)
                .Where(i => i.ImportFormTypeID == formTypeId && !i.IsDeleted)
                .Select(i => new ImportFileRecordsDto
                {
                    ID = i.ID,
                    ImportFormTypeID = i.ImportFormTypeID,
                    Name = i.Name,
                    StoredDirectory = i.StoredDirectory,
                    ImportedStatus = i.ImportedStatus,
                    UploadedStatus = i.UploadedStatus,
                    IsDeleted = i.IsDeleted,
                    UploadedDate = i.UploadedDate,
                    UploadedBy = i.UploadedBy,
                    CreatedDate = i.CreatedDate,
                    UpdatedDate = i.UpdatedDate,
                    CreatedBy = i.CreatedBy,
                    UpdatedBy = i.UpdatedBy,
                    ImportFormTypeName = i.ImportFormType != null ? i.ImportFormType.Name : null,
                    UploadedByUserName = i.UploadedByUser != null ? $"{i.UploadedByUser.FirstName} {i.UploadedByUser.LastName}" : null,
                    CreatedByUserName = i.CreatedByUser != null ? $"{i.CreatedByUser.FirstName} {i.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = i.UpdatedByUser != null ? $"{i.UpdatedByUser.FirstName} {i.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            return Ok(importFileRecords);
        }
    }
}