
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;
using Microsoft.EntityFrameworkCore;

namespace ControlTower.Models.ServiceReportSystem
{
    public class FormStatus
    {
        [Key]
        public Guid ID { get; set; }
        public string? Description { get; set; }
        public string? Remark { get; set; }

        [ForeignKey("FormStatusWarehouse")]
        public Guid FormStatusWarehouseID { get; set; }
        public FormStatusWarehouses FormStatusWarehouse { get; set; }

        [ForeignKey("ServiceReportForm")]
        public Guid ServiceReportFormID { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid? CreatedBy { get; set; }

        public User CreatedByUser { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }
        public User UpdatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        
        public bool IsDeleted { get; set; }
    }
}