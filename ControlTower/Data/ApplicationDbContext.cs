using Microsoft.EntityFrameworkCore;
using ControlTower.Models.EmployeeManagementSystem;
using ControlTower.Models.ServiceReportSystem;

namespace ControlTower.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Models.EmployeeManagementSystem.User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<AccessLevel> AccessLevels { get; set; }
        public DbSet<UserApplicationAccess> UserApplicationAccesses { get; set; }


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
        public DbSet<IssueReportWarehouse> IssueReportWarehouses { get; set; }
        public DbSet<IssueFoundWarehouse> IssueFoundWarehouses { get; set; }
        public DbSet<ActionTakenWarehouse> ActionTakenWarehouses { get; set; }
        public DbSet<FurtherActionTakenWarehouse> FurtherActionTakenWarehouses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Employee relationships
            modelBuilder.Entity<Models.EmployeeManagementSystem.User>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.EmployeeManagementSystem.User>()
                .HasOne(e => e.Occupation)
                .WithMany(o => o.Employees)
                .HasForeignKey(e => e.OccupationID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure self-referencing relationships for Employee
            modelBuilder.Entity<Models.EmployeeManagementSystem.User>()
                .HasOne(e => e.CreatedByEmployee)
                .WithMany(e => e.CreatedEmployees)
                .HasForeignKey(e => e.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Models.EmployeeManagementSystem.User>()
                .HasOne(e => e.UpdatedByEmployee)
                .WithMany(e => e.UpdatedEmployees)
                .HasForeignKey(e => e.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Department relationships
            modelBuilder.Entity<Department>()
                .HasOne(d => d.CreatedByEmployee)
                .WithMany(e => e.CreatedDepartments)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.UpdatedByEmployee)
                .WithMany(e => e.UpdatedDepartments)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Occupation relationships
            modelBuilder.Entity<Occupation>()
                .HasOne(o => o.CreatedByEmployee)
                .WithMany(e => e.CreatedOccupations)
                .HasForeignKey(o => o.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Occupation>()
                .HasOne(o => o.UpdatedByEmployee)
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
                .HasOne(a => a.CreatedByEmployee)
                .WithMany()
                .HasForeignKey(a => a.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.UpdatedByEmployee)
                .WithMany()
                .HasForeignKey(a => a.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure unique constraints
            modelBuilder.Entity<Application>()
                .HasIndex(a => a.ApplicationName)
                .IsUnique();

            // Configure EmployeeApplicationAccess relationships
            modelBuilder.Entity<UserApplicationAccess>()
                .HasOne(eaa => eaa.Employee)
                .WithMany()
                .HasForeignKey(eaa => eaa.EmployeeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserApplicationAccess>()
                .HasOne(eaa => eaa.Application)
                .WithMany()
                .HasForeignKey(eaa => eaa.ApplicationID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserApplicationAccess>()
                .HasOne(eaa => eaa.GrantedByEmployee)
                .WithMany()
                .HasForeignKey(eaa => eaa.GrantedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserApplicationAccess>()
                .HasOne(eaa => eaa.CreatedByEmployee)
                .WithMany()
                .HasForeignKey(eaa => eaa.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserApplicationAccess>()
                .HasOne(eaa => eaa.UpdatedByEmployee)
                .WithMany()
                .HasForeignKey(eaa => eaa.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Create unique index for Employee-Application combination
            modelBuilder.Entity<UserApplicationAccess>()
                .HasIndex(eaa => new { eaa.EmployeeID, eaa.ApplicationID })
                .IsUnique();
        
            // AccessLevel configurations
            modelBuilder.Entity<AccessLevel>()
                .HasIndex(al => al.LevelName)
                .IsUnique();
                
            modelBuilder.Entity<AccessLevel>()
                .HasOne(al => al.CreatedByEmployee)
                .WithMany()
                .HasForeignKey(al => al.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);
                
            modelBuilder.Entity<AccessLevel>()
                .HasOne(al => al.UpdatedByEmployee)
                .WithMany()
                .HasForeignKey(al => al.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Update EmployeeApplicationAccess configuration
            modelBuilder.Entity<UserApplicationAccess>()
                .HasOne(eaa => eaa.AccessLevel)
                .WithMany(al => al.UserApplicationAccesses)
                .HasForeignKey(eaa => eaa.AccessLevelID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}