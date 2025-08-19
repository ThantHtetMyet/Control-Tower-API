using System;

namespace ControlTower.DTOs.ServiceReportSystem
{
    public class MaterialUsedDto
    {
        public Guid ID { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string SerialNo { get; set; }
    }

    public class CreateMaterialUsedDto
    {
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string SerialNo { get; set; }
    }

    public class UpdateMaterialUsedDto
    {
        public Guid? ID { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string SerialNo { get; set; }
    }
}