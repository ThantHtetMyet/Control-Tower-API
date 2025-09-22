using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class RTUPMReportFormDto
    {
        public Guid ID { get; set; }
        public Guid ReportFormID { get; set; }
        public Guid PMReportFormTypeID { get; set; }
        public string? ProjectNo { get; set; }
        public string? Customer { get; set; }
        public DateTime? DateOfService { get; set; }
        public string? CleaningOfCabinet { get; set; }
        public string? Remarks { get; set; }
        public string? AttendedBy { get; set; }
        public string? ApprovedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        // Navigation properties for display
        public string? PMReportFormTypeName { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
    }

    public class CreateRTUPMReportFormDto
    {
        [Required]
        public Guid ReportFormID { get; set; }

        [Required]
        public Guid PMReportFormTypeID { get; set; }

        public string? ProjectNo { get; set; }
        public string? Customer { get; set; }
        public DateTime? DateOfService { get; set; }
        public string? CleaningOfCabinet { get; set; }
        public string? Remarks { get; set; }
        public string? AttendedBy { get; set; }
        public string? ApprovedBy { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }
    }

    public class UpdateRTUPMReportFormDto
    {
        // Basic RTU PM Report Form data
        public PMReportFormRTUDto? PmReportFormRTU { get; set; }

        // Related table data
        public List<PMMainRtuCabinetDto>? PmMainRtuCabinet { get; set; }
        public List<PMChamberMagneticContactDto>? PmChamberMagneticContact { get; set; }
        public List<PMRTUCabinetCoolingDto>? PmRTUCabinetCooling { get; set; }
        public List<PMDVREquipmentDto>? PmDVREquipment { get; set; }

        // Image data (optional for updates)
        public List<ReportFormImageDto>? PmMainRtuCabinetImages { get; set; }
        public List<ReportFormImageDto>? PmChamberMagneticContactImages { get; set; }
        public List<ReportFormImageDto>? PmRTUCabinetCoolingImages { get; set; }
        public List<ReportFormImageDto>? PmDVREquipmentImages { get; set; }

        public Guid? UpdatedBy { get; set; }
    }

}