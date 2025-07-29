
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;
using Microsoft.EntityFrameworkCore;

namespace ControlTower.Models.ServiceReportSystem
{
    public class ServiceReportForm
    {
        [Key]
        public Guid ID { get; set; }

        [StringLength(100)]
        public string? JobNumber { get; set; }

        public string Customer { get; set; }
        public string ContactNo { get; set; }

        [ForeignKey("ProjectNo")]
        public Guid ProjectNoID { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public ProjectNoWarehouse ProjectNo { get; set; }

        [ForeignKey("System")]
        public Guid SystemID { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public SystemWarehouse System { get; set; }

        [ForeignKey("Location")]
        public Guid LocationID { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public LocationWarehouse Location { get; set; }

        [ForeignKey("FollowupAction")]
        public Guid FollowupActionID { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public FollowupActionWarehouse FollowupAction { get; set; }

        public DateTime? FailureDetectedDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        
        [ForeignKey("CreatedByUser")]
        public Guid? CreatedBy { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User? CreatedByUser { get; set; }
        
        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public User? UpdatedByUser { get; set; }

        // Update related collections to use Guid
        public ICollection<IssueReported>? IssueReported { get; set; }
        public ICollection<IssueFound>? IssueFound { get; set; }
        public ICollection<ActionTaken>? ActionTaken { get; set; }
        public ICollection<ServiceType>? ServiceType { get; set; }
        public ICollection<FurtherActionTaken>? FurtherActionTaken { get; set; }
        public ICollection<FormStatus>? FormStatus { get; set; }
    }
}