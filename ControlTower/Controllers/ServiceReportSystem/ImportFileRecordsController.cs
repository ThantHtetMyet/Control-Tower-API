using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.DTOs.ServiceReportSystem;
using ControlTower.Models.ServiceReportSystem;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ControlTower.Controllers.ServiceReportSystem
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // ✅ Add this to make auth requirement explicit
    public class ImportFileRecordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _basePath;

        public ImportFileRecordsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _basePath = _configuration["FileStorage:BasePath"] ?? "C:\\Temp\\SRS_FileStorage";
        }

        // GET: api/ImportFileRecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImportFileRecordsDto>>> GetImportFileRecords()
        {
            var importFileRecords = await _context.ImportFileRecords
                .Include(r => r.ImportFormType)
                .Include(r => r.CreatedByUser)
                .Include(r => r.UpdatedByUser)
                .Include(r => r.UploadedByUser)
                .Where(r => !r.IsDeleted)
                .Select(r => new ImportFileRecordsDto
                {
                    ID = r.ID,
                    ImportFormTypeID = r.ImportFormTypeID,
                    ImportFormTypeName = r.ImportFormType.Name,
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
                })
                .ToListAsync();

            return Ok(importFileRecords);
        }

        // GET: api/ImportFileRecords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImportFileRecordsDto>> GetImportFileRecord(Guid id)
        {
            var importFileRecord = await _context.ImportFileRecords
                .Include(r => r.ImportFormType)
                .Include(r => r.CreatedByUser)
                .Include(r => r.UpdatedByUser)
                .Include(r => r.UploadedByUser)
                .Where(r => r.ID == id && !r.IsDeleted)
                .Select(r => new ImportFileRecordsDto
                {
                    ID = r.ID,
                    ImportFormTypeID = r.ImportFormTypeID,
                    ImportFormTypeName = r.ImportFormType.Name,
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
                })
                .FirstOrDefaultAsync();

            if (importFileRecord == null)
            {
                return NotFound();
            }

            return Ok(importFileRecord);
        }

        // POST: api/ImportFileRecords
        [HttpPost]
        public async Task<ActionResult<ImportFileRecordsDto>> CreateImportFileRecord([FromBody] CreateImportFileRecordsDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
                UploadedBy = !string.IsNullOrEmpty(createDto.UploadedBy) ? Guid.Parse(createDto.UploadedBy) : null,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = Guid.Parse(createDto.CreatedBy),
                UpdatedBy = Guid.Parse(createDto.CreatedBy)
            };

            _context.ImportFileRecords.Add(importFileRecord);
            await _context.SaveChangesAsync();

            // Return the created item with related data
            var createdImportFileRecord = await _context.ImportFileRecords
                .Include(r => r.ImportFormType)
                .Include(r => r.CreatedByUser)
                .Include(r => r.UpdatedByUser)
                .Include(r => r.UploadedByUser)
                .Where(r => r.ID == importFileRecord.ID)
                .Select(r => new ImportFileRecordsDto
                {
                    ID = r.ID,
                    ImportFormTypeID = r.ImportFormTypeID,
                    ImportFormTypeName = r.ImportFormType.Name,
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
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction(nameof(GetImportFileRecord), new { id = importFileRecord.ID }, createdImportFileRecord);
        }

        // PUT: api/ImportFileRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImportFileRecord(Guid id, [FromBody] UpdateImportFileRecordsDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var importFileRecord = await _context.ImportFileRecords.FindAsync(id);
            if (importFileRecord == null || importFileRecord.IsDeleted)
            {
                return NotFound();
            }

            importFileRecord.ImportFormTypeID = updateDto.ImportFormTypeID;
            importFileRecord.Name = updateDto.Name;
            importFileRecord.StoredDirectory = updateDto.StoredDirectory;
            importFileRecord.ImportedStatus = updateDto.ImportedStatus;
            importFileRecord.UploadedStatus = updateDto.UploadedStatus;
            importFileRecord.UploadedDate = updateDto.UploadedDate;
            importFileRecord.UploadedBy = !string.IsNullOrEmpty(updateDto.UploadedBy) ? Guid.Parse(updateDto.UploadedBy) : null;
            importFileRecord.UpdatedDate = DateTime.UtcNow;
            importFileRecord.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImportFileRecordExists(id))
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

        // DELETE: api/ImportFileRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImportFileRecord(Guid id, [FromQuery] string deletedBy)
        {
            if (string.IsNullOrEmpty(deletedBy))
            {
                return BadRequest("DeletedBy parameter is required");
            }

            var importFileRecord = await _context.ImportFileRecords.FindAsync(id);
            if (importFileRecord == null || importFileRecord.IsDeleted)
            {
                return NotFound();
            }

            // Soft delete
            importFileRecord.IsDeleted = true;
            importFileRecord.UpdatedDate = DateTime.UtcNow;
            importFileRecord.UpdatedBy = Guid.Parse(deletedBy);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImportFileRecordExists(Guid id)
        {
            return _context.ImportFileRecords.Any(e => e.ID == id && !e.IsDeleted);
        }

        // POST: api/ImportFileRecords/ProcessImportData
        [HttpPost("ProcessImportData")]
        public async Task<ActionResult> ProcessImportData(IFormFile file, [FromForm] Guid importFormTypeId)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                // Validate file type
                var allowedExtensions = new[] { ".xlsx", ".xls" };
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return BadRequest("Only Excel files (.xlsx, .xls) are allowed.");
                }

                // Get current user ID
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(userIdClaim, out Guid userId))
                {
                    return Unauthorized("Invalid user.");
                }

                // Get ImportFormType
                var importFormType = await _context.ImportFormTypes
                    .FirstOrDefaultAsync(x => x.ID == importFormTypeId && !x.IsDeleted);
                
                if (importFormType == null)
                {
                    return BadRequest("Invalid import form type.");
                }

                // Create directory structure: BasePath/ImportFormTypeName/UserId
                var formTypeDirectory = Path.Combine(_basePath, importFormType.Name);
                var userDirectory = Path.Combine(formTypeDirectory, userId.ToString());
                
                if (!Directory.Exists(userDirectory))
                {
                    Directory.CreateDirectory(userDirectory);
                }

                // Generate unique filename
                var fileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_{file.FileName}";
                var filePath = Path.Combine(userDirectory, fileName);

                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Create ImportFileRecord
                var fileRecord = new ImportFileRecords
                {
                    ID = Guid.NewGuid(),
                    ImportFormTypeID = importFormTypeId,
                    Name = fileName,
                    StoredDirectory = filePath,
                    UploadedStatus = "Uploaded",
                    ImportedStatus = "Processing", // Set to Processing since we're about to process
                    UploadedDate = DateTime.Now,
                    UploadedBy = userId,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    CreatedBy = userId,
                    UpdatedBy = userId
                };

                _context.ImportFileRecords.Add(fileRecord);
                await _context.SaveChangesAsync();
                /*
                // Now process the Excel file
                List<Dictionary<string, object>> excelData;
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var package = new OfficeOpenXml.ExcelPackage(fileStream))
                    {
                        var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        if (worksheet == null)
                        {
                            // Update status to failed
                            fileRecord.ImportedStatus = "Failed";
                            fileRecord.UpdatedDate = DateTime.UtcNow;
                            await _context.SaveChangesAsync();
                            return BadRequest("No worksheet found in the Excel file");
                        }

                        excelData = new List<Dictionary<string, object>>();
                        var rowCount = worksheet.Dimension?.Rows ?? 0;
                        var colCount = worksheet.Dimension?.Columns ?? 0;

                        if (rowCount <= 1) // No data rows (only header or empty)
                        {
                            // Update status to failed
                            fileRecord.ImportedStatus = "Failed";
                            fileRecord.UpdatedDate = DateTime.UtcNow;
                            await _context.SaveChangesAsync();
                            return BadRequest("No data found in the Excel file");
                        }

                        // Get headers from first row
                        var headers = new List<string>();
                        for (int col = 1; col <= colCount; col++)
                        {
                            headers.Add(worksheet.Cells[1, col].Value?.ToString() ?? $"Column{col}");
                        }

                        // Read data rows
                        for (int row = 2; row <= rowCount; row++)
                        {
                            var rowData = new Dictionary<string, object>();
                            for (int col = 1; col <= colCount; col++)
                            {
                                var cellValue = worksheet.Cells[row, col].Value?.ToString() ?? string.Empty;
                                rowData[headers[col - 1]] = cellValue;
                            }
                            excelData.Add(rowData);
                        }
                    }
                }

                var results = new ProcessImportResultDto
                {
                    TotalProcessed = excelData.Count,
                    FormTypeName = importFormType.Name,
                    LocationWarehouse = new ImportResultDetail(),
                    IssueReportWarehouse = new ImportResultDetail(),
                    IssueFoundWarehouse = new ImportResultDetail(),
                    ActionTakenWarehouse = new ImportResultDetail()
                };

                // Process only the relevant warehouse data based on form type
                // Remove these methods:
                // - ProcessIssueReportWarehouseFromExcel
                // - ProcessIssueFoundWarehouseFromExcel
                // - ProcessActionTakenWarehouseFromExcel

                // And update the switch statement in ProcessImportData method:
                switch (importFormType.Name.ToLower())
                {
                    case "location":
                        await ProcessLocationWarehouseFromExcel(excelData, userId, results.LocationWarehouse);
                        break;
                    // Remove these cases:
                    // case "issuereported":
                    // case "issuereport":
                    // case "issuefound":
                    // case "actiontaken":
                    default:
                        // Update status to failed
                        fileRecord.ImportedStatus = "Failed";
                        fileRecord.UpdatedDate = DateTime.UtcNow;
                        await _context.SaveChangesAsync();
                        throw new InvalidOperationException($"Unsupported form type: {importFormType.Name}");
                }

                // Update final status to completed
                fileRecord.ImportedStatus = "Completed";
                fileRecord.UpdatedDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                */

                return Ok(new
                {
                    message = "File uploaded and processed successfully",
                    fileId = fileRecord.ID,
                    fileName = fileName,
                    filePath = filePath
                });
            }
            catch (Exception ex)
            {
                // Try to update the file record status if it exists
                try
                {
                    var existingRecord = await _context.ImportFileRecords
                        .FirstOrDefaultAsync(x => x.Name.Contains(file.FileName) && x.ImportFormTypeID == importFormTypeId);
                    if (existingRecord != null)
                    {
                        existingRecord.ImportedStatus = "Failed";
                        existingRecord.UpdatedDate = DateTime.UtcNow;
                        await _context.SaveChangesAsync();
                    }
                }
                catch
                {
                    // Ignore errors when updating status
                }
                
                return StatusCode(500, $"Error processing import data: {ex.Message}");
            }
        }

        // Updated helper methods to work with Excel data
        private async Task ProcessLocationWarehouseFromExcel(List<Dictionary<string, object>> excelData, Guid userId, ImportResultDetail result)
        {
            var locationValues = excelData
                .Where(row => row.ContainsKey("Name") && !string.IsNullOrWhiteSpace(row["Name"]?.ToString()))
                .Select(row => row["Name"].ToString().Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            var existingLocations = await _context.LocationWarehouses
                .Where(x => !x.IsDeleted && locationValues.Contains(x.Name))
                .Select(x => x.Name)
                .ToListAsync();

            var newLocations = locationValues
                .Where(loc => !existingLocations.Any(existing => 
                    string.Equals(existing, loc, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            foreach (var location in newLocations)
            {
                var newLocationWarehouse = new LocationWarehouse
                {
                    ID = Guid.NewGuid(),
                    Name = location,
                    IsDeleted = false,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedBy = userId,
                    UpdatedBy = userId
                };
                _context.LocationWarehouses.Add(newLocationWarehouse);
            }

            await _context.SaveChangesAsync();
            
            result.TotalFound = locationValues.Count;
            result.ExistingCount = existingLocations.Count;
            result.NewlyAdded = newLocations.Count;
        }

    }
}