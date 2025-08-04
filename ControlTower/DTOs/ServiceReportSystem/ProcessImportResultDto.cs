using ControlTower.DTOs.ServiceReportSystem;

public class ProcessImportResultDto
{
    public int TotalProcessed { get; set; }
    public string FormTypeName { get; set; } // Add this property
    public ImportResultDetail LocationWarehouse { get; set; }
    public ImportResultDetail IssueReportWarehouse { get; set; }
    public ImportResultDetail IssueFoundWarehouse { get; set; }
    public ImportResultDetail ActionTakenWarehouse { get; set; }
}