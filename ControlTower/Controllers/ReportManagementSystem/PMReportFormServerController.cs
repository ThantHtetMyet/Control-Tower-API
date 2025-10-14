using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PMReportFormServerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PMReportFormServerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PMReportFormServer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PMReportFormServerDto>>> GetPMReportFormServer()
        {
            var PMReportFormServer = await _context.PMReportFormServer
                .Include(p => p.ReportForm)
                    .ThenInclude(r => r.ReportFormType)
                .Include(p => p.ReportForm)
                    .ThenInclude(r => r.SystemNameWarehouse)
                .Include(p => p.ReportForm)
                    .ThenInclude(r => r.StationNameWarehouse)
                .Include(p => p.PMReportFormType)
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Where(p => !p.IsDeleted)
                .Select(p => new PMReportFormServerDto
                {
                    ID = p.ID,
                    ReportFormID = p.ReportFormID,
                    PMReportFormTypeID = p.PMReportFormTypeID,
                    ProjectNo = p.ProjectNo,
                    Customer = p.Customer,
                    ReportTitle = p.ReportTitle,
                    AttendedBy = p.AttendedBy,
                    WitnessedBy = p.WitnessedBy,
                    StartDate = p.StartDate,
                    CompletionDate = p.CompletionDate,
                    Remarks = p.Remarks,
                    IsDeleted = p.IsDeleted,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    CreatedBy = p.CreatedBy,
                    UpdatedBy = p.UpdatedBy,
                    PMReportFormTypeName = p.PMReportFormType.Name,
                    CreatedByUserName = $"{p.CreatedByUser.FirstName} {p.CreatedByUser.LastName}",
                    UpdatedByUserName = p.UpdatedByUser != null ? $"{p.UpdatedByUser.FirstName} {p.UpdatedByUser.LastName}" : null,
                    JobNo = p.ReportForm.JobNo,
                    StationName = p.ReportForm.StationNameWarehouse.Name,
                    SystemDescription = p.ReportForm.SystemNameWarehouse.Name
                })
                .ToListAsync();

            return Ok(PMReportFormServer);
        }

        // GET: api/PMReportFormServer/5 - Enhanced with all related PM Server data
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPMReportFormServerWithDetails(Guid id)
        {
            var pmReportFormServer = await _context.PMReportFormServer
                .Include(p => p.ReportForm)
                    .ThenInclude(r => r.ReportFormType)
                .Include(p => p.ReportForm)
                    .ThenInclude(r => r.SystemNameWarehouse)
                .Include(p => p.ReportForm)
                    .ThenInclude(r => r.StationNameWarehouse)
                .Include(p => p.PMReportFormType)
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Where(p => p.ID == id && !p.IsDeleted)
                .FirstOrDefaultAsync();

            if (pmReportFormServer == null)
            {
                return NotFound();
            }

            // Get all related PM Server data
            var pmServerHealths = await _context.PMServerHealths
                .Where(h => h.PMReportFormServerID == id && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    Remarks = h.Remarks,
                    CreatedDate = h.CreatedDate,
                    UpdatedDate = h.UpdatedDate,
                    CreatedBy = h.CreatedBy,
                    UpdatedBy = h.UpdatedBy,
                    CreatedByUserName = h.CreatedByUser != null ? $"{h.CreatedByUser.FirstName} {h.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            var pmServerHardDriveHealths = await _context.PMServerHardDriveHealths
                .Where(h => h.PMReportFormServerID == id && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    Remarks = h.Remarks,
                    CreatedDate = h.CreatedDate,
                    UpdatedDate = h.UpdatedDate,
                    CreatedBy = h.CreatedBy,
                    UpdatedBy = h.UpdatedBy,
                    CreatedByUserName = h.CreatedByUser != null ? $"{h.CreatedByUser.FirstName} {h.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            var pmServerDiskUsageHealths = await _context.PMServerDiskUsageHealths
                .Where(h => h.PMReportFormServerID == id && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    Remarks = h.Remarks,
                    CreatedDate = h.CreatedDate,
                    UpdatedDate = h.UpdatedDate,
                    CreatedBy = h.CreatedBy,
                    UpdatedBy = h.UpdatedBy,
                    CreatedByUserName = h.CreatedByUser != null ? $"{h.CreatedByUser.FirstName} {h.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            var pmServerCPUAndMemoryUsages = await _context.PMServerCPUAndMemoryUsages
                .Where(h => h.PMReportFormServerID == id && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    Remarks = h.Remarks,
                    CreatedDate = h.CreatedDate,
                    UpdatedDate = h.UpdatedDate,
                    CreatedBy = h.CreatedBy,
                    UpdatedBy = h.UpdatedBy,
                    CreatedByUserName = h.CreatedByUser != null ? $"{h.CreatedByUser.FirstName} {h.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            var pmServerNetworkHealths = await _context.PMServerNetworkHealths
                .Where(h => h.PMReportFormServerID == id && !h.IsDeleted)
                .Include(h => h.NetworkStatus)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    NetworkStatusID = h.NetworkStatusID,
                    NetworkStatusName = h.NetworkStatus.Name,
                    Remarks = h.Remarks,
                    CreatedDate = h.CreatedDate,
                    UpdatedDate = h.UpdatedDate,
                    CreatedBy = h.CreatedBy,
                    UpdatedBy = h.UpdatedBy,
                    CreatedByUserName = h.CreatedByUser != null ? $"{h.CreatedByUser.FirstName} {h.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            var pmServerWillowlynxProcessStatuses = await _context.PMServerWillowlynxProcessStatuses
                .Where(h => h.PMReportFormServerID == id && !h.IsDeleted)
                .Include(h => h.WillowlynxProcessStatus)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    WillowlynxProcessStatusID = h.WillowlynxProcessStatusID,
                    WillowlynxProcessStatusName = h.WillowlynxProcessStatus.Name,
                    Remarks = h.Remarks,
                    CreatedDate = h.CreatedDate,
                    UpdatedDate = h.UpdatedDate,
                    CreatedBy = h.CreatedBy,
                    UpdatedBy = h.UpdatedBy,
                    CreatedByUserName = h.CreatedByUser != null ? $"{h.CreatedByUser.FirstName} {h.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            var pmServerWillowlynxNetworkStatuses = await _context.PMServerWillowlynxNetworkStatuses
                .Where(h => h.PMReportFormServerID == id && !h.IsDeleted)
                .Include(h => h.WillowlynxNetworkStatus)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    WillowlynxNetworkStatusID = h.WillowlynxNetworkStatusID,
                    WillowlynxNetworkStatusName = h.WillowlynxNetworkStatus.Name,
                    Remarks = h.Remarks,
                    CreatedDate = h.CreatedDate,
                    UpdatedDate = h.UpdatedDate,
                    CreatedBy = h.CreatedBy,
                    UpdatedBy = h.UpdatedBy,
                    CreatedByUserName = h.CreatedByUser != null ? $"{h.CreatedByUser.FirstName} {h.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            var pmServerWillowlynxRTUStatuses = await _context.PMServerWillowlynxRTUStatuses
                .Where(h => h.PMReportFormServerID == id && !h.IsDeleted)
                .Include(h => h.WillowlynxRTUStatus)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    WillowlynxRTUStatusID = h.WillowlynxRTUStatusID,
                    WillowlynxRTUStatusName = h.WillowlynxRTUStatus.Name,
                    Remarks = h.Remarks,
                    CreatedDate = h.CreatedDate,
                    UpdatedDate = h.UpdatedDate,
                    CreatedBy = h.CreatedBy,
                    UpdatedBy = h.UpdatedBy,
                    CreatedByUserName = h.CreatedByUser != null ? $"{h.CreatedByUser.FirstName} {h.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            var pmServerWillowlynxHistoricalTrends = await _context.PMServerWillowlynxHistoricalTrends
                .Where(h => h.PMReportFormServerID == id && !h.IsDeleted)
                .Include(h => h.WillowlynxHistoricalTrendStatus)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    WillowlynxHistoricalTrendStatusID = h.WillowlynxHistoricalTrendStatusID,
                    WillowlynxHistoricalTrendStatusName = h.WillowlynxHistoricalTrendStatus.Name,
                    Remarks = h.Remarks,
                    CreatedDate = h.CreatedDate,
                    UpdatedDate = h.UpdatedDate,
                    CreatedBy = h.CreatedBy,
                    UpdatedBy = h.UpdatedBy,
                    CreatedByUserName = h.CreatedByUser != null ? $"{h.CreatedByUser.FirstName} {h.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            var pmServerWillowlynxHistoricalReports = await _context.PMServerWillowlynxHistoricalReports
                .Where(h => h.PMReportFormServerID == id && !h.IsDeleted)
                .Include(h => h.WillowlynxHistoricalReportStatus)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    WillowlynxHistoricalReportStatusID = h.WillowlynxHistoricalReportStatusID,
                    WillowlynxHistoricalReportStatusName = h.WillowlynxHistoricalReportStatus.Name,
                    Remarks = h.Remarks,
                    CreatedDate = h.CreatedDate,
                    UpdatedDate = h.UpdatedDate,
                    CreatedBy = h.CreatedBy,
                    UpdatedBy = h.UpdatedBy,
                    CreatedByUserName = h.CreatedByUser != null ? $"{h.CreatedByUser.FirstName} {h.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            var pmServerWillowlynxCCTVCameras = await _context.PMServerWillowlynxCCTVCameras
                .Where(h => h.PMReportFormServerID == id && !h.IsDeleted)
                .Include(h => h.WillowlynxCCTVCameraStatus)
                .Include(h => h.YesNoStatus)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    WillowlynxCCTVCameraStatusID = h.WillowlynxCCTVCameraStatusID,
                    WillowlynxCCTVCameraStatusName = h.WillowlynxCCTVCameraStatus.Name,
                    YesNoStatusID = h.YesNoStatusID,
                    YesNoStatusName = h.YesNoStatus.Name,
                    Remarks = h.Remarks,
                    CreatedDate = h.CreatedDate,
                    UpdatedDate = h.UpdatedDate,
                    CreatedBy = h.CreatedBy,
                    UpdatedBy = h.UpdatedBy,
                    CreatedByUserName = h.CreatedByUser != null ? $"{h.CreatedByUser.FirstName} {h.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            var pmServerMonthlyDatabaseCreations = await _context.PMServerMonthlyDatabaseCreations
                .Where(h => h.PMReportFormServerID == id && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    Remarks = h.Remarks,
                    CreatedDate = h.CreatedDate,
                    UpdatedDate = h.UpdatedDate,
                    CreatedBy = h.CreatedBy,
                    UpdatedBy = h.UpdatedBy,
                    CreatedByUserName = h.CreatedByUser != null ? $"{h.CreatedByUser.FirstName} {h.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            var pmServerMonthlyDatabaseBackups = await _context.PMServerMonthlyDatabaseBackups
                .Where(h => h.PMReportFormServerID == id && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    Remarks = h.Remarks,
                    CreatedDate = h.CreatedDate,
                    UpdatedDate = h.UpdatedDate,
                    CreatedBy = h.CreatedBy,
                    UpdatedBy = h.UpdatedBy,
                    CreatedByUserName = h.CreatedByUser != null ? $"{h.CreatedByUser.FirstName} {h.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            var pmServerTimeSyncs = await _context.PMServerTimeSyncs
                .Where(h => h.PMReportFormServerID == id && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    Remarks = h.Remarks,
                    CreatedDate = h.CreatedDate,
                    UpdatedDate = h.UpdatedDate,
                    CreatedBy = h.CreatedBy,
                    UpdatedBy = h.UpdatedBy,
                    CreatedByUserName = h.CreatedByUser != null ? $"{h.CreatedByUser.FirstName} {h.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            var pmServerHotFixes = await _context.PMServerHotFixes
                .Where(h => h.PMReportFormServerID == id && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    Remarks = h.Remarks,
                    CreatedDate = h.CreatedDate,
                    UpdatedDate = h.UpdatedDate,
                    CreatedBy = h.CreatedBy,
                    UpdatedBy = h.UpdatedBy,
                    CreatedByUserName = h.CreatedByUser != null ? $"{h.CreatedByUser.FirstName} {h.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            var pmServerFailOvers = await _context.PMServerFailOvers
                .Where(h => h.PMReportFormServerID == id && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    Remarks = h.Remarks,
                    CreatedDate = h.CreatedDate,
                    UpdatedDate = h.UpdatedDate,
                    CreatedBy = h.CreatedBy,
                    UpdatedBy = h.UpdatedBy,
                    CreatedByUserName = h.CreatedByUser != null ? $"{h.CreatedByUser.FirstName} {h.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            var pmServerASAFirewalls = await _context.PMServerASAFirewalls
                .Where(h => h.PMReportFormServerID == id && !h.IsDeleted)
                .Include(h => h.ASAFirewallStatus)
                .Include(h => h.ResultStatus)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    ASAFirewallStatusID = h.ASAFirewallStatusID,
                    ASAFirewallStatusName = h.ASAFirewallStatus.Name,
                    ResultStatusID = h.ResultStatusID,
                    ResultStatusName = h.ResultStatus.Name,
                    CreatedDate = h.CreatedDate,
                    UpdatedDate = h.UpdatedDate,
                    CreatedBy = h.CreatedBy,
                    UpdatedBy = h.UpdatedBy,
                    CreatedByUserName = h.CreatedByUser != null ? $"{h.CreatedByUser.FirstName} {h.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            var pmServerSoftwarePatchSummaries = await _context.PMServerSoftwarePatchSummaries
                .Where(h => h.PMReportFormServerID == id && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    SerialNo = h.SerialNo,
                    ServerName = h.ServerName,
                    PreviousPatch = h.PreviousPatch,
                    CurrentPatch = h.CurrentPatch,
                    Remarks = h.Remarks,
                    CreatedDate = h.CreatedDate,
                    UpdatedDate = h.UpdatedDate,
                    CreatedBy = h.CreatedBy,
                    UpdatedBy = h.UpdatedBy,
                    CreatedByUserName = h.CreatedByUser != null ? $"{h.CreatedByUser.FirstName} {h.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            var result = new
            {
                // Main PM Report Form Server data
                PMReportFormServer = new PMReportFormServerDto
                {
                    ID = pmReportFormServer.ID,
                    ReportFormID = pmReportFormServer.ReportFormID,
                    PMReportFormTypeID = pmReportFormServer.PMReportFormTypeID,
                    ProjectNo = pmReportFormServer.ProjectNo,
                    Customer = pmReportFormServer.Customer,
                    ReportTitle = pmReportFormServer.ReportTitle,
                    AttendedBy = pmReportFormServer.AttendedBy,
                    WitnessedBy = pmReportFormServer.WitnessedBy,
                    StartDate = pmReportFormServer.StartDate,
                    CompletionDate = pmReportFormServer.CompletionDate,
                    Remarks = pmReportFormServer.Remarks,
                    IsDeleted = pmReportFormServer.IsDeleted,
                    CreatedDate = pmReportFormServer.CreatedDate,
                    UpdatedDate = pmReportFormServer.UpdatedDate,
                    CreatedBy = pmReportFormServer.CreatedBy,
                    UpdatedBy = pmReportFormServer.UpdatedBy,
                    PMReportFormTypeName = pmReportFormServer.PMReportFormType.Name,
                    CreatedByUserName = $"{pmReportFormServer.CreatedByUser.FirstName} {pmReportFormServer.CreatedByUser.LastName}",
                    UpdatedByUserName = pmReportFormServer.UpdatedByUser != null ? $"{pmReportFormServer.UpdatedByUser.FirstName} {pmReportFormServer.UpdatedByUser.LastName}" : null,
                    JobNo = pmReportFormServer.ReportForm.JobNo,
                    StationName = pmReportFormServer.ReportForm.StationNameWarehouse.Name,
                    SystemDescription = pmReportFormServer.ReportForm.SystemNameWarehouse.Name
                },

                // All related PM Server data arrays
                PMServerHealths = pmServerHealths,
                PMServerHardDriveHealths = pmServerHardDriveHealths,
                PMServerDiskUsageHealths = pmServerDiskUsageHealths,
                PMServerCPUAndMemoryUsages = pmServerCPUAndMemoryUsages,
                PMServerNetworkHealths = pmServerNetworkHealths,
                PMServerWillowlynxProcessStatuses = pmServerWillowlynxProcessStatuses,
                PMServerWillowlynxNetworkStatuses = pmServerWillowlynxNetworkStatuses,
                PMServerWillowlynxRTUStatuses = pmServerWillowlynxRTUStatuses,
                PMServerWillowlynxHistoricalTrends = pmServerWillowlynxHistoricalTrends,
                PMServerWillowlynxHistoricalReports = pmServerWillowlynxHistoricalReports,
                PMServerWillowlynxCCTVCameras = pmServerWillowlynxCCTVCameras,
                PMServerMonthlyDatabaseCreations = pmServerMonthlyDatabaseCreations,
                PMServerMonthlyDatabaseBackups = pmServerMonthlyDatabaseBackups,
                PMServerTimeSyncs = pmServerTimeSyncs,
                PMServerHotFixes = pmServerHotFixes,
                PMServerFailOvers = pmServerFailOvers,
                PMServerASAFirewalls = pmServerASAFirewalls,
                PMServerSoftwarePatchSummaries = pmServerSoftwarePatchSummaries
            };

            return Ok(result);
        }

        // POST: api/PMReportFormServer
        [HttpPost]
        public async Task<ActionResult<PMReportFormServerDto>> PostPMReportFormServer(CreatePMReportFormServerDto createDto)
        {
            // Validate that ReportForm exists
            var reportFormExists = await _context.ReportForms.AnyAsync(r => r.ID == createDto.ReportFormID && !r.IsDeleted);
            if (!reportFormExists)
            {
                return BadRequest("Invalid ReportFormID");
            }

            // Validate that PMReportFormType exists
            var pmReportFormTypeExists = await _context.PMReportFormTypes.AnyAsync(t => t.ID == createDto.PMReportFormTypeID && !t.IsDeleted);
            if (!pmReportFormTypeExists)
            {
                return BadRequest("Invalid PMReportFormTypeID");
            }

            // Validate that CreatedBy user exists
            var userExists = await _context.Users.AnyAsync(u => u.ID == createDto.CreatedBy && !u.IsDeleted);
            if (!userExists)
            {
                return BadRequest("Invalid CreatedBy user");
            }

            var pmReportFormServer = new PMReportFormServer
            {
                ID = Guid.NewGuid(),
                ReportFormID = createDto.ReportFormID,
                PMReportFormTypeID = createDto.PMReportFormTypeID,
                ProjectNo = createDto.ProjectNo,
                Customer = createDto.Customer,
                ReportTitle = createDto.ReportTitle,
                AttendedBy = createDto.AttendedBy,
                WitnessedBy = createDto.WitnessedBy,
                StartDate = createDto.StartDate,
                CompletionDate = createDto.CompletionDate,
                Remarks = createDto.Remarks,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = createDto.CreatedBy
            };

            _context.PMReportFormServer.Add(pmReportFormServer);
            await _context.SaveChangesAsync();

            var responseDto = new PMReportFormServerDto
            {
                ID = pmReportFormServer.ID,
                ReportFormID = pmReportFormServer.ReportFormID,
                PMReportFormTypeID = pmReportFormServer.PMReportFormTypeID,
                ProjectNo = pmReportFormServer.ProjectNo,
                Customer = pmReportFormServer.Customer,
                ReportTitle = pmReportFormServer.ReportTitle,
                AttendedBy = pmReportFormServer.AttendedBy,
                WitnessedBy = pmReportFormServer.WitnessedBy,
                StartDate = pmReportFormServer.StartDate,
                CompletionDate = pmReportFormServer.CompletionDate,
                Remarks = pmReportFormServer.Remarks,
                IsDeleted = pmReportFormServer.IsDeleted,
                CreatedDate = pmReportFormServer.CreatedDate,
                UpdatedDate = pmReportFormServer.UpdatedDate,
                CreatedBy = pmReportFormServer.CreatedBy,
                UpdatedBy = pmReportFormServer.UpdatedBy
            };

            return CreatedAtAction(nameof(GetPMReportFormServer), new { id = pmReportFormServer.ID }, responseDto);
        }

        // PUT: api/PMReportFormServer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPMReportFormServer(Guid id, UpdatePMReportFormServerDto updateDto)
        {
            var pmReportFormServer = await _context.PMReportFormServer.FindAsync(id);
            if (pmReportFormServer == null || pmReportFormServer.IsDeleted)
            {
                return NotFound();
            }

            // Validate that PMReportFormType exists
            var pmReportFormTypeExists = await _context.PMReportFormTypes.AnyAsync(t => t.ID == updateDto.PMReportFormTypeID && !t.IsDeleted);
            if (!pmReportFormTypeExists)
            {
                return BadRequest("Invalid PMReportFormTypeID");
            }

            // Validate that UpdatedBy user exists if provided
            if (updateDto.UpdatedBy.HasValue)
            {
                var userExists = await _context.Users.AnyAsync(u => u.ID == updateDto.UpdatedBy.Value && !u.IsDeleted);
                if (!userExists)
                {
                    return BadRequest("Invalid UpdatedBy user");
                }
            }

            pmReportFormServer.PMReportFormTypeID = updateDto.PMReportFormTypeID;
            pmReportFormServer.ProjectNo = updateDto.ProjectNo;
            pmReportFormServer.Customer = updateDto.Customer;
            pmReportFormServer.ReportTitle = updateDto.ReportTitle;
            pmReportFormServer.AttendedBy = updateDto.AttendedBy;
            pmReportFormServer.WitnessedBy = updateDto.WitnessedBy;
            pmReportFormServer.StartDate = updateDto.StartDate;
            pmReportFormServer.CompletionDate = updateDto.CompletionDate;
            pmReportFormServer.Remarks = updateDto.Remarks;
            pmReportFormServer.UpdatedDate = DateTime.UtcNow;
            pmReportFormServer.UpdatedBy = updateDto.UpdatedBy;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PMReportFormServerExists(id))
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

        // DELETE: api/PMReportFormServer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePMReportFormServer(Guid id)
        {
            var pmReportFormServer = await _context.PMReportFormServer.FindAsync(id);
            if (pmReportFormServer == null || pmReportFormServer.IsDeleted)
            {
                return NotFound();
            }

            // Soft delete
            pmReportFormServer.IsDeleted = true;
            pmReportFormServer.UpdatedDate = DateTime.UtcNow;

            // Get current user ID from claims if available
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (Guid.TryParse(currentUserId, out Guid userId))
            {
                pmReportFormServer.UpdatedBy = userId;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PMReportFormServerExists(Guid id)
        {
            return _context.PMReportFormServer.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}