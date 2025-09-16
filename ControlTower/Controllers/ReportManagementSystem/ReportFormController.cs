using ControlTower.Data;
using ControlTower.DTOs.ReportManagementSystem;
using ControlTower.Models.ReportManagementSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportFormController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportFormController(ApplicationDbContext context)
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

        // GET: api/ReportForm
        [HttpGet]
        public async Task<ActionResult<object>> GetReportForms(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string search = "",
            [FromQuery] Guid? reportFormTypeId = null,
            [FromQuery] Guid? SystemNameWarehouseID = null,
            [FromQuery] Guid? StationNameWarehouseID = null)
        {
            var query = _context.ReportForms
                .Where(rf => !rf.IsDeleted)
                .Include(rf => rf.ReportFormType)
                .Include(rf => rf.SystemNameWarehouse)
                .Include(rf => rf.StationNameWarehouse)
                .Include(rf => rf.CreatedByUser)
                .Include(rf => rf.UpdatedByUser)
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(rf => rf.JobNo.Contains(search) ||
                                         rf.ReportFormType.Name.Contains(search) ||
                                         rf.SystemNameWarehouse.Name.Contains(search) ||
                                         rf.StationNameWarehouse.Name.Contains(search) ||
                                         rf.CreatedByUser.FirstName.Contains(search) ||
                                         rf.CreatedByUser.LastName.Contains(search));
            }

            // Apply filters
            if (reportFormTypeId.HasValue)
            {
                query = query.Where(rf => rf.ReportFormTypeID == reportFormTypeId.Value);
            }

            if (SystemNameWarehouseID.HasValue)
            {
                query = query.Where(rf => rf.SystemNameWarehouseID == SystemNameWarehouseID.Value);
            }

            if (StationNameWarehouseID.HasValue)
            {
                query = query.Where(rf => rf.StationNameWarehouseID == StationNameWarehouseID.Value);
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var reportForms = await query
                .OrderByDescending(rf => rf.CreatedDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(rf => new
                {
                    ID = rf.ID,
                    ReportFormTypeID = rf.ReportFormTypeID,
                    ReportFormTypeName = rf.ReportFormType.Name,
                    JobNo = rf.JobNo,
                    SystemNameWarehouseID = rf.SystemNameWarehouseID,
                    SystemNameWarehouseName = rf.SystemNameWarehouse.Name,
                    StationNameWarehouseID = rf.StationNameWarehouseID,
                    StationNameWarehouseName = rf.StationNameWarehouse.Name,
                    // Get CM Report data if exists
                    CMCustomer = _context.CMReportForms
                        .Where(cm => cm.ReportFormID == rf.ID && !cm.IsDeleted)
                        .Select(cm => cm.Customer)
                        .FirstOrDefault(),
                    CMProjectNo = _context.CMReportForms
                        .Where(cm => cm.ReportFormID == rf.ID && !cm.IsDeleted)
                        .Select(cm => cm.ProjectNo)
                        .FirstOrDefault(),
                    // Get PM Report data if exists
                    PMCustomer = _context.PMReportFormRTU
                        .Where(pm => pm.ReportFormID == rf.ID && !pm.IsDeleted)
                        .Select(pm => pm.Customer)
                        .FirstOrDefault(),
                    PMProjectNo = _context.PMReportFormRTU
                        .Where(pm => pm.ReportFormID == rf.ID && !pm.IsDeleted)
                        .Select(pm => pm.ProjectNo)
                        .FirstOrDefault(),
                    IsDeleted = rf.IsDeleted,
                    CreatedDate = rf.CreatedDate,
                    UpdatedDate = rf.UpdatedDate,
                    CreatedBy = rf.CreatedBy,
                    CreatedByUserName = rf.CreatedByUser.FirstName + " " + rf.CreatedByUser.LastName,
                    UpdatedBy = rf.UpdatedBy,
                    UpdatedByUserName = rf.UpdatedByUser != null ? rf.UpdatedByUser.FirstName + " " + rf.UpdatedByUser.LastName : null,
                    UploadStatus = rf.UploadStatus,
                    UploadHostname = rf.UploadHostname,
                    UploadIPAddress = rf.UploadIPAddress,
                    FormStatus = rf.FormStatus
                })
                .ToListAsync();

            return Ok(new
            {
                data = reportForms,
                totalCount,
                totalPages,
                currentPage = page,
                pageSize
            });
        }

        // GET: api/ReportForm/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportFormDto>> GetReportForm(Guid id)
        {
            var reportForm = await _context.ReportForms
                .Where(rf => rf.ID == id && !rf.IsDeleted)
                .Include(rf => rf.ReportFormType)
                .Include(rf => rf.SystemNameWarehouse)
                .Include(rf => rf.StationNameWarehouse)
                .Include(rf => rf.CreatedByUser)
                .Include(rf => rf.UpdatedByUser)
                .Select(rf => new ReportFormDto
                {
                    ID = rf.ID,
                    ReportFormTypeID = rf.ReportFormTypeID,
                    ReportFormTypeName = rf.ReportFormType.Name,
                    JobNo = rf.JobNo,
                    SystemNameWarehouseID = rf.SystemNameWarehouseID,
                    SystemNameWarehouseName = rf.SystemNameWarehouse.Name,
                    StationNameWarehouseID = rf.StationNameWarehouseID,
                    StationNameWarehouseName = rf.StationNameWarehouse.Name,
                    IsDeleted = rf.IsDeleted,
                    CreatedDate = rf.CreatedDate,
                    UpdatedDate = rf.UpdatedDate,
                    CreatedBy = rf.CreatedBy,
                    CreatedByUserName = rf.CreatedByUser.FirstName + " " + rf.CreatedByUser.LastName,
                    UpdatedBy = rf.UpdatedBy,
                    UpdatedByUserName = rf.UpdatedByUser != null ? rf.UpdatedByUser.FirstName + " " + rf.UpdatedByUser.LastName : null,
                    UploadStatus = rf.UploadStatus,
                    UploadHostname = rf.UploadHostname,
                    UploadIPAddress = rf.UploadIPAddress,
                    FormStatus = rf.FormStatus
                })
                .FirstOrDefaultAsync();

            if (reportForm == null)
            {
                return NotFound(new { message = "ReportForm not found" });
            }

            return Ok(reportForm);
        }

        // GET: api/ReportForm/ByReportFormType/5
        [HttpGet("ByReportFormType/{reportFormTypeId}")]
        public async Task<ActionResult<IEnumerable<ReportFormDto>>> GetReportFormsByType(Guid reportFormTypeId)
        {
            var reportForms = await _context.ReportForms
                .Where(rf => rf.ReportFormTypeID == reportFormTypeId && !rf.IsDeleted)
                .Include(rf => rf.ReportFormType)
                .Include(rf => rf.SystemNameWarehouse)
                .Include(rf => rf.StationNameWarehouse)
                .Include(rf => rf.CreatedByUser)
                .Include(rf => rf.UpdatedByUser)
                .Select(rf => new ReportFormDto
                {
                    ID = rf.ID,
                    ReportFormTypeID = rf.ReportFormTypeID,
                    ReportFormTypeName = rf.ReportFormType.Name,
                    JobNo = rf.JobNo,
                    SystemNameWarehouseID = rf.SystemNameWarehouseID,
                    SystemNameWarehouseName = rf.SystemNameWarehouse.Name,
                    StationNameWarehouseID = rf.StationNameWarehouseID,
                    StationNameWarehouseName = rf.StationNameWarehouse.Name,
                    IsDeleted = rf.IsDeleted,
                    CreatedDate = rf.CreatedDate,
                    UpdatedDate = rf.UpdatedDate,
                    CreatedBy = rf.CreatedBy,
                    CreatedByUserName = rf.CreatedByUser.FirstName + " " + rf.CreatedByUser.LastName,
                    UpdatedBy = rf.UpdatedBy,
                    UpdatedByUserName = rf.UpdatedByUser != null ? rf.UpdatedByUser.FirstName + " " + rf.UpdatedByUser.LastName : null,
                    UploadStatus = rf.UploadStatus,
                    UploadHostname = rf.UploadHostname,
                    UploadIPAddress = rf.UploadIPAddress,
                    FormStatus = rf.FormStatus
                })
                .OrderByDescending(rf => rf.CreatedDate)
                .ToListAsync();

            return Ok(reportForms);
        }

        // GET: api/ReportForm/BySystemName/5
        [HttpGet("BySystemName/{SystemNameWarehouseID}")]
        public async Task<ActionResult<IEnumerable<ReportFormDto>>> GetReportFormsBySystemName(Guid SystemNameWarehouseID)
        {
            var reportForms = await _context.ReportForms
                .Where(rf => rf.SystemNameWarehouseID == SystemNameWarehouseID && !rf.IsDeleted)
                .Include(rf => rf.ReportFormType)
                .Include(rf => rf.SystemNameWarehouse)
                .Include(rf => rf.StationNameWarehouse)
                .Include(rf => rf.CreatedByUser)
                .Include(rf => rf.UpdatedByUser)
                .Select(rf => new ReportFormDto
                {
                    ID = rf.ID,
                    ReportFormTypeID = rf.ReportFormTypeID,
                    ReportFormTypeName = rf.ReportFormType.Name,
                    JobNo = rf.JobNo,
                    SystemNameWarehouseID = rf.SystemNameWarehouseID,
                    SystemNameWarehouseName = rf.SystemNameWarehouse.Name,
                    StationNameWarehouseID = rf.StationNameWarehouseID,
                    StationNameWarehouseName = rf.StationNameWarehouse.Name,
                    IsDeleted = rf.IsDeleted,
                    CreatedDate = rf.CreatedDate,
                    UpdatedDate = rf.UpdatedDate,
                    CreatedBy = rf.CreatedBy,
                    CreatedByUserName = rf.CreatedByUser.FirstName + " " + rf.CreatedByUser.LastName,
                    UpdatedBy = rf.UpdatedBy,
                    UpdatedByUserName = rf.UpdatedByUser != null ? rf.UpdatedByUser.FirstName + " " + rf.UpdatedByUser.LastName : null,
                    UploadStatus = rf.UploadStatus,
                    UploadHostname = rf.UploadHostname,
                    UploadIPAddress = rf.UploadIPAddress,
                    FormStatus = rf.FormStatus
                })
                .OrderByDescending(rf => rf.CreatedDate)
                .ToListAsync();

            return Ok(reportForms);
        }

        // GET: api/ReportForm/ByStationName/5
        [HttpGet("ByStationName/{StationNameWarehouseID}")]
        public async Task<ActionResult<IEnumerable<object>>> GetReportFormsByStationName(Guid StationNameWarehouseID)
        {
            var reportForms = await _context.ReportForms
                .Where(rf => rf.StationNameWarehouseID == StationNameWarehouseID && !rf.IsDeleted)
                .Include(rf => rf.ReportFormType)
                .Include(rf => rf.SystemNameWarehouse)
                .Include(rf => rf.StationNameWarehouse)
                .Include(rf => rf.CreatedByUser)
                .Include(rf => rf.UpdatedByUser)
                .OrderByDescending(rf => rf.CreatedDate)
                .Select(rf => new
                {
                    ID = rf.ID,
                    ReportFormTypeID = rf.ReportFormTypeID,
                    ReportFormTypeName = rf.ReportFormType.Name,
                    JobNo = rf.JobNo,
                    SystemNameWarehouseID = rf.SystemNameWarehouseID,
                    SystemNameWarehouseName = rf.SystemNameWarehouse.Name,
                    StationNameWarehouseID = rf.StationNameWarehouseID,
                    StationNameWarehouseName = rf.StationNameWarehouse.Name,
                    CreatedDate = rf.CreatedDate,
                    CreatedBy = rf.CreatedBy,
                    CreatedByUserName = rf.CreatedByUser.FirstName + " " + rf.CreatedByUser.LastName,
                    UpdatedBy = rf.UpdatedBy,
                    UpdatedByUserName = rf.UpdatedByUser != null ? rf.UpdatedByUser.FirstName + " " + rf.UpdatedByUser.LastName : null,
                    FormStatus = rf.FormStatus
                })
                .ToListAsync();

            return Ok(reportForms);
        }

        // POST: api/ReportForm
        [HttpPost]
        public async Task<ActionResult<ReportFormDto>> CreateReportForm(CreateReportFormDto createDto)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            // Validate ReportFormType exists
            var reportFormTypeExists = await _context.ReportFormTypes
                .AnyAsync(rft => rft.ID == createDto.ReportFormTypeID && !rft.IsDeleted);
            if (!reportFormTypeExists)
            {
                return BadRequest(new { message = "Invalid ReportFormTypeID" });
            }

            // Validate SystemNameWarehouse exists
            var systemNameExists = await _context.SystemNameWarehouses
                .AnyAsync(s => s.ID == createDto.SystemNameWarehouseID && !s.IsDeleted);
            if (!systemNameExists)
            {
                return BadRequest(new { message = "Invalid SystemNameWarehouseID" });
            }

            // Validate StationNameWarehouse exists and belongs to the SystemName
            var stationNameExists = await _context.StationNameWarehouses
                .AnyAsync(s => s.ID == createDto.StationNameWarehouseID && s.SystemNameWarehouseID == createDto.SystemNameWarehouseID && !s.IsDeleted);
            if (!stationNameExists)
            {
                return BadRequest(new { message = "Invalid StationNameWarehouseID or StationName does not belong to the specified SystemName" });
            }

            var reportForm = new ReportForm
            {
                ID = Guid.NewGuid(),
                ReportFormTypeID = createDto.ReportFormTypeID,
                JobNo = createDto.JobNo,
                SystemNameWarehouseID = createDto.SystemNameWarehouseID,
                StationNameWarehouseID = createDto.StationNameWarehouseID,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = currentUserId.Value,
                UploadStatus = createDto.UploadStatus,
                UploadHostname = createDto.UploadHostname,
                UploadIPAddress = createDto.UploadIPAddress,
                FormStatus = createDto.FormStatus
            };

            _context.ReportForms.Add(reportForm);
            await _context.SaveChangesAsync();

            var result = await _context.ReportForms
                .Where(rf => rf.ID == reportForm.ID)
                .Include(rf => rf.ReportFormType)
                .Include(rf => rf.SystemNameWarehouse)
                .Include(rf => rf.StationNameWarehouse)
                .Include(rf => rf.CreatedByUser)
                .Select(rf => new ReportFormDto
                {
                    ID = rf.ID,
                    ReportFormTypeID = rf.ReportFormTypeID,
                    ReportFormTypeName = rf.ReportFormType.Name,
                    JobNo = rf.JobNo,
                    SystemNameWarehouseID = rf.SystemNameWarehouseID,
                    SystemNameWarehouseName = rf.SystemNameWarehouse.Name,
                    StationNameWarehouseID = rf.StationNameWarehouseID,
                    StationNameWarehouseName = rf.StationNameWarehouse.Name,
                    IsDeleted = rf.IsDeleted,
                    CreatedDate = rf.CreatedDate,
                    UpdatedDate = rf.UpdatedDate,
                    CreatedBy = rf.CreatedBy,
                    CreatedByUserName = rf.CreatedByUser.FirstName + " " + rf.CreatedByUser.LastName,
                    UpdatedBy = rf.UpdatedBy,
                    UpdatedByUserName = null,
                    UploadStatus = rf.UploadStatus,
                    UploadHostname = rf.UploadHostname,
                    UploadIPAddress = rf.UploadIPAddress,
                    FormStatus = rf.FormStatus
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction(nameof(GetReportForm), new { id = reportForm.ID }, result);
        }

        // PUT: api/ReportForm/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReportForm(Guid id, UpdateReportFormDto updateDto)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var reportForm = await _context.ReportForms.FindAsync(id);
            if (reportForm == null || reportForm.IsDeleted)
            {
                return NotFound(new { message = "ReportForm not found" });
            }

            // Validate ReportFormType exists
            var reportFormTypeExists = await _context.ReportFormTypes
                .AnyAsync(rft => rft.ID == updateDto.ReportFormTypeID && !rft.IsDeleted);
            if (!reportFormTypeExists)
            {
                return BadRequest(new { message = "Invalid ReportFormTypeID" });
            }

            // Validate SystemNameWarehouse exists
            var systemNameExists = await _context.SystemNameWarehouses
                .AnyAsync(s => s.ID == updateDto.SystemNameWarehouseID && !s.IsDeleted);
            if (!systemNameExists)
            {
                return BadRequest(new { message = "Invalid SystemNameWarehouseID" });
            }

            // Validate StationNameWarehouse exists and belongs to the SystemName
            var stationNameExists = await _context.StationNameWarehouses
                .AnyAsync(s => s.ID == updateDto.StationNameWarehouseID && s.SystemNameWarehouseID == updateDto.SystemNameWarehouseID && !s.IsDeleted);
            if (!stationNameExists)
            {
                return BadRequest(new { message = "Invalid StationNameWarehouseID or StationName does not belong to the specified SystemName" });
            }

            reportForm.ReportFormTypeID = updateDto.ReportFormTypeID;
            reportForm.JobNo = updateDto.JobNo;
            reportForm.SystemNameWarehouseID = updateDto.SystemNameWarehouseID;
            reportForm.StationNameWarehouseID = updateDto.StationNameWarehouseID;
            reportForm.UploadStatus = updateDto.UploadStatus;
            reportForm.UploadHostname = updateDto.UploadHostname;
            reportForm.UploadIPAddress = updateDto.UploadIPAddress;
            reportForm.FormStatus = updateDto.FormStatus;
            reportForm.UpdatedDate = DateTime.UtcNow;
            reportForm.UpdatedBy = currentUserId.Value;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportFormExists(id))
                {
                    return NotFound(new { message = "ReportForm not found" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ReportForm/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportForm(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var reportForm = await _context.ReportForms.FindAsync(id);
            if (reportForm == null || reportForm.IsDeleted)
            {
                return NotFound(new { message = "ReportForm not found" });
            }

            reportForm.IsDeleted = true;
            reportForm.UpdatedDate = DateTime.UtcNow;
            reportForm.UpdatedBy = currentUserId.Value;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportFormExists(Guid id)
        {
            return _context.ReportForms.Any(rf => rf.ID == id && !rf.IsDeleted);
        }
        
        // GET: api/ReportForm/NextJobNumber
        [HttpGet("NextJobNumber")]
        public async Task<ActionResult<object>> GetNextJobNumber()
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                if (currentUserId == null)
                {
                    return Unauthorized(new { message = "User not authenticated" });
                }

                // Get current user's StaffCardID
                var currentUser = await _context.Users
                    .Where(u => u.ID == currentUserId.Value && !u.IsDeleted)
                    .FirstOrDefaultAsync();
                
                if (currentUser == null)
                {
                    return NotFound(new { message = "Current user not found" });
                }

                // Count total existing ReportForms (not deleted) + 1
                var totalCount = await _context.ReportForms
                    .Where(rf => !rf.IsDeleted)
                    .CountAsync();
                
                var nextSequentialNumber = totalCount + 1;
                
                // Generate JobNo: StaffCardID + 4-digit sequential number
                var nextJobNo = $"{currentUser.StaffCardID}{nextSequentialNumber:D4}";

                return Ok(new { jobNumber = nextJobNo });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error generating next job number", error = ex.Message });
            }
        }
    }
}