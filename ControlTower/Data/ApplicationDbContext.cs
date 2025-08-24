using Microsoft.EntityFrameworkCore;
using ControlTower.Models.EmployeeManagementSystem;
using ControlTower.Models.ServiceReportSystem;
using ControlTower.Models.NewsPortalSystem;

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
        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<AccessLevel> AccessLevels { get; set; }
        public DbSet<UserApplicationAccess> UserApplicationAccesses { get; set; }

        // Service Report System
        public DbSet<ServiceReportForm> ServiceReportForms { get; set; }
        public DbSet<ProjectNoWarehouse> ProjectNoWarehouses { get; set; }
        public DbSet<SystemWarehouse> SystemWarehouses { get; set; }
        public DbSet<LocationWarehouse> LocationWarehouses { get; set; }
        public DbSet<FollowupActionWarehouse> FollowupActionWarehouses { get; set; }
        public DbSet<ServiceTypeWarehouse> ServiceTypeWarehouses { get; set; }
        public DbSet<FormStatusWarehouses> FormStatusWarehouses { get; set; }
        public DbSet<IssueReported> IssueReported { get; set; }
        public DbSet<IssueFound> IssueFound { get; set; }
        public DbSet<ActionTaken> ActionTaken { get; set; }
        public DbSet<MaterialUsed> MaterialsUsed { get; set; }
        public DbSet<FurtherActionTakenWarehouse> FurtherActionTakenWarehouses { get; set; }
        public DbSet<ImportFormTypes> ImportFormTypes { get; set; }
        public DbSet<ImportFileRecords> ImportFileRecords { get; set; }

        // News Portal System
        public DbSet<News> News { get; set; }
        public DbSet<NewsCategory> NewsCategory { get; set; }
        public DbSet<NewsImages> NewsImages { get; set; }
        public DbSet<NewsComments> NewsComments { get; set; }
        public DbSet<NewsReactions> NewsReactions { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Employee relationships
            modelBuilder.Entity<Models.EmployeeManagementSystem.User>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(e => e.DepartmentID)
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

            // Company unique constraint
            modelBuilder.Entity<Company>()
                .HasIndex(c => c.Name)
                .IsUnique();

            // Configure Department relationships
            modelBuilder.Entity<Department>()
                .HasOne(d => d.CreatedByUser)
                .WithMany(e => e.CreatedDepartments)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.UpdatedByUser)
                .WithMany(e => e.UpdatedDepartments)
                .HasForeignKey(d => d.UpdatedBy)
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

            // Configure unique constraints
            modelBuilder.Entity<Models.EmployeeManagementSystem.User>()
                .HasIndex(e => e.StaffCardID)
                .IsUnique();

            modelBuilder.Entity<Models.EmployeeManagementSystem.User>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<Department>()
                .HasIndex(d => d.Name)
                .IsUnique();

            modelBuilder.Entity<Occupation>()
                .HasIndex(o => o.OccupationName)
                .IsUnique();

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

            // Configure unique constraints
            modelBuilder.Entity<Application>()
                .HasIndex(a => a.ApplicationName)
                .IsUnique();

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

            // Create unique index for Employee-Application combination
            modelBuilder.Entity<UserApplicationAccess>()
                .HasIndex(eaa => new { eaa.UserID, eaa.ApplicationID })
                .IsUnique();
        
            // AccessLevel configurations
            modelBuilder.Entity<AccessLevel>()
                .HasIndex(al => al.LevelName)
                .IsUnique();
                
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
                .IsUnique();

            modelBuilder.Entity<NewsCategory>()
                .HasIndex(c => c.Slug)
                .IsUnique();

            // News unique constraints
            modelBuilder.Entity<News>()
                .HasIndex(n => n.Slug)
                .IsUnique();

            // Comments self-referencing relationship
            modelBuilder.Entity<NewsComments>()
                .HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentCommentID)
                .OnDelete(DeleteBehavior.Restrict);

            // Reactions unique constraint (one reaction per user per news)
            modelBuilder.Entity<NewsReactions>()
                .HasIndex(r => new { r.NewsID, r.UserID })
                .IsUnique();
        }
    }
}