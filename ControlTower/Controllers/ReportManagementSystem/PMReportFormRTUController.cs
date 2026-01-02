using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Threading;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PMReportFormRTUController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PMReportFormRTUController> _logger;
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public PMReportFormRTUController(
            ApplicationDbContext context,
            IConfiguration configuration,
            ILogger<PMReportFormRTUController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        // GET: api/PMReportForm
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PMReportFormRTUDto>>> GetPMReportFormRTU()
        {
            var PMReportFormRTU = await _context.PMReportFormRTU
                .Include(p => p.PMReportFormType)
                .Include(p => p.FormStatusWarehouse)
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Where(p => !p.IsDeleted)
                .Select(p => new PMReportFormRTUDto
                {
                    ID = p.ID,
                    ReportFormID = p.ReportFormID,
                    PMReportFormTypeID = p.PMReportFormTypeID,
                    FormstatusID = p.FormstatusID,
                    ProjectNo = p.ProjectNo,
                    Customer = p.Customer,
                    DateOfService = p.DateOfService,
                    CleaningOfCabinet = p.CleaningOfCabinet,
                    Remarks = p.Remarks,
                    AttendedBy = p.AttendedBy,
                    ApprovedBy = p.ApprovedBy,
                    IsDeleted = p.IsDeleted,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    CreatedBy = p.CreatedBy,
                    UpdatedBy = p.UpdatedBy,
                    PMReportFormTypeName = p.PMReportFormType != null ? p.PMReportFormType.Name : null,
                    FormStatusName = p.FormStatusWarehouse != null ? p.FormStatusWarehouse.Name : null,
                    CreatedByUserName = p.CreatedByUser != null ? $"{p.CreatedByUser.FirstName} {p.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = p.UpdatedByUser != null ? $"{p.UpdatedByUser.FirstName} {p.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            return Ok(PMReportFormRTU);
        }

        // GET: api/PMReportForm/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PMReportFormRTUDto>> GetPMReportForm(Guid id)
        {
            var pmReportForm = await _context.PMReportFormRTU
                .Include(p => p.PMReportFormType)
                .Include(p => p.FormStatusWarehouse)
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Where(p => p.ID == id && !p.IsDeleted)
                .Select(p => new PMReportFormRTUDto
                {
                    ID = p.ID,
                    ReportFormID = p.ReportFormID,
                    PMReportFormTypeID = p.PMReportFormTypeID,
                    FormstatusID = p.FormstatusID,
                    ProjectNo = p.ProjectNo,
                    Customer = p.Customer,
                    DateOfService = p.DateOfService,
                    CleaningOfCabinet = p.CleaningOfCabinet,
                    Remarks = p.Remarks,
                    AttendedBy = p.AttendedBy,
                    ApprovedBy = p.ApprovedBy,
                    IsDeleted = p.IsDeleted,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    CreatedBy = p.CreatedBy,
                    UpdatedBy = p.UpdatedBy,
                    PMReportFormTypeName = p.PMReportFormType != null ? p.PMReportFormType.Name : null,
                    FormStatusName = p.FormStatusWarehouse != null ? p.FormStatusWarehouse.Name : null,
                    CreatedByUserName = p.CreatedByUser != null ? $"{p.CreatedByUser.FirstName} {p.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = p.UpdatedByUser != null ? $"{p.UpdatedByUser.FirstName} {p.UpdatedByUser.LastName}" : null
                })
                .FirstOrDefaultAsync();

            if (pmReportForm == null)
            {
                return NotFound(new { message = "PM report form not found." });
            }

            return Ok(pmReportForm);
        }

        // POST: api/PMReportForm
        [HttpPost]
        public async Task<ActionResult<PMReportFormRTUDto>> CreatePMReportForm(CreatePMReportFormRTUDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pmReportFormType = await _context.PMReportFormTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.ID == createDto.PMReportFormTypeID && !t.IsDeleted);

            var formStatusExists = await _context.FormStatusWarehouses
                .AnyAsync(f => f.ID == createDto.FormstatusID && !f.IsDeleted);
            if (!formStatusExists)
            {
                return BadRequest(new { message = "Invalid FormstatusID." });
            }

            if (pmReportFormType == null)
            {
                return BadRequest(new { message = "Invalid PM Report Form Type ID." });
            }

            var pmReportForm = new PMReportFormRTU
            {
                ID = Guid.NewGuid(),
                ReportFormID = createDto.ReportFormID,
                PMReportFormTypeID = createDto.PMReportFormTypeID,
                FormstatusID = createDto.FormstatusID,
                ProjectNo = createDto.ProjectNo,
                Customer = createDto.Customer,
                ReportTitle = string.IsNullOrWhiteSpace(createDto.ReportTitle)
                    ? $"Preventative Maintenance ({pmReportFormType.Name?.ToUpperInvariant()})"
                    : createDto.ReportTitle,
                DateOfService = createDto.DateOfService,
                CleaningOfCabinet = createDto.CleaningOfCabinet,
                Remarks = createDto.Remarks,
                AttendedBy = createDto.AttendedBy,
                ApprovedBy = createDto.ApprovedBy,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = null, // Set based on authentication context
                UpdatedBy = null  // Set based on authentication context
            };

            _context.PMReportFormRTU.Add(pmReportForm);
            await _context.SaveChangesAsync();

            // Create the response DTO directly instead of calling GetPMReportForm
            var createdPMReportForm = await _context.PMReportFormRTU
                .Include(p => p.PMReportFormType)
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Where(p => p.ID == pmReportForm.ID && !p.IsDeleted)
                .Select(p => new PMReportFormRTUDto
                {
                    ID = p.ID,
                    ReportFormID = p.ReportFormID,
                    PMReportFormTypeID = p.PMReportFormTypeID,
                    ProjectNo = p.ProjectNo,
                    Customer = p.Customer,
                    DateOfService = p.DateOfService,
                    CleaningOfCabinet = p.CleaningOfCabinet,
                    Remarks = p.Remarks,
                    AttendedBy = p.AttendedBy,
                    ApprovedBy = p.ApprovedBy,
                    IsDeleted = p.IsDeleted,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    CreatedBy = p.CreatedBy,
                    UpdatedBy = p.UpdatedBy,
                    PMReportFormTypeName = p.PMReportFormType != null ? p.PMReportFormType.Name : null,
                    CreatedByUserName = p.CreatedByUser != null ? $"{p.CreatedByUser.FirstName} {p.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = p.UpdatedByUser != null ? $"{p.UpdatedByUser.FirstName} {p.UpdatedByUser.LastName}" : null
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction(nameof(GetPMReportForm), new { id = pmReportForm.ID }, createdPMReportForm);
        }

        // PUT: api/PMReportForm/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePMReportForm(Guid id, UpdatePMReportFormRTUDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pmReportForm = await _context.PMReportFormRTU
                .Include(p => p.ReportForm)
                .FirstOrDefaultAsync(p => p.ID == id && !p.IsDeleted);

            if (pmReportForm == null)
            {
                return NotFound(new { message = "PM report form not found." });
            }

            if (updateDto.FormstatusID.HasValue)
            {
                var formStatusWarehouse = await _context.FormStatusWarehouses
                    .FirstOrDefaultAsync(f => f.ID == updateDto.FormstatusID.Value && !f.IsDeleted);
                if (formStatusWarehouse == null)
                {
                    return BadRequest(new { message = "Invalid FormstatusID." });
                }
                pmReportForm.FormstatusID = updateDto.FormstatusID.Value;
                
                // Also update the parent ReportForm's FormStatus field
                if (pmReportForm.ReportForm != null)
                {
                    pmReportForm.ReportForm.FormStatus = formStatusWarehouse.Name;
                }
            }
            
            if (updateDto.PMReportFormTypeID.HasValue)
            {
                var pmReportFormType = await _context.PMReportFormTypes
                    .FirstOrDefaultAsync(t => t.ID == updateDto.PMReportFormTypeID.Value && !t.IsDeleted);
                if (pmReportFormType == null)
                {
                    return BadRequest(new { message = "Invalid PMReportFormTypeID." });
                }
                pmReportForm.PMReportFormTypeID = updateDto.PMReportFormTypeID.Value;
            }
            
            pmReportForm.ProjectNo = updateDto.ProjectNo;
            pmReportForm.Customer = updateDto.Customer;
            pmReportForm.DateOfService = updateDto.DateOfService;
            pmReportForm.CleaningOfCabinet = updateDto.CleaningOfCabinet;
            pmReportForm.Remarks = updateDto.Remarks;
            pmReportForm.AttendedBy = updateDto.AttendedBy;
            pmReportForm.ApprovedBy = updateDto.ApprovedBy;
            pmReportForm.UpdatedDate = DateTime.UtcNow;
            pmReportForm.UpdatedBy = null; // Set based on authentication context

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "PM report form updated successfully." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "The record was modified by another user. Please refresh and try again." });
            }
        }

        // DELETE: api/PMReportForm/5 (Soft Delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePMReportForm(Guid id)
        {
            var pmReportForm = await _context.PMReportFormRTU
                .FirstOrDefaultAsync(p => p.ID == id && !p.IsDeleted);

            if (pmReportForm == null)
            {
                return NotFound(new { message = "PM report form not found." });
            }

            // Soft delete
            pmReportForm.IsDeleted = true;
            pmReportForm.UpdatedDate = DateTime.UtcNow;
            pmReportForm.UpdatedBy = null; // Set based on authentication context

            await _context.SaveChangesAsync();

            return Ok(new { message = "PM report form deleted successfully." });
        }

        [HttpPost("{id}/generate-pdf")]
        public async Task<IActionResult> GenerateRtuPmPdf(Guid id, CancellationToken cancellationToken)
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
                var downloadName = result.FileName ?? $"RTU_PM_Report_{reportForm.JobNo ?? id.ToString()}.pdf";
                return File(result.FileContent, "application/pdf", downloadName);
            }
            catch (OperationCanceledException)
            {
                return StatusCode(504, "Timed out waiting for PDF generation.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate RTU PM report for {ReportId}", id);
                return StatusCode(500, $"Failed to generate PDF: {ex.Message}");
            }
        }

        private async Task<PdfGenerationResult> GeneratePdfAsync(Guid reportId, string requestedBy, CancellationToken cancellationToken)
        {
            var mqttFactory = new MqttFactory();
            using var mqttClient = mqttFactory.CreateMqttClient();
            var mqttOptions = BuildMqttOptions();

            var statusTopic = $"controltower/rtu_pm_reportform_pdf_status/{reportId}";
            var requestTopic = $"controltower/rtu_pm_reportform_pdf/{reportId}";
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
                        _logger.LogError(jsonEx, "Failed to parse PDF status payload: {Payload}", payload);
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
                .WithClientId($"controltower_rtu_api_{Guid.NewGuid():N}")
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

        // POST: api/PMReportFormRTU/{id}/generate-final-report-pdf
        // New endpoint for generating final report PDF with signatures
        [HttpPost("{id}/generate-final-report-pdf")]
        public async Task<IActionResult> GenerateRtuPmFinalReportPdf(Guid id, CancellationToken cancellationToken)
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
                var originalName = $"RTUPM_FinalReport_{reportForm.JobNo ?? id.ToString()}.pdf";
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
            var topicKey = "rtu_pm_reportform_signature_pdf";
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
                        _logger.LogError(jsonEx, "Failed to parse RTU PM Final Report PDF status payload: {Payload}", payload);
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
                report_type = "rtu_pm_final_report"
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

        public sealed record PdfGenerationResult(byte[] FileContent, string FileName);

        public sealed class PdfStatusMessage
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
