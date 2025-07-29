namespace ControlTower.DTOs.ServiceReportSystem
{
    public class IssueReportedDto
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
    }

    public class CreateIssueReportedDto
    {
        public string Description { get; set; }
        public string Remark { get; set; }
    }

    public class UpdateIssueReportedDto
    {
        public Guid ID { get; set; }
        public string? Description { get; set; }  // Make optional
        public string? Remark { get; set; }
        public string? UpdatedBy { get; set; }    // Make optional
    }

    public class IssueFoundDto
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
    }

    public class CreateIssueFoundDto
    {
        public string Description { get; set; }
        public string Remark { get; set; }
    }


    public class ActionTakenDto
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }  // Add missing Remark property
    }

    public class CreateActionTakenDto
    {
        public string Description { get; set; }
    }

    public class UpdateActionTakenDto
    {
        public Guid ID { get; set; }
        public string? Description { get; set; }  // Make optional
        public string? Remark { get; set; }       // Add missing Remark
        public string? UpdatedBy { get; set; }    // Make optional
    }

    public class UpdateFurtherActionDto
    {
        public Guid ID { get; set; }
        public string? Description { get; set; }  // Make optional
        public string? Remark { get; set; }
        public string? UpdatedBy { get; set; }    // Make optional
    }

    public class ServiceTypeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
    }

    public class FormStatusDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
    }

    // Add missing FurtherActionDto
    public class FurtherActionDto
    {
        public Guid ID { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
    }

    public class CreateFurtherActionDto
    {
        public string Description { get; set; }
        public string Remark { get; set; }
    }

}