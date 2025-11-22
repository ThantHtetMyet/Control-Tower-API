using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.ReportManagementSystem
{
    public class PMReportFormRTU
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("ReportForm")]
        public Guid ReportFormID { get; set; }

        [ForeignKey("PMReportFormType")]
        public Guid PMReportFormTypeID { get; set; }

        public string? ReportTitle { get; set; }
        public string? ProjectNo { get; set; }
        public string? Customer { get; set; }
        public DateTime? DateOfService { get; set; }
        public string? CleaningOfCabinet { get; set; }
        public string? Remarks { get; set; }
        public string? AttendedBy { get; set; }
        public string? ApprovedBy { get; set; }
        [ForeignKey("FormStatusWarehouse")]
        public Guid FormstatusID { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid? CreatedBy { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }

        // Navigation properties
        public virtual ReportForm? ReportForm { get; set; }
        public virtual PMReportFormType? PMReportFormType { get; set; }
        public virtual FormStatusWarehouse? FormStatusWarehouse { get; set; }
        public virtual User? CreatedByUser { get; set; }
        public virtual User? UpdatedByUser { get; set; }

        // Related entities
        public virtual ICollection<PMMainRtuCabinet> PMMainRtuCabinets { get; set; } = new List<PMMainRtuCabinet>();
        public virtual ICollection<PMChamberMagneticContact> PMChamberMagneticContacts { get; set; } = new List<PMChamberMagneticContact>();
        public virtual ICollection<PMRTUCabinetCooling> PMRTUCabinetCoolings { get; set; } = new List<PMRTUCabinetCooling>();
        public virtual ICollection<PMDVREquipment> PMDVREquipments { get; set; } = new List<PMDVREquipment>();
    }
}
