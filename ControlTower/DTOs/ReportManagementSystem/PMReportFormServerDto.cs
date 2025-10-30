using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class PMReportFormServerDto
    {
        public Guid ID { get; set; }
        public Guid ReportFormID { get; set; }
        public Guid PMReportFormTypeID { get; set; }
        public string? ProjectNo { get; set; }
        public string? Customer { get; set; }
        public string? ReportTitle { get; set; }
        public string? AttendedBy { get; set; }
        public string? WitnessedBy { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string? Remarks { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        // Navigation properties for display
        public string? PMReportFormTypeName { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
        public string? JobNo { get; set; } // From parent ReportForm
        public string? StationName { get; set; }
        public string? SystemDescription { get; set; }

        // PM Server Component Data
        public PMServerHealthDataDto? ServerHealthData { get; set; }
        public PMServerDiskUsageDataDto? DiskUsageData { get; set; }
        public PMServerCPUAndMemoryDataDto? CpuAndRamUsageData { get; set; }
        public PMServerNetworkHealthDataDto? NetworkHealthData { get; set; }
        public PMServerWillowlynxProcessStatusDataDto? WillowlynxProcessStatusData { get; set; }
        public PMServerWillowlynxNetworkStatusDataDto? WillowlynxNetworkStatusData { get; set; }
        public PMServerWillowlynxRTUStatusDataDto? WillowlynxRTUStatusData { get; set; }
        public PMServerWillowlynxHistoricalTrendDataDto? WillowlynxHistorialTrendData { get; set; }
        public PMServerWillowlynxHistoricalReportDataDto? WillowlynxHistoricalReportData { get; set; }
        public PMServerWillowlynxCCTVCameraDataDto? WillowlynxSumpPitCCTVCameraData { get; set; }
        public PMServerMonthlyDatabaseCreationDataDto? MonthlyDatabaseCreationData { get; set; }
        public PMServerDatabaseBackupDataDto? DatabaseBackupData { get; set; }
        public PMServerTimeSyncDataDto? TimeSyncData { get; set; }
        public PMServerHotFixesDataDto? HotFixesData { get; set; }
        public PMServerFailOverDataDto? AutoFailOverData { get; set; }
        public PMServerASAFirewallDataDto? AsaFirewallData { get; set; }
        public PMServerSoftwarePatchDataDto? SoftwarePatchData { get; set; }
        public PMServerHardDriveHealthDataDto? HardDriveHealthData { get; set; }
    }

    public class CreatePMReportFormServerDto
    {
        [Required]
        public Guid ReportFormID { get; set; }

        [Required]
        public Guid PMReportFormTypeID { get; set; }

        [StringLength(255)]
        public string? ProjectNo { get; set; }

        [StringLength(255)]
        public string? Customer { get; set; }

        [StringLength(255)]
        public string? ReportTitle { get; set; }

        [StringLength(255)]
        public string? AttendedBy { get; set; }

        [StringLength(255)]
        public string? WitnessedBy { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        [StringLength(1000)]
        public string? Remarks { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }

        // PM Server Component Data
        public PMServerHealthDataDto? ServerHealthData { get; set; }
        public PMServerDiskUsageDataDto? DiskUsageData { get; set; }
        public PMServerCPUAndMemoryDataDto? CpuAndRamUsageData { get; set; }
        public PMServerNetworkHealthDataDto? NetworkHealthData { get; set; }
        public PMServerWillowlynxProcessStatusDataDto? WillowlynxProcessStatusData { get; set; }
        public PMServerWillowlynxNetworkStatusDataDto? WillowlynxNetworkStatusData { get; set; }
        public PMServerWillowlynxRTUStatusDataDto? WillowlynxRTUStatusData { get; set; }
        public PMServerWillowlynxHistoricalTrendDataDto? WillowlynxHistorialTrendData { get; set; }
        public PMServerWillowlynxHistoricalReportDataDto? WillowlynxHistoricalReportData { get; set; }
        public PMServerWillowlynxCCTVCameraDataDto? WillowlynxSumpPitCCTVCameraData { get; set; }
        public PMServerMonthlyDatabaseCreationDataDto? MonthlyDatabaseCreationData { get; set; }
        public PMServerDatabaseBackupDataDto? DatabaseBackupData { get; set; }
        public PMServerTimeSyncDataDto? TimeSyncData { get; set; }
        public PMServerHotFixesDataDto? HotFixesData { get; set; }
        public PMServerFailOverDataDto? AutoFailOverData { get; set; }
        public PMServerASAFirewallDataDto? AsaFirewallData { get; set; }
        public PMServerSoftwarePatchDataDto? SoftwarePatchData { get; set; }
        public PMServerHardDriveHealthDataDto? HardDriveHealthData { get; set; }
    }

    public class UpdatePMReportFormServerDto
    {
        [Required]
        public Guid PMReportFormTypeID { get; set; }

        [StringLength(255)]
        public string? ProjectNo { get; set; }

        [StringLength(255)]
        public string? Customer { get; set; }

        [StringLength(255)]
        public string? ReportTitle { get; set; }

        [StringLength(255)]
        public string? AttendedBy { get; set; }

        [StringLength(255)]
        public string? WitnessedBy { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        [StringLength(1000)]
        public string? Remarks { get; set; }

        public Guid? UpdatedBy { get; set; }

        // PM Server Component Data with Update DTOs
        public UpdatePMServerHealthDataDto? ServerHealthData { get; set; }
        public UpdatePMServerDiskUsageDataDto? DiskUsageData { get; set; }
        public UpdatePMServerCPUAndMemoryDataDto? CpuAndRamUsageData { get; set; }
        public PMServerNetworkHealthDataDto? NetworkHealthData { get; set; }
        public PMServerWillowlynxProcessStatusDataDto? WillowlynxProcessStatusData { get; set; }
        public PMServerWillowlynxNetworkStatusDataDto? WillowlynxNetworkStatusData { get; set; }
        public PMServerWillowlynxRTUStatusDataDto? WillowlynxRTUStatusData { get; set; }
        public PMServerWillowlynxHistoricalTrendDataDto? WillowlynxHistorialTrendData { get; set; }
        public PMServerWillowlynxHistoricalReportDataDto? WillowlynxHistoricalReportData { get; set; }
        public PMServerWillowlynxCCTVCameraDataDto? WillowlynxSumpPitCCTVCameraData { get; set; }
        public UpdatePMServerMonthlyDatabaseCreationDataDto? MonthlyDatabaseCreationData { get; set; }
        public UpdatePMServerDatabaseBackupDataDto? DatabaseBackupData { get; set; }
        public UpdatePMServerTimeSyncDataDto? TimeSyncData { get; set; }
        public UpdatePMServerHotFixesDataDto? HotFixesData { get; set; }
        public UpdatePMServerFailOverDataDto? AutoFailOverData { get; set; }
        public UpdatePMServerASAFirewallDataDto? AsaFirewallData { get; set; }
        public UpdatePMServerSoftwarePatchDataDto? SoftwarePatchData { get; set; }
        public UpdatePMServerHardDriveHealthDataDto? HardDriveHealthData { get; set; }
    }

    // Update Component Data DTOs
    public class UpdatePMServerHealthDataDto
    {
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public List<UpdatePMServerHealthDetailDto>? Details { get; set; }
    }

    public class UpdatePMServerDiskUsageDataDto
    {
        public List<UpdatePMServerDiskUsageDetailDto> Details { get; set; } = new List<UpdatePMServerDiskUsageDetailDto>();
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class UpdatePMServerCPUAndMemoryDataDto
    {
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public List<UpdatePMServerCPUUsageDetailDto>? CPUUsageDetails { get; set; }
        public List<UpdatePMServerMemoryUsageDetailDto>? MemoryUsageDetails { get; set; }
    }

    public class UpdatePMServerMonthlyDatabaseCreationDataDto
    {
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public List<UpdatePMServerMonthlyDatabaseCreationDetailDto>? Details { get; set; }
    }

    public class UpdatePMServerDatabaseBackupDataDto
    {
        [StringLength(1000)]
        public string? Remarks { get; set; }
        [StringLength(500)]
        public string? LatestBackupFileName { get; set; }
        public List<UpdatePMServerMSSQLDatabaseBackupDetailDto>? MSSQLDetails { get; set; }
        public List<UpdatePMServerSCADADataBackupDetailDto>? SCADADetails { get; set; }
    }

    public class UpdatePMServerTimeSyncDataDto
    {
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public List<UpdatePMServerTimeSyncDetailDto>? Details { get; set; }
    }

    public class UpdatePMServerHotFixesDataDto
    {
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public List<UpdatePMServerHotFixesDetailDto>? Details { get; set; }
    }

    public class UpdatePMServerFailOverDataDto
    {
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public List<UpdatePMServerFailOverDetailDto>? Details { get; set; }
    }

    public class UpdatePMServerASAFirewallDataDto
    {
        public List<UpdatePMServerASAFirewallDetailDto>? Details { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class UpdatePMServerSoftwarePatchDataDto
    {
        public List<UpdatePMServerSoftwarePatchDetailDto>? Details { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class UpdatePMServerHardDriveHealthDataDto
    {
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public List<UpdatePMServerHardDriveHealthDetailDto>? Details { get; set; }
    }

    // Component Data DTOs
    public class PMServerHealthDataDto
    {
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public List<PMServerHealthDetailDto>? Details { get; set; }
    }

    public class PMServerHealthDetailDto
    {
        [StringLength(200)]
        public string? ServerName { get; set; }
        public Guid ResultStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class PMServerDiskUsageDataDto
    {
        public List<PMServerDiskUsageDetailDto> Details { get; set; } = new List<PMServerDiskUsageDetailDto>();
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class PMServerDiskUsageDetailDto
    {
        [StringLength(200)]
        public string? DiskName { get; set; }
        [StringLength(200)]
        public string? ServerName { get; set; }
        [StringLength(100)]
        public string? Capacity { get; set; }
        [StringLength(100)]
        public string? FreeSpace { get; set; }
        [StringLength(100)]
        public string? Usage { get; set; }
        public Guid ServerDiskStatusID { get; set; }  // Foreign key to ServerDiskStatus table
        public Guid ResultStatusID { get; set; }      // Foreign key to ResultStatus table
        [StringLength(500)]
        public string? Remarks { get; set; }
    }

    public class PMServerCPUAndMemoryDataDto
    {
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public List<PMServerCPUUsageDetailDto>? CPUUsageDetails { get; set; }
        public List<PMServerMemoryUsageDetailDto>? MemoryUsageDetails { get; set; }
    }

    public class PMServerCPUUsageDetailDto
    {
        [StringLength(50)]
        public string? SerialNo { get; set; }
        [StringLength(200)]
        public string? ServerName { get; set; }
        [StringLength(100)]
        public string? CPUUsage { get; set; }
        public Guid ResultStatusID { get; set; }
        [StringLength(500)]
        public string? Remarks { get; set; }
    }

    public class PMServerMemoryUsageDetailDto
    {
        [StringLength(50)]
        public string? SerialNo { get; set; }
        [StringLength(200)]
        public string? ServerName { get; set; }
        [StringLength(100)]
        public string? MemorySize { get; set; }
        [StringLength(100)]
        public string? MemoryInUse { get; set; }
        public Guid ResultStatusID { get; set; }
        [StringLength(500)]
        public string? Remarks { get; set; }
    }

    public class PMServerNetworkHealthDataDto
    {
        public Guid YesNoStatusID { get; set; }
        public DateTime? DateChecked { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class PMServerWillowlynxProcessStatusDataDto
    {
        public Guid YesNoStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class PMServerWillowlynxProcessStatusDetailDto
    {
        [StringLength(200)]
        public string? ProcessName { get; set; }
        public Guid? YesNoStatusID { get; set; }
    }

    public class PMServerWillowlynxNetworkStatusDataDto
    {
        public Guid YesNoStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class PMServerWillowlynxNetworkStatusDetailDto
    {
        [StringLength(200)]
        public string? NetworkName { get; set; }
        public Guid? WillowlynxNetworkStatusID { get; set; }
    }

    public class PMServerWillowlynxRTUStatusDataDto
    {
        public Guid YesNoStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class PMServerWillowlynxRTUStatusDetailDto
    {
        [StringLength(200)]
        public string? RTUName { get; set; }
        public Guid? WillowlynxRTUStatusID { get; set; }
    }

    public class PMServerWillowlynxHistoricalTrendDataDto
    {
        public Guid YesNoStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class PMServerWillowlynxHistoricalTrendDetailDto
    {
        [StringLength(200)]
        public string? TrendName { get; set; }
        public Guid? WillowlynxHistoricalTrendStatusID { get; set; }
    }

    public class PMServerWillowlynxHistoricalReportDataDto
    {
        public Guid YesNoStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class PMServerWillowlynxHistoricalReportDetailDto
    {
        [StringLength(200)]
        public string? ReportName { get; set; }
        public Guid? WillowlynxHistoricalReportStatusID { get; set; }
    }

    public class PMServerWillowlynxCCTVCameraDataDto
    {
        public Guid YesNoStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class PMServerWillowlynxCCTVCameraDetailDto
    {
        [StringLength(200)]
        public string? CameraName { get; set; }
        public Guid? WillowlynxCCTVCameraStatusID { get; set; }
    }

    public class PMServerMonthlyDatabaseCreationDataDto
    {
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public List<PMServerMonthlyDatabaseCreationDetailDto>? Details { get; set; }
    }

    public class PMServerMonthlyDatabaseCreationDetailDto
    {
        [StringLength(100)]
        public string? SerialNo { get; set; }
        [StringLength(500)]
        public string? ServerName { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid YesNoStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class PMServerDatabaseBackupDataDto
    {
        [StringLength(1000)]
        public string? Remarks { get; set; }
        [StringLength(500)]
        public string? LatestBackupFileName { get; set; }
        public List<PMServerMSSQLDatabaseBackupDetailDto>? MSSQLDetails { get; set; }
        public List<PMServerSCADADataBackupDetailDto>? SCADADetails { get; set; }
    }

    public class PMServerMSSQLDatabaseBackupDetailDto
    {
        [StringLength(100)]
        public string? SerialNo { get; set; }
        [StringLength(500)]
        public string? ServerName { get; set; }
        public Guid YesNoStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class PMServerSCADADataBackupDetailDto
    {
        [StringLength(100)]
        public string? SerialNo { get; set; }
        [StringLength(500)]
        public string? ServerName { get; set; }
        public Guid YesNoStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class PMServerTimeSyncDataDto
    {
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public List<PMServerTimeSyncDetailDto>? Details { get; set; }
    }

    public class PMServerTimeSyncDetailDto
    {
        [StringLength(100)]
        public string? SerialNo { get; set; }
        [StringLength(500)]
        public string? ServerName { get; set; }
        public Guid ResultStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class PMServerHotFixesDataDto
    {
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public List<PMServerHotFixesDetailDto>? Details { get; set; }
    }

    public class PMServerHotFixesDetailDto
    {
        [StringLength(100)]
        public string? SerialNo { get; set; }
        [StringLength(500)]
        public string? ServerName { get; set; }
        [StringLength(500)]
        public string? LatestHotFixsApplied { get; set; }
        public DateTime? DateInstalled { get; set; }
        public Guid ResultStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class PMServerFailOverDataDto
    {
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public List<PMServerFailOverDetailDto>? Details { get; set; }
    }

    public class PMServerFailOverDetailDto
    {
        public Guid YesNoStatusID { get; set; }
        public string? ToServer { get; set; }
        public string? FromServer { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class PMServerASAFirewallDataDto
    {
        public List<PMServerASAFirewallDetailDto>? Details { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class PMServerASAFirewallDetailDto
    {
        public int SerialNumber { get; set; }
        [StringLength(500)]
        public string? CommandInput { get; set; }
        public Guid ASAFirewallStatusID { get; set; }
        public Guid ResultStatusID { get; set; }
    }

    public class PMServerSoftwarePatchDataDto
    {
        public List<PMServerSoftwarePatchDetailDto>? Details { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class PMServerSoftwarePatchDetailDto
    {
        [StringLength(100)]
        public string? SerialNo { get; set; }
        [StringLength(500)]
        public string? ServerName { get; set; }
        [StringLength(500)]
        public string? PreviousPatch { get; set; }
        [StringLength(500)]
        public string? CurrentPatch { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    public class PMServerHardDriveHealthDataDto
    {
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public List<PMServerHardDriveHealthDetailDto>? Details { get; set; }
    }

    public class PMServerHardDriveHealthDetailDto
    {
        [StringLength(200)]
        public string? ServerName { get; set; }
        [StringLength(200)]
        public string? HardDriveName { get; set; }
        public DateTime? DateChecked { get; set; }
        [StringLength(100)]
        public string? DriveLetter { get; set; }
        [StringLength(100)]
        public string? DriveType { get; set; }
        public Guid? YesNoStatusID { get; set; }
        public decimal? Temperature { get; set; }
        public Guid ResultStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
    }

    // Update DTOs with tracking properties
    public class UpdatePMServerHealthDetailDto
    {
        public Guid? ID { get; set; }
        [StringLength(200)]
        public string? ServerName { get; set; }
        public Guid ResultStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public bool IsNew { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }

    public class UpdatePMServerDiskUsageDetailDto
    {
        public Guid? ID { get; set; }
        [StringLength(200)]
        public string? DiskName { get; set; }
        [StringLength(200)]
        public string? ServerName { get; set; }
        [StringLength(100)]
        public string? Capacity { get; set; }
        [StringLength(100)]
        public string? FreeSpace { get; set; }
        [StringLength(100)]
        public string? Usage { get; set; }
        public Guid ServerDiskStatusID { get; set; }
        public Guid ResultStatusID { get; set; }
        [StringLength(500)]
        public string? Remarks { get; set; }
        public bool IsNew { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }

    public class UpdatePMServerCPUUsageDetailDto
    {
        public Guid? ID { get; set; }
        [StringLength(50)]
        public string? SerialNo { get; set; }
        [StringLength(200)]
        public string? ServerName { get; set; }
        [StringLength(100)]
        public string? CPUUsage { get; set; }
        public Guid ResultStatusID { get; set; }
        [StringLength(500)]
        public string? Remarks { get; set; }
        public bool IsNew { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }

    public class UpdatePMServerMemoryUsageDetailDto
    {
        public Guid? ID { get; set; }
        [StringLength(50)]
        public string? SerialNo { get; set; }
        [StringLength(200)]
        public string? ServerName { get; set; }
        [StringLength(100)]
        public string? MemorySize { get; set; }
        [StringLength(100)]
        public string? MemoryInUse { get; set; }
        public Guid ResultStatusID { get; set; }
        [StringLength(500)]
        public string? Remarks { get; set; }
        public bool IsNew { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }

    public class UpdatePMServerMonthlyDatabaseCreationDetailDto
    {
        public Guid? ID { get; set; }
        [StringLength(100)]
        public string? SerialNo { get; set; }
        [StringLength(500)]
        public string? ServerName { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid YesNoStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public bool IsNew { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }

    public class UpdatePMServerMSSQLDatabaseBackupDetailDto
    {
        public Guid? ID { get; set; }
        [StringLength(100)]
        public string? SerialNo { get; set; }
        [StringLength(500)]
        public string? ServerName { get; set; }
        public Guid YesNoStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public bool IsNew { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }

    public class UpdatePMServerSCADADataBackupDetailDto
    {
        public Guid? ID { get; set; }
        [StringLength(100)]
        public string? SerialNo { get; set; }
        [StringLength(500)]
        public string? ServerName { get; set; }
        public Guid YesNoStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public bool IsNew { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }

    public class UpdatePMServerTimeSyncDetailDto
    {
        public Guid? ID { get; set; }
        [StringLength(100)]
        public string? SerialNo { get; set; }
        [StringLength(500)]
        public string? ServerName { get; set; }
        public Guid ResultStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public bool IsNew { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }

    public class UpdatePMServerHotFixesDetailDto
    {
        public Guid? ID { get; set; }
        [StringLength(100)]
        public string? SerialNo { get; set; }
        [StringLength(500)]
        public string? ServerName { get; set; }
        [StringLength(500)]
        public string? LatestHotFixsApplied { get; set; }
        public DateTime? DateInstalled { get; set; }
        public Guid ResultStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public bool IsNew { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }

    public class UpdatePMServerFailOverDetailDto
    {
        public Guid? ID { get; set; }
        public Guid YesNoStatusID { get; set; }
        public string? ToServer { get; set; }
        public string? FromServer { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public bool IsNew { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }

    public class UpdatePMServerASAFirewallDetailDto
    {
        public Guid? ID { get; set; }
        public int SerialNumber { get; set; }
        [StringLength(500)]
        public string? CommandInput { get; set; }
        public Guid ASAFirewallStatusID { get; set; }
        public Guid ResultStatusID { get; set; }
        public bool IsNew { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }

    public class UpdatePMServerSoftwarePatchDetailDto
    {
        public Guid? ID { get; set; }
        [StringLength(100)]
        public string? SerialNo { get; set; }
        [StringLength(500)]
        public string? ServerName { get; set; }
        [StringLength(500)]
        public string? PreviousPatch { get; set; }
        [StringLength(500)]
        public string? CurrentPatch { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public bool IsNew { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }

    public class UpdatePMServerHardDriveHealthDetailDto
    {
        public Guid? ID { get; set; }
        [StringLength(200)]
        public string? ServerName { get; set; }
        [StringLength(200)]
        public string? HardDriveName { get; set; }
        public DateTime? DateChecked { get; set; }
        [StringLength(100)]
        public string? DriveLetter { get; set; }
        [StringLength(100)]
        public string? DriveType { get; set; }
        public Guid? YesNoStatusID { get; set; }
        public decimal? Temperature { get; set; }
        public Guid ResultStatusID { get; set; }
        [StringLength(1000)]
        public string? Remarks { get; set; }
        public bool IsNew { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
}