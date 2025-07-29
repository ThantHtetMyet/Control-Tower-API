using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.DTOs.ServiceReportSystem;
using ControlTower.Models.EmployeeManagementSystem;
using ControlTower.Models.ServiceReportSystem;

namespace ServiceReportSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceReportController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServiceReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ServiceReport
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceReportDto>>> GetServiceReports()
        {
            var serviceReports = await _context.ServiceReportForms
                .Include(s => s.ProjectNo)
                .Include(s => s.System)
                .Include(s => s.Location)
                .Include(s => s.ServiceType)
                    .ThenInclude(st => st.ServiceTypeWarehouse)
                .Include(s => s.FormStatus)
                    .ThenInclude(fs => fs.FormStatusWarehouse)
                .Where(s => !s.IsDeleted)
                .Select(s => new ServiceReportDto
                {
                    ID = s.ID,  // Add this missing line!
                    JobNumber = s.JobNumber,
                    Customer = s.Customer,
                    SystemName = s.System.Name,
                    LocationName = s.Location.Name,
                    ProjectNoID = s.ProjectNoID,
                    ProjectNumberName = s.ProjectNo.ProjectNumber,
                    ServiceType = s.ServiceType.Select(st => new ServiceTypeDto
                    {
                        Id = st.ServiceTypeWarehouseID,
                        Name = st.ServiceTypeWarehouse.Name,
                        Remark = st.Remark
                    }).ToList(),
                    FormStatus = s.FormStatus.Select(fs => new FormStatusDto
                    {
                        Id = fs.FormStatusWarehouseID,
                        Name = fs.FormStatusWarehouse.Name,
                        Remark = fs.Remark
                    }).ToList()
                })
                .ToListAsync();

            return Ok(serviceReports);
        }


        // GET: api/ServiceReport/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceReportDto>> GetServiceReport(Guid id)
        {
            var serviceReport = await _context.ServiceReportForms
                .Include(s => s.ProjectNo)
                .Include(s => s.System)
                .Include(s => s.Location)
                .Include(s => s.FollowupAction)
                .Include(s => s.ServiceType)
                    .ThenInclude(st => st.ServiceTypeWarehouse)
                .Include(s => s.FormStatus)
                    .ThenInclude(fs => fs.FormStatusWarehouse)
                .Include(s => s.CreatedByUser)
                .Include(s => s.UpdatedByUser)
                .Include(s => s.IssueReported)
                    .ThenInclude(ir => ir.IssueReportWarehouse)
                .Include(s => s.IssueFound)
                    .ThenInclude(issueFound => issueFound.IssueFoundWarehouse)
                .Include(s => s.ActionTaken)
                    .ThenInclude(at => at.ActionTakenWarehouse)
                .Include(s => s.FurtherActionTaken)
                    .ThenInclude(fat => fat.FurtherActionTakenWarehouse)
                .FirstOrDefaultAsync(s => s.ID == id && !s.IsDeleted);

            if (serviceReport == null)
            {
                return NotFound();
            }

            var dto = new ServiceReportDto
            {
                ID = serviceReport.ID,
                JobNumber = serviceReport.JobNumber,
                ContactNo = serviceReport.ContactNo,
                Customer = serviceReport.Customer,
                ProjectNoID = serviceReport.ProjectNoID,
                ProjectNumberName = serviceReport.ProjectNo.ProjectNumber,
                SystemID = serviceReport.SystemID,
                SystemName = serviceReport.System.Name,
                LocationID = serviceReport.LocationID,
                LocationName = serviceReport.Location.Name,
                FollowupActionID = serviceReport.FollowupActionID,
                FollowupActionNo = serviceReport.FollowupAction.FollowupActionNo,
                FailureDetectedDate = serviceReport.FailureDetectedDate,
                ResponseDate = serviceReport.ResponseDate,
                ArrivalDate = serviceReport.ArrivalDate,
                CompletionDate = serviceReport.CompletionDate,
                CreatedDate = serviceReport.CreatedDate,
                CreatedByUserName = serviceReport.CreatedByUser.FirstName,
                UpdatedDate = serviceReport.UpdatedDate,
                UpdatedByUserName = serviceReport.UpdatedByUser.FirstName,
                
                // Map collections
                ServiceType = serviceReport.ServiceType.Select(st => new ServiceTypeDto
                {
                    Id = st.ServiceTypeWarehouseID,
                    Name = st.ServiceTypeWarehouse.Name,
                    Remark = st.Remark
                }).ToList(),
                
                FormStatus = serviceReport.FormStatus.Select(fs => new FormStatusDto
                {
                    Id = fs.FormStatusWarehouseID,
                    Name = fs.FormStatusWarehouse.Name,
                    Remark = fs.Remark
                }).ToList(),
                
                // Fix mappings to extract warehouse IDs instead of record IDs
                IssueReported = serviceReport.IssueReported.Select(ir => new IssueReportedDto
                {
                    ID = ir.IssueReportWarehouseID,  // ✅ Changed from ir.ID
                    Description = ir.IssueReportWarehouse?.Name ?? string.Empty,
                    Remark = ir.Remark
                }).ToList(),
                
                IssueFound = serviceReport.IssueFound.Select(ifound => new IssueFoundDto
                {
                    ID = ifound.IssueFoundWarehouseID,  // ✅ Changed from ifound.ID
                    Description = ifound.IssueFoundWarehouse?.Name ?? string.Empty,
                    Remark = ifound.Remark
                }).ToList(),
                
                ActionTaken = serviceReport.ActionTaken.Select(at => new ActionTakenDto
                {
                    ID = at.ActionTakenWarehouseID,  // ✅ Changed from at.ID
                    Description = at.ActionTakenWarehouse?.Name ?? string.Empty,
                    Remark = at.Remark
                }).ToList(),
                
                FurtherActionTaken = serviceReport.FurtherActionTaken.Select(fat => new FurtherActionDto
                {
                    ID = fat.FurtherActionTakenWarehouseID,  // ✅ Changed from fat.ID
                    Description = fat.FurtherActionTakenWarehouse?.Name ?? string.Empty,
                    Remark = fat.Remark
                }).ToList(),
                
                // Set remark fields from first items (if any)
                ServiceTypeRemark = serviceReport.ServiceType.FirstOrDefault()?.Remark,
                IssueReportedRemark = serviceReport.IssueReported.FirstOrDefault()?.Remark,
                IssueFoundRemark = serviceReport.IssueFound.FirstOrDefault()?.Remark,
                ActionTakenRemark = serviceReport.ActionTaken.FirstOrDefault()?.Remark,
                FurtherActionTakenRemark = serviceReport.FurtherActionTaken.FirstOrDefault()?.Remark,
                FormStatusRemark = serviceReport.FormStatus.FirstOrDefault()?.Remark
            };

            return dto;
        }
        [HttpGet("NextJobNumber")]
        public async Task<ActionResult<string>> GetNextJobNumber()
        {
            var latestJobNumber = await _context.ServiceReportForms
                .Where(s => s.JobNumber != null)
                .OrderByDescending(s => s.CreatedDate) // Change to order by CreatedDate instead of ID
                .Select(s => s.JobNumber)
                .FirstOrDefaultAsync();

            // Generate the next job number
            string nextJobNumber;
            if (string.IsNullOrEmpty(latestJobNumber))
            {
                nextJobNumber = "M001"; // Initial job number
            }
            else
            {
                // Extract the numeric part and increment
                int currentNumber = int.Parse(latestJobNumber.Substring(2));
                nextJobNumber = $"M{(currentNumber + 1):D3}"; // Format with leading zeros
            }

            return Ok(new { jobNumber = nextJobNumber });
        }

        // POST: api/ServiceReport
        [HttpPost]
        public async Task<ActionResult<ServiceReportDto>> CreateServiceReport([FromBody] CreateServiceReportDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Generate the next job number
            string nextJobNumber = createDto.JobNumber;


            // Map DTO to Entity
            var serviceReport = new ServiceReportForm
            {
                ID = Guid.NewGuid(),
                JobNumber = nextJobNumber,
                Customer = createDto.Customer,
                ContactNo = createDto.ContactNo,
                ProjectNoID = createDto.ProjectNoID,
                SystemID = createDto.SystemID,
                LocationID = createDto.LocationID,
                FollowupActionID = createDto.FollowupActionID,
                FailureDetectedDate = createDto.FailureDetectedDate,
                ResponseDate = createDto.ResponseDate,
                ArrivalDate = createDto.ArrivalDate,
                CompletionDate = createDto.CompletionDate,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = Guid.Parse(createDto.CreatedBy),
                UpdatedBy = Guid.Parse(createDto.CreatedBy)
            };

            // Add related entities
            serviceReport.IssueReported = new List<IssueReported>
            {
                new IssueReported
                {
                    IssueReportWarehouseID = createDto.IssueReported[0].Id,
                    Remark = createDto.IssueReported[0].Remark,
                    ServiceReportFormID = serviceReport.ID,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedBy = Guid.Parse(createDto.CreatedBy),
                    UpdatedBy = Guid.Parse(createDto.CreatedBy),
                    IsDeleted = false,
                }
            };

            serviceReport.IssueFound = new List<IssueFound>
            {
                new IssueFound
                {
                    IssueFoundWarehouseID = createDto.IssueFound[0].Id,
                    ServiceReportFormID = serviceReport.ID,
                    Remark = createDto.IssueFound[0].Remark,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedBy = Guid.Parse(createDto.CreatedBy),
                    UpdatedBy = Guid.Parse(createDto.CreatedBy),
                    IsDeleted = false,
                }
            };

            serviceReport.ActionTaken = new List<ActionTaken>
            {
                new ActionTaken
                {
                    ActionTakenWarehouseID = createDto.ActionTaken[0].Id,
                    ServiceReportFormID = serviceReport.ID,
                    Remark = createDto.ActionTaken[0].Remark,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedBy = Guid.Parse(createDto.CreatedBy),
                    UpdatedBy = Guid.Parse(createDto.CreatedBy),
                    IsDeleted = false,
                }
            };

            serviceReport.ServiceType = new List<ServiceType>
            {
                new ServiceType
                {
                    ServiceTypeWarehouseID = createDto.ServiceType[0].Id,
                    Remark = createDto.ServiceType[0].Remark,
                    ServiceReportFormID = serviceReport.ID,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedBy = Guid.Parse(createDto.CreatedBy),
                    UpdatedBy = Guid.Parse(createDto.CreatedBy),
                    IsDeleted = false,
                }
            };

            serviceReport.FurtherActionTaken = new List<FurtherActionTaken>
            {
                new FurtherActionTaken
                {
                    FurtherActionTakenWarehouseID = createDto.FurtherAction[0].Id,
                    ServiceReportFormID = serviceReport.ID,
                    Remark = createDto.FurtherAction[0].Remark,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedBy = Guid.Parse(createDto.CreatedBy),
                    UpdatedBy = Guid.Parse(createDto.CreatedBy),
                    IsDeleted = false,
                }
            };

            serviceReport.FormStatus = new List<FormStatus>
            {
                new FormStatus
                {
                    FormStatusWarehouseID = createDto.FormStatus[0].Id,
                    ServiceReportFormID = serviceReport.ID,
                    Remark = createDto.FormStatus[0].Remark,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedBy = Guid.Parse(createDto.CreatedBy),
                    UpdatedBy = Guid.Parse(createDto.CreatedBy),
                    IsDeleted = false,
                }
            };

            // Add the ServiceReportForm to the DbContext
            _context.ServiceReportForms.Add(serviceReport);

            // Save all changes
            await _context.SaveChangesAsync();

            var responseDto = new ServiceReportDto
            {
                ID = serviceReport.ID,
                JobNumber = serviceReport.JobNumber,
                Customer = serviceReport.Customer,
                FailureDetectedDate = serviceReport.FailureDetectedDate,
                ResponseDate = serviceReport.ResponseDate,
                ArrivalDate = serviceReport.ArrivalDate,
                CompletionDate = serviceReport.CompletionDate
            };

            return CreatedAtAction(nameof(GetServiceReport), new { id = serviceReport.ID }, responseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceReport(Guid id, UpdateServiceReportDto updateDto)
        {
            var serviceReport = await _context.ServiceReportForms
                .Include(s => s.ServiceType)
                .Include(s => s.FormStatus)
                .Include(s => s.IssueReported)
                .Include(s => s.IssueFound)
                .Include(s => s.ActionTaken)
                .Include(s => s.FurtherActionTaken)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (serviceReport == null || serviceReport.IsDeleted)
            {
                return NotFound();
            }

            // Update basic fields
            serviceReport.Customer = updateDto.Customer;
            serviceReport.ContactNo = updateDto.ContactNo;
            serviceReport.ProjectNoID = updateDto.ProjectNoID;
            serviceReport.SystemID = updateDto.SystemID;
            serviceReport.LocationID = updateDto.LocationID;
            serviceReport.FollowupActionID = updateDto.FollowupActionID;
            serviceReport.FailureDetectedDate = updateDto.FailureDetectedDate;
            serviceReport.ResponseDate = updateDto.ResponseDate;
            serviceReport.ArrivalDate = updateDto.ArrivalDate;
            serviceReport.CompletionDate = updateDto.CompletionDate;
            serviceReport.UpdatedDate = DateTime.UtcNow;
            serviceReport.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);

            // Update ServiceType if changed
            if (updateDto.ServiceType?.Any() == true)
            {
                var existingServiceType = serviceReport.ServiceType.FirstOrDefault();
                if (existingServiceType != null)
                {
                    existingServiceType.ServiceTypeWarehouseID = updateDto.ServiceType[0].Id;
                    existingServiceType.Remark = updateDto.ServiceType[0].Remark;
                    existingServiceType.UpdatedDate = DateTime.UtcNow;
                    existingServiceType.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);
                }
            }

            // Update FormStatus if changed
            if (updateDto.FormStatus?.Any() == true)
            {
                var existingFormStatus = serviceReport.FormStatus.FirstOrDefault();
                if (existingFormStatus != null)
                {
                    existingFormStatus.FormStatusWarehouseID = updateDto.FormStatus[0].Id;
                    existingFormStatus.Remark = updateDto.FormStatus[0].Remark;
                    existingFormStatus.UpdatedDate = DateTime.UtcNow;
                    existingFormStatus.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);
                }
            }

            // Update IssueReported if changed
            if (updateDto.IssueReported?.Any() == true)
            {
                var existingIssueReported = serviceReport.IssueReported.FirstOrDefault();
                if (existingIssueReported != null)
                {
                    existingIssueReported.IssueReportWarehouseID = updateDto.IssueReported[0].Id;
                    existingIssueReported.Remark = updateDto.IssueReported[0].Remark;
                    existingIssueReported.UpdatedDate = DateTime.UtcNow;
                    existingIssueReported.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);
                }
            }

            // Update IssueFound if changed
            if (updateDto.IssueFound?.Any() == true)
            {
                var existingIssueFound = serviceReport.IssueFound.FirstOrDefault();
                if (existingIssueFound != null)
                {
                    existingIssueFound.IssueFoundWarehouseID = updateDto.IssueFound[0].Id;
                    existingIssueFound.Remark = updateDto.IssueFound[0].Remark;
                    existingIssueFound.UpdatedDate = DateTime.UtcNow;
                    existingIssueFound.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);
                }
            }

            // Update ActionTaken if changed
            if (updateDto.ActionTaken?.Any() == true)
            {
                var existingActionTaken = serviceReport.ActionTaken.FirstOrDefault();
                if (existingActionTaken != null)
                {
                    existingActionTaken.ActionTakenWarehouseID = updateDto.ActionTaken[0].Id;
                    existingActionTaken.Remark = updateDto.ActionTaken[0].Remark;
                    existingActionTaken.UpdatedDate = DateTime.UtcNow;
                    existingActionTaken.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);
                }
            }

            // Update FurtherAction if changed
            if (updateDto.FurtherAction?.Any() == true)
            {
                var existingFurtherAction = serviceReport.FurtherActionTaken.FirstOrDefault();
                if (existingFurtherAction != null)
                {
                    existingFurtherAction.FurtherActionTakenWarehouseID = updateDto.FurtherAction[0].Id;
                    existingFurtherAction.Remark = updateDto.FurtherAction[0].Remark;
                    existingFurtherAction.UpdatedDate = DateTime.UtcNow;
                    existingFurtherAction.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceReportExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // In DeleteServiceReport method - parameter should be Guid
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceReport(Guid id, [FromQuery] string updatedBy)
        {
            var serviceReport = await _context.ServiceReportForms.FindAsync(id);
            if (serviceReport == null || serviceReport.IsDeleted)
            {
                return NotFound();
            }

            serviceReport.IsDeleted = true;
            serviceReport.UpdatedDate = DateTime.UtcNow;
            if (!string.IsNullOrEmpty(updatedBy))
            {
                serviceReport.UpdatedBy = Guid.Parse(updatedBy);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceReportExists(Guid id)
        {
            return _context.ServiceReportForms.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}