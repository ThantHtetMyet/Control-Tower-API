using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ReportManagementSystem
{
    public class ServerHostNameWarehouseDto
    {
        public Guid ID { get; set; }
        public Guid StationNameID { get; set; }
        public string StationNameWarehouseName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public class CreateServerHostNameWarehouseDto
    {
        [Required(ErrorMessage = "StationNameID is required")]
        public Guid StationNameID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters")]
        public string Name { get; set; } = string.Empty;
    }

    public class UpdateServerHostNameWarehouseDto
    {
        [Required(ErrorMessage = "StationNameID is required")]
        public Guid StationNameID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters")]
        public string Name { get; set; } = string.Empty;
    }
}

