using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMReportFormController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CMReportFormController> _logger;
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public CMReportFormController(
            ApplicationDbContext context,
            IConfiguration configuration,
            ILogger<CMReportFormController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        // GET: api/CMReportForm
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CMReportFormDto>>> GetCMReportForms()
        {
            var cmReportForms = await _context.CMReportForms
                .Include(c => c.ReportForm)
                    .ThenInclude(s => s.ReportFormType)
                .Include(c => c.FurtherActionTakenWarehouse)
                .Include(c => c.FormStatusWarehouse)
                .Include(c => c.CreatedByUser)
                .Include(c => c.UpdatedByUser)
                .Where(c => !c.IsDeleted)
                .Select(c => new CMReportFormDto
                {
                    ID = c.ID,
                    ReportFormID = c.ReportFormID,
                    CMReportFormTypeID = c.CMReportFormTypeID,
                    FurtherActionTakenID = c.FurtherActionTakenID,
                    FormstatusID = c.FormstatusID,
                    Customer = c.Customer,
                    ReportTitle = c.ReportTitle,
                    ProjectNo = c.ProjectNo,
                    IssueReportedDescription = c.IssueReportedDescription,
                    IssueFoundDescription = c.IssueFoundDescription,
                    ActionTakenDescription = c.ActionTakenDescription,
                    FailureDetectedDate = c.FailureDetectedDate,
                    ResponseDate = c.ResponseDate,
                    ArrivalDate = c.ArrivalDate,
                    CompletionDate = c.CompletionDate,
                    AttendedBy = c.AttendedBy,
                    ApprovedBy = c.ApprovedBy,
                    IsDeleted = c.IsDeleted,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate,
                    CreatedBy = c.CreatedBy,
                    UpdatedBy = c.UpdatedBy,
                    Remark = c.Remark,
                    ReportFormTypeName = c.ReportForm.ReportFormType != null ? c.ReportForm.ReportFormType.Name : null,
                    FurtherActionTakenName = c.FurtherActionTakenWarehouse.Name,
                    FormStatusName = c.FormStatusWarehouse.Name,
                    CreatedByUserName = $"{c.CreatedByUser.FirstName} {c.CreatedByUser.LastName}",
                    UpdatedByUserName = c.UpdatedByUser != null ? $"{c.UpdatedByUser.FirstName} {c.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            return Ok(cmReportForms);
        }

        // GET: api/CMReportForm/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CMReportFormDto>> GetCMReportForm(Guid id)
        {
            var cmReportForm = await _context.CMReportForms
                .Include(c => c.ReportForm)
                    .ThenInclude(s => s.ReportFormType)
                .Include(c => c.FurtherActionTakenWarehouse)
                .Include(c => c.FormStatusWarehouse)
                .Include(c => c.CreatedByUser)
                .Include(c => c.UpdatedByUser)
                .Where(c => c.ID == id && !c.IsDeleted)
                .Select(c => new CMReportFormDto
                {
                    ID = c.ID,
                    ReportFormID = c.ReportFormID,
                    CMReportFormTypeID = c.CMReportFormTypeID,
                    FurtherActionTakenID = c.FurtherActionTakenID,
                    FormstatusID = c.FormstatusID,
                    Customer = c.Customer,
                    ReportTitle = c.ReportTitle,
                    ProjectNo = c.ProjectNo,
                    IssueReportedDescription = c.IssueReportedDescription,
                    IssueFoundDescription = c.IssueFoundDescription,
                    ActionTakenDescription = c.ActionTakenDescription,
                    FailureDetectedDate = c.FailureDetectedDate,
                    ResponseDate = c.ResponseDate,
                    ArrivalDate = c.ArrivalDate,
                    CompletionDate = c.CompletionDate,
                    AttendedBy = c.AttendedBy,
                    ApprovedBy = c.ApprovedBy,
                    IsDeleted = c.IsDeleted,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate,
                    CreatedBy = c.CreatedBy,
                    UpdatedBy = c.UpdatedBy,
                    Remark = c.Remark,
                    ReportFormTypeName = c.ReportForm.ReportFormType != null ? c.ReportForm.ReportFormType.Name : null,
                    FurtherActionTakenName = c.FurtherActionTakenWarehouse.Name,
                    FormStatusName = c.FormStatusWarehouse.Name,
                    CreatedByUserName = $"{c.CreatedByUser.FirstName} {c.CreatedByUser.LastName}",
                    UpdatedByUserName = c.UpdatedByUser != null ? $"{c.UpdatedByUser.FirstName} {c.UpdatedByUser.LastName}" : null
                })
                .FirstOrDefaultAsync();

            if (cmReportForm == null)
            {
                return NotFound(new { message = "CM Report Form not found." });
            }

            return Ok(cmReportForm);
        }

        // GET: api/CMReportForm/ByReportForm/5
        [HttpGet("ByReportForm/{reportFormId}")]
        public async Task<ActionResult<IEnumerable<CMReportFormDto>>> GetCMReportFormsByReportForm(Guid reportFormId)
        {
            var cmReportForms = await _context.CMReportForms
                .Include(c => c.ReportForm)
                    .ThenInclude(s => s.ReportFormType)
                .Include(c => c.FurtherActionTakenWarehouse)
                .Include(c => c.FormStatusWarehouse)
                .Include(c => c.CreatedByUser)
                .Include(c => c.UpdatedByUser)
                .Where(c => c.ReportFormID == reportFormId && !c.IsDeleted)
                .Select(c => new CMReportFormDto
                {
                    ID = c.ID,
                    ReportFormID = c.ReportFormID,
                    FurtherActionTakenID = c.FurtherActionTakenID,
                    FormstatusID = c.FormstatusID,
                    Customer = c.Customer,
                    ProjectNo = c.ProjectNo,
                    // Removed: SystemDescription = c.SystemDescription,
                    IssueReportedDescription = c.IssueReportedDescription,
                    IssueFoundDescription = c.IssueFoundDescription,
                    ActionTakenDescription = c.ActionTakenDescription,
                    FailureDetectedDate = c.FailureDetectedDate,
                    ResponseDate = c.ResponseDate,
                    ArrivalDate = c.ArrivalDate,
                    CompletionDate = c.CompletionDate,
                    AttendedBy = c.AttendedBy,
                    ApprovedBy = c.ApprovedBy,
                    IsDeleted = c.IsDeleted,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate,
                    CreatedBy = c.CreatedBy,
                    UpdatedBy = c.UpdatedBy,
                    ReportFormTypeName = c.ReportForm.ReportFormType != null ? c.ReportForm.ReportFormType.Name : null,
                    FurtherActionTakenName = c.FurtherActionTakenWarehouse.Name,
                    FormStatusName = c.FormStatusWarehouse.Name,
                    CreatedByUserName = $"{c.CreatedByUser.FirstName} {c.CreatedByUser.LastName}",
                    UpdatedByUserName = c.UpdatedByUser != null ? $"{c.UpdatedByUser.FirstName} {c.UpdatedByUser.LastName}" : null,
                    JobNo = c.ReportForm.JobNo // Add JobNo from parent ReportForm
                })
                .ToListAsync();

            return Ok(cmReportForms);
        }

        // POST: api/CMReportForm
        [HttpPost]
        public async Task<ActionResult<CMReportFormDto>> CreateCMReportForm(CreateCMReportFormDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate foreign key relationships
            var reportFormExists = await _context.ReportForms
                .AnyAsync(r => r.ID == createDto.ReportFormID && !r.IsDeleted);
            if (!reportFormExists)
            {
                return BadRequest(new { message = "Invalid Report Form ID." });
            }

            // Validate CMReportFormTypeID
            var cmReportFormTypeExists = await _context.CMReportFormTypes
                .AnyAsync(c => c.ID == createDto.CMReportFormTypeID && !c.IsDeleted);
            if (!cmReportFormTypeExists)
            {
                return BadRequest(new { message = "Invalid CM Report Form Type ID." });
            }

            // Only validate FurtherActionTakenID if it has a value (allow null/empty)
            if (createDto.FurtherActionTakenID.HasValue)
            {
                var furtherActionExists = await _context.FurtherActionTakenWarehouses
                    .AnyAsync(f => f.ID == createDto.FurtherActionTakenID && !f.IsDeleted);
                if (!furtherActionExists)
                {
                    return BadRequest(new { message = "Invalid Further Action Taken ID." });
                }
            }

            var formStatusExists = await _context.FormStatusWarehouses
                .AnyAsync(f => f.ID == createDto.FormstatusID && !f.IsDeleted);
            if (!formStatusExists)
            {
                return BadRequest(new { message = "Invalid Form Status ID." });
            } 

            var userExists = await _context.Users
                .AnyAsync(u => u.ID == createDto.CreatedBy && !u.IsDeleted);
            if (!userExists)
            {
                return BadRequest(new { message = "Invalid User ID." });
            }

            var cmReportForm = new CMReportForm
            {
                ID = Guid.NewGuid(),
                ReportFormID = createDto.ReportFormID,
                CMReportFormTypeID = createDto.CMReportFormTypeID,
                FurtherActionTakenID = createDto.FurtherActionTakenID,
                FormstatusID = createDto.FormstatusID,
                Customer = createDto.Customer,
                ReportTitle = createDto.ReportTitle,
                ProjectNo = createDto.ProjectNo,
                IssueReportedDescription = createDto.IssueReportedDescription,
                IssueFoundDescription = createDto.IssueFoundDescription,
                ActionTakenDescription = createDto.ActionTakenDescription,
                FailureDetectedDate = createDto.FailureDetectedDate,
                ResponseDate = createDto.ResponseDate,
                ArrivalDate = createDto.ArrivalDate,
                CompletionDate = createDto.CompletionDate,
                AttendedBy = createDto.AttendedBy,
                ApprovedBy = createDto.ApprovedBy,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = createDto.CreatedBy,
                Remark = createDto.Remark,
                UpdatedBy = null
            };

            _context.CMReportForms.Add(cmReportForm);
            await _context.SaveChangesAsync();

            // Return a simple DTO with just the essential data instead of calling GetCMReportForm
            var responseDto = new CMReportFormDto
            {
                ID = cmReportForm.ID,
                ReportFormID = cmReportForm.ReportFormID,
                CMReportFormTypeID = cmReportForm.CMReportFormTypeID,
                FurtherActionTakenID = cmReportForm.FurtherActionTakenID,
                FormstatusID = cmReportForm.FormstatusID,
                Customer = cmReportForm.Customer,
                ReportTitle = cmReportForm.ReportTitle,
                ProjectNo = cmReportForm.ProjectNo,
                IssueReportedDescription = cmReportForm.IssueReportedDescription,
                IssueFoundDescription = cmReportForm.IssueFoundDescription,
                ActionTakenDescription = cmReportForm.ActionTakenDescription,
                FailureDetectedDate = cmReportForm.FailureDetectedDate,
                ResponseDate = cmReportForm.ResponseDate,
                ArrivalDate = cmReportForm.ArrivalDate,
                CompletionDate = cmReportForm.CompletionDate,
                AttendedBy = cmReportForm.AttendedBy,
                ApprovedBy = cmReportForm.ApprovedBy,
                IsDeleted = cmReportForm.IsDeleted,
                CreatedDate = cmReportForm.CreatedDate,
                UpdatedDate = cmReportForm.UpdatedDate,
                CreatedBy = cmReportForm.CreatedBy,
                UpdatedBy = cmReportForm.UpdatedBy,
                Remark = cmReportForm.Remark
            };

            return CreatedAtAction(nameof(GetCMReportForm), new { id = cmReportForm.ID }, responseDto);
        }

        // PUT: api/CMReportForm/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCMReportForm(Guid id, UpdateCMReportFormDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cmReportForm = await _context.CMReportForms
                .FirstOrDefaultAsync(c => c.ID == id && !c.IsDeleted);

            if (cmReportForm == null)
            {
                return NotFound(new { message = "CM Report Form not found." });
            }

            // Validate foreign key relationships - only validate FurtherActionTakenID if it has a value
            if (updateDto.FurtherActionTakenID.HasValue)
            {
                var furtherActionExists = await _context.FurtherActionTakenWarehouses
                    .AnyAsync(f => f.ID == updateDto.FurtherActionTakenID && !f.IsDeleted);
                if (!furtherActionExists)
                {
                    return BadRequest(new { message = "Invalid Further Action Taken ID." });
                }
            }

            var formStatusExists = await _context.FormStatusWarehouses
                .AnyAsync(f => f.ID == updateDto.FormstatusID && !f.IsDeleted);
            if (!formStatusExists)
            {
                return BadRequest(new { message = "Invalid Form Status ID." });
            }

            if (updateDto.UpdatedBy.HasValue)
            {
                var userExists = await _context.Users
                    .AnyAsync(u => u.ID == updateDto.UpdatedBy && !u.IsDeleted);
                if (!userExists)
                {
                    return BadRequest(new { message = "Invalid Updated By User ID." });
                }
            }

            // Update properties
            cmReportForm.FurtherActionTakenID = updateDto.FurtherActionTakenID;
            cmReportForm.FormstatusID = updateDto.FormstatusID;
            cmReportForm.Customer = updateDto.Customer;
            cmReportForm.ProjectNo = updateDto.ProjectNo;
            cmReportForm.IssueReportedDescription = updateDto.IssueReportedDescription;
            cmReportForm.IssueFoundDescription = updateDto.IssueFoundDescription;
            cmReportForm.ActionTakenDescription = updateDto.ActionTakenDescription;
            cmReportForm.FailureDetectedDate = updateDto.FailureDetectedDate;
            cmReportForm.ResponseDate = updateDto.ResponseDate;
            cmReportForm.ArrivalDate = updateDto.ArrivalDate;
            cmReportForm.CompletionDate = updateDto.CompletionDate;
            cmReportForm.AttendedBy = updateDto.AttendedBy;
            cmReportForm.ApprovedBy = updateDto.ApprovedBy;
            cmReportForm.UpdatedDate = DateTime.UtcNow;
            cmReportForm.UpdatedBy = updateDto.UpdatedBy;
            cmReportForm.Remark = updateDto.Remark;
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "CM Report Form updated successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The record was modified by another user. Please refresh and try again." });
            }
        }

        // DELETE: api/CMReportForm/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCMReportForm(Guid id)
        {
            var cmReportForm = await _context.CMReportForms
                .FirstOrDefaultAsync(c => c.ID == id && !c.IsDeleted);

            if (cmReportForm == null)
            {
                return NotFound(new { message = "CM Report Form not found." });
            }

            // Soft delete
            cmReportForm.IsDeleted = true;
            cmReportForm.UpdatedDate = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "CM Report Form deleted successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The record was modified by another user. Please refresh and try again." });
            }
        }

        [HttpPost("{id}/generate-pdf")]
        public async Task<IActionResult> GenerateCmReportPdf(Guid id, CancellationToken cancellationToken)
        {
            var reportForm = await _context.ReportForms
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ID == id && !r.IsDeleted, cancellationToken);

            if (reportForm == null)
            {
                return NotFound($"Report form with ID {id} was not found.");
            }

            var requestedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "api_user";

            try
            {
                var result = await GeneratePdfAsync(id, requestedBy, cancellationToken);
                var downloadName = result.FileName ?? $"CM_Report_{reportForm.JobNo ?? id.ToString()}.pdf";
                return File(result.FileContent, "application/pdf", downloadName);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(504, "Timed out waiting for PDF generation.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate CM report for {ReportId}", id);
                return StatusCode(500, $"Failed to generate PDF: {ex.Message}");
            }
        }

        // POST: api/CMReportForm/{id}/generate-final-report-pdf
        // New endpoint for generating final report PDF with signatures
        [HttpPost("{id}/generate-final-report-pdf")]
        public async Task<IActionResult> GenerateCmFinalReportPdf(Guid id, CancellationToken cancellationToken)
        {
            var reportForm = await _context.ReportForms
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ID == id && !r.IsDeleted, cancellationToken);

            if (reportForm == null)
            {
                return NotFound($"Report form with ID {id} was not found.");
            }

            var requestedBy = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "api_user";

            try
            {
                var result = await GenerateFinalReportPdfAsync(id, requestedBy, cancellationToken);
                
                // Validate result
                if (result == null)
                {
                    _logger.LogError("PDF generation returned null result for ReportForm ID: {ReportId}", id);
                    return StatusCode(500, "PDF generation failed - no result returned.");
                }
                
                if (result.FileContent == null || result.FileContent.Length == 0)
                {
                    _logger.LogError("PDF generation returned empty file content for ReportForm ID: {ReportId}", id);
                    return StatusCode(500, "PDF generation failed - empty file returned.");
                }
                
                if (string.IsNullOrWhiteSpace(result.FileName))
                {
                    _logger.LogError("PDF generation returned no filename for ReportForm ID: {ReportId}", id);
                    return StatusCode(500, "PDF generation failed - no filename returned.");
                }
                
                _logger.LogInformation("PDF generation successful for ReportForm ID: {ReportId}, File: {FileName}, Size: {Size} bytes", 
                    id, result.FileName, result.FileContent.Length);
                
                // Use the EXACT same pattern as manual upload (UploadFinalReport endpoint)
                var basePath = _configuration["ReportManagementSystemFileStorage:BasePath"] ?? "C:\\Temp\\ReportFormFiles";
                var reportFolder = Path.Combine(basePath, id.ToString());
                var finalReportFolder = Path.Combine(reportFolder, "ReportForm_FinalReport");
                Directory.CreateDirectory(finalReportFolder);

                // Generate filename with timestamp (EXACTLY like manual upload)
                var originalName = $"CM_FinalReport_{reportForm.JobNo ?? id.ToString()}.pdf";
                var safeName = $"{DateTime.UtcNow:yyyyMMddHHmmssfff}_{originalName}";
                var savedPath = Path.Combine(finalReportFolder, safeName);
                var relativePath = Path.Combine(id.ToString(), "ReportForm_FinalReport", safeName);

                // Save the PDF file (copying from Python's temp location)
                await System.IO.File.WriteAllBytesAsync(savedPath, result.FileContent, cancellationToken);

                // Clean up Python's temp file
                var pythonTempPath = Path.Combine(GetPdfOutputDirectory(), result.FileName);
                if (System.IO.File.Exists(pythonTempPath))
                {
                    try
                    {
                        System.IO.File.Delete(pythonTempPath);
                        _logger.LogInformation("Cleaned up temporary PDF from Python generator: {TempPath}", pythonTempPath);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to delete temporary PDF file: {TempPath}", pythonTempPath);
                    }
                }

                // Get user ID and create database record (EXACTLY like manual upload)
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!Guid.TryParse(userIdClaim, out Guid userId))
                {
                    return Unauthorized(new { message = "User context is not available." });
                }

                // Create database record with EXACT same structure as manual upload
                var entity = new ReportFormFinalReport
                {
                    ID = Guid.NewGuid(),
                    ReportFormID = id,
                    AttachmentName = originalName,  // User-friendly name (matching manual upload)
                    AttachmentPath = relativePath,  // Relative path with timestamp (matching manual upload)
                    IsDeleted = false,
                    UploadedDate = DateTime.UtcNow,
                    UploadedBy = userId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = userId
                };

                _context.ReportFormFinalReports.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Ok(new { 
                    message = "Final report PDF generated and saved successfully.",
                    fileName = originalName,
                    reportFormId = id
                });
            }
            catch (OperationCanceledException)
            {
                return StatusCode(504, "Timed out waiting for final report PDF generation.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate final report PDF for {ReportId}", id);
                return StatusCode(500, $"Failed to generate final report PDF: {ex.Message}");
            }
        }

        private async Task<PdfGenerationResult> GenerateFinalReportPdfAsync(Guid reportId, string requestedBy, CancellationToken cancellationToken)
        {
            var mqttFactory = new MqttFactory();
            using var mqttClient = mqttFactory.CreateMqttClient();
            var mqttOptions = BuildMqttOptions();

            // Use different topic for final report generation with signatures
            var topicKey = "cm_reportform_signature_pdf";
            var statusTopic = $"controltower/{topicKey}_status/{reportId}";
            var requestTopic = $"controltower/{topicKey}/{reportId}";

            var completionSource = new TaskCompletionSource<PdfStatusMessage>(TaskCreationOptions.RunContinuationsAsynchronously);

            mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                _logger.LogInformation("[MQTT RECEIVED] Topic: {Topic}", e.ApplicationMessage.Topic);
                
                if (e.ApplicationMessage.Topic == statusTopic)
                {
                    var payload = e.ApplicationMessage.ConvertPayloadToString();
                    _logger.LogInformation("[MQTT PAYLOAD] {Payload}", payload);
                    
                    try
                    {
                        var status = JsonSerializer.Deserialize<PdfStatusMessage>(payload, _jsonOptions);
                        if (status != null && !string.IsNullOrWhiteSpace(status.Status))
                        {
                            var normalized = status.Status.ToLowerInvariant();
                            _logger.LogInformation("[MQTT STATUS] Status: {Status}, Message: {Message}, FileName: {FileName}", 
                                status.Status, status.Message, status.FileName);
                                
                            if (normalized == "completed" || normalized == "failed")
                            {
                                _logger.LogInformation("[MQTT] Setting completion result with status: {Status}", normalized);
                                completionSource.TrySetResult(status);
                            }
                        }
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError(jsonEx, "Failed to parse CM Final Report PDF status payload: {Payload}", payload);
                    }
                }
                else
                {
                    _logger.LogWarning("[MQTT MISMATCH] Received message on topic {ReceivedTopic}, expected {ExpectedTopic}", 
                        e.ApplicationMessage.Topic, statusTopic);
                }

                return Task.CompletedTask;
            };

            await mqttClient.ConnectAsync(mqttOptions, cancellationToken);
            _logger.LogInformation("[MQTT] Connected to broker, subscribing to: {StatusTopic}", statusTopic);
            await mqttClient.SubscribeAsync(statusTopic, MqttQualityOfServiceLevel.AtLeastOnce, cancellationToken);
            _logger.LogInformation("[MQTT] Subscribed successfully, waiting for status updates");

            var payload = JsonSerializer.Serialize(new
            {
                report_id = reportId.ToString(),
                requested_by = requestedBy,
                timestamp = DateTime.UtcNow.ToString("o"),
                report_type = "cm_final_report"
            });

            var publishMessage = new MqttApplicationMessageBuilder()
                .WithTopic(requestTopic)
                .WithPayload(payload)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .Build();

            _logger.LogInformation("[MQTT] Publishing request to: {RequestTopic}", requestTopic);
            _logger.LogInformation("[MQTT] Request payload: {Payload}", payload);
            await mqttClient.PublishAsync(publishMessage, cancellationToken);
            _logger.LogInformation("[MQTT] Request published, waiting for response on: {StatusTopic}", statusTopic);

            var timeoutSeconds = _configuration.GetValue("PDFGenerator:StatusTimeoutSeconds", 120);
            using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeoutCts.CancelAfter(TimeSpan.FromSeconds(timeoutSeconds));

            PdfStatusMessage statusMessage;
            try
            {
                using (timeoutCts.Token.Register(() => completionSource.TrySetCanceled(), useSynchronizationContext: false))
                {
                    statusMessage = await completionSource.Task;
                }
            }
            finally
            {
                if (mqttClient.IsConnected)
                {
                    await mqttClient.UnsubscribeAsync(statusTopic, cancellationToken);
                    await mqttClient.DisconnectAsync(cancellationToken: cancellationToken);
                }
            }

            if (!statusMessage.Status.Equals("completed", StringComparison.OrdinalIgnoreCase))
            {
                var errorMessage = statusMessage.Message ?? "Final report PDF generation failed.";
                throw new InvalidOperationException(errorMessage);
            }

            var fileName = statusMessage.FileName;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new InvalidOperationException("Final report PDF generation completed but no file name was provided.");
            }

            var outputDirectory = GetPdfOutputDirectory();
            var filePath = Path.Combine(outputDirectory, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException($"Generated final report PDF not found at {filePath}.");
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath, cancellationToken);
            return new PdfGenerationResult(fileBytes, fileName);
        }

        private async Task<PdfGenerationResult> GeneratePdfAsync(Guid reportId, string requestedBy, CancellationToken cancellationToken)
        {
            var mqttFactory = new MqttFactory();
            using var mqttClient = mqttFactory.CreateMqttClient();
            var mqttOptions = BuildMqttOptions();

            var topicKey = "cm_reportform_pdf";
            var statusTopic = $"controltower/{topicKey}_status/{reportId}";
            var requestTopic = $"controltower/{topicKey}/{reportId}";

            var completionSource = new TaskCompletionSource<PdfStatusMessage>(TaskCreationOptions.RunContinuationsAsynchronously);

            mqttClient.ApplicationMessageReceivedAsync += e =>
            {
                if (e.ApplicationMessage.Topic == statusTopic)
                {
                    var payload = e.ApplicationMessage.ConvertPayloadToString();
                    try
                    {
                        var status = JsonSerializer.Deserialize<PdfStatusMessage>(payload, _jsonOptions);
                        if (status != null && !string.IsNullOrWhiteSpace(status.Status))
                        {
                            var normalized = status.Status.ToLowerInvariant();
                            if (normalized == "completed" || normalized == "failed")
                            {
                                completionSource.TrySetResult(status);
                            }
                        }
                    }
                    catch (JsonException jsonEx)
                    {
                        _logger.LogError(jsonEx, "Failed to parse CM PDF status payload: {Payload}", payload);
                    }
                }

                return Task.CompletedTask;
            };

            await mqttClient.ConnectAsync(mqttOptions, cancellationToken);
            await mqttClient.SubscribeAsync(statusTopic, MqttQualityOfServiceLevel.AtLeastOnce, cancellationToken);

            var payload = JsonSerializer.Serialize(new
            {
                report_id = reportId.ToString(),
                requested_by = requestedBy,
                timestamp = DateTime.UtcNow.ToString("o")
            });

            var publishMessage = new MqttApplicationMessageBuilder()
                .WithTopic(requestTopic)
                .WithPayload(payload)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .Build();

            await mqttClient.PublishAsync(publishMessage, cancellationToken);

            var timeoutSeconds = _configuration.GetValue("PDFGenerator:StatusTimeoutSeconds", 120);
            using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeoutCts.CancelAfter(TimeSpan.FromSeconds(timeoutSeconds));

            PdfStatusMessage statusMessage;
            try
            {
                using (timeoutCts.Token.Register(() => completionSource.TrySetCanceled(), useSynchronizationContext: false))
                {
                    statusMessage = await completionSource.Task;
                }
            }
            finally
            {
                if (mqttClient.IsConnected)
                {
                    await mqttClient.UnsubscribeAsync(statusTopic, cancellationToken);
                    await mqttClient.DisconnectAsync(cancellationToken: cancellationToken);
                }
            }

            if (!statusMessage.Status.Equals("completed", StringComparison.OrdinalIgnoreCase))
            {
                var errorMessage = statusMessage.Message ?? "PDF generation failed.";
                throw new InvalidOperationException(errorMessage);
            }

            var fileName = statusMessage.FileName;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new InvalidOperationException("PDF generation completed but no file name was provided.");
            }

            var outputDirectory = GetPdfOutputDirectory();
            var filePath = Path.Combine(outputDirectory, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException($"Generated PDF not found at {filePath}.");
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath, cancellationToken);
            return new PdfGenerationResult(fileBytes, fileName);
        }

        private MqttClientOptions BuildMqttOptions()
        {
            var host = _configuration["MQTT:Host"] ?? "localhost";
            var port = _configuration.GetValue("MQTT:Port", 1883);
            var username = _configuration["MQTT:Username"];
            var password = _configuration["MQTT:Password"];

            var builder = new MqttClientOptionsBuilder()
                .WithClientId($"controltower_cm_api_{Guid.NewGuid():N}")
                .WithTcpServer(host, port)
                .WithCleanSession();

            if (!string.IsNullOrEmpty(username))
            {
                builder.WithCredentials(username, password);
            }

            return builder.Build();
        }

        private string GetPdfOutputDirectory()
        {
            var configuredPath = _configuration["PDFGenerator:OutputDirectory"];
            if (string.IsNullOrWhiteSpace(configuredPath))
            {
                // Updated to match new clean folder structure
                configuredPath = Path.Combine("ControlTower_Python", "PDF_Generator", "PDF_File");
            }

            var resolved = ResolvePdfPath(configuredPath);
            Directory.CreateDirectory(resolved);
            return resolved;
        }

        private static string ResolvePdfPath(string configuredPath)
        {
            if (Path.IsPathRooted(configuredPath))
            {
                return Path.GetFullPath(configuredPath);
            }

            var solutionRoot = FindSolutionRoot();
            if (!string.IsNullOrEmpty(solutionRoot))
            {
                return Path.GetFullPath(Path.Combine(solutionRoot, configuredPath));
            }

            return Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, configuredPath));
        }

        private static string? FindSolutionRoot()
        {
            var current = new DirectoryInfo(AppContext.BaseDirectory);
            while (current != null)
            {
                if (System.IO.File.Exists(Path.Combine(current.FullName, "ControlTower.sln")))
                {
                    return current.FullName;
                }
                current = current.Parent;
            }

            return null;
        }

        private sealed record PdfGenerationResult(byte[] FileContent, string FileName);

        private sealed class PdfStatusMessage
        {
            [JsonPropertyName("report_id")]
            public string? ReportId { get; set; }

            [JsonPropertyName("status")]
            public string Status { get; set; } = string.Empty;

            [JsonPropertyName("message")]
            public string? Message { get; set; }

            [JsonPropertyName("file_name")]
            public string? FileName { get; set; }
        }
    }
}
