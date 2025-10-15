using Microsoft.EntityFrameworkCore;
using ControlTower.Models.EmployeeManagementSystem;
using ControlTower.Models.NewsPortalSystem;
using ControlTower.Models.RoomBookingSystem;
using ControlTower.Models.ReportManagementSystem;

namespace ControlTower.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Employee Management System
        public DbSet<Models.EmployeeManagementSystem.User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<SubDepartment> SubDepartments { get; set; }
        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<OccupationLevel> OccupationLevels { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<AccessLevel> AccessLevels { get; set; }
        public DbSet<UserApplicationAccess> UserApplicationAccesses { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<ImageType> ImageTypes { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomBookingStatus> RoomBookingStatus { get; set; }
        public DbSet<RoomBooking> RoomBookings { get; set; }
        public DbSet<CMMaterialUsed> CMMaterialUsed { get; set; }
        // News Portal System
        public DbSet<News> News { get; set; }
        public DbSet<NewsCategory> NewsCategory { get; set; }
        public DbSet<NewsImages> NewsImages { get; set; }
        public DbSet<NewsComments> NewsComments { get; set; }
        public DbSet<NewsReactions> NewsReactions { get; set; }

        // Report Management System
        public DbSet<ImportFormTypes> ImportFormTypes { get; set; }
        public DbSet<ImportFileRecords> ImportFileRecords { get; set; }
        public DbSet<ReportFormType> ReportFormTypes { get; set; }
        public DbSet<CMReportFormType> CMReportFormTypes { get; set; }
        public DbSet<PMReportFormType> PMReportFormTypes { get; set; }
        public DbSet<FormStatusWarehouse> FormStatusWarehouses { get; set; }
        public DbSet<FurtherActionTakenWarehouse> FurtherActionTakenWarehouses { get; set; }
        public DbSet<ReportForm> ReportForms { get; set; }
        public DbSet<SystemNameWarehouse> SystemNameWarehouses { get; set; }
        public DbSet<StationNameWarehouse> StationNameWarehouses { get; set; }
        public DbSet<ReportFormImageType> ReportFormImageTypes { get; set; }
        public DbSet<ReportFormImage> ReportFormImages { get; set; }
        public DbSet<CMReportForm> CMReportForms { get; set; }
        public DbSet<ResultStatus> ResultStatuses { get; set; }

        // PM Report Management System
        public DbSet<PMReportFormRTU> PMReportFormRTU { get; set; }
        public DbSet<PMReportFormServer> PMReportFormServer { get; set; }
        public DbSet<PMServerHealth> PMServerHealths { get; set; }
        public DbSet<PMServerHealthDetails> PMServerHealthDetails { get; set; }
        public DbSet<PMServerHardDriveHealth> PMServerHardDriveHealths { get; set; }
        public DbSet<PMServerHardDriveHealthDetails> PMServerHardDriveHealthDetails { get; set; }
        public DbSet<PMServerDiskUsageHealth> PMServerDiskUsageHealths { get; set; }
        public DbSet<PMServerDiskUsageHealthDetails> PMServerDiskUsageHealthDetails { get; set; }
        public DbSet<PMServerCPUAndMemoryUsage> PMServerCPUAndMemoryUsages { get; set; }
        public DbSet<PMServerCPUUsageDetails> PMServerCPUUsageDetails { get; set; }
        public DbSet<PMServerMemoryUsageDetails> PMServerMemoryUsageDetails { get; set; }
        public DbSet<PMServerNetworkHealth> PMServerNetworkHealths { get; set; }
        public DbSet<ServerDiskStatus> ServerDiskStatuses { get; set; }
        public DbSet<YesNoStatus> YesNoStatuses { get; set; }
        public DbSet<PMServerWillowlynxProcessStatus> PMServerWillowlynxProcessStatuses { get; set; }
        public DbSet<PMServerWillowlynxNetworkStatus> PMServerWillowlynxNetworkStatuses { get; set; }
        public DbSet<WillowlynxNetworkStatus> WillowlynxNetworkStatuses { get; set; }
        public DbSet<PMServerWillowlynxRTUStatus> PMServerWillowlynxRTUStatuses { get; set; }
        public DbSet<WillowlynxRTUStatus> WillowlynxRTUStatuses { get; set; }
        public DbSet<PMServerWillowlynxHistoricalTrend> PMServerWillowlynxHistoricalTrends { get; set; }
        public DbSet<WillowlynxHistoricalTrendStatus> WillowlynxHistoricalTrendStatuses { get; set; }
        public DbSet<PMServerWillowlynxHistoricalReport> PMServerWillowlynxHistoricalReports { get; set; }
        public DbSet<WillowlynxHistoricalReportStatus> WillowlynxHistoricalReportStatuses { get; set; }
        public DbSet<PMServerWillowlynxCCTVCamera> PMServerWillowlynxCCTVCameras { get; set; }
        public DbSet<WillowlynxCCTVCameraStatus> WillowlynxCCTVCameraStatuses { get; set; }
        public DbSet<PMServerMonthlyDatabaseCreation> PMServerMonthlyDatabaseCreations { get; set; }
        public DbSet<PMServerMonthlyDatabaseCreationDetails> PMServerMonthlyDatabaseCreationDetails { get; set; }
        public DbSet<PMServerMonthlyDatabaseBackup> PMServerMonthlyDatabaseBackups { get; set; }
        public DbSet<PMServerMonthlyDatabaseBackupDetails> PMServerMonthlyDatabaseBackupDetails { get; set; }
        public DbSet<PMServerMonthlySCADADataBackupDetails> PMServerMonthlySCADADataBackupDetails { get; set; }
        public DbSet<PMServerTimeSync> PMServerTimeSyncs { get; set; }
        public DbSet<PMServerTimeSyncDetails> PMServerTimeSyncDetails { get; set; }
        public DbSet<PMServerHotFixes> PMServerHotFixes { get; set; }
        public DbSet<PMServerHotFixesDetails> PMServerHotFixesDetails { get; set; }
        public DbSet<PMServerFailOver> PMServerFailOvers { get; set; }
        public DbSet<PMServerFailOverDetails> PMServerFailOverDetails { get; set; }
        public DbSet<PMServerASAFirewall> PMServerASAFirewalls { get; set; }
        public DbSet<PMServerSoftwarePatchSummary> PMServerSoftwarePatchSummaries { get; set; }
        public DbSet<ASAFirewallStatus> ASAFirewallStatuses { get; set; }
        public DbSet<PMMainRtuCabinet> PMMainRtuCabinets { get; set; }
        public DbSet<PMChamberMagneticContact> PMChamberMagneticContacts { get; set; }
        public DbSet<PMRTUCabinetCooling> PMRTUCabinetCoolings { get; set; }
        public DbSet<PMDVREquipment> PMDVREquipments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(e => e.SubDepartment)
                .WithMany(sd => sd.Users)
                .HasForeignKey(e => e.SubDepartmentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.EmployeeManagementSystem.User>()
                .HasOne(e => e.Occupation)
                .WithMany(o => o.Users)
                .HasForeignKey(e => e.OccupationID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure self-referencing relationships for Employee
            modelBuilder.Entity<Models.EmployeeManagementSystem.User>()
                .HasOne(e => e.CreatedByUser)
                .WithMany(e => e.CreatedUsers)
                .HasForeignKey(e => e.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.EmployeeManagementSystem.User>()
                .HasOne(e => e.UpdatedByUser)
                .WithMany(e => e.UpdatedUsers)
                .HasForeignKey(e => e.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure User-Company relationship
            modelBuilder.Entity<Models.EmployeeManagementSystem.User>()
                .HasOne(u => u.Company)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CompanyID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Company relationships
            modelBuilder.Entity<Company>()
                .HasOne(c => c.CreatedByUser)
                .WithMany(u => u.CreatedCompanies)
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Company>()
                .HasOne(c => c.UpdatedByUser)
                .WithMany(u => u.UpdatedCompanies)
                .HasForeignKey(c => c.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Company unique constraint - Updated to handle soft deletion
            modelBuilder.Entity<Company>()
                .HasIndex(c => c.Name)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");


            // Configure SubDepartment relationships
            modelBuilder.Entity<SubDepartment>()
                .HasOne(sd => sd.Department)
                .WithMany(d => d.SubDepartments)
                .HasForeignKey(sd => sd.DepartmentID)
                .OnDelete(DeleteBehavior.Restrict);

            // Replace the existing SubDepartment relationship configurations (around lines 110-120)
            modelBuilder.Entity<SubDepartment>()
                .HasOne(sd => sd.CreatedByUser)
                .WithMany(u => u.CreatedSubDepartments)
                .HasForeignKey(sd => sd.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SubDepartment>()
                .HasOne(sd => sd.UpdatedByUser)
                .WithMany(u => u.UpdatedSubDepartments)
                .HasForeignKey(sd => sd.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Occupation relationships
            modelBuilder.Entity<Occupation>()
                .HasOne(o => o.CreatedByUser)
                .WithMany(e => e.CreatedOccupations)
                .HasForeignKey(o => o.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Occupation>()
                .HasOne(o => o.UpdatedByUser)
                .WithMany(e => e.UpdatedOccupations)
                .HasForeignKey(o => o.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure OccupationLevel relationships
            modelBuilder.Entity<OccupationLevel>()
                .HasOne(ol => ol.CreatedByUser)
                .WithMany()
                .HasForeignKey(ol => ol.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OccupationLevel>()
                .HasOne(ol => ol.UpdatedByUser)
                .WithMany()
                .HasForeignKey(ol => ol.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure unique constraints - Updated to handle soft deletion
            modelBuilder.Entity<Models.EmployeeManagementSystem.User>()
                .HasIndex(e => e.StaffCardID)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            modelBuilder.Entity<Models.EmployeeManagementSystem.User>()
                .HasIndex(e => e.Email)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            modelBuilder.Entity<Department>()
                .HasIndex(d => d.Name)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            // Updated constraint - allows same name with different levels
            modelBuilder.Entity<Occupation>()
            .HasIndex(o => new { o.OccupationName, o.OccupationLevelID })
            .IsUnique()
            .HasFilter("[IsDeleted] = 0");

            modelBuilder.Entity<OccupationLevel>()
                .HasIndex(ol => ol.LevelName)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            // Configure Application relationships
            modelBuilder.Entity<Application>()
                .HasOne(a => a.CreatedByUser)
                .WithMany()
                .HasForeignKey(a => a.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.UpdatedByUser)
                .WithMany()
                .HasForeignKey(a => a.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure unique constraints - Updated to handle soft deletion
            modelBuilder.Entity<Application>()
                .HasIndex(a => a.ApplicationName)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            // Configure EmployeeApplicationAccess relationships
            modelBuilder.Entity<UserApplicationAccess>()
                .HasOne(eaa => eaa.User)
                .WithMany()
                .HasForeignKey(eaa => eaa.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserApplicationAccess>()
                .HasOne(eaa => eaa.Application)
                .WithMany()
                .HasForeignKey(eaa => eaa.ApplicationID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserApplicationAccess>()
                .HasOne(eaa => eaa.GrantedByUser)
                .WithMany()
                .HasForeignKey(eaa => eaa.GrantedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserApplicationAccess>()
                .HasOne(eaa => eaa.CreatedByUser)
                .WithMany()
                .HasForeignKey(eaa => eaa.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserApplicationAccess>()
                .HasOne(eaa => eaa.UpdatedByUser)
                .WithMany()
                .HasForeignKey(eaa => eaa.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Create unique index for Employee-Application combination (excluding soft-deleted records)
            modelBuilder.Entity<UserApplicationAccess>()
                .HasIndex(eaa => new { eaa.UserID, eaa.ApplicationID })
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            // AccessLevel configurations
            modelBuilder.Entity<AccessLevel>()
                .HasIndex(al => al.LevelName)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            modelBuilder.Entity<AccessLevel>()
                .HasOne(al => al.CreatedByUser)
                .WithMany()
                .HasForeignKey(al => al.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AccessLevel>()
                .HasOne(al => al.UpdatedByUser)
                .WithMany()
                .HasForeignKey(al => al.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Update EmployeeApplicationAccess configuration
            modelBuilder.Entity<UserApplicationAccess>()
                .HasOne(eaa => eaa.AccessLevel)
                .WithMany(al => al.UserApplicationAccesses)
                .HasForeignKey(eaa => eaa.AccessLevelID)
                .OnDelete(DeleteBehavior.Restrict);

            // UserImage configurations
            modelBuilder.Entity<UserImage>()
                .HasOne(ui => ui.User)
                .WithMany(u => u.UserImages)
                .HasForeignKey(ui => ui.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserImage>()
                .HasOne(ui => ui.ImageType)
                .WithMany(it => it.UserImages)
                .HasForeignKey(ui => ui.ImageTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserImage>()
                .HasOne(ui => ui.UploadedByUser)
                .WithMany()
                .HasForeignKey(ui => ui.UploadedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserImage>()
                .HasOne(ui => ui.CreatedByUser)
                .WithMany()
                .HasForeignKey(ui => ui.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserImage>()
                .HasOne(ui => ui.UpdatedByUser)
                .WithMany()
                .HasForeignKey(ui => ui.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // ImageType configurations - Updated to handle soft deletion
            modelBuilder.Entity<ImageType>()
                .HasIndex(it => it.ImageTypeName)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            modelBuilder.Entity<ImageType>()
                .HasOne(it => it.CreatedByUser)
                .WithMany()
                .HasForeignKey(it => it.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ImageType>()
                .HasOne(it => it.UpdatedByUser)
                .WithMany()
                .HasForeignKey(it => it.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Building configurations - Updated to use correct property name
            modelBuilder.Entity<Building>()
                .HasIndex(b => b.Name)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            modelBuilder.Entity<Building>()
                .HasOne(b => b.CreatedByUser)
                .WithMany()
                .HasForeignKey(b => b.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Building>()
                .HasOne(b => b.UpdatedByUser)
                .WithMany()
                .HasForeignKey(b => b.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Room configurations - Updated to use correct property names
            modelBuilder.Entity<Room>()
                .HasIndex(r => new { r.Name, r.BuildingID })
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            modelBuilder.Entity<Room>()
                .HasOne(r => r.Building)
                .WithMany() // Building doesn't have Rooms navigation property
                .HasForeignKey(r => r.BuildingID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Room>()
                .HasOne(r => r.CreatedByUser)
                .WithMany()
                .HasForeignKey(r => r.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Room>()
                .HasOne(r => r.UpdatedByUser)
                .WithMany()
                .HasForeignKey(r => r.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // News Portal System configurations

            // Category self-referencing relationship
            modelBuilder.Entity<NewsCategory>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            // Category unique constraints
            modelBuilder.Entity<NewsCategory>()
                .HasIndex(c => c.Name)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            modelBuilder.Entity<NewsCategory>()
                .HasIndex(c => c.Slug)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            // News unique constraints - Updated to handle soft deletion
            modelBuilder.Entity<News>()
                .HasIndex(n => n.Slug)
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");

            // Comments self-referencing relationship
            modelBuilder.Entity<NewsComments>()
                .HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentCommentID)
                .OnDelete(DeleteBehavior.Restrict);

            // Reactions unique constraint (one reaction per user per news)
            modelBuilder.Entity<NewsReactions>()
                .HasIndex(r => new { r.NewsID, r.UserID })
                .IsUnique()
                .HasFilter("[IsDeleted] = 0");



            // Configure CMReportForm foreign key relationships
            modelBuilder.Entity<CMReportForm>()
                .HasOne(c => c.CreatedByUser)
                .WithMany()
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CMReportForm>()
                .HasOne(c => c.UpdatedByUser)
                .WithMany()
                .HasForeignKey(c => c.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure CMMaterialUsed foreign key relationships
            modelBuilder.Entity<CMMaterialUsed>()
                .HasOne(cm => cm.CMReportForm)
                .WithMany()
                .HasForeignKey(cm => cm.CMReportFormID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CMMaterialUsed>()
                .HasOne(cm => cm.CreatedByUser)
                .WithMany()
                .HasForeignKey(cm => cm.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CMMaterialUsed>()
                .HasOne(cm => cm.UpdatedByUser)
                .WithMany()
                .HasForeignKey(cm => cm.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure ReportForm foreign key relationships
            modelBuilder.Entity<ReportForm>()
                .HasOne(r => r.CreatedByUser)
                .WithMany()
                .HasForeignKey(r => r.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReportForm>()
                .HasOne(r => r.UpdatedByUser)
                .WithMany()
                .HasForeignKey(r => r.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);

                        
            // In your OnModelCreating method, add these configurations:
            modelBuilder.Entity<ReportForm>()
                .HasOne(r => r.SystemNameWarehouse)
                .WithMany(s => s.ReportForms)
                .HasForeignKey(r => r.SystemNameWarehouseID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReportForm>()
                .HasOne(r => r.StationNameWarehouse)
                .WithMany(s => s.ReportForms)
                .HasForeignKey(r => r.StationNameWarehouseID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StationNameWarehouse>()
                .HasOne(s => s.SystemNameWarehouse)
                .WithMany(sys => sys.StationNameWarehouses)
                .HasForeignKey(s => s.SystemNameWarehouseID)
                .OnDelete(DeleteBehavior.NoAction);

    
                
            // Configure ReportFormType foreign key relationships
            modelBuilder.Entity<ReportFormType>()
                .HasOne(rt => rt.CreatedByUser)
                .WithMany()
                .HasForeignKey(rt => rt.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReportFormType>()
                .HasOne(rt => rt.UpdatedByUser)
                .WithMany()
                .HasForeignKey(rt => rt.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure CMReportFormType foreign key relationships
            modelBuilder.Entity<CMReportFormType>()
                .HasOne(crt => crt.CreatedByUser)
                .WithMany()
                .HasForeignKey(crt => crt.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CMReportFormType>()
                .HasOne(crt => crt.UpdatedByUser)
                .WithMany()
                .HasForeignKey(crt => crt.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure CMReportForm foreign key relationships
            modelBuilder.Entity<CMReportForm>()
                .HasOne(c => c.CMReportFormType)
                .WithMany()
                .HasForeignKey(c => c.CMReportFormTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CMReportForm>()
                .HasOne(c => c.CreatedByUser)
                .WithMany()
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CMReportForm>()
                .HasOne(c => c.UpdatedByUser)
                .WithMany()
                .HasForeignKey(c => c.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure ReportFormImageType foreign key relationships
            modelBuilder.Entity<ReportFormImageType>()
                .HasOne(rit => rit.CreatedByUser)
                .WithMany()
                .HasForeignKey(rit => rit.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReportFormImageType>()
                .HasOne(rit => rit.UpdatedByUser)
                .WithMany()
                .HasForeignKey(rit => rit.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure ReportFormImage foreign key relationships
            modelBuilder.Entity<ReportFormImage>()
                .HasOne(ri => ri.UploadedByUser)
                .WithMany()
                .HasForeignKey(ri => ri.UploadedBy)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure PMReportForm relationships
            modelBuilder.Entity<PMReportFormRTU>()
                .HasOne(p => p.ReportForm)
                .WithMany()
                .HasForeignKey(p => p.ReportFormID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMReportFormRTU>()
                .HasOne(p => p.PMReportFormType)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMReportFormRTU>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMReportFormRTU>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMMainRtuCabinet relationships
            modelBuilder.Entity<PMMainRtuCabinet>()
                .HasOne(p => p.PMReportForm)
                .WithMany(r => r.PMMainRtuCabinets)
                .HasForeignKey(p => p.PMReportFormRTUID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMMainRtuCabinet>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMMainRtuCabinet>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMChamberMagneticContact relationships
            modelBuilder.Entity<PMChamberMagneticContact>()
                .HasOne(p => p.PMReportForm)
                .WithMany(r => r.PMChamberMagneticContacts)
                .HasForeignKey(p => p.PMReportFormRTUID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMChamberMagneticContact>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMChamberMagneticContact>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMRTUCabinetCooling relationships
            modelBuilder.Entity<PMRTUCabinetCooling>()
                .HasOne(p => p.PMReportForm)
                .WithMany(r => r.PMRTUCabinetCoolings)
                .HasForeignKey(p => p.PMReportFormRTUID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMRTUCabinetCooling>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMRTUCabinetCooling>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMDVREquipment relationships
            modelBuilder.Entity<PMDVREquipment>()
                .HasOne(p => p.PMReportForm)
                .WithMany(r => r.PMDVREquipments)
                .HasForeignKey(p => p.PMReportFormRTUID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMDVREquipment>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMDVREquipment>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMReportFormServer relationships
            modelBuilder.Entity<PMReportFormServer>()
                .HasOne(p => p.ReportForm)
                .WithMany()
                .HasForeignKey(p => p.ReportFormID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMReportFormServer>()
                .HasOne(p => p.PMReportFormType)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMReportFormServer>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMReportFormServer>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerHealth relationships
            modelBuilder.Entity<PMServerHealth>()
                .HasOne(p => p.PMReportFormServer)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormServerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerHealth>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerHealth>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerHealthDetails relationships
            modelBuilder.Entity<PMServerHealthDetails>()
                .HasOne(p => p.PMServerHealth)
                .WithMany(h => h.PMServerHealthDetails)
                .HasForeignKey(p => p.PMServerHealthID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerHealthDetails>()
                .HasOne(p => p.ResultStatus)
                .WithMany()
                .HasForeignKey(p => p.ResultStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerHealthDetails>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerHealthDetails>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerHardDriveHealth relationships
            modelBuilder.Entity<PMServerHardDriveHealth>()
                .HasOne(p => p.PMReportFormServer)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormServerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerHardDriveHealth>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerHardDriveHealth>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerHardDriveHealthDetails relationships
            modelBuilder.Entity<PMServerHardDriveHealthDetails>()
                .HasOne(p => p.PMServerHardDriveHealth)
                .WithMany(h => h.PMServerHardDriveHealthDetails)
                .HasForeignKey(p => p.PMServerHardDriveHealthID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerHardDriveHealthDetails>()
                .HasOne(p => p.ResultStatus)
                .WithMany()
                .HasForeignKey(p => p.ResultStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerHardDriveHealthDetails>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerHardDriveHealthDetails>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerDiskUsageHealth relationships
            modelBuilder.Entity<PMServerDiskUsageHealth>()
                .HasOne(p => p.PMReportFormServer)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormServerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerDiskUsageHealth>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerDiskUsageHealth>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerDiskUsageHealthDetails relationships
            modelBuilder.Entity<PMServerDiskUsageHealthDetails>()
                .HasOne(p => p.PMServerDiskUsageHealth)
                .WithMany(h => h.PMServerDiskUsageHealthDetails)
                .HasForeignKey(p => p.PMServerDiskUsageHealthID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerDiskUsageHealthDetails>()
                .HasOne(p => p.ServerDiskStatus)
                .WithMany()
                .HasForeignKey(p => p.ServerDiskStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerDiskUsageHealthDetails>()
                .HasOne(p => p.ResultStatus)
                .WithMany()
                .HasForeignKey(p => p.ResultStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerDiskUsageHealthDetails>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerDiskUsageHealthDetails>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerCPUAndMemoryUsage relationships
            modelBuilder.Entity<PMServerCPUAndMemoryUsage>()
                .HasOne(p => p.PMReportFormServer)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormServerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerCPUAndMemoryUsage>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerCPUAndMemoryUsage>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerCPUUsageDetails relationships
            modelBuilder.Entity<PMServerCPUUsageDetails>()
                .HasOne(p => p.PMServerCPUAndMemoryUsage)
                .WithMany(h => h.PMServerCPUUsageDetails)
                .HasForeignKey(p => p.PMServerCPUAndMemoryUsageID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerCPUUsageDetails>()
                .HasOne(p => p.ResultStatus)
                .WithMany()
                .HasForeignKey(p => p.ResultStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerCPUUsageDetails>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerCPUUsageDetails>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerMemoryUsageDetails relationships
            modelBuilder.Entity<PMServerMemoryUsageDetails>()
                .HasOne(p => p.PMServerCPUAndMemoryUsage)
                .WithMany(h => h.PMServerMemoryUsageDetails)
                .HasForeignKey(p => p.PMServerCPUAndMemoryUsageID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerMemoryUsageDetails>()
                .HasOne(p => p.ResultStatus)
                .WithMany()
                .HasForeignKey(p => p.ResultStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerMemoryUsageDetails>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerMemoryUsageDetails>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerNetworkHealth relationships
            modelBuilder.Entity<PMServerNetworkHealth>()
                .HasOne(p => p.PMReportFormServer)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormServerID)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<PMServerNetworkHealth>()
                .HasOne(p => p.YesNoStatus)
                .WithMany()
                .HasForeignKey(p => p.YesNoStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerNetworkHealth>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerNetworkHealth>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerWillowlynxProcessStatus relationships
            modelBuilder.Entity<PMServerWillowlynxProcessStatus>()
                .HasOne(p => p.PMReportFormServer)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormServerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxProcessStatus>()
                .HasOne(p => p.YesNoStatus)
                .WithMany()
                .HasForeignKey(p => p.YesNoStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxProcessStatus>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxProcessStatus>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerWillowlynxNetworkStatus relationships
            modelBuilder.Entity<PMServerWillowlynxNetworkStatus>()
                .HasOne(p => p.PMReportFormServer)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormServerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxNetworkStatus>()
                .HasOne(p => p.WillowlynxNetworkStatus)
                .WithMany()
                .HasForeignKey(p => p.WillowlynxNetworkStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxNetworkStatus>()
                .HasOne(p => p.YesNoStatus)
                .WithMany()
                .HasForeignKey(p => p.YesNoStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxNetworkStatus>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxNetworkStatus>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerWillowlynxRTUStatus relationships
            modelBuilder.Entity<PMServerWillowlynxRTUStatus>()
                .HasOne(p => p.PMReportFormServer)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormServerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxRTUStatus>()
                .HasOne(p => p.WillowlynxRTUStatus)
                .WithMany()
                .HasForeignKey(p => p.WillowlynxRTUStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxRTUStatus>()
                .HasOne(p => p.YesNoStatus)
                .WithMany()
                .HasForeignKey(p => p.YesNoStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxRTUStatus>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxRTUStatus>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerWillowlynxHistoricalTrend relationships
            modelBuilder.Entity<PMServerWillowlynxHistoricalTrend>()
                .HasOne(p => p.PMReportFormServer)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormServerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxHistoricalTrend>()
                .HasOne(p => p.WillowlynxHistoricalTrendStatus)
                .WithMany()
                .HasForeignKey(p => p.WillowlynxHistoricalTrendStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxHistoricalTrend>()
                .HasOne(p => p.YesNoStatus)
                .WithMany()
                .HasForeignKey(p => p.YesNoStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxHistoricalTrend>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxHistoricalTrend>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerWillowlynxHistoricalReport relationships
            modelBuilder.Entity<PMServerWillowlynxHistoricalReport>()
                .HasOne(p => p.PMReportFormServer)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormServerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxHistoricalReport>()
                .HasOne(p => p.WillowlynxHistoricalReportStatus)
                .WithMany()
                .HasForeignKey(p => p.WillowlynxHistoricalReportStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxHistoricalReport>()
                .HasOne(p => p.YesNoStatus)
                .WithMany()
                .HasForeignKey(p => p.YesNoStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxHistoricalReport>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxHistoricalReport>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerWillowlynxCCTVCamera relationships
            modelBuilder.Entity<PMServerWillowlynxCCTVCamera>()
                .HasOne(p => p.PMReportFormServer)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormServerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxCCTVCamera>()
                .HasOne(p => p.WillowlynxCCTVCameraStatus)
                .WithMany()
                .HasForeignKey(p => p.WillowlynxCCTVCameraStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxCCTVCamera>()
                .HasOne(p => p.YesNoStatus)
                .WithMany()
                .HasForeignKey(p => p.YesNoStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxCCTVCamera>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerWillowlynxCCTVCamera>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerMonthlyDatabaseCreation relationships
            modelBuilder.Entity<PMServerMonthlyDatabaseCreation>()
                .HasOne(p => p.PMReportFormServer)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormServerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerMonthlyDatabaseCreation>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerMonthlyDatabaseCreation>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerMonthlyDatabaseCreationDetails relationships
            modelBuilder.Entity<PMServerMonthlyDatabaseCreationDetails>()
                .HasOne(p => p.PMServerMonthlyDatabaseCreation)
                .WithMany(h => h.PMServerMonthlyDatabaseCreationDetails)
                .HasForeignKey(p => p.PMServerMonthlyDatabaseCreationID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerMonthlyDatabaseCreationDetails>()
                .HasOne(p => p.YesNoStatus)
                .WithMany()
                .HasForeignKey(p => p.YesNoStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerMonthlyDatabaseCreationDetails>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerMonthlyDatabaseCreationDetails>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerMonthlyDatabaseBackup relationships
            modelBuilder.Entity<PMServerMonthlyDatabaseBackup>()
                .HasOne(p => p.PMReportFormServer)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormServerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerMonthlyDatabaseBackup>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerMonthlyDatabaseBackup>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerMonthlyDatabaseBackupDetails relationships
            modelBuilder.Entity<PMServerMonthlyDatabaseBackupDetails>()
                .HasOne(p => p.PMServerMonthlyDatabaseBackup)
                .WithMany(h => h.PMServerMonthlyDatabaseBackupDetails)
                .HasForeignKey(p => p.PMServerMonthlyDatabaseBackupID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerMonthlyDatabaseBackupDetails>()
                .HasOne(p => p.YesNoStatus)
                .WithMany()
                .HasForeignKey(p => p.YesNoStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerMonthlyDatabaseBackupDetails>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerMonthlyDatabaseBackupDetails>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerMonthlySCADADataBackupDetails relationships
            modelBuilder.Entity<PMServerMonthlySCADADataBackupDetails>()
                .HasOne(p => p.PMServerMonthlyDatabaseBackup)
                .WithMany(h => h.PMServerMonthlySCADADataBackupDetails)
                .HasForeignKey(p => p.PMServerMonthlyDatabaseBackupID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerMonthlySCADADataBackupDetails>()
                .HasOne(p => p.YesNoStatus)
                .WithMany()
                .HasForeignKey(p => p.YesNoStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerMonthlySCADADataBackupDetails>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerMonthlySCADADataBackupDetails>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerTimeSync relationships
            modelBuilder.Entity<PMServerTimeSync>()
                .HasOne(p => p.PMReportFormServer)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormServerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerTimeSync>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerTimeSync>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerTimeSyncDetails relationships
            modelBuilder.Entity<PMServerTimeSyncDetails>()
                .HasOne(p => p.PMServerTimeSync)
                .WithMany(h => h.PMServerTimeSyncDetails)
                .HasForeignKey(p => p.PMServerTimeSyncID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerTimeSyncDetails>()
                .HasOne(p => p.ResultStatus)
                .WithMany()
                .HasForeignKey(p => p.ResultStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerTimeSyncDetails>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerTimeSyncDetails>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerHotFixes relationships
            modelBuilder.Entity<PMServerHotFixes>()
                .HasOne(p => p.PMReportFormServer)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormServerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerHotFixes>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerHotFixes>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerHotFixesDetails relationships
            modelBuilder.Entity<PMServerHotFixesDetails>()
                .HasOne(p => p.PMServerHotFixes)
                .WithMany(h => h.PMServerHotFixesDetails)
                .HasForeignKey(p => p.PMServerHotFixesID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerHotFixesDetails>()
                .HasOne(p => p.ResultStatus)
                .WithMany()
                .HasForeignKey(p => p.ResultStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerHotFixesDetails>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerHotFixesDetails>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerFailOver relationships
            modelBuilder.Entity<PMServerFailOver>()
                .HasOne(p => p.PMReportFormServer)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormServerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerFailOver>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerFailOver>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure PMServerFailOverDetails relationships
            modelBuilder.Entity<PMServerFailOverDetails>()
                .HasOne(p => p.PMServerFailOver)
                .WithMany(h => h.PMServerFailOverDetails)
                .HasForeignKey(p => p.PMServerFailOverID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerFailOverDetails>()
                .HasOne(p => p.YesNoStatus)
                .WithMany()
                .HasForeignKey(p => p.YesNoStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerFailOverDetails>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerFailOverDetails>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // PMServerASAFirewall relationships
            modelBuilder.Entity<PMServerASAFirewall>()
                .HasOne(p => p.PMReportFormServer)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormServerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerASAFirewall>()
                .HasOne(p => p.ASAFirewallStatus)
                .WithMany()
                .HasForeignKey(p => p.ASAFirewallStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerASAFirewall>()
                .HasOne(p => p.ResultStatus)
                .WithMany()
                .HasForeignKey(p => p.ResultStatusID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerASAFirewall>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerASAFirewall>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // PMServerSoftwarePatchSummary relationships
            modelBuilder.Entity<PMServerSoftwarePatchSummary>()
                .HasOne(p => p.PMReportFormServer)
                .WithMany()
                .HasForeignKey(p => p.PMReportFormServerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerSoftwarePatchSummary>()
                .HasOne(p => p.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PMServerSoftwarePatchSummary>()
                .HasOne(p => p.UpdatedByUser)
                .WithMany()
                .HasForeignKey(p => p.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
