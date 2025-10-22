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
            // First, find the ReportForm by the provided ID
            var reportForm = await _context.ReportForms
                .Include(r => r.ReportFormType)
                .Include(r => r.SystemNameWarehouse)
                .Include(r => r.StationNameWarehouse)
                .Where(r => r.ID == id && !r.IsDeleted)
                .FirstOrDefaultAsync();

            if (reportForm == null)
            {
                return NotFound("ReportForm not found");
            }

            // Then find the PMReportFormServer using the ReportForm ID
            var pmReportFormServer = await _context.PMReportFormServer
                .Include(p => p.PMReportFormType)
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Where(p => p.ReportFormID == reportForm.ID && !p.IsDeleted)
                .FirstOrDefaultAsync();

            if (pmReportFormServer == null)
            {
                return NotFound("PMReportFormServer not found for this ReportForm");
            }

            // Get all related PM Server data using the PMReportFormServer ID
            var pmServerHealths = await _context.PMServerHealths
                .Where(h => h.PMReportFormServerID == pmReportFormServer.ID && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Include(h => h.PMServerHealthDetails)
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
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null,
                    Details = h.PMServerHealthDetails.Where(d => !d.IsDeleted).Select(d => new
                    {
                        ID = d.ID,
                        PMServerHealthID = d.PMServerHealthID,
                        ResultStatusID = d.ResultStatusID, // Using correct ResultStatusID property
                        ResultStatusName = d.ResultStatus.Name, // Getting display name from navigation property
                        ServerName = d.ServerName,
                        Remarks = d.Remarks,
                        CreatedDate = d.CreatedDate,
                        UpdatedDate = d.UpdatedDate,
                        CreatedBy = d.CreatedBy,
                        UpdatedBy = d.UpdatedBy
                    }).ToList()
                })
                .ToListAsync();

            var pmServerHardDriveHealths = await _context.PMServerHardDriveHealths
                .Where(h => h.PMReportFormServerID == pmReportFormServer.ID && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Include(h => h.PMServerHardDriveHealthDetails)
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
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null,
                    Details = h.PMServerHardDriveHealthDetails.Where(d => !d.IsDeleted).Select(d => new
                    {
                        ID = d.ID,
                        PMServerHardDriveHealthID = d.PMServerHardDriveHealthID,
                        ServerName = d.ServerName,
                        HardDriveName = d.ServerName, // Using ServerName as HardDriveName since HardDriveName doesn't exist in model
                        ResultStatusID = d.ResultStatusID, // Using correct ResultStatusID property
                        ResultStatusName = d.ResultStatus.Name, // Getting display name from navigation property
                        Remarks = d.Remarks,
                        CreatedDate = d.CreatedDate,
                        UpdatedDate = d.UpdatedDate,
                        CreatedBy = d.CreatedBy,
                        UpdatedBy = d.UpdatedBy
                    }).ToList()
                })
                .ToListAsync();

            var pmServerDiskUsageHealths = await _context.PMServerDiskUsageHealths
                .Where(h => h.PMReportFormServerID == pmReportFormServer.ID && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Include(h => h.PMServerDiskUsageHealthDetails)
                    .ThenInclude(d => d.ServerDiskStatus)
                .Include(h => h.PMServerDiskUsageHealthDetails)
                    .ThenInclude(d => d.ResultStatus)
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
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null,
                    Details = h.PMServerDiskUsageHealthDetails.Where(d => !d.IsDeleted).Select(d => new
                    {
                        ID = d.ID,
                        PMServerDiskUsageHealthID = d.PMServerDiskUsageHealthID,
                        ServerName = d.ServerName,
                        DiskName = d.DiskName,
                        Capacity = d.Capacity,
                        FreeSpace = d.FreeSpace,
                        Usage = d.Usage,
                        ServerDiskStatusID = d.ServerDiskStatusID,
                        ServerDiskStatusName = d.ServerDiskStatus.Name, // Getting display name from navigation property
                        ResultStatusID = d.ResultStatusID,
                        ResultStatusName = d.ResultStatus.Name, // Getting display name from navigation property
                        Remarks = d.Remarks,
                        CreatedDate = d.CreatedDate,
                        UpdatedDate = d.UpdatedDate,
                        CreatedBy = d.CreatedBy,
                        UpdatedBy = d.UpdatedBy
                    }).ToList()
                })
                .ToListAsync();

            var pmServerCPUAndMemoryUsages = await _context.PMServerCPUAndMemoryUsages
                .Where(h => h.PMReportFormServerID == pmReportFormServer.ID && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Include(h => h.PMServerCPUUsageDetails)
                .Include(h => h.PMServerMemoryUsageDetails)
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
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null,
                    CPUUsageDetails = h.PMServerCPUUsageDetails.Where(d => !d.IsDeleted).Select(d => new
                    {
                        ID = d.ID,
                        PMServerCPUAndMemoryUsageID = d.PMServerCPUAndMemoryUsageID,
                        SerialNo = d.SerialNo,
                        ResultStatusID = d.ResultStatusID, 
                        ResultStatusName = d.ResultStatus.Name, 
                        ServerName = d.ServerName,
                        CPUUsagePercentage = d.CPUUsage,
                        Remarks = d.Remarks,
                        CreatedDate = d.CreatedDate,
                        UpdatedDate = d.UpdatedDate,
                        CreatedBy = d.CreatedBy,
                        UpdatedBy = d.UpdatedBy
                    }).ToList(),
                    MemoryUsageDetails = h.PMServerMemoryUsageDetails.Where(d => !d.IsDeleted).Select(d => new
                    {
                        ID = d.ID,
                        PMServerCPUAndMemoryUsageID = d.PMServerCPUAndMemoryUsageID,
                        SerialNo = d.SerialNo,
                        ResultStatusID = d.ResultStatusID, 
                        ResultStatusName = d.ResultStatus.Name, 
                        ServerName = d.ServerName,
                        MemorySize = d.MemorySize,
                        MemoryUsagePercentage = d.MemoryInUse,
                        Remarks = d.Remarks,
                        CreatedDate = d.CreatedDate,
                        UpdatedDate = d.UpdatedDate,
                        CreatedBy = d.CreatedBy,
                        UpdatedBy = d.UpdatedBy
                    }).ToList()
                })
                .ToListAsync();

            var pmServerNetworkHealths = await _context.PMServerNetworkHealths
                .Where(h => h.PMReportFormServerID == pmReportFormServer.ID && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    YesNoStatusID = h.YesNoStatusID,
                    YesNoStatusName = h.YesNoStatus.Name,
                    DateChecked = h.DateChecked,
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
                .Where(h => h.PMReportFormServerID == pmReportFormServer.ID && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
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

            var pmServerWillowlynxNetworkStatuses = await _context.PMServerWillowlynxNetworkStatuses
                .Where(h => h.PMReportFormServerID == pmReportFormServer.ID && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
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

            var pmServerWillowlynxRTUStatuses = await _context.PMServerWillowlynxRTUStatuses
                .Where(h => h.PMReportFormServerID == pmReportFormServer.ID && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
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

            var pmServerWillowlynxHistoricalTrends = await _context.PMServerWillowlynxHistoricalTrends
                .Where(h => h.PMReportFormServerID == pmReportFormServer.ID && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
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

            var pmServerWillowlynxHistoricalReports = await _context.PMServerWillowlynxHistoricalReports
                .Where(h => h.PMReportFormServerID == pmReportFormServer.ID && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
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

            var pmServerWillowlynxCCTVCameras = await _context.PMServerWillowlynxCCTVCameras
                .Where(h => h.PMReportFormServerID == pmReportFormServer.ID && !h.IsDeleted)
                .Include(h => h.YesNoStatus)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
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
                .Where(h => h.PMReportFormServerID == pmReportFormServer.ID && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Include(h => h.PMServerMonthlyDatabaseCreationDetails)
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
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null,
                    Details = h.PMServerMonthlyDatabaseCreationDetails.Where(d => !d.IsDeleted).Select(d => new
                    {
                        ID = d.ID,
                        PMServerMonthlyDatabaseCreationID = d.PMServerMonthlyDatabaseCreationID,
                        YesNoStatusID = d.YesNoStatusID,
                        YesNoStatusName = d.YesNoStatus.Name,
                        ServerName = d.ServerName,
                        DatabaseName = d.ServerName, // Using ServerName as DatabaseName since DatabaseName doesn't exist in model
                        Remarks = d.Remarks,
                        CreatedDate = d.CreatedDate,
                        UpdatedDate = d.UpdatedDate,
                        CreatedBy = d.CreatedBy,
                        UpdatedBy = d.UpdatedBy
                    }).ToList()
                })
                .ToListAsync();

            var pmServerDatabaseBackups = await _context.PMServerDatabaseBackups
                .Where(h => h.PMReportFormServerID == pmReportFormServer.ID && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Include(h => h.PMServerMSSQLDatabaseBackupDetails)
                .Include(h => h.PMServerSCADADataBackupDetails)
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
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null,
                    MSSQLDatabaseBackupDetails = h.PMServerMSSQLDatabaseBackupDetails.Where(d => !d.IsDeleted).Select(d => new
                    {
                        ID = d.ID,
                        PMServerMonthlyDatabaseBackupID = d.PMServerDatabaseBackupID, // Using PMServerDatabaseBackupID since PMServerMonthlyDatabaseBackupID doesn't exist
                        YesNoStatusID = d.YesNoStatusID,
                        YesNoStatusName = d.YesNoStatus.Name,
                        ServerName = d.ServerName,
                        DatabaseName = d.ServerName, // Using ServerName as DatabaseName since DatabaseName doesn't exist in model
                        Remarks = d.Remarks,
                        CreatedDate = d.CreatedDate,
                        UpdatedDate = d.UpdatedDate,
                        CreatedBy = d.CreatedBy,
                        UpdatedBy = d.UpdatedBy
                    }).ToList(),
                    SCADADataBackupDetails = h.PMServerSCADADataBackupDetails.Where(d => !d.IsDeleted).Select(d => new
                    {
                        ID = d.ID,
                        PMServerMonthlyDatabaseBackupID = d.PMServerDatabaseBackupID, // Using PMServerDatabaseBackupID since PMServerMonthlyDatabaseBackupID doesn't exist
                        YesNoStatusID = d.YesNoStatusID,
                        YesNoStatusName = d.YesNoStatus.Name,
                        ServerName = d.ServerName,
                        SCADADataName = d.ServerName, // Using ServerName as SCADADataName since SCADADataName doesn't exist in model
                        Remarks = d.Remarks,
                        CreatedDate = d.CreatedDate,
                        UpdatedDate = d.UpdatedDate,
                        CreatedBy = d.CreatedBy,
                        UpdatedBy = d.UpdatedBy
                    }).ToList()
                })
                .ToListAsync();

            var pmServerTimeSyncs = await _context.PMServerTimeSyncs
                .Where(h => h.PMReportFormServerID == pmReportFormServer.ID && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Include(h => h.PMServerTimeSyncDetails)
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
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null,
                    Details = h.PMServerTimeSyncDetails.Where(d => !d.IsDeleted).Select(d => new
                    {
                        ID = d.ID,
                        PMServerTimeSyncID = d.PMServerTimeSyncID,
                        ResultStatusID = d.ResultStatusID,
                        ResultStatusName = d.ResultStatus.Name,
                        ServerName = d.ServerName,
                        TimeSyncSource = d.ServerName, // Using ServerName as TimeSyncSource since TimeSyncSource doesn't exist in model
                        Remarks = d.Remarks,
                        CreatedDate = d.CreatedDate,
                        UpdatedDate = d.UpdatedDate,
                        CreatedBy = d.CreatedBy,
                        UpdatedBy = d.UpdatedBy
                    }).ToList()
                })
                .ToListAsync();

            var pmServerHotFixes = await _context.PMServerHotFixes
                .Where(h => h.PMReportFormServerID == pmReportFormServer.ID && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Include(h => h.PMServerHotFixesDetails)
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
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null,
                    Details = h.PMServerHotFixesDetails.Where(d => !d.IsDeleted).Select(d => new
                    {
                        ID = d.ID,
                        PMServerHotFixesID = d.PMServerHotFixesID,
                        ResultStatusID = d.ResultStatusID,
                        ResultStatusName = d.ResultStatus.Name,
                        ServerName = d.ServerName,
                        HotFixName = d.LatestHotFixsApplied, // Using LatestHotFixsApplied since HotFixName doesn't exist in model
                        Remarks = d.Remarks,
                        CreatedDate = d.CreatedDate,
                        UpdatedDate = d.UpdatedDate,
                        CreatedBy = d.CreatedBy,
                        UpdatedBy = d.UpdatedBy
                    }).ToList()
                })
                .ToListAsync();

            var pmServerFailOvers = await _context.PMServerFailOvers
                .Where(h => h.PMReportFormServerID == pmReportFormServer.ID && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Include(h => h.PMServerFailOverDetails)
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
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null,
                    Details = h.PMServerFailOverDetails.Where(d => !d.IsDeleted).Select(d => new
                    {
                        ID = d.ID,
                        PMServerFailOverID = d.PMServerFailOverID,
                        YesNoStatusID = d.YesNoStatusID,
                        YesNoStatusName = d.YesNoStatus.Name,
                        FromServer = d.FromServer,
                        ToServer = d.ToServer,
                        Remarks = d.Remarks,
                        CreatedDate = d.CreatedDate,
                        UpdatedDate = d.UpdatedDate,
                        CreatedBy = d.CreatedBy,
                        UpdatedBy = d.UpdatedBy
                    }).ToList()
                })
                .ToListAsync();

            var pmServerASAFirewalls = await _context.PMServerASAFirewalls
                .Where(h => h.PMReportFormServerID == pmReportFormServer.ID && !h.IsDeleted)
                .Include(h => h.ASAFirewallStatus)
                .Include(h => h.ResultStatus)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Select(h => new
                {
                    ID = h.ID,
                    PMReportFormServerID = h.PMReportFormServerID,
                    SerialNumber = h.SerialNumber,
                    CommandInput = h.CommandInput,
                    ASAFirewallStatusID = h.ASAFirewallStatusID,
                    ASAFirewallStatusName = h.ASAFirewallStatus.Name,
                    ResultStatusID = h.ResultStatusID,
                    ResultStatusName = h.ResultStatus.Name,
                    Remarks = h.Remarks,
                    CreatedDate = h.CreatedDate,
                    UpdatedDate = h.UpdatedDate,
                    CreatedBy = h.CreatedBy,
                    UpdatedBy = h.UpdatedBy,
                    CreatedByUserName = h.CreatedByUser != null ? $"{h.CreatedByUser.FirstName} {h.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            var pmServerSoftwarePatchSummaries = await _context.PMServerSoftwarePatchSummaries
                .Where(h => h.PMReportFormServerID == pmReportFormServer.ID && !h.IsDeleted)
                .Include(h => h.CreatedByUser)
                .Include(h => h.UpdatedByUser)
                .Include(h => h.PMServerSoftwarePatchDetails)
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
                    UpdatedByUserName = h.UpdatedByUser != null ? $"{h.UpdatedByUser.FirstName} {h.UpdatedByUser.LastName}" : null,
                    Details = h.PMServerSoftwarePatchDetails.Where(d => !d.IsDeleted).Select(d => new
                    {
                        ID = d.ID,
                        SerialNo = d.SerialNo,
                        ServerName = d.ServerName,
                        PreviousPatch = d.PreviousPatch,
                        CurrentPatch = d.CurrentPatch,
                        Remarks = d.Remarks,
                        CreatedDate = d.CreatedDate,
                        UpdatedDate = d.UpdatedDate,
                        CreatedBy = d.CreatedBy,
                        UpdatedBy = d.UpdatedBy
                    }).ToList()
                })
                .ToListAsync();

            var result = new
            {
                // ReportForm data
                ReportForm = new
                {
                    ID = reportForm.ID,
                    JobNo = reportForm.JobNo,
                    ReportFormTypeID = reportForm.ReportFormTypeID,
                    ReportFormTypeName = reportForm.ReportFormType?.Name,
                    SystemNameWarehouseID = reportForm.SystemNameWarehouseID,
                    SystemDescription = reportForm.SystemNameWarehouse?.Name,
                    StationNameWarehouseID = reportForm.StationNameWarehouseID,
                    StationName = reportForm.StationNameWarehouse?.Name,
                    CreatedDate = reportForm.CreatedDate,
                    UpdatedDate = reportForm.UpdatedDate,
                    CreatedBy = reportForm.CreatedBy,
                    UpdatedBy = reportForm.UpdatedBy
                },

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
                    PMReportFormTypeName = pmReportFormServer.PMReportFormType?.Name,
                    CreatedByUserName = pmReportFormServer.CreatedByUser != null ? $"{pmReportFormServer.CreatedByUser.FirstName} {pmReportFormServer.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = pmReportFormServer.UpdatedByUser != null ? $"{pmReportFormServer.UpdatedByUser.FirstName} {pmReportFormServer.UpdatedByUser.LastName}" : null,
                    JobNo = reportForm.JobNo,
                    StationName = reportForm.StationNameWarehouse?.Name,
                    SystemDescription = reportForm.SystemNameWarehouse?.Name,
                    
                    // Map server health data to DTO
                    ServerHealthData = pmServerHealths.FirstOrDefault() != null ? new PMServerHealthDataDto
                    {
                        Remarks = pmServerHealths.FirstOrDefault()?.Remarks,
                        Details = pmServerHealths.FirstOrDefault()?.Details?.Select(d => new PMServerHealthDetailDto
                        {
                            ServerName = d.ServerName,
                            ResultStatusID = d.ResultStatusID,
                            Remarks = d.Remarks
                        }).ToList()
                    } : null
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
                PMServerDatabaseBackups = pmServerDatabaseBackups,
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
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
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

                // Create PM Server component data
                await CreatePMServerComponentData(pmReportFormServer.ID, createDto);

                await transaction.CommitAsync();

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
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task CreatePMServerComponentData(Guid pmReportFormServerID, CreatePMReportFormServerDto createDto)
        {
            // Server Health Data
            if (createDto.ServerHealthData != null)
            {
                var serverHealth = new PMServerHealth
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    Remarks = createDto.ServerHealthData.Remarks,
                    IsDeleted = false,
                    CreatedBy = createDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow
                };
                _context.PMServerHealths.Add(serverHealth);

                if (createDto.ServerHealthData.Details != null)
                {
                    foreach (var detail in createDto.ServerHealthData.Details)
                    {
                        var healthDetail = new PMServerHealthDetails
                        {
                            ID = Guid.NewGuid(),
                            PMServerHealthID = serverHealth.ID,
                            ServerName = detail.ServerName,
                            ResultStatusID = detail.ResultStatusID,
                            Remarks = detail.Remarks ?? "",
                            IsDeleted = false,
                            CreatedBy = createDto.CreatedBy,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.PMServerHealthDetails.Add(healthDetail);
                    }
                }
            }

            // Disk Usage Data
            if (createDto.DiskUsageData != null)
            {
                var diskUsage = new PMServerDiskUsageHealth
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    Remarks = createDto.DiskUsageData.Remarks,
                    IsDeleted = false,
                    CreatedBy = createDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow
                };
                _context.PMServerDiskUsageHealths.Add(diskUsage);

                if (createDto.DiskUsageData.Details != null)
                {
                    foreach (var detail in createDto.DiskUsageData.Details)
                    {
                        var diskDetail = new PMServerDiskUsageHealthDetails
                        {
                            ID = Guid.NewGuid(),
                            PMServerDiskUsageHealthID = diskUsage.ID,
                            ServerName = detail.ServerName,
                            DiskName = detail.DiskName,
                            Capacity = detail.Capacity,
                            FreeSpace = detail.FreeSpace,
                            Usage = detail.Usage,
                            ServerDiskStatusID = detail.ServerDiskStatusID,
                            ResultStatusID = detail.ResultStatusID,
                            Remarks = detail.Remarks,
                            IsDeleted = false,
                            CreatedBy = createDto.CreatedBy,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.PMServerDiskUsageHealthDetails.Add(diskDetail);
                    }
                }
            }

            // CPU and Memory Data
            if (createDto.CpuAndRamUsageData != null)
            {
                var cpuMemory = new PMServerCPUAndMemoryUsage
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    Remarks = createDto.CpuAndRamUsageData.Remarks,
                    IsDeleted = false,
                    CreatedBy = createDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow
                };
                _context.PMServerCPUAndMemoryUsages.Add(cpuMemory);

                if (createDto.CpuAndRamUsageData.CPUUsageDetails != null)
                {
                    foreach (var detail in createDto.CpuAndRamUsageData.CPUUsageDetails)
                    {
                        var cpuDetail = new PMServerCPUUsageDetails
                        {
                            ID = Guid.NewGuid(),
                            PMServerCPUAndMemoryUsageID = cpuMemory.ID,
                            SerialNo = detail.SerialNo,
                            ServerName = detail.ServerName,
                            CPUUsage = detail.CPUUsage,
                            ResultStatusID = detail.ResultStatusID,
                            Remarks = detail.Remarks,
                            IsDeleted = false,
                            CreatedBy = createDto.CreatedBy,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.PMServerCPUUsageDetails.Add(cpuDetail);
                    }
                }

                if (createDto.CpuAndRamUsageData.MemoryUsageDetails != null)
                {
                    foreach (var detail in createDto.CpuAndRamUsageData.MemoryUsageDetails)
                    {
                        var memoryDetail = new PMServerMemoryUsageDetails
                        {
                            ID = Guid.NewGuid(),
                            PMServerCPUAndMemoryUsageID = cpuMemory.ID,
                            SerialNo = detail.SerialNo,
                            ServerName = detail.ServerName,
                            MemorySize = detail.MemorySize,
                            MemoryInUse = detail.MemoryInUse,
                            ResultStatusID = detail.ResultStatusID,
                            Remarks = detail.Remarks,
                            IsDeleted = false,
                            CreatedBy = createDto.CreatedBy,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.PMServerMemoryUsageDetails.Add(memoryDetail);
                    }
                }
            }

            // Network Health Data
            if (createDto.NetworkHealthData != null)
            {
                var networkHealth = new PMServerNetworkHealth
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    YesNoStatusID = createDto.NetworkHealthData.YesNoStatusID,
                    DateChecked = createDto.NetworkHealthData.DateChecked,
                    Remarks = createDto.NetworkHealthData.Remarks,
                    IsDeleted = false,
                    CreatedBy = createDto.CreatedBy,
                    UpdatedBy = createDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };
                _context.PMServerNetworkHealths.Add(networkHealth);
            }

            // Willowlynx Process Status Data
            if (createDto.WillowlynxProcessStatusData != null)
            {
                var processStatus = new PMServerWillowlynxProcessStatus
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    YesNoStatusID = createDto.WillowlynxProcessStatusData.YesNoStatusID,
                    Remarks = createDto.WillowlynxProcessStatusData.Remarks,
                    IsDeleted = false,
                    CreatedBy = createDto.CreatedBy,
                    UpdatedBy = createDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };
                _context.PMServerWillowlynxProcessStatuses.Add(processStatus);
            }

            // Willowlynx Network Status Data
            if (createDto.WillowlynxNetworkStatusData != null)
            {
                var networkStatus = new PMServerWillowlynxNetworkStatus
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    YesNoStatusID = createDto.WillowlynxNetworkStatusData.YesNoStatusID,
                    Remarks = createDto.WillowlynxNetworkStatusData.Remarks,
                    IsDeleted = false,
                    CreatedBy = createDto.CreatedBy,
                    UpdatedBy = createDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };
                _context.PMServerWillowlynxNetworkStatuses.Add(networkStatus);
            }

            // Willowlynx RTU Status Data
            if (createDto.WillowlynxRTUStatusData != null)
            {
                var rtuStatus = new PMServerWillowlynxRTUStatus
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    YesNoStatusID = createDto.WillowlynxRTUStatusData.YesNoStatusID,
                    Remarks = createDto.WillowlynxRTUStatusData.Remarks,
                    IsDeleted = false,
                    CreatedBy = createDto.CreatedBy,
                    UpdatedBy = createDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };
                _context.PMServerWillowlynxRTUStatuses.Add(rtuStatus);
            }

            // Willowlynx Historical Trend Data
            if (createDto.WillowlynxHistorialTrendData != null)
            {
                var historicalTrend = new PMServerWillowlynxHistoricalTrend
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    YesNoStatusID = createDto.WillowlynxHistorialTrendData.YesNoStatusID,
                    Remarks = createDto.WillowlynxHistorialTrendData.Remarks,
                    IsDeleted = false,
                    CreatedBy = createDto.CreatedBy,
                    UpdatedBy = createDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };
                _context.PMServerWillowlynxHistoricalTrends.Add(historicalTrend);
            }

            // Willowlynx Historical Report Data
            if (createDto.WillowlynxHistoricalReportData != null)
            {
                var historicalReport = new PMServerWillowlynxHistoricalReport
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    YesNoStatusID = createDto.WillowlynxHistoricalReportData.YesNoStatusID,
                    Remarks = createDto.WillowlynxHistoricalReportData.Remarks,
                    IsDeleted = false,
                    CreatedBy = createDto.CreatedBy,
                    UpdatedBy = createDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };
                _context.PMServerWillowlynxHistoricalReports.Add(historicalReport);
            }

            // Willowlynx CCTV Camera Data
            if (createDto.WillowlynxSumpPitCCTVCameraData != null)
            {
                var cctvCamera = new PMServerWillowlynxCCTVCamera
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    YesNoStatusID = createDto.WillowlynxSumpPitCCTVCameraData.YesNoStatusID,
                    Remarks = createDto.WillowlynxSumpPitCCTVCameraData.Remarks,
                    IsDeleted = false,
                    CreatedBy = createDto.CreatedBy,
                    UpdatedBy = createDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                };
                _context.PMServerWillowlynxCCTVCameras.Add(cctvCamera);
            }

            // Monthly Database Creation Data
            if (createDto.MonthlyDatabaseCreationData != null)
            {
                var dbCreation = new PMServerMonthlyDatabaseCreation
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    Remarks = createDto.MonthlyDatabaseCreationData.Remarks,
                    IsDeleted = false,
                    CreatedBy = createDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow
                };
                _context.PMServerMonthlyDatabaseCreations.Add(dbCreation);

                if (createDto.MonthlyDatabaseCreationData.Details != null)
                {
                    foreach (var detail in createDto.MonthlyDatabaseCreationData.Details)
                    {
                        var dbCreationDetail = new PMServerMonthlyDatabaseCreationDetails
                        {
                            ID = Guid.NewGuid(),
                            PMServerMonthlyDatabaseCreationID = dbCreation.ID,
                            SerialNo = detail.SerialNo,
                            ServerName = detail.ServerName,
                            YesNoStatusID = detail.YesNoStatusID,
                            Remarks = detail.Remarks,
                            IsDeleted = false,
                            CreatedBy = createDto.CreatedBy,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.PMServerMonthlyDatabaseCreationDetails.Add(dbCreationDetail);
                    }
                }
            }

            // Database Backup Data
            if (createDto.DatabaseBackupData != null)
            {
                var dbBackup = new PMServerDatabaseBackup
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    Remarks = createDto.DatabaseBackupData.Remarks,
                    LatestBackupFileName = createDto.DatabaseBackupData.LatestBackupFileName,
                    IsDeleted = false,
                    CreatedBy = createDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow
                };
                _context.PMServerDatabaseBackups.Add(dbBackup);

                // Handle MSSQL Database Backup Details
                if (createDto.DatabaseBackupData.MSSQLDetails != null)
                {
                    foreach (var detail in createDto.DatabaseBackupData.MSSQLDetails)
                    {
                        var mssqlBackupDetail = new PMServerMSSQLDatabaseBackupDetails
                        {
                            ID = Guid.NewGuid(),
                            PMServerDatabaseBackupID = dbBackup.ID,
                            SerialNo = detail.SerialNo,
                            ServerName = detail.ServerName,
                            YesNoStatusID = detail.YesNoStatusID,
                            Remarks = detail.Remarks,
                            IsDeleted = false,
                            CreatedBy = createDto.CreatedBy,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.PMServerMSSQLDatabaseBackupDetails.Add(mssqlBackupDetail);
                    }
                }

                // Handle SCADA Data Backup Details
                if (createDto.DatabaseBackupData.SCADADetails != null)
                {
                    foreach (var detail in createDto.DatabaseBackupData.SCADADetails)
                    {
                        var scadaBackupDetail = new PMServerSCADADataBackupDetails
                        {
                            ID = Guid.NewGuid(),
                            PMServerDatabaseBackupID = dbBackup.ID,
                            SerialNo = detail.SerialNo,
                            ServerName = detail.ServerName,
                            YesNoStatusID = detail.YesNoStatusID,
                            Remarks = detail.Remarks,
                            IsDeleted = false,
                            CreatedBy = createDto.CreatedBy,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.PMServerSCADADataBackupDetails.Add(scadaBackupDetail);
                    }
                }
            }

            // Time Sync Data
            if (createDto.TimeSyncData != null)
            {
                var timeSync = new PMServerTimeSync
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    Remarks = createDto.TimeSyncData.Remarks,
                    IsDeleted = false,
                    CreatedBy = createDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow
                };
                _context.PMServerTimeSyncs.Add(timeSync);

                if (createDto.TimeSyncData.Details != null)
                {
                    foreach (var detail in createDto.TimeSyncData.Details)
                    {
                        var timeSyncDetail = new PMServerTimeSyncDetails
                        {
                            ID = Guid.NewGuid(),
                            PMServerTimeSyncID = timeSync.ID,
                            SerialNo = detail.SerialNo,
                            ServerName = detail.ServerName,
                            ResultStatusID = detail.ResultStatusID,
                            Remarks = detail.Remarks,
                            IsDeleted = false,
                            CreatedBy = createDto.CreatedBy,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.PMServerTimeSyncDetails.Add(timeSyncDetail);
                    }
                }
            }

            // Hot Fixes Data
            if (createDto.HotFixesData != null)
            {
                var hotFixes = new PMServerHotFixes
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    Remarks = createDto.HotFixesData.Remarks,
                    IsDeleted = false,
                    CreatedBy = createDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow
                };
                _context.PMServerHotFixes.Add(hotFixes);

                if (createDto.HotFixesData.Details != null)
                {
                    foreach (var detail in createDto.HotFixesData.Details)
                    {
                        var hotFixDetail = new PMServerHotFixesDetails
                        {
                            ID = Guid.NewGuid(),
                            PMServerHotFixesID = hotFixes.ID,
                            SerialNo = detail.SerialNo,
                            ServerName = detail.ServerName,
                            LatestHotFixsApplied = detail.LatestHotFixsApplied,
                            ResultStatusID = detail.ResultStatusID,
                            Remarks = detail.Remarks,
                            IsDeleted = false,
                            CreatedBy = createDto.CreatedBy,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.PMServerHotFixesDetails.Add(hotFixDetail);
                    }
                }
            }

            // Auto Fail Over Data
            if (createDto.AutoFailOverData != null)
            {
                var failOver = new PMServerFailOver
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    Remarks = createDto.AutoFailOverData.Remarks,
                    IsDeleted = false,
                    CreatedBy = createDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow
                };
                _context.PMServerFailOvers.Add(failOver);

                if (createDto.AutoFailOverData.Details != null)
                {
                    foreach (var detail in createDto.AutoFailOverData.Details)
                    {
                        var failOverDetail = new PMServerFailOverDetails
                        {
                            ID = Guid.NewGuid(),
                            PMServerFailOverID = failOver.ID,
                            YesNoStatusID = detail.YesNoStatusID,
                            ToServer = detail.ToServer,
                            FromServer = detail.FromServer,
                            Remarks = detail.Remarks,
                            IsDeleted = false,
                            CreatedBy = createDto.CreatedBy,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.PMServerFailOverDetails.Add(failOverDetail);
                    }
                }
            }

            // ASA Firewall Data
            if (createDto.AsaFirewallData != null)
            {
                if (createDto.AsaFirewallData.Details != null)
                {
                    foreach (var detail in createDto.AsaFirewallData.Details)
                    {
                        var asaFirewall = new PMServerASAFirewall
                        {
                            ID = Guid.NewGuid(),
                            PMReportFormServerID = pmReportFormServerID,
                            SerialNumber = detail.SerialNumber,
                            CommandInput = detail.CommandInput,
                            ASAFirewallStatusID = detail.ASAFirewallStatusID,
                            ResultStatusID = detail.ResultStatusID,
                            Remarks = createDto.AsaFirewallData.Remarks,
                            IsDeleted = false,
                            CreatedBy = createDto.CreatedBy,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.PMServerASAFirewalls.Add(asaFirewall);
                    }
                }
            }

            // Software Patch Data
            if (createDto.SoftwarePatchData != null)
            {
                var softwarePatch = new PMServerSoftwarePatchSummary
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    Remarks = createDto.SoftwarePatchData.Remarks,
                    IsDeleted = false,
                    CreatedBy = createDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow
                };
                _context.PMServerSoftwarePatchSummaries.Add(softwarePatch);

                if (createDto.SoftwarePatchData.Details != null)
                {
                    foreach (var detail in createDto.SoftwarePatchData.Details)
                    {
                        var softwarePatchDetail = new PMServerSoftwarePatchDetails
                        {
                            ID = Guid.NewGuid(),
                            PMServerSoftwarePatchSummaryID = softwarePatch.ID,
                            SerialNo = detail.SerialNo,
                            ServerName = detail.ServerName,
                            PreviousPatch = detail.PreviousPatch,
                            CurrentPatch = detail.CurrentPatch,
                            Remarks = detail.Remarks,
                            IsDeleted = false,
                            CreatedBy = createDto.CreatedBy,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.PMServerSoftwarePatchDetails.Add(softwarePatchDetail);
                    }
                }
            }

            // Hard Drive Health Data
            if (createDto.HardDriveHealthData != null)
            {
                var hardDriveHealth = new PMServerHardDriveHealth
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    Remarks = createDto.HardDriveHealthData.Remarks,
                    IsDeleted = false,
                    CreatedBy = createDto.CreatedBy,
                    CreatedDate = DateTime.UtcNow
                };
                _context.PMServerHardDriveHealths.Add(hardDriveHealth);

                if (createDto.HardDriveHealthData.Details != null)
                {
                    foreach (var detail in createDto.HardDriveHealthData.Details)
                    {
                        var hardDriveDetail = new PMServerHardDriveHealthDetails
                        {
                            ID = Guid.NewGuid(),
                            PMServerHardDriveHealthID = hardDriveHealth.ID,
                            ServerName = detail.ServerName,
                            ResultStatusID = detail.ResultStatusID,
                            Remarks = detail.Remarks,
                            IsDeleted = false,
                            CreatedBy = createDto.CreatedBy,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.PMServerHardDriveHealthDetails.Add(hardDriveDetail);
                    }
                }
            }

            // Save all component data changes
            await _context.SaveChangesAsync();
        }

        // PUT: api/PMReportFormServer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPMReportFormServer(Guid id, UpdatePMReportFormServerDto updateDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
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

                // Update main PM Report Form Server
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

                // Update PM Server component data
                await UpdatePMServerComponentData(id, updateDto);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return NoContent();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task UpdatePMServerComponentData(Guid pmReportFormServerID, UpdatePMReportFormServerDto updateDto)
        {
            // Update Server Health Data
            if (updateDto.ServerHealthData != null)
            {
                // Remove existing data
                var existingServerHealth = await _context.PMServerHealths
                    .Include(h => h.PMServerHealthDetails)
                    .Where(h => h.PMReportFormServerID == pmReportFormServerID && !h.IsDeleted)
                    .ToListAsync();

                foreach (var health in existingServerHealth)
                {
                    health.IsDeleted = true;
                    foreach (var detail in health.PMServerHealthDetails)
                    {
                        detail.IsDeleted = true;
                    }
                }

                // Add new data
                var serverHealth = new PMServerHealth
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    Remarks = updateDto.ServerHealthData.Remarks,
                    UpdatedBy = updateDto.UpdatedBy,
                    CreatedDate = DateTime.UtcNow
                };
                _context.PMServerHealths.Add(serverHealth);

                if (updateDto.ServerHealthData.Details != null)
                {
                    foreach (var detail in updateDto.ServerHealthData.Details)
                    {
                        var healthDetail = new PMServerHealthDetails
                        {
                            ID = Guid.NewGuid(),
                            PMServerHealthID = serverHealth.ID,
                            ServerName = detail.ServerName,
                            ResultStatusID = detail.ResultStatusID,
                            Remarks = detail.Remarks ?? "",
                            UpdatedBy = updateDto.UpdatedBy,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.PMServerHealthDetails.Add(healthDetail);
                    }
                }
            }

            // Update Disk Usage Data
            if (updateDto.DiskUsageData != null)
            {
                // Remove existing data
                var existingDiskUsage = await _context.PMServerDiskUsageHealths
                    .Include(d => d.PMServerDiskUsageHealthDetails)
                    .Where(d => d.PMReportFormServerID == pmReportFormServerID && !d.IsDeleted)
                    .ToListAsync();

                foreach (var disk in existingDiskUsage)
                {
                    disk.IsDeleted = true;
                    foreach (var detail in disk.PMServerDiskUsageHealthDetails)
                    {
                        detail.IsDeleted = true;
                    }
                }

                // Add new data
                var diskUsage = new PMServerDiskUsageHealth
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    Remarks = updateDto.DiskUsageData.Remarks,
                    UpdatedBy = updateDto.UpdatedBy,
                    CreatedDate = DateTime.UtcNow
                };
                _context.PMServerDiskUsageHealths.Add(diskUsage);

                if (updateDto.DiskUsageData.Details != null)
                {
                    foreach (var detail in updateDto.DiskUsageData.Details)
                    {
                        var diskDetail = new PMServerDiskUsageHealthDetails
                        {
                            ID = Guid.NewGuid(),
                            PMServerDiskUsageHealthID = diskUsage.ID,
                            ServerName = detail.ServerName,
                            DiskName = detail.DiskName,
                            Capacity = detail.Capacity,
                            FreeSpace = detail.FreeSpace,
                            Usage = detail.Usage,
                            ServerDiskStatusID = detail.ServerDiskStatusID,
                            ResultStatusID = detail.ResultStatusID,
                            Remarks = detail.Remarks,
                            IsDeleted = false,
                            UpdatedBy = updateDto.UpdatedBy,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.PMServerDiskUsageHealthDetails.Add(diskDetail);
                    }
                }
            }

            // Update CPU and Memory Data
            if (updateDto.CpuAndRamUsageData != null)
            {
                // Remove existing data
                var existingCpuMemory = await _context.PMServerCPUAndMemoryUsages
                    .Include(c => c.PMServerCPUUsageDetails)
                    .Include(c => c.PMServerMemoryUsageDetails)
                    .Where(c => c.PMReportFormServerID == pmReportFormServerID && !c.IsDeleted)
                    .ToListAsync();

                foreach (var cpu in existingCpuMemory)
                {
                    cpu.IsDeleted = true;
                    foreach (var detail in cpu.PMServerCPUUsageDetails)
                    {
                        detail.IsDeleted = true;
                    }
                    foreach (var detail in cpu.PMServerMemoryUsageDetails)
                    {
                        detail.IsDeleted = true;
                    }
                }

                // Add new data
                var cpuMemory = new PMServerCPUAndMemoryUsage
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    Remarks = updateDto.CpuAndRamUsageData.Remarks,
                    UpdatedBy = updateDto.UpdatedBy,
                    CreatedDate = DateTime.UtcNow
                };
                _context.PMServerCPUAndMemoryUsages.Add(cpuMemory);

                if (updateDto.CpuAndRamUsageData.CPUUsageDetails != null)
                {
                    foreach (var detail in updateDto.CpuAndRamUsageData.CPUUsageDetails)
                    {
                        var cpuDetail = new PMServerCPUUsageDetails
                        {
                            ID = Guid.NewGuid(),
                            PMServerCPUAndMemoryUsageID = cpuMemory.ID,
                            SerialNo = detail.SerialNo,
                            ServerName = detail.ServerName,
                            CPUUsage = detail.CPUUsage,
                            ResultStatusID = detail.ResultStatusID,
                            Remarks = detail.Remarks,
                            IsDeleted = false,
                            UpdatedBy = updateDto.UpdatedBy,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.PMServerCPUUsageDetails.Add(cpuDetail);
                    }
                }

                if (updateDto.CpuAndRamUsageData.MemoryUsageDetails != null)
                {
                    foreach (var detail in updateDto.CpuAndRamUsageData.MemoryUsageDetails)
                    {
                        var memoryDetail = new PMServerMemoryUsageDetails
                        {
                            ID = Guid.NewGuid(),
                            PMServerCPUAndMemoryUsageID = cpuMemory.ID,
                            SerialNo = detail.SerialNo,
                            ServerName = detail.ServerName,
                            MemorySize = detail.MemorySize,
                            MemoryInUse = detail.MemoryInUse,
                            ResultStatusID = detail.ResultStatusID,
                            Remarks = detail.Remarks,
                            IsDeleted = false,
                            UpdatedBy = updateDto.UpdatedBy,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.PMServerMemoryUsageDetails.Add(memoryDetail);
                    }
                }
            }

            // Add similar update logic for other components...
            // Network Health, Willowlynx components, Database operations, etc.

            await _context.SaveChangesAsync();
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