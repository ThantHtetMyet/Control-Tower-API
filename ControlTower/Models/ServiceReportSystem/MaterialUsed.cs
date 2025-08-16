using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;
using Microsoft.EntityFrameworkCore;

namespace ControlTower.Models.ServiceReportSystem
{
    public class MaterialUsed
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("ServiceReportForm")]
        public Guid ServiceReportFormID { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string? Description { get; set; }

        public string? SerialNo { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid? CreatedBy { get; set; }
        public User? CreatedByUser { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }
        public User? UpdatedByUser { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}