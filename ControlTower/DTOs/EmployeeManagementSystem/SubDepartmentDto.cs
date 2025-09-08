using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.EmployeeManagementSystem
{
    public class CreateSubDepartmentDto
    {
        [Required]
        public Guid DepartmentID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(200)]
        public string? Remark { get; set; }

        public int? Rating { get; set; }
    }

    public class UpdateSubDepartmentDto
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        public Guid DepartmentID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(200)]
        public string? Remark { get; set; }

        public int? Rating { get; set; }
    }

    public class SubDepartmentDto
    {
        public Guid ID { get; set; }
        public Guid DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Remark { get; set; }
        public int? Rating { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public string? CreatedByName { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? UpdatedByName { get; set; }
    }
}