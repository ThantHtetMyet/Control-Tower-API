using System.ComponentModel.DataAnnotations;

namespace ControlTower.DTOs.ServiceReportSystem
{
    public class ProcessImportDataDto
    {
        [Required]
        public Guid ImportFileRecordId { get; set; }
        
        [Required]
        public string UserId { get; set; }

        [Required]
        public string ImportFormType { get; set; }
    }


    public class ImportResultDetail
    {
        public int TotalFound { get; set; }
        public int ExistingCount { get; set; }
        public int NewlyAdded { get; set; }
    }
}