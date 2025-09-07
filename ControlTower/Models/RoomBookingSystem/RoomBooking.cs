using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControlTower.Models.EmployeeManagementSystem;

namespace ControlTower.Models.RoomBookingSystem
{
    public class RoomBooking
    {
        [Key]
        public Guid ID { get; set; }

        [Required]
        [ForeignKey("Room")]
        public Guid RoomID { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        [ForeignKey("Status")]
        public Guid StatusID { get; set; }

        [Required]
        [ForeignKey("RequestedByUser")]
        public Guid RequestedBy { get; set; }

        [ForeignKey("ApprovedByUser")]
        public Guid? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }

        [StringLength(300)]
        public string? RejectionReason { get; set; }

        [StringLength(300)]
        public string? CancellationReason { get; set; }

        [ForeignKey("CancelledByUser")]
        public Guid? CancelledBy { get; set; }

        [StringLength(200)]
        public string? RecurrenceRule { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [ForeignKey("CreatedByUser")]
        public Guid? CreatedBy { get; set; }

        [ForeignKey("UpdatedByUser")]
        public Guid? UpdatedBy { get; set; }

        // Add RowVersion property for optimistic concurrency control
        [Timestamp]
        public byte[] RowVersion { get; set; }

        // Navigation properties
        public virtual Room Room { get; set; }
        public virtual RoomBookingStatus Status { get; set; }
        public virtual User RequestedByUser { get; set; }
        public virtual User? ApprovedByUser { get; set; }
        public virtual User? CancelledByUser { get; set; }
        public virtual User? CreatedByUser { get; set; }
        public virtual User? UpdatedByUser { get; set; }
    }
}