using System;
using System.Collections.Generic;

namespace ControlTower.DTOs.ServiceReportSystem
{
    public class ServiceReportDto
    {
        public Guid ID { get; set; }
        public string JobNumber { get; set; }
        public string Customer { get; set; }
        public string ContactNo { get; set; }

        public Guid ProjectNoID { get; set; }
        public string ProjectNumberName { get; set; }
        public Guid SystemID { get; set; }
        public string SystemName { get; set; }
        public Guid LocationID { get; set; }
        public string LocationName { get; set; }
        public Guid FollowupActionID { get; set; }
        public string FollowupActionNo { get; set; }
        public Guid ServiceTypeID { get; set; }
        public string ServiceTypeName { get; set; }
        public Guid FormStatusID { get; set; }
        public string FormStatusName { get; set; }
        public DateTime? FailureDetectedDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUserName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedByUserName { get; set; }
        public string? ServiceTypeRemark { get; set; }
        public string? IssueReportedRemark { get; set; }
        public string? IssueFoundRemark { get; set; }
        public string? ActionTakenRemark { get; set; }
        public string? FurtherActionTakenRemark { get; set; }
        public string? FormStatusRemark { get; set; }
        
        // Add missing collections
        public List<ServiceTypeDto> ServiceType { get; set; }
        public List<FormStatusDto> FormStatus { get; set; }
        public List<IssueReportedDto> IssueReported { get; set; }
        public List<IssueFoundDto> IssueFound { get; set; }
        public List<ActionTakenDto> ActionTaken { get; set; }
        public List<FurtherActionDto> FurtherActionTaken { get; set; }
    }

    public class CreateServiceReportDto
    {
        public string JobNumber { get; set; }
        public string ContactNo { get; set; }
        public string Customer { get; set; }
        public Guid ProjectNoID { get; set; }
        public Guid SystemID { get; set; }
        public Guid LocationID { get; set; }
        public Guid FollowupActionID { get; set; }
        public DateTime? FailureDetectedDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public List<TempItem> FormStatus { get; set; }
        public List<TempItem> ServiceType { get; set; }
        public List<TempItem> IssueReported { get; set; }
        public List<TempItem> IssueFound { get; set; }
        public List<TempItem> ActionTaken { get; set; }
        public List<TempItem> FurtherAction { get; set; }
        public string CreatedBy { get; set; }  // This remains string as it's parsed later
    }

    public class TempItem
    {
        public Guid Id { get; set; }
        public string Remark { get; set; }
    }

    public class UpdateServiceReportDto
    {
        public string Customer { get; set; }
        public string ContactNo { get; set; }  // Add this field
        public Guid ProjectNoID { get; set; }
        public Guid SystemID { get; set; }
        public Guid LocationID { get; set; }
        public Guid FollowupActionID { get; set; }
        public DateTime? FailureDetectedDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        
        // Change these to match frontend structure (TempItem with Id and Remark)
        public List<TempItem>? ServiceType { get; set; }
        public List<TempItem>? FormStatus { get; set; }
        public List<TempItem>? IssueReported { get; set; }
        public List<TempItem>? IssueFound { get; set; }
        public List<TempItem>? ActionTaken { get; set; }
        public List<TempItem>? FurtherAction { get; set; }
        
        public string UpdatedBy { get; set; }
    }
}