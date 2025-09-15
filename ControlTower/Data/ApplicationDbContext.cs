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
        public DbSet<MaterialUsed> MaterialUsed { get; set; }

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
        public DbSet<FormStatusWarehouse> FormStatusWarehouses { get; set; }
        public DbSet<FurtherActionTakenWarehouse> FurtherActionTakenWarehouses { get; set; }
        public DbSet<ReportForm> ReportForms { get; set; }
        public DbSet<ReportFormImageType> ReportFormImageTypes { get; set; }
        public DbSet<ReportFormImage> ReportFormImages { get; set; }
        public DbSet<CMReportForm> CMReportForms { get; set; }

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


            // Configure MaterialUsed foreign key relationships
            modelBuilder.Entity<MaterialUsed>()
                .HasOne(m => m.CreatedByUser)
                .WithMany()
                .HasForeignKey(m => m.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<MaterialUsed>()
                .HasOne(m => m.UpdatedByUser)
                .WithMany()
                .HasForeignKey(m => m.UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);

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
        }
    }
}
