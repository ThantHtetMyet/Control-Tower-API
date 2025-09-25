using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class CMReportFormTypeDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
    }

    public class CreateCMReportFormTypeDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }

    public class UpdateCMReportFormTypeDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}