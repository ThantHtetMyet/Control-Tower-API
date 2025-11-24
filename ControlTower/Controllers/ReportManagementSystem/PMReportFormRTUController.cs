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
                configuredPath = Path.Combine("ControlTower_Python", "PDF_Generator", "Server_PM_ReportForm_PDF", "PDF_File");
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
