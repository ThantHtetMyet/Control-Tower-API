using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.EmployeeManagementSystem
{
    public class CreateCompanyDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(255)]
        public string? Remark { get; set; }

        public Guid CreatedBy { get; set; }
    }

    public class UpdateCompanyDto
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(255)]
        public string? Remark { get; set; }

        public Guid? UpdatedBy { get; set; }
    }

    public class CompanyDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Remark { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
        public int EmployeeCount { get; set; }
    }
}