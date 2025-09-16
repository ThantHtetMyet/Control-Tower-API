using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class StationNameWarehouseDto
    {
        public Guid ID { get; set; }
        public Guid SystemNameWarehouseID { get; set; }
        public string SystemNameWarehouseName { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public string CreatedByUserName { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? UpdatedByUserName { get; set; }
    }

    public class CreateStationNameWarehouseDto
    {
        [Required(ErrorMessage = "SystemNameID is required")]
        public Guid SystemNameWarehouseID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters")]
        public string Name { get; set; }
    }

    public class UpdateStationNameWarehouseDto
    {
        [Required(ErrorMessage = "SystemNameID is required")]
        public Guid SystemNameWarehouseID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters")]
        public string Name { get; set; }
    }
}