using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PMReportFormServerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PMReportFormServerController> _logger;
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public PMReportFormServerController(
            ApplicationDbContext context,
            IConfiguration configuration,
            ILogger<PMReportFormServerController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
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
                .Include(p => p.FormStatusWarehouse)
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Where(p => !p.IsDeleted)
                .Select(p => new PMReportFormServerDto
                {
                    ID = p.ID,
                    ReportFormID = p.ReportFormID,
                    PMReportFormTypeID = p.PMReportFormTypeID,
                    FormstatusID = p.FormstatusID,
                    ProjectNo = p.ProjectNo,
                    Customer = p.Customer,
                    ReportTitle = p.ReportTitle,
                    SignOffData = new SignOffDataDto
                    {
                        AttendedBy = p.AttendedBy,
                        WitnessedBy = p.WitnessedBy,
                        StartDate = p.StartDate,
                        CompletionDate = p.CompletionDate,
                        Remarks = p.Remarks
                    },
                    IsDeleted = p.IsDeleted,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    CreatedBy = p.CreatedBy,
                    UpdatedBy = p.UpdatedBy,
                    PMReportFormTypeName = p.PMReportFormType.Name,
                    FormStatusName = p.FormStatusWarehouse != null ? p.FormStatusWarehouse.Name : null,
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
                .Include(p => p.FormStatusWarehouse)
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
                        ServerEntryIndex = d.ServerEntryIndex, // Include ServerEntryIndex to distinguish duplicate server names
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
                        SerialNo = d.SerialNo,
                        YesNoStatusID = d.YesNoStatusID,
                        YesNoStatusName = d.YesNoStatus.Name,
                        ServerName = d.ServerName,
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
                    LatestBackupFileName = h.LatestBackupFileName,
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
                        SerialNo = d.SerialNo,
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
                        SerialNo = d.SerialNo,
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
                        SerialNo = d.SerialNo,
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
                        SerialNo = d.SerialNo,
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
                    FormstatusID = pmReportFormServer.FormstatusID,
                    ProjectNo = pmReportFormServer.ProjectNo,
                    Customer = pmReportFormServer.Customer,
                    ReportTitle = pmReportFormServer.ReportTitle,
                    SignOffData = new SignOffDataDto
                    {
                        AttendedBy = pmReportFormServer.AttendedBy,
                        WitnessedBy = pmReportFormServer.WitnessedBy,
                        StartDate = pmReportFormServer.StartDate,
                        CompletionDate = pmReportFormServer.CompletionDate,
                        Remarks = pmReportFormServer.Remarks
                    },
                    IsDeleted = pmReportFormServer.IsDeleted,
                    CreatedDate = pmReportFormServer.CreatedDate,
                    UpdatedDate = pmReportFormServer.UpdatedDate,
                    CreatedBy = pmReportFormServer.CreatedBy,
                    UpdatedBy = pmReportFormServer.UpdatedBy,
                    PMReportFormTypeName = pmReportFormServer.PMReportFormType?.Name,
                    FormStatusName = pmReportFormServer.FormStatusWarehouse?.Name,
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

                var formStatusExists = await _context.FormStatusWarehouses.AnyAsync(f => f.ID == createDto.FormstatusID && !f.IsDeleted);
                if (!formStatusExists)
                {
                    return BadRequest("Invalid FormstatusID");
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
                    FormstatusID = createDto.FormstatusID,
                    ProjectNo = createDto.ProjectNo,
                    Customer = createDto.Customer,
                    ReportTitle = createDto.ReportTitle,
                    AttendedBy = createDto.SignOffData?.AttendedBy,
                    WitnessedBy = createDto.SignOffData?.WitnessedBy,
                    StartDate = createDto.SignOffData?.StartDate,
                    CompletionDate = createDto.SignOffData?.CompletionDate,
                    Remarks = createDto.SignOffData?.Remarks,
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
                    FormstatusID = pmReportFormServer.FormstatusID,
                    ProjectNo = pmReportFormServer.ProjectNo,
                    Customer = pmReportFormServer.Customer,
                    ReportTitle = pmReportFormServer.ReportTitle,
                    SignOffData = new SignOffDataDto
                    {
                        AttendedBy = pmReportFormServer.AttendedBy,
                        WitnessedBy = pmReportFormServer.WitnessedBy,
                        StartDate = pmReportFormServer.StartDate,
                        CompletionDate = pmReportFormServer.CompletionDate,
                        Remarks = pmReportFormServer.Remarks
                    },
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

        // PUT: api/PMReportFormServer/5 - Enhanced update with proper tracking
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPMReportFormServer(Guid id, UpdatePMReportFormServerDto updateDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Find the PMReportFormServer by ReportForm ID or PMReportFormServer ID
                PMReportFormServer? pmReportFormServer = null;
                
                // First try to find by PMReportFormServer ID directly
                pmReportFormServer = await _context.PMReportFormServer.FindAsync(id);
                
                // If not found, try to find by ReportForm ID
                if (pmReportFormServer == null)
                {
                    var reportForm = await _context.ReportForms.FindAsync(id);
                    if (reportForm != null && !reportForm.IsDeleted)
                    {
                        pmReportFormServer = await _context.PMReportFormServer
                            .Where(p => p.ReportFormID == id && !p.IsDeleted)
                            .FirstOrDefaultAsync();
                    }
                }

                if (pmReportFormServer == null || pmReportFormServer.IsDeleted)
                {
                    return NotFound("PMReportFormServer not found");
                }

                await _context.Entry(pmReportFormServer).Reference(p => p.FormStatusWarehouse).LoadAsync();
                var currentStatusName = pmReportFormServer.FormStatusWarehouse?.Name?.Trim();
                if (!string.IsNullOrEmpty(currentStatusName) &&
                    currentStatusName.Equals("Close", StringComparison.OrdinalIgnoreCase))
                {
                    return BadRequest("Closed Server PM reports cannot be modified.");
                }

                // Validate that PMReportFormType exists
                var pmReportFormTypeExists = await _context.PMReportFormTypes.AnyAsync(t => t.ID == updateDto.PMReportFormTypeID && !t.IsDeleted);
                if (!pmReportFormTypeExists)
                {
                    return BadRequest("Invalid PMReportFormTypeID");
                }

                if (updateDto.FormstatusID.HasValue)
                {
                    var formStatusExists = await _context.FormStatusWarehouses.AnyAsync(f => f.ID == updateDto.FormstatusID.Value && !f.IsDeleted);
                    if (!formStatusExists)
                    {
                        return BadRequest("Invalid FormstatusID");
                    }
                    pmReportFormServer.FormstatusID = updateDto.FormstatusID.Value;
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

                // Update main PM Report Form Server properties
                pmReportFormServer.PMReportFormTypeID = updateDto.PMReportFormTypeID;
                pmReportFormServer.ProjectNo = updateDto.ProjectNo;
                pmReportFormServer.Customer = updateDto.Customer;
                pmReportFormServer.ReportTitle = updateDto.ReportTitle;
                pmReportFormServer.AttendedBy = updateDto.SignOffData?.AttendedBy;
                pmReportFormServer.WitnessedBy = updateDto.SignOffData?.WitnessedBy;
                pmReportFormServer.StartDate = updateDto.SignOffData?.StartDate;
                pmReportFormServer.CompletionDate = updateDto.SignOffData?.CompletionDate;
                pmReportFormServer.Remarks = updateDto.SignOffData?.Remarks;
                pmReportFormServer.UpdatedDate = DateTime.UtcNow;
                pmReportFormServer.UpdatedBy = updateDto.UpdatedBy;

                // Update PM Server component data with proper tracking
                await UpdatePMServerComponentDataWithTracking(pmReportFormServer.ID, updateDto);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private async Task UpdatePMServerComponentDataWithTracking(Guid pmReportFormServerID, UpdatePMReportFormServerDto updateDto)
        {
            // Update Server Health Data with tracking
            if (updateDto.ServerHealthData != null)
            {
                await UpdateServerHealthDataWithTracking(pmReportFormServerID, updateDto.ServerHealthData, updateDto.UpdatedBy);
            }

            // Update Disk Usage Data with tracking
            if (updateDto.DiskUsageData != null)
            {
                await UpdateDiskUsageDataWithTracking(pmReportFormServerID, updateDto.DiskUsageData, updateDto.UpdatedBy);
            }

            // Update CPU and Memory Data with tracking
            if (updateDto.CpuAndRamUsageData != null)
            {
                await UpdateCpuAndMemoryDataWithTracking(pmReportFormServerID, updateDto.CpuAndRamUsageData, updateDto.UpdatedBy);
            }

            // Update Monthly Database Creation Data with tracking
            if (updateDto.MonthlyDatabaseCreationData != null)
            {
                await UpdateMonthlyDatabaseCreationDataWithTracking(pmReportFormServerID, updateDto.MonthlyDatabaseCreationData, updateDto.UpdatedBy);
            }

            // Update Database Backup Data with tracking
            if (updateDto.DatabaseBackupData != null)
            {
                await UpdateDatabaseBackupDataWithTracking(pmReportFormServerID, updateDto.DatabaseBackupData, updateDto.UpdatedBy);
            }

            // Update Time Sync Data with tracking
            if (updateDto.TimeSyncData != null)
            {
                await UpdateTimeSyncDataWithTracking(pmReportFormServerID, updateDto.TimeSyncData, updateDto.UpdatedBy);
            }

            // Update Hot Fixes Data with tracking
            if (updateDto.HotFixesData != null)
            {
                await UpdateHotFixesDataWithTracking(pmReportFormServerID, updateDto.HotFixesData, updateDto.UpdatedBy);
            }

            // Update Auto Fail Over Data with tracking
            if (updateDto.AutoFailOverData != null)
            {
                await UpdateAutoFailOverDataWithTracking(pmReportFormServerID, updateDto.AutoFailOverData, updateDto.UpdatedBy);
            }

            // Update ASA Firewall Data with tracking
            if (updateDto.AsaFirewallData != null)
            {
                await UpdateAsaFirewallDataWithTracking(pmReportFormServerID, updateDto.AsaFirewallData, updateDto.UpdatedBy);
            }

            // Update Software Patch Data with tracking
            if (updateDto.SoftwarePatchData != null)
            {
                await UpdateSoftwarePatchDataWithTracking(pmReportFormServerID, updateDto.SoftwarePatchData, updateDto.UpdatedBy);
            }

            // Update Hard Drive Health Data with tracking
            if (updateDto.HardDriveHealthData != null)
            {
                await UpdateHardDriveHealthDataWithTracking(pmReportFormServerID, updateDto.HardDriveHealthData, updateDto.UpdatedBy);
            }

            // Update Network Health Data with tracking
            if (updateDto.NetworkHealthData != null)
            {
                await UpdateNetworkHealthDataWithTracking(pmReportFormServerID, updateDto.NetworkHealthData, updateDto.UpdatedBy);
            }

            // Update Willowlynx Process Status Data with tracking
            if (updateDto.WillowlynxProcessStatusData != null)
            {
                await UpdateWillowlynxProcessStatusDataWithTracking(pmReportFormServerID, updateDto.WillowlynxProcessStatusData, updateDto.UpdatedBy);
            }

            // Update Willowlynx Network Status Data with tracking
            if (updateDto.WillowlynxNetworkStatusData != null)
            {
                await UpdateWillowlynxNetworkStatusDataWithTracking(pmReportFormServerID, updateDto.WillowlynxNetworkStatusData, updateDto.UpdatedBy);
            }

            // Update Willowlynx RTU Status Data with tracking
            if (updateDto.WillowlynxRTUStatusData != null)
            {
                await UpdateWillowlynxRTUStatusDataWithTracking(pmReportFormServerID, updateDto.WillowlynxRTUStatusData, updateDto.UpdatedBy);
            }

            // Update Willowlynx Historical Trend Data with tracking
            if (updateDto.WillowlynxHistoricalTrendData != null)
            {
                await UpdateWillowlynxHistoricalTrendDataWithTracking(pmReportFormServerID, updateDto.WillowlynxHistoricalTrendData, updateDto.UpdatedBy);
            }

            // Update Willowlynx Historical Report Data with tracking
            if (updateDto.WillowlynxHistoricalReportData != null)
            {
                await UpdateWillowlynxHistoricalReportDataWithTracking(pmReportFormServerID, updateDto.WillowlynxHistoricalReportData, updateDto.UpdatedBy);
            }

            // Update Willowlynx Sump Pit CCTV Camera Data with tracking
            if (updateDto.WillowlynxSumpPitCCTVCameraData != null)
            {
                await UpdateWillowlynxSumpPitCCTVCameraDataWithTracking(pmReportFormServerID, updateDto.WillowlynxSumpPitCCTVCameraData, updateDto.UpdatedBy);
            }
        }

        private async Task UpdateServerHealthDataWithTracking(Guid pmReportFormServerID, UpdatePMServerHealthDataDto updateData, Guid? updatedBy)
        {
            // Validate that updatedBy user exists if provided
            if (updatedBy.HasValue)
            {
                var userExists = await _context.Users.AnyAsync(u => u.ID == updatedBy.Value && !u.IsDeleted);
                if (!userExists)
                {
                    throw new InvalidOperationException($"Invalid UpdatedBy user ID: {updatedBy.Value}");
                }
            }

            // Get or create the main ServerHealth record
            var serverHealth = await _context.PMServerHealths
                .Include(s => s.PMServerHealthDetails)
                .Where(s => s.PMReportFormServerID == pmReportFormServerID && !s.IsDeleted)
                .FirstOrDefaultAsync();

            if (serverHealth == null)
            {
                // Only create if we have a valid user ID
                if (!updatedBy.HasValue)
                {
                    throw new InvalidOperationException("Cannot create PMServerHealth record without a valid UpdatedBy user ID");
                }

                serverHealth = new PMServerHealth
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    Remarks = updateData.Remarks,
                    IsDeleted = false,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = updatedBy.Value,
                    UpdatedBy = updatedBy
                };
                _context.PMServerHealths.Add(serverHealth);
            }
            else
            {
                serverHealth.Remarks = updateData.Remarks;
                serverHealth.UpdatedBy = updatedBy;
                serverHealth.UpdatedDate = DateTime.UtcNow;
            }

            // Process detail records with tracking
            if (updateData.Details != null)
            {
                foreach (var detailDto in updateData.Details)
                {
                    if (detailDto.IsDeleted && detailDto.ID.HasValue)
                    {
                        // Mark existing record as deleted
                        var existingDetail = await _context.PMServerHealthDetails
                            .Where(d => d.ID == detailDto.ID.Value)
                            .FirstOrDefaultAsync();
                        if (existingDetail != null)
                        {
                            existingDetail.IsDeleted = true;
                            existingDetail.UpdatedBy = updatedBy;
                            existingDetail.UpdatedDate = DateTime.UtcNow;
                        }
                    }
                    else if (detailDto.IsNew)
                    {
                        // Create new record only if updatedBy has a valid value and ResultStatusID is valid
                        if (updatedBy.HasValue && detailDto.ResultStatusID != Guid.Empty)
                        {
                            var newDetail = new PMServerHealthDetails
                            {
                                ID = Guid.NewGuid(),
                                PMServerHealthID = serverHealth.ID,
                                ServerName = detailDto.ServerName,
                                ResultStatusID = detailDto.ResultStatusID,
                                Remarks = detailDto.Remarks,
                                IsDeleted = false,
                                CreatedDate = DateTime.UtcNow,
                                CreatedBy = updatedBy.Value,
                                UpdatedBy = updatedBy
                            };
                            _context.PMServerHealthDetails.Add(newDetail);
                        }
                    }
                    else if (detailDto.ID.HasValue)
                    {
                        // Update existing record only if ResultStatusID is valid
                        var existingDetail = await _context.PMServerHealthDetails
                            .Where(d => d.ID == detailDto.ID.Value)
                            .FirstOrDefaultAsync();
                        if (existingDetail != null && detailDto.ResultStatusID != Guid.Empty)
                        {
                            existingDetail.ServerName = detailDto.ServerName;
                            existingDetail.ResultStatusID = detailDto.ResultStatusID;
                            existingDetail.Remarks = detailDto.Remarks;
                            existingDetail.UpdatedBy = updatedBy;
                            existingDetail.UpdatedDate = DateTime.UtcNow;
                        }
                    }
                }
            }
        }

        private async Task UpdateDiskUsageDataWithTracking(Guid pmReportFormServerID, UpdatePMServerDiskUsageDataDto updateData, Guid? updatedBy)
        {
            // Validate that updatedBy user exists if provided
            if (updatedBy.HasValue)
            {
                var userExists = await _context.Users.AnyAsync(u => u.ID == updatedBy.Value && !u.IsDeleted);
                if (!userExists)
                {
                    throw new InvalidOperationException($"Invalid UpdatedBy user ID: {updatedBy.Value}");
                }
            }

            // Get or create the main DiskUsage record
            var diskUsage = await _context.PMServerDiskUsageHealths
                .Include(d => d.PMServerDiskUsageHealthDetails)
                .Where(d => d.PMReportFormServerID == pmReportFormServerID && !d.IsDeleted)
                .FirstOrDefaultAsync();

            if (diskUsage == null)
            {
                // Only create if we have a valid user ID
                if (!updatedBy.HasValue)
                {
                    throw new InvalidOperationException("Cannot create PMServerDiskUsageHealth record without a valid UpdatedBy user ID");
                }

                diskUsage = new PMServerDiskUsageHealth
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    Remarks = updateData.Remarks,
                    IsDeleted = false,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = updatedBy.Value,
                    UpdatedBy = updatedBy
                };
                _context.PMServerDiskUsageHealths.Add(diskUsage);
            }
            else
            {
                diskUsage.Remarks = updateData.Remarks;
                diskUsage.UpdatedBy = updatedBy;
                diskUsage.UpdatedDate = DateTime.UtcNow;
            }

            // Process detail records with tracking and cascading logic
            // CRITICAL: Process each server entry separately to preserve duplicate server names
            // Instead of grouping by ServerName (which merges duplicates), process each server individually
            
            // Handle new hierarchical structure (servers with disks) - process each server separately
            if (updateData.Servers != null && updateData.Servers.Any())
            {
                // Process each server entry individually to preserve duplicate server names
                // Use ServerEntryIndex from DTO if provided, otherwise use array index as fallback
                int arrayIndex = 0;
                foreach (var server in updateData.Servers)
                {
                    var serverName = server.ServerName;
                    // Use ServerEntryIndex from DTO if provided, otherwise use array index
                    // CRITICAL: For existing servers (with IDs), if ServerEntryIndex is null in DTO,
                    // we should preserve the existing value from database rather than defaulting to arrayIndex
                    // This prevents overwriting ServerEntryIndex when saving from other components (e.g., ServerHealth_Edit)
                    int? serverEntryIndex = server.ServerEntryIndex.HasValue 
                        ? server.ServerEntryIndex.Value 
                        : null; // Don't default to arrayIndex - preserve existing value for existing records
                    
                    // Skip deleted servers (unless they have an ID that needs to be marked as deleted)
                    if (server.IsDeleted && !server.Id.HasValue)
                    {
                        continue; // Skip new servers that are deleted
                    }
                    
                    // Get disks for this specific server entry (not grouped by name)
                    var serverDisks = new List<UpdatePMServerDiskUsageDetailDto>();
                    if (server.Disks != null && server.Disks.Any())
                    {
                        foreach (var disk in server.Disks)
                        {
                            // Skip deleted disks (unless they have an ID that needs to be marked as deleted)
                            if (disk.IsDeleted && !disk.Id.HasValue)
                            {
                                continue;
                            }
                            
                            serverDisks.Add(new UpdatePMServerDiskUsageDetailDto
                            {
                                ID = disk.Id,
                                DiskName = disk.Disk,
                                ServerName = server.ServerName,
                                Capacity = disk.Capacity,
                                FreeSpace = disk.FreeSpace,
                                Usage = disk.Usage,
                                ServerDiskStatusID = disk.Status,
                                ResultStatusID = disk.Check,
                                Remarks = disk.Remarks,
                                IsNew = disk.IsNew,
                                IsDeleted = disk.IsDeleted
                            });
                        }
                    }
                    
                    if (!serverDisks.Any())
                    {
                        continue; // Skip servers with no valid disks
                    }

                    // Process each disk individually (don't group by server name to preserve duplicates)
                    // For duplicate server names, we process each server entry separately
                    foreach (var detailDto in serverDisks)
                    {
                            // Validation: Check if server is deleted before allowing individual disk operations
                            if (detailDto.ID.HasValue)
                            {
                                var existingDisk = await _context.PMServerDiskUsageHealthDetails
                                    .Where(d => d.ID == detailDto.ID.Value)
                                    .FirstOrDefaultAsync();

                                if (existingDisk != null)
                                {
                                    // Skip operations on individual deleted disks (unless restoring)
                                    // CRITICAL: Don't check for other disks with the same server name (that would merge duplicates)
                                    // Just check this specific disk
                                    if (existingDisk.IsDeleted && !detailDto.IsDeleted)
                                    {
                                        continue; // Skip this operation - disk is deleted
                                    }
                                }
                            }

                            if (detailDto.IsDeleted && detailDto.ID.HasValue)
                            {
                                // Mark existing record as deleted
                                var existingDetail = await _context.PMServerDiskUsageHealthDetails
                                    .Where(d => d.ID == detailDto.ID.Value)
                                    .FirstOrDefaultAsync();
                                if (existingDetail != null)
                                {
                                    existingDetail.IsDeleted = true;
                                    existingDetail.UpdatedBy = updatedBy;
                                    existingDetail.UpdatedDate = DateTime.UtcNow;
                                }
                            }
                            else if (detailDto.IsNew)
                            {
                                // Log the incoming data for debugging
                                _logger.LogInformation($"Processing NEW disk - ServerName: {detailDto.ServerName}, DiskName: {detailDto.DiskName}");
                                _logger.LogInformation($"  ResultStatusID: {detailDto.ResultStatusID}, ServerDiskStatusID: {detailDto.ServerDiskStatusID}");
                                _logger.LogInformation($"  IsNew: {detailDto.IsNew}, HasValue(updatedBy): {updatedBy.HasValue}");
                                _logger.LogInformation($"  ResultStatusID != Guid.Empty: {detailDto.ResultStatusID != Guid.Empty}");
                                _logger.LogInformation($"  ServerDiskStatusID != Guid.Empty: {detailDto.ServerDiskStatusID != Guid.Empty}");
                                
                                // Create new record only if updatedBy has a valid value and required IDs are valid
                                // CRITICAL: For duplicate server names, we process each server entry individually
                                // Don't check for other disks with the same server name (that would merge duplicates)
                                if (updatedBy.HasValue && detailDto.ResultStatusID != Guid.Empty && detailDto.ServerDiskStatusID != Guid.Empty)
                                {
                                    _logger.LogInformation($"  CREATING new disk entry for: {detailDto.ServerName} - {detailDto.DiskName}");
                                    var newDetail = new PMServerDiskUsageHealthDetails
                                    {
                                        ID = Guid.NewGuid(),
                                        PMServerDiskUsageHealthID = diskUsage.ID,
                                        ServerName = detailDto.ServerName,
                                        DiskName = detailDto.DiskName,
                                        Capacity = detailDto.Capacity,
                                        FreeSpace = detailDto.FreeSpace,
                                        Usage = detailDto.Usage,
                                        ServerDiskStatusID = detailDto.ServerDiskStatusID,
                                        ResultStatusID = detailDto.ResultStatusID,
                                        Remarks = detailDto.Remarks,
                                        ServerEntryIndex = serverEntryIndex ?? arrayIndex, // For new records, use provided ServerEntryIndex or fallback to arrayIndex
                                        IsDeleted = false,
                                        CreatedDate = DateTime.UtcNow,
                                        CreatedBy = updatedBy.Value,
                                        UpdatedBy = updatedBy
                                    };
                                    _context.PMServerDiskUsageHealthDetails.Add(newDetail);
                                }
                                else
                                {
                                    _logger.LogWarning($"  FAILED validation - updatedBy: {updatedBy?.ToString() ?? "null"}, ResultStatusID: {detailDto.ResultStatusID}, ServerDiskStatusID: {detailDto.ServerDiskStatusID}");
                                }
                            }
                            else if (detailDto.ID.HasValue)
                            {
                                // Update existing record only if required IDs are valid
                                var existingDetail = await _context.PMServerDiskUsageHealthDetails
                                    .Where(d => d.ID == detailDto.ID.Value)
                                    .FirstOrDefaultAsync();
                                if (existingDetail != null && !existingDetail.IsDeleted && 
                                    detailDto.ResultStatusID != Guid.Empty && detailDto.ServerDiskStatusID != Guid.Empty)
                                {
                                    existingDetail.ServerName = detailDto.ServerName;
                                    existingDetail.DiskName = detailDto.DiskName;
                                    existingDetail.Capacity = detailDto.Capacity;
                                    existingDetail.FreeSpace = detailDto.FreeSpace;
                                    existingDetail.Usage = detailDto.Usage;
                                    existingDetail.ServerDiskStatusID = detailDto.ServerDiskStatusID;
                                    existingDetail.ResultStatusID = detailDto.ResultStatusID;
                                    existingDetail.Remarks = detailDto.Remarks;
                                    // CRITICAL: Only update ServerEntryIndex if it's explicitly provided in the DTO
                                    // This prevents overwriting existing ServerEntryIndex values when saving from other components (e.g., ServerHealth_Edit)
                                    // If ServerEntryIndex is not provided (null), preserve the existing value from database
                                    if (serverEntryIndex.HasValue)
                                    {
                                        existingDetail.ServerEntryIndex = serverEntryIndex.Value; // Update server entry index only if explicitly provided
                                    }
                                    // If ServerEntryIndex is null in DTO, keep the existing value (don't overwrite)
                                    existingDetail.UpdatedBy = updatedBy;
                                    existingDetail.UpdatedDate = DateTime.UtcNow;
                                }
                            }
                    }
                    // Increment array index for next server entry
                    arrayIndex++;
                }
            }
            // Handle legacy flat structure for backward compatibility
            // Note: Legacy Details structure is no longer used, but kept for reference
            // If needed in the future, uncomment and ensure UpdatePMServerDiskUsageDataDto has Details property
            
            else if (updateData.Details != null && updateData.Details.Any())
            {
                // Process legacy flat structure - group by ServerName (original behavior)
                var serverGroups = updateData.Details.GroupBy(d => d.ServerName).ToList();

                foreach (var serverGroup in serverGroups)
                {
                    var serverName = serverGroup.Key;
                    var serverDisks = serverGroup.ToList();

                    // Check if this is a server-level deletion (all disks for a server are marked as deleted)
                    var isServerDeletion = serverDisks.All(d => d.IsDeleted) && serverDisks.Any();
                    
                    // Check if this is a server-level restoration (all disks for a server are being restored)
                    var isServerRestoration = serverDisks.All(d => !d.IsDeleted) && 
                                            serverDisks.Any(d => d.ID.HasValue) &&
                                            await _context.PMServerDiskUsageHealthDetails
                                                .AnyAsync(existing => serverDisks.Select(sd => sd.ID).Contains(existing.ID) && existing.IsDeleted);

                    if (isServerDeletion)
                    {
                        // Server deletion: cascade to all disks for this server
                        var allServerDisks = await _context.PMServerDiskUsageHealthDetails
                            .Where(d => d.PMServerDiskUsageHealthID == diskUsage.ID && 
                                       d.ServerName == serverName && !d.IsDeleted)
                            .ToListAsync();

                        foreach (var disk in allServerDisks)
                        {
                            disk.IsDeleted = true;
                            disk.UpdatedBy = updatedBy;
                            disk.UpdatedDate = DateTime.UtcNow;
                        }
                    }
                    else if (isServerRestoration)
                    {
                        // Server restoration: cascade to all disks for this server
                        var allServerDisks = await _context.PMServerDiskUsageHealthDetails
                            .Where(d => d.PMServerDiskUsageHealthID == diskUsage.ID && 
                                       d.ServerName == serverName && d.IsDeleted)
                            .ToListAsync();

                        foreach (var disk in allServerDisks)
                        {
                            disk.IsDeleted = false;
                            disk.UpdatedBy = updatedBy;
                            disk.UpdatedDate = DateTime.UtcNow;
                        }
                    }
                    else
                    {
                        // Individual disk operations
                        foreach (var detailDto in serverDisks)
                        {
                            // Validation: Check if server is deleted before allowing individual disk operations
                            if (detailDto.ID.HasValue)
                            {
                                var existingDisk = await _context.PMServerDiskUsageHealthDetails
                                    .Where(d => d.ID == detailDto.ID.Value)
                                    .FirstOrDefaultAsync();

                                if (existingDisk != null)
                                {
                                    // Check if any disk for this server is deleted (indicating server is deleted)
                                    var serverHasDeletedDisks = await _context.PMServerDiskUsageHealthDetails
                                        .AnyAsync(d => d.PMServerDiskUsageHealthID == diskUsage.ID && 
                                                      d.ServerName == serverName && d.IsDeleted);

                                    // Skip individual operations if server is considered deleted
                                    if (serverHasDeletedDisks && !detailDto.IsDeleted)
                                    {
                                        continue; // Skip this operation - server is deleted
                                    }

                                    // Skip operations on individual deleted disks (unless restoring)
                                    if (existingDisk.IsDeleted && !detailDto.IsDeleted)
                                    {
                                        continue; // Skip this operation - disk is deleted
                                    }
                                }
                            }

                            if (detailDto.IsDeleted && detailDto.ID.HasValue)
                            {
                                // Mark existing record as deleted
                                var existingDetail = await _context.PMServerDiskUsageHealthDetails
                                    .Where(d => d.ID == detailDto.ID.Value)
                                    .FirstOrDefaultAsync();
                                if (existingDetail != null)
                                {
                                    existingDetail.IsDeleted = true;
                                    existingDetail.UpdatedBy = updatedBy;
                                    existingDetail.UpdatedDate = DateTime.UtcNow;
                                }
                            }
                            else if (detailDto.IsNew)
                            {
                                // Create new record only if updatedBy has a valid value and required IDs are valid
                                if (updatedBy.HasValue && detailDto.ResultStatusID != Guid.Empty && detailDto.ServerDiskStatusID != Guid.Empty)
                                {
                                    // For legacy structure, use 0 as ServerEntryIndex since we group by ServerName
                                    var newDetail = new PMServerDiskUsageHealthDetails
                                    {
                                        ID = Guid.NewGuid(),
                                        PMServerDiskUsageHealthID = diskUsage.ID,
                                        ServerName = detailDto.ServerName,
                                        DiskName = detailDto.DiskName,
                                        Capacity = detailDto.Capacity,
                                        FreeSpace = detailDto.FreeSpace,
                                        Usage = detailDto.Usage,
                                        ServerDiskStatusID = detailDto.ServerDiskStatusID,
                                        ResultStatusID = detailDto.ResultStatusID,
                                        Remarks = detailDto.Remarks,
                                        ServerEntryIndex = 0, // Legacy structure uses 0 as default
                                        IsDeleted = false,
                                        CreatedDate = DateTime.UtcNow,
                                        CreatedBy = updatedBy.Value,
                                        UpdatedBy = updatedBy
                                    };
                                    _context.PMServerDiskUsageHealthDetails.Add(newDetail);
                                }
                            }
                            else if (detailDto.ID.HasValue)
                            {
                                // Update existing record only if required IDs are valid
                                var existingDetail = await _context.PMServerDiskUsageHealthDetails
                                    .Where(d => d.ID == detailDto.ID.Value)
                                    .FirstOrDefaultAsync();
                                if (existingDetail != null && !existingDetail.IsDeleted && 
                                    detailDto.ResultStatusID != Guid.Empty && detailDto.ServerDiskStatusID != Guid.Empty)
                                {
                                    existingDetail.ServerName = detailDto.ServerName;
                                    existingDetail.DiskName = detailDto.DiskName;
                                    existingDetail.Capacity = detailDto.Capacity;
                                    existingDetail.FreeSpace = detailDto.FreeSpace;
                                    existingDetail.Usage = detailDto.Usage;
                                    existingDetail.ServerDiskStatusID = detailDto.ServerDiskStatusID;
                                    existingDetail.ResultStatusID = detailDto.ResultStatusID;
                                    existingDetail.Remarks = detailDto.Remarks;
                                    existingDetail.UpdatedBy = updatedBy;
                                    existingDetail.UpdatedDate = DateTime.UtcNow;
                                }
                            }
                        }
                    }
                }
            }
            
        }

        private async Task UpdateCpuAndMemoryDataWithTracking(Guid pmReportFormServerID, UpdatePMServerCPUAndMemoryDataDto updateData, Guid? updatedBy)
        {
            // Validate that updatedBy user exists if provided
            if (updatedBy.HasValue)
            {
                var userExists = await _context.Users.AnyAsync(u => u.ID == updatedBy.Value && !u.IsDeleted);
                if (!userExists)
                {
                    throw new InvalidOperationException($"Invalid UpdatedBy user ID: {updatedBy.Value}");
                }
            }

            // Get or create the main CPU and Memory record
            var cpuMemory = await _context.PMServerCPUAndMemoryUsages
                .Include(c => c.PMServerCPUUsageDetails)
                .Include(c => c.PMServerMemoryUsageDetails)
                .Where(c => c.PMReportFormServerID == pmReportFormServerID && !c.IsDeleted)
                .FirstOrDefaultAsync();

            if (cpuMemory == null)
            {
                // Only create if we have a valid user ID
                if (!updatedBy.HasValue)
                {
                    throw new InvalidOperationException("Cannot create PMServerCPUAndMemoryUsage record without a valid UpdatedBy user ID");
                }

                cpuMemory = new PMServerCPUAndMemoryUsage
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    Remarks = updateData.Remarks,
                    IsDeleted = false,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = updatedBy.Value,
                    UpdatedBy = updatedBy
                };
                _context.PMServerCPUAndMemoryUsages.Add(cpuMemory);
            }
            else
            {
                cpuMemory.Remarks = updateData.Remarks;
                cpuMemory.UpdatedBy = updatedBy;
                cpuMemory.UpdatedDate = DateTime.UtcNow;
            }

            // Process CPU Usage detail records with tracking
            if (updateData.CPUUsageDetails != null)
            {
                foreach (var detailDto in updateData.CPUUsageDetails)
                {
                    if (detailDto.IsDeleted && detailDto.ID.HasValue)
                    {
                        // Mark existing record as deleted
                        var existingDetail = await _context.PMServerCPUUsageDetails
                            .Where(d => d.ID == detailDto.ID.Value)
                            .FirstOrDefaultAsync();
                        if (existingDetail != null)
                        {
                            existingDetail.IsDeleted = true;
                            existingDetail.UpdatedBy = updatedBy;
                            existingDetail.UpdatedDate = DateTime.UtcNow;
                        }
                    }
                    else if (detailDto.IsNew)
                    {
                        // Create new record only if updatedBy has a valid value
                        if (updatedBy.HasValue)
                        {
                            var newDetail = new PMServerCPUUsageDetails
                            {
                                ID = Guid.NewGuid(),
                                PMServerCPUAndMemoryUsageID = cpuMemory.ID,
                                SerialNo = detailDto.SerialNo,
                                ServerName = detailDto.ServerName,
                                CPUUsage = detailDto.CPUUsage,
                                ResultStatusID = detailDto.ResultStatusID,
                                Remarks = detailDto.Remarks,
                                IsDeleted = false,
                                CreatedDate = DateTime.UtcNow,
                                CreatedBy = updatedBy.Value,
                                UpdatedBy = updatedBy
                            };
                            _context.PMServerCPUUsageDetails.Add(newDetail);
                        }
                    }
                    else if (detailDto.ID.HasValue)
                    {
                        // Update existing record
                        var existingDetail = await _context.PMServerCPUUsageDetails
                            .Where(d => d.ID == detailDto.ID.Value)
                            .FirstOrDefaultAsync();
                        if (existingDetail != null)
                        {
                            existingDetail.SerialNo = detailDto.SerialNo;
                            existingDetail.ServerName = detailDto.ServerName;
                            existingDetail.CPUUsage = detailDto.CPUUsage;
                            existingDetail.ResultStatusID = detailDto.ResultStatusID;
                            existingDetail.Remarks = detailDto.Remarks;
                            existingDetail.UpdatedBy = updatedBy;
                            existingDetail.UpdatedDate = DateTime.UtcNow;
                        }
                    }
                }
            }

            // Process Memory Usage detail records with tracking
            if (updateData.MemoryUsageDetails != null)
            {
                foreach (var detailDto in updateData.MemoryUsageDetails)
                {
                    if (detailDto.IsDeleted && detailDto.ID.HasValue)
                    {
                        // Mark existing record as deleted
                        var existingDetail = await _context.PMServerMemoryUsageDetails
                            .Where(d => d.ID == detailDto.ID.Value)
                            .FirstOrDefaultAsync();
                        if (existingDetail != null)
                        {
                            existingDetail.IsDeleted = true;
                            existingDetail.UpdatedBy = updatedBy;
                            existingDetail.UpdatedDate = DateTime.UtcNow;
                        }
                    }
                    else if (detailDto.IsNew)
                    {
                        // Create new record only if updatedBy has a valid value
                        if (updatedBy.HasValue)
                        {
                            var newDetail = new PMServerMemoryUsageDetails
                            {
                                ID = Guid.NewGuid(),
                                PMServerCPUAndMemoryUsageID = cpuMemory.ID,
                                SerialNo = detailDto.SerialNo,
                                ServerName = detailDto.ServerName,
                                MemorySize = detailDto.MemorySize,
                                MemoryInUse = detailDto.MemoryInUse,
                                ResultStatusID = detailDto.ResultStatusID,
                                Remarks = detailDto.Remarks,
                                IsDeleted = false,
                                CreatedDate = DateTime.UtcNow,
                                CreatedBy = updatedBy.Value,
                                UpdatedBy = updatedBy
                            };
                            _context.PMServerMemoryUsageDetails.Add(newDetail);
                        }
                    }
                    else if (detailDto.ID.HasValue)
                    {
                        // Update existing record
                        var existingDetail = await _context.PMServerMemoryUsageDetails
                            .Where(d => d.ID == detailDto.ID.Value)
                            .FirstOrDefaultAsync();
                        if (existingDetail != null)
                        {
                            existingDetail.SerialNo = detailDto.SerialNo;
                            existingDetail.ServerName = detailDto.ServerName;
                            existingDetail.MemorySize = detailDto.MemorySize;
                            existingDetail.MemoryInUse = detailDto.MemoryInUse;
                            existingDetail.ResultStatusID = detailDto.ResultStatusID;
                            existingDetail.Remarks = detailDto.Remarks;
                            existingDetail.UpdatedBy = updatedBy;
                            existingDetail.UpdatedDate = DateTime.UtcNow;
                        }
                    }
                }
            }
        }

        // Placeholder methods for other components - implement as needed
        private async Task UpdateMonthlyDatabaseCreationDataWithTracking(Guid pmReportFormServerID, UpdatePMServerMonthlyDatabaseCreationDataDto updateData, Guid? updatedBy)
        {
            // Get existing Monthly Database Creation record
            var existingRecord = await _context.PMServerMonthlyDatabaseCreations
                .Include(x => x.PMServerMonthlyDatabaseCreationDetails)
                .FirstOrDefaultAsync(x => x.PMReportFormServerID == pmReportFormServerID && !x.IsDeleted);

            if (existingRecord == null)
            {
                // Create new record only if updatedBy has a valid value
                if (updatedBy.HasValue)
                {
                    var newRecord = new PMServerMonthlyDatabaseCreation
                    {
                        ID = Guid.NewGuid(),
                        PMReportFormServerID = pmReportFormServerID,
                        Remarks = updateData.Remarks,
                        IsDeleted = false,
                        CreatedBy = updatedBy.Value,
                        CreatedDate = DateTime.UtcNow
                    };
                    _context.PMServerMonthlyDatabaseCreations.Add(newRecord);

                    // Add new details
                    if (updateData.Details != null)
                    {
                        foreach (var detail in updateData.Details.Where(d => !d.IsDeleted))
                        {
                            var newDetail = new PMServerMonthlyDatabaseCreationDetails
                            {
                                ID = Guid.NewGuid(),
                                PMServerMonthlyDatabaseCreationID = newRecord.ID,
                                SerialNo = detail.SerialNo,
                                ServerName = detail.ServerName,
                                YesNoStatusID = detail.YesNoStatusID,
                                Remarks = detail.Remarks,
                                IsDeleted = false,
                                CreatedBy = updatedBy.Value,
                                CreatedDate = DateTime.UtcNow
                            };
                            _context.PMServerMonthlyDatabaseCreationDetails.Add(newDetail);
                        }
                    }
                }
            }
            else
            {
                // Update existing record
                existingRecord.Remarks = updateData.Remarks;
                existingRecord.UpdatedBy = updatedBy;
                existingRecord.UpdatedDate = DateTime.UtcNow;

                // Handle details with tracking
                if (updateData.Details != null)
                {
                    foreach (var detail in updateData.Details)
                    {
                        if (detail.IsDeleted && detail.ID.HasValue)
                        {
                            // Soft delete existing detail
                            var existingDetail = existingRecord.PMServerMonthlyDatabaseCreationDetails
                                .FirstOrDefault(d => d.ID == detail.ID.Value);
                            if (existingDetail != null)
                            {
                                existingDetail.IsDeleted = true;
                                existingDetail.UpdatedBy = updatedBy;
                                existingDetail.UpdatedDate = DateTime.UtcNow;
                            }
                        }
                        else if (detail.IsNew || !detail.ID.HasValue)
                        {
                            // Create new detail only if updatedBy has a valid value
                            if (updatedBy.HasValue)
                            {
                                var newDetail = new PMServerMonthlyDatabaseCreationDetails
                                {
                                    ID = Guid.NewGuid(),
                                    PMServerMonthlyDatabaseCreationID = existingRecord.ID,
                                    SerialNo = detail.SerialNo,
                                    ServerName = detail.ServerName,
                                    YesNoStatusID = detail.YesNoStatusID,
                                    Remarks = detail.Remarks,
                                    IsDeleted = false,
                                    CreatedBy = updatedBy.Value,
                                    CreatedDate = DateTime.UtcNow
                                };
                                _context.PMServerMonthlyDatabaseCreationDetails.Add(newDetail);
                            }
                        }
                        
                        else if (detail.ID.HasValue)
                        {
                            // Update existing detail
                            var existingDetail = existingRecord.PMServerMonthlyDatabaseCreationDetails
                                .FirstOrDefault(d => d.ID == detail.ID.Value);
                            if (existingDetail != null)
                            {
                                existingDetail.SerialNo = detail.SerialNo;
                                existingDetail.ServerName = detail.ServerName;
                                existingDetail.YesNoStatusID = detail.YesNoStatusID;
                                existingDetail.Remarks = detail.Remarks;
                                existingDetail.UpdatedBy = updatedBy;
                                existingDetail.UpdatedDate = DateTime.UtcNow;
                            }
                        }
                    }
                }
            }
        }

        private async Task UpdateDatabaseBackupDataWithTracking(Guid pmReportFormServerID, UpdatePMServerDatabaseBackupDataDto updateData, Guid? updatedBy)
        {
            // Get existing Database Backup record
            var existingRecord = await _context.PMServerDatabaseBackups
                .Include(x => x.PMServerMSSQLDatabaseBackupDetails)
                .Include(x => x.PMServerSCADADataBackupDetails)
                .FirstOrDefaultAsync(x => x.PMReportFormServerID == pmReportFormServerID && !x.IsDeleted);

            if (existingRecord == null)
            {
                // Create new record only if updatedBy has a valid value
                if (updatedBy.HasValue)
                {
                    var newRecord = new PMServerDatabaseBackup
                    {
                        ID = Guid.NewGuid(),
                        PMReportFormServerID = pmReportFormServerID,
                        Remarks = updateData.Remarks,
                        LatestBackupFileName = updateData.LatestBackupFileName,
                        IsDeleted = false,
                        CreatedBy = updatedBy.Value,
                        CreatedDate = DateTime.UtcNow
                    };
                    _context.PMServerDatabaseBackups.Add(newRecord);

                    // Add new MSSQL details
                    if (updateData.MSSQLDetails != null)
                    {
                        foreach (var detail in updateData.MSSQLDetails.Where(d => !d.IsDeleted))
                        {
                            var newDetail = new PMServerMSSQLDatabaseBackupDetails
                            {
                                ID = Guid.NewGuid(),
                                PMServerDatabaseBackupID = newRecord.ID,
                                SerialNo = detail.SerialNo,
                                ServerName = detail.ServerName,
                                YesNoStatusID = detail.YesNoStatusID,
                                Remarks = detail.Remarks,
                                IsDeleted = false,
                                CreatedBy = updatedBy.Value,
                                CreatedDate = DateTime.UtcNow
                            };
                            _context.PMServerMSSQLDatabaseBackupDetails.Add(newDetail);
                        }
                    }

                    // Add new SCADA details
                    if (updateData.SCADADetails != null)
                    {
                        foreach (var detail in updateData.SCADADetails.Where(d => !d.IsDeleted))
                        {
                            var newDetail = new PMServerSCADADataBackupDetails
                            {
                                ID = Guid.NewGuid(),
                                PMServerDatabaseBackupID = newRecord.ID,
                                SerialNo = detail.SerialNo,
                                ServerName = detail.ServerName,
                                YesNoStatusID = detail.YesNoStatusID,
                                Remarks = detail.Remarks,
                                IsDeleted = false,
                                CreatedBy = updatedBy.Value,
                                CreatedDate = DateTime.UtcNow
                            };
                            _context.PMServerSCADADataBackupDetails.Add(newDetail);
                        }
                    }
                }
            }
            else
            {
                // Update existing record
                existingRecord.Remarks = updateData.Remarks;
                existingRecord.LatestBackupFileName = updateData.LatestBackupFileName;
                existingRecord.UpdatedBy = updatedBy;
                existingRecord.UpdatedDate = DateTime.UtcNow;

                // Handle MSSQL details with tracking
                if (updateData.MSSQLDetails != null)
                {
                    foreach (var detail in updateData.MSSQLDetails)
                    {
                        if (detail.IsDeleted && detail.ID.HasValue)
                        {
                            // Soft delete existing detail
                            var existingDetail = existingRecord.PMServerMSSQLDatabaseBackupDetails
                                .FirstOrDefault(d => d.ID == detail.ID.Value);
                            if (existingDetail != null)
                            {
                                existingDetail.IsDeleted = true;
                                existingDetail.UpdatedBy = updatedBy;
                                existingDetail.UpdatedDate = DateTime.UtcNow;
                            }
                        }
                        else if (detail.IsNew || !detail.ID.HasValue)
                        {
                            // Create new detail only if updatedBy has a valid value
                            if (updatedBy.HasValue)
                            {
                                var newDetail = new PMServerMSSQLDatabaseBackupDetails
                                {
                                    ID = Guid.NewGuid(),
                                    PMServerDatabaseBackupID = existingRecord.ID,
                                    SerialNo = detail.SerialNo,
                                    ServerName = detail.ServerName,
                                    YesNoStatusID = detail.YesNoStatusID,
                                    Remarks = detail.Remarks,
                                    IsDeleted = false,
                                    CreatedBy = updatedBy.Value,
                                    CreatedDate = DateTime.UtcNow
                                };
                                _context.PMServerMSSQLDatabaseBackupDetails.Add(newDetail);
                            }
                        }
                        else if (detail.ID.HasValue)
                        {
                            // Update existing detail
                            var existingDetail = existingRecord.PMServerMSSQLDatabaseBackupDetails
                                .FirstOrDefault(d => d.ID == detail.ID.Value);
                            if (existingDetail != null)
                            {
                                existingDetail.SerialNo = detail.SerialNo;
                                existingDetail.ServerName = detail.ServerName;
                                existingDetail.YesNoStatusID = detail.YesNoStatusID;
                                existingDetail.Remarks = detail.Remarks;
                                existingDetail.UpdatedBy = updatedBy;
                                existingDetail.UpdatedDate = DateTime.UtcNow;
                            }
                        }
                    }
                }

                // Handle SCADA details with tracking
                if (updateData.SCADADetails != null)
                {
                    foreach (var detail in updateData.SCADADetails)
                    {
                        if (detail.IsDeleted && detail.ID.HasValue)
                        {
                            // Soft delete existing detail
                            var existingDetail = existingRecord.PMServerSCADADataBackupDetails
                                .FirstOrDefault(d => d.ID == detail.ID.Value);
                            if (existingDetail != null)
                            {
                                existingDetail.IsDeleted = true;
                                existingDetail.UpdatedBy = updatedBy;
                                existingDetail.UpdatedDate = DateTime.UtcNow;
                            }
                        }
                        else if (detail.IsNew || !detail.ID.HasValue)
                        {
                            // Create new detail only if updatedBy has a valid value
                            if (updatedBy.HasValue)
                            {
                                var newDetail = new PMServerSCADADataBackupDetails
                                {
                                    ID = Guid.NewGuid(),
                                    PMServerDatabaseBackupID = existingRecord.ID,
                                    SerialNo = detail.SerialNo,
                                    ServerName = detail.ServerName,
                                    YesNoStatusID = detail.YesNoStatusID,
                                    Remarks = detail.Remarks,
                                    IsDeleted = false,
                                    CreatedBy = updatedBy.Value,
                                    CreatedDate = DateTime.UtcNow
                                };
                                _context.PMServerSCADADataBackupDetails.Add(newDetail);
                            }
                        }
                        else if (detail.ID.HasValue)
                        {
                            // Update existing detail
                            var existingDetail = existingRecord.PMServerSCADADataBackupDetails
                                .FirstOrDefault(d => d.ID == detail.ID.Value);
                            if (existingDetail != null)
                            {
                                existingDetail.SerialNo = detail.SerialNo;
                                existingDetail.ServerName = detail.ServerName;
                                existingDetail.YesNoStatusID = detail.YesNoStatusID;
                                existingDetail.Remarks = detail.Remarks;
                                existingDetail.UpdatedBy = updatedBy;
                                existingDetail.UpdatedDate = DateTime.UtcNow;
                            }
                        }
                    }
                }
            }
        }

        private async Task UpdateTimeSyncDataWithTracking(Guid pmReportFormServerID, UpdatePMServerTimeSyncDataDto updateData, Guid? updatedBy)
        {
            // Get existing Time Sync record
            var existingRecord = await _context.PMServerTimeSyncs
                .Include(x => x.PMServerTimeSyncDetails)
                .FirstOrDefaultAsync(x => x.PMReportFormServerID == pmReportFormServerID && !x.IsDeleted);

            if (existingRecord == null)
            {
                // Create new record only if updatedBy has a valid value
                if (updatedBy.HasValue)
                {
                    var newRecord = new PMServerTimeSync
                    {
                        ID = Guid.NewGuid(),
                        PMReportFormServerID = pmReportFormServerID,
                        Remarks = updateData.Remarks,
                        IsDeleted = false,
                        CreatedBy = updatedBy.Value,
                        CreatedDate = DateTime.UtcNow
                    };
                    _context.PMServerTimeSyncs.Add(newRecord);

                    // Add new details
                    if (updateData.Details != null)
                    {
                        foreach (var detail in updateData.Details.Where(d => !d.IsDeleted))
                        {
                            var newDetail = new PMServerTimeSyncDetails
                                {
                                    ID = Guid.NewGuid(),
                                    PMServerTimeSyncID = newRecord.ID,
                                    SerialNo = detail.SerialNo,
                                    ServerName = detail.ServerName,
                                    ResultStatusID = detail.ResultStatusID,
                                    Remarks = detail.Remarks,
                                    IsDeleted = false,
                                    CreatedBy = updatedBy.Value,
                                    CreatedDate = DateTime.UtcNow
                                };
                            _context.PMServerTimeSyncDetails.Add(newDetail);
                        }
                    }
                }
            }
            else
            {
                // Update existing record
                existingRecord.Remarks = updateData.Remarks;
                existingRecord.UpdatedBy = updatedBy;
                existingRecord.UpdatedDate = DateTime.UtcNow;

                // Handle details with tracking
                if (updateData.Details != null)
                {
                    foreach (var detail in updateData.Details)
                    {
                        if (detail.IsDeleted && detail.ID.HasValue)
                        {
                            // Soft delete existing detail
                            var existingDetail = existingRecord.PMServerTimeSyncDetails
                                .FirstOrDefault(d => d.ID == detail.ID.Value);
                            if (existingDetail != null)
                            {
                                existingDetail.IsDeleted = true;
                                existingDetail.UpdatedBy = updatedBy;
                                existingDetail.UpdatedDate = DateTime.UtcNow;
                            }
                        }
                        else if (detail.IsNew || !detail.ID.HasValue)
                        {
                            // Create new detail only if updatedBy has a valid value
                            if (updatedBy.HasValue)
                            {
                                var newDetail = new PMServerTimeSyncDetails
                                {
                                    ID = Guid.NewGuid(),
                                    PMServerTimeSyncID = existingRecord.ID,
                                    SerialNo = detail.SerialNo,
                                    ServerName = detail.ServerName,
                                    ResultStatusID = detail.ResultStatusID,
                                    Remarks = detail.Remarks,
                                    IsDeleted = false,
                                    CreatedBy = updatedBy.Value,
                                    CreatedDate = DateTime.UtcNow
                                };
                                _context.PMServerTimeSyncDetails.Add(newDetail);
                            }
                        }
                        else if (detail.ID.HasValue)
                        {
                            // Update existing detail
                            var existingDetail = existingRecord.PMServerTimeSyncDetails
                                .FirstOrDefault(d => d.ID == detail.ID.Value);
                            if (existingDetail != null)
                            {
                                existingDetail.SerialNo = detail.SerialNo;
                                existingDetail.ServerName = detail.ServerName;
                                existingDetail.ResultStatusID = detail.ResultStatusID;
                                existingDetail.Remarks = detail.Remarks;
                                existingDetail.UpdatedBy = updatedBy;
                                existingDetail.UpdatedDate = DateTime.UtcNow;
                            }
                        }
                    }
                }
            }
        }

        private async Task UpdateHotFixesDataWithTracking(Guid pmReportFormServerID, UpdatePMServerHotFixesDataDto updateData, Guid? updatedBy)
        {
            // Get existing Hot Fixes record
            var existingRecord = await _context.PMServerHotFixes
                .Include(x => x.PMServerHotFixesDetails)
                .FirstOrDefaultAsync(x => x.PMReportFormServerID == pmReportFormServerID && !x.IsDeleted);

            if (existingRecord == null)
            {
                // Create new record only if updatedBy has a valid value
                if (updatedBy.HasValue)
                {
                    var newRecord = new PMServerHotFixes
                    {
                        ID = Guid.NewGuid(),
                        PMReportFormServerID = pmReportFormServerID,
                        Remarks = updateData.Remarks,
                        IsDeleted = false,
                        CreatedBy = updatedBy.Value,
                        CreatedDate = DateTime.UtcNow
                    };
                    _context.PMServerHotFixes.Add(newRecord);

                    // Add new details
                    if (updateData.Details != null)
                    {
                        foreach (var detail in updateData.Details.Where(d => !d.IsDeleted))
                        {
                            var newDetail = new PMServerHotFixesDetails
                            {
                                ID = Guid.NewGuid(),
                                PMServerHotFixesID = newRecord.ID,
                                SerialNo = detail.SerialNo,
                                ServerName = detail.ServerName,
                                LatestHotFixsApplied = detail.LatestHotFixsApplied,
                                ResultStatusID = detail.ResultStatusID,
                                Remarks = detail.Remarks,
                                IsDeleted = false,
                                CreatedBy = updatedBy.Value,
                                CreatedDate = DateTime.UtcNow
                            };
                            _context.PMServerHotFixesDetails.Add(newDetail);
                        }
                    }
                }
            }
            else
            {
                // Update existing record
                existingRecord.Remarks = updateData.Remarks;
                existingRecord.UpdatedBy = updatedBy;
                existingRecord.UpdatedDate = DateTime.UtcNow;

                // Handle details with tracking
                if (updateData.Details != null)
                {
                    foreach (var detail in updateData.Details)
                    {
                        if (detail.IsDeleted && detail.ID.HasValue)
                        {
                            // Soft delete existing detail
                            var existingDetail = existingRecord.PMServerHotFixesDetails
                                .FirstOrDefault(d => d.ID == detail.ID.Value);
                            if (existingDetail != null)
                            {
                                existingDetail.IsDeleted = true;
                                existingDetail.UpdatedBy = updatedBy;
                                existingDetail.UpdatedDate = DateTime.UtcNow;
                            }
                        }
                        else if (detail.IsNew || !detail.ID.HasValue)
                        {
                            // Create new detail only if updatedBy has a valid value
                            if (updatedBy.HasValue)
                            {
                                var newDetail = new PMServerHotFixesDetails
                                {
                                    ID = Guid.NewGuid(),
                                    PMServerHotFixesID = existingRecord.ID,
                                    SerialNo = detail.SerialNo,
                                    ServerName = detail.ServerName,
                                    LatestHotFixsApplied = detail.LatestHotFixsApplied,
                                    ResultStatusID = detail.ResultStatusID,
                                    Remarks = detail.Remarks,
                                    IsDeleted = false,
                                    CreatedBy = updatedBy.Value,
                                    CreatedDate = DateTime.UtcNow
                                };
                                _context.PMServerHotFixesDetails.Add(newDetail);
                            }
                        }
                        else if (detail.ID.HasValue)
                        {
                            // Update existing detail
                            var existingDetail = existingRecord.PMServerHotFixesDetails
                                .FirstOrDefault(d => d.ID == detail.ID.Value);
                            if (existingDetail != null)
                            {
                                existingDetail.SerialNo = detail.SerialNo;
                                existingDetail.ServerName = detail.ServerName;
                                existingDetail.LatestHotFixsApplied = detail.LatestHotFixsApplied;
                                existingDetail.ResultStatusID = detail.ResultStatusID;
                                existingDetail.Remarks = detail.Remarks;
                                existingDetail.UpdatedBy = updatedBy;
                                existingDetail.UpdatedDate = DateTime.UtcNow;
                            }
                        }
                    }
                }
            }
        }

        private async Task UpdateAutoFailOverDataWithTracking(Guid pmReportFormServerID, UpdatePMServerFailOverDataDto updateData, Guid? updatedBy)
        {
            // Get existing Fail Over record
            var existingRecord = await _context.PMServerFailOvers
                .Include(x => x.PMServerFailOverDetails)
                .FirstOrDefaultAsync(x => x.PMReportFormServerID == pmReportFormServerID && !x.IsDeleted);

            if (existingRecord == null)
            {
                // Create new record only if updatedBy has a valid value
                if (updatedBy.HasValue)
                {
                    var newRecord = new PMServerFailOver
                    {
                        ID = Guid.NewGuid(),
                        PMReportFormServerID = pmReportFormServerID,
                        Remarks = updateData.Remarks,
                        IsDeleted = false,
                        CreatedBy = updatedBy.Value,
                        CreatedDate = DateTime.UtcNow
                    };
                    _context.PMServerFailOvers.Add(newRecord);

                    // Add new details
                    if (updateData.Details != null)
                    {
                        foreach (var detail in updateData.Details.Where(d => !d.IsDeleted))
                        {
                            var newDetail = new PMServerFailOverDetails
                            {
                                ID = Guid.NewGuid(),
                                PMServerFailOverID = newRecord.ID,
                                YesNoStatusID = detail.YesNoStatusID,
                                ToServer = detail.ToServer,
                                FromServer = detail.FromServer,
                                Remarks = detail.Remarks,
                                IsDeleted = false,
                                CreatedBy = updatedBy.Value,
                                CreatedDate = DateTime.UtcNow
                            };
                            _context.PMServerFailOverDetails.Add(newDetail);
                        }
                    }
                }
            }
            else
            {
                // Update existing record
                existingRecord.Remarks = updateData.Remarks;
                existingRecord.UpdatedBy = updatedBy;
                existingRecord.UpdatedDate = DateTime.UtcNow;

                // Handle details with tracking
                if (updateData.Details != null)
                {
                    foreach (var detail in updateData.Details)
                    {
                        if (detail.IsDeleted && detail.ID.HasValue)
                        {
                            // Soft delete existing detail
                            var existingDetail = existingRecord.PMServerFailOverDetails
                                .FirstOrDefault(d => d.ID == detail.ID.Value);
                            if (existingDetail != null)
                            {
                                existingDetail.IsDeleted = true;
                                existingDetail.UpdatedBy = updatedBy;
                                existingDetail.UpdatedDate = DateTime.UtcNow;
                            }
                        }
                        else if (detail.IsNew || !detail.ID.HasValue)
                        {
                            // Create new detail only if updatedBy has a valid value
                            if (updatedBy.HasValue)
                            {
                                var newDetail = new PMServerFailOverDetails
                                {
                                    ID = Guid.NewGuid(),
                                    PMServerFailOverID = existingRecord.ID,
                                    YesNoStatusID = detail.YesNoStatusID,
                                    ToServer = detail.ToServer,
                                    FromServer = detail.FromServer,
                                    Remarks = detail.Remarks,
                                    IsDeleted = false,
                                    CreatedBy = updatedBy.Value,
                                    CreatedDate = DateTime.UtcNow
                                };
                                _context.PMServerFailOverDetails.Add(newDetail);
                            }
                        }
                        else if (detail.ID.HasValue)
                        {
                            // Update existing detail
                            var existingDetail = existingRecord.PMServerFailOverDetails
                                .FirstOrDefault(d => d.ID == detail.ID.Value);
                            if (existingDetail != null)
                            {
                                existingDetail.YesNoStatusID = detail.YesNoStatusID;
                                existingDetail.ToServer = detail.ToServer;
                                existingDetail.FromServer = detail.FromServer;
                                existingDetail.Remarks = detail.Remarks;
                                existingDetail.UpdatedBy = updatedBy;
                                existingDetail.UpdatedDate = DateTime.UtcNow;
                            }
                        }
                    }
                }
            }
        }

        private async Task UpdateAsaFirewallDataWithTracking(Guid pmReportFormServerID, UpdatePMServerASAFirewallDataDto updateData, Guid? updatedBy)
        {
            // Handle ASA Firewall details with tracking (Note: ASA Firewall doesn't have a parent record, only details)
            if (updateData.Details != null)
            {
                // Get existing ASA Firewall records
                var existingRecords = await _context.PMServerASAFirewalls
                    .Where(x => x.PMReportFormServerID == pmReportFormServerID && !x.IsDeleted)
                    .ToListAsync();

                foreach (var detail in updateData.Details)
                {
                    if (detail.IsDeleted && detail.ID.HasValue)
                    {
                        // Soft delete existing record
                        var existingRecord = existingRecords.FirstOrDefault(r => r.ID == detail.ID.Value);
                        if (existingRecord != null)
                        {
                            existingRecord.IsDeleted = true;
                            existingRecord.UpdatedBy = updatedBy;
                            existingRecord.UpdatedDate = DateTime.UtcNow;
                        }
                    }
                    else if (detail.IsNew || !detail.ID.HasValue)
                    {
                        // Create new record only if updatedBy has a valid value
                        if (updatedBy.HasValue)
                        {
                            var newRecord = new PMServerASAFirewall
                            {
                                ID = Guid.NewGuid(),
                                PMReportFormServerID = pmReportFormServerID,
                                SerialNumber = detail.SerialNumber,
                                CommandInput = detail.CommandInput,
                                ASAFirewallStatusID = detail.ASAFirewallStatusID,
                                ResultStatusID = detail.ResultStatusID,
                                Remarks = updateData.Remarks, // Use parent remarks since detail doesn't have remarks
                                IsDeleted = false,
                                CreatedBy = updatedBy.Value,
                                CreatedDate = DateTime.UtcNow
                            };
                            _context.PMServerASAFirewalls.Add(newRecord);
                        }
                    }
                    else if (detail.ID.HasValue)
                    {
                        // Update existing record
                        var existingRecord = existingRecords.FirstOrDefault(r => r.ID == detail.ID.Value);
                        if (existingRecord != null)
                        {
                            existingRecord.SerialNumber = detail.SerialNumber;
                            existingRecord.CommandInput = detail.CommandInput;
                            existingRecord.ASAFirewallStatusID = detail.ASAFirewallStatusID;
                            existingRecord.ResultStatusID = detail.ResultStatusID;
                            existingRecord.Remarks = updateData.Remarks; // Use parent remarks since detail doesn't have remarks
                            existingRecord.UpdatedBy = updatedBy;
                            existingRecord.UpdatedDate = DateTime.UtcNow;
                        }
                    }
                }
            }
        }

        private async Task UpdateSoftwarePatchDataWithTracking(Guid pmReportFormServerID, UpdatePMServerSoftwarePatchDataDto updateData, Guid? updatedBy)
        {
            // Get existing Software Patch Summary record
            var existingRecord = await _context.PMServerSoftwarePatchSummaries
                .Include(x => x.PMServerSoftwarePatchDetails)
                .FirstOrDefaultAsync(x => x.PMReportFormServerID == pmReportFormServerID && !x.IsDeleted);

            if (existingRecord == null)
            {
                // Create new record only if updatedBy has a valid value
                if (updatedBy.HasValue)
                {
                    var newRecord = new PMServerSoftwarePatchSummary
                    {
                        ID = Guid.NewGuid(),
                        PMReportFormServerID = pmReportFormServerID,
                        Remarks = updateData.Remarks,
                        IsDeleted = false,
                        CreatedBy = updatedBy.Value,
                        CreatedDate = DateTime.UtcNow
                    };
                    _context.PMServerSoftwarePatchSummaries.Add(newRecord);

                    // Add new details
                    if (updateData.Details != null)
                    {
                        foreach (var detail in updateData.Details.Where(d => !d.IsDeleted))
                        {
                            var newDetail = new PMServerSoftwarePatchDetails
                            {
                                ID = Guid.NewGuid(),
                                PMServerSoftwarePatchSummaryID = newRecord.ID,
                                SerialNo = detail.SerialNo,
                                ServerName = detail.ServerName,
                                PreviousPatch = detail.PreviousPatch,
                                CurrentPatch = detail.CurrentPatch,
                                Remarks = detail.Remarks,
                                IsDeleted = false,
                                CreatedBy = updatedBy.Value,
                                CreatedDate = DateTime.UtcNow
                            };
                            _context.PMServerSoftwarePatchDetails.Add(newDetail);
                        }
                    }
                }
            }
            else
            {
                // Update existing record
                existingRecord.Remarks = updateData.Remarks;
                existingRecord.UpdatedBy = updatedBy;
                existingRecord.UpdatedDate = DateTime.UtcNow;

                // Handle details with tracking
                if (updateData.Details != null)
                {
                    foreach (var detail in updateData.Details)
                    {
                        if (detail.IsDeleted && detail.ID.HasValue)
                        {
                            // Soft delete existing detail
                            var existingDetail = existingRecord.PMServerSoftwarePatchDetails
                                .FirstOrDefault(d => d.ID == detail.ID.Value);
                            if (existingDetail != null)
                            {
                                existingDetail.IsDeleted = true;
                                existingDetail.UpdatedBy = updatedBy;
                                existingDetail.UpdatedDate = DateTime.UtcNow;
                            }
                        }
                        else if (detail.IsNew || !detail.ID.HasValue)
                        {
                            // Create new detail only if updatedBy has a valid value
                            if (updatedBy.HasValue)
                            {
                                var newDetail = new PMServerSoftwarePatchDetails
                                {
                                    ID = Guid.NewGuid(),
                                    PMServerSoftwarePatchSummaryID = existingRecord.ID,
                                    SerialNo = detail.SerialNo,
                                    ServerName = detail.ServerName,
                                    PreviousPatch = detail.PreviousPatch,
                                    CurrentPatch = detail.CurrentPatch,
                                    Remarks = detail.Remarks,
                                    IsDeleted = false,
                                    CreatedBy = updatedBy.Value,
                                    CreatedDate = DateTime.UtcNow
                                };
                                _context.PMServerSoftwarePatchDetails.Add(newDetail);
                            }
                        }
                        else if (detail.ID.HasValue)
                        {
                            // Update existing detail
                            var existingDetail = existingRecord.PMServerSoftwarePatchDetails
                                .FirstOrDefault(d => d.ID == detail.ID.Value);
                            if (existingDetail != null)
                            {
                                existingDetail.SerialNo = detail.SerialNo;
                                existingDetail.ServerName = detail.ServerName;
                                existingDetail.PreviousPatch = detail.PreviousPatch;
                                existingDetail.CurrentPatch = detail.CurrentPatch;
                                existingDetail.Remarks = detail.Remarks;
                                existingDetail.UpdatedBy = updatedBy;
                                existingDetail.UpdatedDate = DateTime.UtcNow;
                            }
                        }
                    }
                }
            }
        }

        private async Task UpdateHardDriveHealthDataWithTracking(Guid pmReportFormServerID, UpdatePMServerHardDriveHealthDataDto updateData, Guid? updatedBy)
        {
            // Validate that updatedBy user exists if provided
            if (updatedBy.HasValue)
            {
                var userExists = await _context.Users.AnyAsync(u => u.ID == updatedBy.Value && !u.IsDeleted);
                if (!userExists)
                {
                    throw new InvalidOperationException($"Invalid UpdatedBy user ID: {updatedBy.Value}");
                }
            }

            // Get existing Hard Drive Health record
            var existingRecord = await _context.PMServerHardDriveHealths
                .Include(x => x.PMServerHardDriveHealthDetails)
                .FirstOrDefaultAsync(x => x.PMReportFormServerID == pmReportFormServerID && !x.IsDeleted);

            if (existingRecord == null)
            {
                // Only create if we have a valid user ID
                if (!updatedBy.HasValue)
                {
                    throw new InvalidOperationException("Cannot create PMServerHardDriveHealth record without a valid UpdatedBy user ID");
                }

                var newRecord = new PMServerHardDriveHealth
                {
                    ID = Guid.NewGuid(),
                    PMReportFormServerID = pmReportFormServerID,
                    Remarks = updateData.Remarks,
                    IsDeleted = false,
                    CreatedBy = updatedBy.Value,
                    CreatedDate = DateTime.UtcNow
                };
                _context.PMServerHardDriveHealths.Add(newRecord);

                // Add new details
                if (updateData.Details != null)
                {
                    foreach (var detail in updateData.Details.Where(d => !d.IsDeleted && d.ResultStatusID != Guid.Empty))
                    {
                        var newDetail = new PMServerHardDriveHealthDetails
                        {
                            ID = Guid.NewGuid(),
                            PMServerHardDriveHealthID = newRecord.ID,
                            ServerName = detail.ServerName,
                            ResultStatusID = detail.ResultStatusID,
                            Remarks = detail.Remarks,
                            IsDeleted = false,
                            CreatedBy = updatedBy.Value,
                            CreatedDate = DateTime.UtcNow
                        };
                        _context.PMServerHardDriveHealthDetails.Add(newDetail);
                    }
                }
            }
            else
            {
                // Update existing record
                existingRecord.Remarks = updateData.Remarks;
                existingRecord.UpdatedBy = updatedBy;
                existingRecord.UpdatedDate = DateTime.UtcNow;

                // Handle details with tracking
                if (updateData.Details != null)
                {
                    foreach (var detail in updateData.Details)
                    {
                        if (detail.IsDeleted && detail.ID.HasValue)
                        {
                            // Soft delete existing detail
                            var existingDetail = existingRecord.PMServerHardDriveHealthDetails
                                .FirstOrDefault(d => d.ID == detail.ID.Value);
                            if (existingDetail != null)
                            {
                                existingDetail.IsDeleted = true;
                                existingDetail.UpdatedBy = updatedBy;
                                existingDetail.UpdatedDate = DateTime.UtcNow;
                            }
                        }
                        else if (detail.IsNew || !detail.ID.HasValue)
                        {
                            // Create new detail only if updatedBy has a valid value and ResultStatusID is valid
                            if (updatedBy.HasValue && detail.ResultStatusID != Guid.Empty)
                            {
                                var newDetail = new PMServerHardDriveHealthDetails
                                {
                                    ID = Guid.NewGuid(),
                                    PMServerHardDriveHealthID = existingRecord.ID,
                                    ServerName = detail.ServerName,
                                    ResultStatusID = detail.ResultStatusID,
                                    Remarks = detail.Remarks,
                                    IsDeleted = false,
                                    CreatedBy = updatedBy.Value,
                                    CreatedDate = DateTime.UtcNow
                                };
                                _context.PMServerHardDriveHealthDetails.Add(newDetail);
                            }
                        }
                        else if (detail.ID.HasValue)
                        {
                            // Update existing detail only if ResultStatusID is valid
                            var existingDetail = existingRecord.PMServerHardDriveHealthDetails
                                .FirstOrDefault(d => d.ID == detail.ID.Value);
                            if (existingDetail != null && detail.ResultStatusID != Guid.Empty)
                            {
                                existingDetail.ServerName = detail.ServerName;
                                existingDetail.ResultStatusID = detail.ResultStatusID;
                                existingDetail.Remarks = detail.Remarks;
                                existingDetail.UpdatedBy = updatedBy;
                                existingDetail.UpdatedDate = DateTime.UtcNow;
                            }
                        }
                    }
                }
            }
        }

        private async Task UpdateNetworkHealthDataWithTracking(Guid pmReportFormServerID, PMServerNetworkHealthDataDto updateData, Guid? updatedBy)
        {
            // Get existing NetworkHealth record
            var existingNetworkHealth = await _context.PMServerNetworkHealths
                .Where(n => n.PMReportFormServerID == pmReportFormServerID && !n.IsDeleted)
                .FirstOrDefaultAsync();

            if (existingNetworkHealth == null)
            {
                // Create new record if it doesn't exist
                if (updatedBy.HasValue)
                {
                    var newNetworkHealth = new PMServerNetworkHealth
                    {
                        ID = Guid.NewGuid(),
                        PMReportFormServerID = pmReportFormServerID,
                        YesNoStatusID = updateData.YesNoStatusID,
                        DateChecked = updateData.DateChecked,
                        Remarks = updateData.Remarks,
                        IsDeleted = false,
                        CreatedBy = updatedBy.Value,
                        UpdatedBy = updatedBy.Value,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    };
                    _context.PMServerNetworkHealths.Add(newNetworkHealth);
                }
            }
            else
            {
                // Update existing record
                existingNetworkHealth.YesNoStatusID = updateData.YesNoStatusID;
                existingNetworkHealth.DateChecked = updateData.DateChecked;
                existingNetworkHealth.Remarks = updateData.Remarks;
                existingNetworkHealth.UpdatedBy = updatedBy.Value;
                existingNetworkHealth.UpdatedDate = DateTime.UtcNow;
            }
        }

        private async Task UpdateWillowlynxProcessStatusDataWithTracking(Guid pmReportFormServerID, PMServerWillowlynxProcessStatusDataDto updateData, Guid? updatedBy)
        {
            // Get existing Willowlynx Process Status record
            var existingProcessStatus = await _context.PMServerWillowlynxProcessStatuses
                .Where(w => w.PMReportFormServerID == pmReportFormServerID && !w.IsDeleted)
                .FirstOrDefaultAsync();

            if (existingProcessStatus == null)
            {
                // Create new record if it doesn't exist
                if (updatedBy.HasValue && updateData.YesNoStatusID != Guid.Empty)
                {
                    var newProcessStatus = new PMServerWillowlynxProcessStatus
                    {
                        ID = Guid.NewGuid(),
                        PMReportFormServerID = pmReportFormServerID,
                        YesNoStatusID = updateData.YesNoStatusID,
                        Remarks = updateData.Remarks,
                        IsDeleted = false,
                        CreatedBy = updatedBy.Value,
                        UpdatedBy = updatedBy.Value,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    };
                    _context.PMServerWillowlynxProcessStatuses.Add(newProcessStatus);
                }
            }
            else
            {
                // Update existing record
                if (updateData.YesNoStatusID != Guid.Empty)
                {
                    existingProcessStatus.YesNoStatusID = updateData.YesNoStatusID;
                }
                existingProcessStatus.Remarks = updateData.Remarks;
                existingProcessStatus.UpdatedBy = updatedBy.Value;
                existingProcessStatus.UpdatedDate = DateTime.UtcNow;
            }
        }

        private async Task UpdateWillowlynxNetworkStatusDataWithTracking(Guid pmReportFormServerID, PMServerWillowlynxNetworkStatusDataDto updateData, Guid? updatedBy)
        {
            // Get existing Willowlynx Network Status record
            var existingNetworkStatus = await _context.PMServerWillowlynxNetworkStatuses
                .Where(w => w.PMReportFormServerID == pmReportFormServerID && !w.IsDeleted)
                .FirstOrDefaultAsync();

            if (existingNetworkStatus == null)
            {
                // Create new record if it doesn't exist
                if (updatedBy.HasValue && updateData.YesNoStatusID != Guid.Empty)
                {
                    var newNetworkStatus = new PMServerWillowlynxNetworkStatus
                    {
                        ID = Guid.NewGuid(),
                        PMReportFormServerID = pmReportFormServerID,
                        YesNoStatusID = updateData.YesNoStatusID,
                        Remarks = updateData.Remarks,
                        IsDeleted = false,
                        CreatedBy = updatedBy.Value,
                        UpdatedBy = updatedBy.Value,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    };
                    _context.PMServerWillowlynxNetworkStatuses.Add(newNetworkStatus);
                }
            }
            else
            {
                // Update existing record
                if (updateData.YesNoStatusID != Guid.Empty)
                {
                    existingNetworkStatus.YesNoStatusID = updateData.YesNoStatusID;
                }
                existingNetworkStatus.Remarks = updateData.Remarks;
                existingNetworkStatus.UpdatedBy = updatedBy.Value;
                existingNetworkStatus.UpdatedDate = DateTime.UtcNow;
            }
             
        }

        private async Task UpdateWillowlynxRTUStatusDataWithTracking(Guid pmReportFormServerID, PMServerWillowlynxRTUStatusDataDto updateData, Guid? updatedBy)
        {
            // Get existing Willowlynx RTU Status record
            var existingRTUStatus = await _context.PMServerWillowlynxRTUStatuses
                .Where(w => w.PMReportFormServerID == pmReportFormServerID && !w.IsDeleted)
                .FirstOrDefaultAsync();

            if (existingRTUStatus == null)
            {
                // Create new record if it doesn't exist
                if (updatedBy.HasValue && updateData.YesNoStatusID != Guid.Empty)
                {
                    var newRTUStatus = new PMServerWillowlynxRTUStatus
                    {
                        ID = Guid.NewGuid(),
                        PMReportFormServerID = pmReportFormServerID,
                        YesNoStatusID = updateData.YesNoStatusID,
                        Remarks = updateData.Remarks,
                        IsDeleted = false,
                        CreatedBy = updatedBy.Value,
                        UpdatedBy = updatedBy.Value,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    };
                    _context.PMServerWillowlynxRTUStatuses.Add(newRTUStatus);
                }
            }
            else
            {
                // Update existing record
                if (updateData.YesNoStatusID != Guid.Empty)
                {
                    existingRTUStatus.YesNoStatusID = updateData.YesNoStatusID;
                }
                existingRTUStatus.Remarks = updateData.Remarks;
                existingRTUStatus.UpdatedBy = updatedBy.Value;
                existingRTUStatus.UpdatedDate = DateTime.UtcNow;
            }
        }

        private async Task UpdateWillowlynxHistoricalTrendDataWithTracking(Guid pmReportFormServerID, PMServerWillowlynxHistoricalTrendDataDto updateData, Guid? updatedBy)
        {
            // Get existing Willowlynx Historical Trend record
            var existingHistoricalTrend = await _context.PMServerWillowlynxHistoricalTrends
                .Where(w => w.PMReportFormServerID == pmReportFormServerID && !w.IsDeleted)
                .FirstOrDefaultAsync();

            if (existingHistoricalTrend == null)
            {
                // Create new record if it doesn't exist
                if (updatedBy.HasValue && updateData.YesNoStatusID != Guid.Empty)
                {
                    var newHistoricalTrend = new PMServerWillowlynxHistoricalTrend
                    {
                        ID = Guid.NewGuid(),
                        PMReportFormServerID = pmReportFormServerID,
                        YesNoStatusID = updateData.YesNoStatusID,
                        Remarks = updateData.Remarks,
                        IsDeleted = false,
                        CreatedBy = updatedBy.Value,
                        UpdatedBy = updatedBy.Value,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    };
                    _context.PMServerWillowlynxHistoricalTrends.Add(newHistoricalTrend);
                }
            }
            else
            {
                // Update existing record
                if (updateData.YesNoStatusID != Guid.Empty)
                {
                    existingHistoricalTrend.YesNoStatusID = updateData.YesNoStatusID;
                }
                existingHistoricalTrend.Remarks = updateData.Remarks;
                existingHistoricalTrend.UpdatedBy = updatedBy.Value;
                existingHistoricalTrend.UpdatedDate = DateTime.UtcNow;
            }
        }

        private async Task UpdateWillowlynxHistoricalReportDataWithTracking(Guid pmReportFormServerID, PMServerWillowlynxHistoricalReportDataDto updateData, Guid? updatedBy)
        {
            // Get existing Willowlynx Historical Report record
            var existingHistoricalReport = await _context.PMServerWillowlynxHistoricalReports
                .Where(w => w.PMReportFormServerID == pmReportFormServerID && !w.IsDeleted)
                .FirstOrDefaultAsync();

            if (existingHistoricalReport == null)
            {
                // Create new record if it doesn't exist
                if (updatedBy.HasValue && updateData.YesNoStatusID != Guid.Empty)
                {
                    var newHistoricalReport = new PMServerWillowlynxHistoricalReport
                    {
                        ID = Guid.NewGuid(),
                        PMReportFormServerID = pmReportFormServerID,
                        YesNoStatusID = updateData.YesNoStatusID,
                        Remarks = updateData.Remarks,
                        IsDeleted = false,
                        CreatedBy = updatedBy.Value,
                        UpdatedBy = updatedBy.Value,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    };
                    _context.PMServerWillowlynxHistoricalReports.Add(newHistoricalReport);
                }
            }
            else
            {
                // Update existing record
                if (updateData.YesNoStatusID != Guid.Empty)
                {
                    existingHistoricalReport.YesNoStatusID = updateData.YesNoStatusID;
                }
                existingHistoricalReport.Remarks = updateData.Remarks;
                existingHistoricalReport.UpdatedBy = updatedBy.Value;
                existingHistoricalReport.UpdatedDate = DateTime.UtcNow;
            }
        }

        private async Task UpdateWillowlynxSumpPitCCTVCameraDataWithTracking(Guid pmReportFormServerID, PMServerWillowlynxCCTVCameraDataDto updateData, Guid? updatedBy)
        {
            // Get existing Willowlynx CCTV Camera record
            var existingCCTVCamera = await _context.PMServerWillowlynxCCTVCameras
                .Where(w => w.PMReportFormServerID == pmReportFormServerID && !w.IsDeleted)
                .FirstOrDefaultAsync();

            if (existingCCTVCamera == null)
            {
                // Create new record if it doesn't exist
                if (updatedBy.HasValue && updateData.YesNoStatusID != Guid.Empty)
                {
                    var newCCTVCamera = new PMServerWillowlynxCCTVCamera
                    {
                        ID = Guid.NewGuid(),
                        PMReportFormServerID = pmReportFormServerID,
                        YesNoStatusID = updateData.YesNoStatusID,
                        Remarks = updateData.Remarks,
                        IsDeleted = false,
                        CreatedBy = updatedBy.Value,
                        UpdatedBy = updatedBy.Value,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow
                    };
                    _context.PMServerWillowlynxCCTVCameras.Add(newCCTVCamera);
                }
            }
            else
            {
                // Update existing record
                if (updateData.YesNoStatusID != Guid.Empty)
                {
                    existingCCTVCamera.YesNoStatusID = updateData.YesNoStatusID;
                }
                existingCCTVCamera.Remarks = updateData.Remarks;
                existingCCTVCamera.UpdatedBy = updatedBy.Value;
                existingCCTVCamera.UpdatedDate = DateTime.UtcNow;
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
                        // Only create new records if UpdatedBy has a valid value
                        if (updateDto.UpdatedBy.HasValue)
                        {
                            var healthDetail = new PMServerHealthDetails
                            {
                                ID = Guid.NewGuid(),
                                PMServerHealthID = serverHealth.ID,
                                ServerName = detail.ServerName,
                                ResultStatusID = detail.ResultStatusID,
                                Remarks = detail.Remarks ?? "",
                                CreatedBy = updateDto.UpdatedBy.Value,
                                UpdatedBy = updateDto.UpdatedBy,
                                CreatedDate = DateTime.UtcNow
                            };
                            _context.PMServerHealthDetails.Add(healthDetail);
                        }
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

        [HttpPost("{id}/generate-pdf")]
        public async Task<IActionResult> GenerateServerPmPdf(Guid id, CancellationToken cancellationToken)
        {
            var reportForm = await _context.ReportForms
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ID == id && !r.IsDeleted, cancellationToken);

            if (reportForm == null)
            {
                return NotFound($"Report form with ID {id} was not found.");
            }

            var requestedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "api_user";

            try
            {
                var result = await GeneratePdfAsync(id, requestedBy, cancellationToken);
                var downloadName = result.FileName ?? $"Server_PM_Report_{reportForm.JobNo ?? id.ToString()}.pdf";
                return File(result.FileContent, "application/pdf", downloadName);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(504, "Timed out waiting for PDF generation.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate Server PM report for {ReportId}", id);
                return StatusCode(500, $"Failed to generate PDF: {ex.Message}");
            }
        }

        private async Task<PdfGenerationResult> GeneratePdfAsync(Guid reportId, string requestedBy, CancellationToken cancellationToken)
        {
            var mqttFactory = new MqttFactory();
            using var mqttClient = mqttFactory.CreateMqttClient();
            var mqttOptions = BuildMqttOptions();

            var statusTopic = $"controltower/server_pm_reportform_pdf_status/{reportId}";
            var requestTopic = $"controltower/server_pm_reportform_pdf/{reportId}";
            var completionSource = new TaskCompletionSource<PdfStatusMessage>(TaskCreationOptions.RunContinuationsAsynchronously);

            mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                if (e.ApplicationMessage.Topic == statusTopic)
                {
                    var payload = e.ApplicationMessage.ConvertPayloadToString();
                    try
                    {
                        var status = JsonSerializer.Deserialize<PdfStatusMessage>(payload, _jsonOptions);
                        if (status != null && !string.IsNullOrWhiteSpace(status.Status))
                        {
                            var normalized = status.Status.ToLowerInvariant();
                            if (normalized == "completed" || normalized == "failed")
                            {
                                completionSource.TrySetResult(status);
                            }
                        }
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError(jsonEx, "Failed to parse PDF status payload: {Payload}", payload);
                    }
                }

                return Task.CompletedTask;
            };

            await mqttClient.ConnectAsync(mqttOptions, cancellationToken);
            await mqttClient.SubscribeAsync(statusTopic, MqttQualityOfServiceLevel.AtLeastOnce, cancellationToken);

            var payload = JsonSerializer.Serialize(new
            {
                report_id = reportId.ToString(),
                requested_by = requestedBy,
                timestamp = DateTime.UtcNow.ToString("o")
            });

            var publishMessage = new MqttApplicationMessageBuilder()
                .WithTopic(requestTopic)
                .WithPayload(payload)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .Build();

            await mqttClient.PublishAsync(publishMessage, cancellationToken);

            var timeoutSeconds = _configuration.GetValue("PDFGenerator:StatusTimeoutSeconds", 120);
            using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeoutCts.CancelAfter(TimeSpan.FromSeconds(timeoutSeconds));

            PdfStatusMessage statusMessage;
            try
            {
                using (timeoutCts.Token.Register(() => completionSource.TrySetCanceled(), useSynchronizationContext: false))
                {
                    statusMessage = await completionSource.Task;
                }
            }
            finally
            {
                if (mqttClient.IsConnected)
                {
                    await mqttClient.UnsubscribeAsync(statusTopic, cancellationToken);
                    await mqttClient.DisconnectAsync(cancellationToken: cancellationToken);
                }
            }

            if (!statusMessage.Status.Equals("completed", StringComparison.OrdinalIgnoreCase))
            {
                var errorMessage = statusMessage.Message ?? "PDF generation failed.";
                throw new InvalidOperationException(errorMessage);
            }

            var fileName = statusMessage.FileName;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new InvalidOperationException("PDF generation completed but no file name was provided.");
            }

            var outputDirectory = GetPdfOutputDirectory();
            var filePath = Path.Combine(outputDirectory, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException($"Generated PDF not found at {filePath}.");
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath, cancellationToken);
            return new PdfGenerationResult(fileBytes, fileName);
        }

        private MqttClientOptions BuildMqttOptions()
        {
            var host = _configuration["MQTT:Host"] ?? "localhost";
            var port = _configuration.GetValue("MQTT:Port", 1883);
            var username = _configuration["MQTT:Username"];
            var password = _configuration["MQTT:Password"];

            var builder = new MqttClientOptionsBuilder()
                .WithClientId($"controltower_api_{Guid.NewGuid():N}")
                .WithTcpServer(host, port)
                .WithCleanSession();

            if (!string.IsNullOrEmpty(username))
            {
                builder.WithCredentials(username, password);
            }

            return builder.Build();
        }

        private string GetPdfOutputDirectory()
        {
            var configuredPath = _configuration["PDFGenerator:OutputDirectory"];
            if (string.IsNullOrWhiteSpace(configuredPath))
            {
                configuredPath = Path.Combine("ControlTower_Python", "PDF_Generator", "PDF_File");
            }

            var resolvedPath = ResolvePdfPath(configuredPath);
            Directory.CreateDirectory(resolvedPath);
            return resolvedPath;
        }

        private static string ResolvePdfPath(string configuredPath)
        {
            if (Path.IsPathRooted(configuredPath))
            {
                return Path.GetFullPath(configuredPath);
            }

            var solutionRoot = FindSolutionRoot();
            if (!string.IsNullOrEmpty(solutionRoot))
            {
                return Path.GetFullPath(Path.Combine(solutionRoot, configuredPath));
            }

            return Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, configuredPath));
        }

        private static string? FindSolutionRoot()
        {
            var current = new DirectoryInfo(AppContext.BaseDirectory);
            while (current != null)
            {
                if (System.IO.File.Exists(Path.Combine(current.FullName, "ControlTower.sln")))
                {
                    return current.FullName;
                }
                current = current.Parent;
            }

            return null;
        }

        // POST: api/PMReportFormServer/{id}/generate-final-report-pdf
        // New endpoint for generating final report PDF with signatures
        [HttpPost("{id}/generate-final-report-pdf")]
        public async Task<IActionResult> GenerateServerPmFinalReportPdf(Guid id, CancellationToken cancellationToken)
        {
            var reportForm = await _context.ReportForms
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ID == id && !r.IsDeleted, cancellationToken);

            if (reportForm == null)
            {
                return NotFound($"Report form with ID {id} was not found.");
            }

            var requestedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "api_user";

            try
            {
                var result = await GenerateFinalReportPdfAsync(id, requestedBy, cancellationToken);
                
                // Validate result
                if (result == null)
                {
                    _logger.LogError("PDF generation returned null result for ReportForm ID: {ReportId}", id);
                    return StatusCode(500, "PDF generation failed - no result returned.");
                }
                
                if (result.FileContent == null || result.FileContent.Length == 0)
                {
                    _logger.LogError("PDF generation returned empty file content for ReportForm ID: {ReportId}", id);
                    return StatusCode(500, "PDF generation failed - empty file returned.");
                }
                
                if (string.IsNullOrWhiteSpace(result.FileName))
                {
                    _logger.LogError("PDF generation returned no filename for ReportForm ID: {ReportId}", id);
                    return StatusCode(500, "PDF generation failed - no filename returned.");
                }
                
                _logger.LogInformation("PDF generation successful for ReportForm ID: {ReportId}, File: {FileName}, Size: {Size} bytes", 
                    id, result.FileName, result.FileContent.Length);
                
                // Use the EXACT same pattern as manual upload (UploadFinalReport endpoint)
                var basePath = _configuration["ReportManagementSystemFileStorage:BasePath"] ?? "C:\\Temp\\ReportFormFiles";
                var reportFolder = Path.Combine(basePath, id.ToString());
                var finalReportFolder = Path.Combine(reportFolder, "ReportForm_FinalReport");
                Directory.CreateDirectory(finalReportFolder);

                // Generate filename with timestamp (EXACTLY like manual upload)
                var originalName = $"ServerPM_FinalReport_{reportForm.JobNo ?? id.ToString()}.pdf";
                var safeName = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}_{originalName}";
                var savedPath = Path.Combine(finalReportFolder, safeName);
                var relativePath = Path.Combine(id.ToString(), "ReportForm_FinalReport", safeName);

                // Save the PDF file (copying from Python's temp location)
                await System.IO.File.WriteAllBytesAsync(savedPath, result.FileContent, cancellationToken);

                // Clean up Python's temp file
                var pythonTempPath = Path.Combine(GetPdfOutputDirectory(), result.FileName);
                if (System.IO.File.Exists(pythonTempPath))
                {
                    try
                    {
                        System.IO.File.Delete(pythonTempPath);
                        _logger.LogInformation("Cleaned up temporary PDF from Python generator: {TempPath}", pythonTempPath);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to delete temporary PDF file: {TempPath}", pythonTempPath);
                    }
                }

                // Get user ID and create database record (EXACTLY like manual upload)
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(userIdClaim, out Guid userId))
                {
                    return Unauthorized(new { message = "User context is not available." });
                }

                // Create database record with EXACT same structure as manual upload
                var entity = new ReportFormFinalReport
                {
                    ID = Guid.NewGuid(),
                    ReportFormID = id,
                    AttachmentName = originalName,  // User-friendly name (matching manual upload)
                    AttachmentPath = relativePath,  // Relative path with timestamp (matching manual upload)
                    IsDeleted = false,
                    UploadedDate = DateTime.UtcNow,
                    UploadedBy = userId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = userId
                };

                _context.ReportFormFinalReports.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Ok(new { 
                    message = "Final report PDF generated and saved successfully.",
                    fileName = originalName,
                    reportFormId = id
                });
            }
            catch (OperationCanceledException)
            {
                return StatusCode(504, "Timed out waiting for final report PDF generation.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate final report PDF for {ReportId}", id);
                return StatusCode(500, $"Failed to generate final report PDF: {ex.Message}");
            }
        }

        private async Task<PdfGenerationResult> GenerateFinalReportPdfAsync(Guid reportId, string requestedBy, CancellationToken cancellationToken)
        {
            var mqttFactory = new MqttFactory();
            using var mqttClient = mqttFactory.CreateMqttClient();
            var mqttOptions = BuildMqttOptions();

            // Use different topic for final report generation with signatures
            var topicKey = "server_pm_reportform_signature_pdf";
            var statusTopic = $"controltower/{topicKey}_status/{reportId}";
            var requestTopic = $"controltower/{topicKey}/{reportId}";

            var completionSource = new TaskCompletionSource<PdfStatusMessage>(TaskCreationOptions.RunContinuationsAsynchronously);

            mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                _logger.LogInformation("[MQTT RECEIVED] Topic: {Topic}", e.ApplicationMessage.Topic);
                
                if (e.ApplicationMessage.Topic == statusTopic)
                {
                    var payload = e.ApplicationMessage.ConvertPayloadToString();
                    _logger.LogInformation("[MQTT PAYLOAD] {Payload}", payload);
                    
                    try
                    {
                        var status = JsonSerializer.Deserialize<PdfStatusMessage>(payload, _jsonOptions);
                        if (status != null && !string.IsNullOrWhiteSpace(status.Status))
                        {
                            var normalized = status.Status.ToLowerInvariant();
                            _logger.LogInformation("[MQTT STATUS] Status: {Status}, Message: {Message}, FileName: {FileName}", 
                                status.Status, status.Message, status.FileName);
                                
                            if (normalized == "completed" || normalized == "failed")
                            {
                                _logger.LogInformation("[MQTT] Setting completion result with status: {Status}", normalized);
                                completionSource.TrySetResult(status);
                            }
                        }
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError(jsonEx, "Failed to parse Server PM Final Report PDF status payload: {Payload}", payload);
                    }
                }
                else
                {
                    _logger.LogWarning("[MQTT MISMATCH] Received message on topic {ReceivedTopic}, expected {ExpectedTopic}", 
                        e.ApplicationMessage.Topic, statusTopic);
                }

                return Task.CompletedTask;
            };

            await mqttClient.ConnectAsync(mqttOptions, cancellationToken);
            _logger.LogInformation("[MQTT] Connected to broker, subscribing to: {StatusTopic}", statusTopic);
            await mqttClient.SubscribeAsync(statusTopic, MqttQualityOfServiceLevel.AtLeastOnce, cancellationToken);
            _logger.LogInformation("[MQTT] Subscribed successfully, waiting for status updates");

            var payload = JsonSerializer.Serialize(new
            {
                report_id = reportId.ToString(),
                requested_by = requestedBy,
                timestamp = DateTime.UtcNow.ToString("o"),
                report_type = "server_pm_final_report"
            });

            var publishMessage = new MqttApplicationMessageBuilder()
                .WithTopic(requestTopic)
                .WithPayload(payload)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .Build();

            _logger.LogInformation("[MQTT] Publishing request to: {RequestTopic}", requestTopic);
            _logger.LogInformation("[MQTT] Request payload: {Payload}", payload);
            await mqttClient.PublishAsync(publishMessage, cancellationToken);
            _logger.LogInformation("[MQTT] Request published, waiting for response on: {StatusTopic}", statusTopic);

            var timeoutSeconds = _configuration.GetValue("PDFGenerator:StatusTimeoutSeconds", 120);
            using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeoutCts.CancelAfter(TimeSpan.FromSeconds(timeoutSeconds));

            PdfStatusMessage statusMessage;
            try
            {
                using (timeoutCts.Token.Register(() => completionSource.TrySetCanceled(), useSynchronizationContext: false))
                {
                    statusMessage = await completionSource.Task;
                }
            }
            finally
            {
                if (mqttClient.IsConnected)
                {
                    await mqttClient.UnsubscribeAsync(statusTopic, cancellationToken);
                    await mqttClient.DisconnectAsync(cancellationToken: cancellationToken);
                }
            }

            if (!statusMessage.Status.Equals("completed", StringComparison.OrdinalIgnoreCase))
            {
                var errorMessage = statusMessage.Message ?? "PDF generation failed.";
                throw new InvalidOperationException(errorMessage);
            }

            var fileName = statusMessage.FileName;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new InvalidOperationException("PDF generation completed but no file name was provided.");
            }

            var outputDirectory = GetPdfOutputDirectory();
            var filePath = Path.Combine(outputDirectory, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException($"Generated final report PDF not found at {filePath}.");
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath, cancellationToken);
            return new PdfGenerationResult(fileBytes, fileName);
        }

        private bool PMReportFormServerExists(Guid id)
        {
            return _context.PMReportFormServer.Any(e => e.ID == id && !e.IsDeleted);
        }

        public sealed record PdfGenerationResult(byte[] FileContent, string FileName);

        public sealed class PdfStatusMessage
        {
            [JsonPropertyName("report_id")]
            public string? ReportId { get; set; }

            [JsonPropertyName("status")]
            public string Status { get; set; } = string.Empty;

            [JsonPropertyName("message")]
            public string? Message { get; set; }

            [JsonPropertyName("file_name")]
            public string? FileName { get; set; }
        }
    }
}
