
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;
using Microsoft.EntityFrameworkCore;

namespace ControlTower.Models.ServiceReportSystem
{
    public class ServiceType
    {
        [Key]
        public Guid ID { get; set; }

        public string? Description { get; set; }
        public string? Remark { get; set; }

        [ForeignKey("ServiceTypeWarehouse")]
        public Guid ServiceTypeWarehouseID { get; set; }
        public ServiceTypeWarehouse ServiceTypeWarehouse { get; set; }

        [ForeignKey("ServiceReportForm")]
        public Guid ServiceReportFormID { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid? CreatedBy { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }
        public User UpdatedByUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}