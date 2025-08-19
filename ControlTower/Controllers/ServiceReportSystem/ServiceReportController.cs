using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.DTOs.ServiceReportSystem;
using ControlTower.Models.EmployeeManagementSystem;
using ControlTower.Models.ServiceReportSystem;
using ControlTower.DTOs;

namespace ControlTower.Controllers.ServiceReportSystem
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
        // Add this new endpoint after the existing GetServiceReports method

        // GET: api/ServiceReport/search
        [HttpGet("search")]
        public async Task<ActionResult<PagedResult<ServiceReportDto>>> SearchServiceReports(
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchField = null,
            [FromQuery] string? searchValue = null,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            var baseQuery = _context.ServiceReportForms
                .Where(s => !s.IsDeleted);

            // Apply field-specific search
            if (!string.IsNullOrEmpty(searchField) && !string.IsNullOrEmpty(searchValue))
            {
                switch (searchField.ToLower())
                {
                    case "jobno":
                    case "jobnumber":
                        baseQuery = baseQuery.Where(s => s.JobNumber.Contains(searchValue));
                        break;
                    case "customer":
                        baseQuery = baseQuery.Where(s => s.Customer.Contains(searchValue));
                        break;
                    case "projectno":
                    case "project":
                        baseQuery = baseQuery.Where(s => s.ProjectNo.ProjectNumber.Contains(searchValue));
                        break;
                    case "system":
                        baseQuery = baseQuery.Where(s => s.System.Name.Contains(searchValue));
                        break;
                    case "location":
                        baseQuery = baseQuery.Where(s => s.Location.Name.Contains(searchValue));
                        break;
                    case "status":
                        baseQuery = baseQuery.Where(s => s.FormStatus.Any(fs => fs.FormStatusWarehouse.Name.Contains(searchValue)));
                        break;
                }
            }

            // Apply date range filter
            if (startDate.HasValue)
            {
                baseQuery = baseQuery.Where(s => s.CreatedDate >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                baseQuery = baseQuery.Where(s => s.CreatedDate <= endDate.Value.AddDays(1).AddTicks(-1));
            }

            var totalCount = await baseQuery.CountAsync();

            var query = baseQuery
                .Include(s => s.ProjectNo)
                .Include(s => s.System)
                .Include(s => s.Location)
                .Include(s => s.ServiceType)
                    .ThenInclude(st => st.ServiceTypeWarehouse)
                .Include(s => s.FormStatus)
                    .ThenInclude(fs => fs.FormStatusWarehouse)
                .Include(s => s.CreatedByUser)
                .Include(s => s.UpdatedByUser)
                .OrderByDescending(s => s.UpdatedDate ?? s.CreatedDate);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new ServiceReportDto
                {
                    ID = s.ID,
                    JobNumber = s.JobNumber,
                    Customer = s.Customer,
                    SystemName = s.System.Name,
                    LocationName = s.Location.Name,
                    ProjectNoID = s.ProjectNoID,
                    ProjectNumberName = s.ProjectNo.ProjectNumber,
                    CreatedDate = s.CreatedDate,
                    CreatedByUserName = s.CreatedByUser.FirstName ?? string.Empty,
                    UpdatedDate = s.UpdatedDate,
                    UpdatedByUserName = s.UpdatedByUser.FirstName ?? string.Empty,
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

            var result = new PagedResult<ServiceReportDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };

            return Ok(result);
        }
        // GET: api/ServiceReport
        [HttpGet]
        public async Task<ActionResult<PagedResult<ServiceReportDto>>> GetServiceReports([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 100) pageSize = 100;

            var baseQuery = _context.ServiceReportForms
                .Where(s => !s.IsDeleted);

            var totalCount = await baseQuery.CountAsync();

            var query = baseQuery
                .Include(s => s.ProjectNo)
                .Include(s => s.System)
                .Include(s => s.Location)
                .Include(s => s.ServiceType)
                    .ThenInclude(st => st.ServiceTypeWarehouse)
                .Include(s => s.FormStatus)
                    .ThenInclude(fs => fs.FormStatusWarehouse)
                .Include(s => s.CreatedByUser)  // Add this line
                .Include(s => s.UpdatedByUser)  // Add this line
                .OrderByDescending(s => s.UpdatedDate ?? s.CreatedDate);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new ServiceReportDto
                {
                    ID = s.ID,
                    JobNumber = s.JobNumber,
                    Customer = s.Customer,
                    SystemName = s.System.Name,
                    LocationName = s.Location.Name,
                    ProjectNoID = s.ProjectNoID,
                    ProjectNumberName = s.ProjectNo.ProjectNumber,
                    CreatedDate = s.CreatedDate,
                    CreatedByUserName = s.CreatedByUser.FirstName ?? string.Empty,  // Add this line
                    UpdatedDate = s.UpdatedDate,  // Add this line
                    UpdatedByUserName = s.UpdatedByUser.FirstName ?? string.Empty,  // Add this line
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

            var result = new PagedResult<ServiceReportDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };

            return Ok(result);
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
                .Include(s => s.MaterialsUsed)
                .Include(s => s.CreatedByUser)
                .Include(s => s.UpdatedByUser)
                .Include(s => s.IssueReported)
                .Include(s => s.IssueFound)
                .Include(s => s.ActionTaken)
                .Include(s => s.FurtherActionTaken)
                    .ThenInclude(fat => fat.FurtherActionTakenWarehouse)
                .Include(s => s.MaterialsUsed) // Add this line
                .FirstOrDefaultAsync(s => s.ID == id && !s.IsDeleted);

            if (serviceReport == null)
            {
                return NotFound();
            }

            var dto = new ServiceReportDto
            {
                ID = serviceReport.ID,
                JobNumber = serviceReport.JobNumber ?? string.Empty,
                ContactNo = serviceReport.ContactNo ?? string.Empty,
                Customer = serviceReport.Customer ?? string.Empty,
                
                ProjectNoID = serviceReport.ProjectNoID,
                ProjectNumberName = serviceReport.ProjectNo?.ProjectNumber ?? string.Empty,
                
                SystemID = serviceReport.SystemID,
                SystemName = serviceReport.System?.Name ?? string.Empty,
                
                LocationID = serviceReport.LocationID,
                LocationName = serviceReport.Location?.Name ?? string.Empty,
                
                FollowupActionID = serviceReport.FollowupActionID,
                FollowupActionNo = serviceReport.FollowupAction?.FollowupActionNo ?? string.Empty,
                
                FailureDetectedDate = serviceReport.FailureDetectedDate,
                ResponseDate = serviceReport.ResponseDate,
                ArrivalDate = serviceReport.ArrivalDate,
                CompletionDate = serviceReport.CompletionDate,
                CreatedDate = serviceReport.CreatedDate,
                CreatedByUserName = serviceReport.CreatedByUser?.FirstName ?? string.Empty,
                UpdatedDate = serviceReport.UpdatedDate,
                UpdatedByUserName = serviceReport.UpdatedByUser?.FirstName ?? string.Empty,
                
                ServiceType = serviceReport.ServiceType?.Select(st => new ServiceTypeDto
                {
                    Id = st.ServiceTypeWarehouseID,
                    Name = st.ServiceTypeWarehouse?.Name ?? string.Empty,
                    Remark = st.Remark
                }).ToList() ?? new List<ServiceTypeDto>(),
                
                FormStatus = serviceReport.FormStatus?.Select(fs => new FormStatusDto
                {
                    Id = fs.FormStatusWarehouseID,
                    Name = fs.FormStatusWarehouse?.Name ?? string.Empty,
                    Remark = fs.Remark
                }).ToList() ?? new List<FormStatusDto>(),
                
                IssueReported = serviceReport.IssueReported?.Select(ir => new IssueReportedDto
                {
                    ID = ir.ID,
                    Description = ir.Description ?? string.Empty,
                    Remark = ir.Remark
                }).ToList() ?? new List<IssueReportedDto>(),
                
                IssueFound = serviceReport.IssueFound?.Select(ifound => new IssueFoundDto
                {
                    ID = ifound.ID,
                    Description = ifound.Description ?? string.Empty,
                    Remark = ifound.Remark
                }).ToList() ?? new List<IssueFoundDto>(),
                
                ActionTaken = serviceReport.ActionTaken?.Select(at => new ActionTakenDto
                {
                    ID = at.ID,
                    Description = at.Description ?? string.Empty,
                    Remark = at.Remark
                }).ToList() ?? new List<ActionTakenDto>(),
                
                FurtherActionTaken = serviceReport.FurtherActionTaken?.Select(fat => new FurtherActionDto
                {
                    ID = fat.FurtherActionTakenWarehouseID,
                    Description = fat.FurtherActionTakenWarehouse?.Name ?? string.Empty,
                    Remark = fat.Remark
                }).ToList() ?? new List<FurtherActionDto>(),
                
                MaterialsUsed = serviceReport.MaterialsUsed?.Select(mu => new MaterialUsedDto
                {
                    ID = mu.ID,
                    Quantity = mu.Quantity,
                    Description = mu.Description,
                    SerialNo = mu.SerialNo
                }).ToList() ?? new List<MaterialUsedDto>(),
                
                ServiceTypeRemark = serviceReport.ServiceType?.FirstOrDefault()?.Remark,
                IssueReportedRemark = serviceReport.IssueReported?.FirstOrDefault()?.Remark,
                IssueFoundRemark = serviceReport.IssueFound?.FirstOrDefault()?.Remark,
                ActionTakenRemark = serviceReport.ActionTaken?.FirstOrDefault()?.Remark,
                FurtherActionTakenRemark = serviceReport.FurtherActionTaken?.FirstOrDefault()?.Remark,
                FormStatusRemark = serviceReport.FormStatus?.FirstOrDefault()?.Remark
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
        // POST: api/ServiceReport
        [HttpPost]
        public async Task<ActionResult<ServiceReportDto>> CreateServiceReport([FromBody] CreateServiceReportDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate CreatedBy exists
            var userId = Guid.Parse(createDto.CreatedBy);
            var userExists = await _context.Users.AnyAsync(u => u.ID == userId);
            if (!userExists)
            {
                return BadRequest("Invalid CreatedBy user ID.");
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
                CreatedBy = userId,
                UpdatedBy = userId
            };

            // Add MaterialsUsed
            if (createDto.MaterialsUsed != null && createDto.MaterialsUsed.Any())
            {
                serviceReport.MaterialsUsed = createDto.MaterialsUsed.Select(m => new MaterialUsed
                {
                    ID = Guid.NewGuid(),
                    Quantity = m.Quantity,
                    Description = m.Description,
                    SerialNo = m.SerialNo,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    IsDeleted = false
                }).ToList();
            }

            // Add IssueReported
            if (createDto.IssueReported != null && createDto.IssueReported.Any())
            {
                serviceReport.IssueReported = createDto.IssueReported.Select(x => new IssueReported
                {
                    Description = x.Description,
                    Remark = x.Remark,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    IsDeleted = false
                }).ToList();
            }

            // Add IssueFound
            if (createDto.IssueFound != null && createDto.IssueFound.Any())
            {
                serviceReport.IssueFound = createDto.IssueFound.Select(x => new IssueFound
                {
                    Description = x.Description,
                    Remark = x.Remark,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    IsDeleted = false
                }).ToList();
            }

            // Add ActionTaken
            if (createDto.ActionTaken != null && createDto.ActionTaken.Any())
            {
                serviceReport.ActionTaken = createDto.ActionTaken.Select(x => new ActionTaken
                {
                    Description = x.Description,
                    Remark = x.Remark,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    CreatedBy = userId,
                    UpdatedBy = userId,
                    IsDeleted = false
                }).ToList();
            }

            // Add ServiceType
            if (createDto.ServiceType != null && createDto.ServiceType.Any())
            {
                serviceReport.ServiceType = createDto.ServiceType
                    .Where(x => x.Id != null && x.Id != Guid.Empty)
                    .Select(x => new ServiceType
                    {
                        ServiceTypeWarehouseID = x.Id.Value,
                        Remark = x.Remark,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        CreatedBy = userId,
                        UpdatedBy = userId,
                        IsDeleted = false
                    }).ToList();
            }

            // Add FurtherActionTaken
            if (createDto.FurtherAction != null && createDto.FurtherAction.Any())
            {
                serviceReport.FurtherActionTaken = createDto.FurtherAction
                    .Where(x => x.Id != null && x.Id != Guid.Empty)
                    .Select(x => new FurtherActionTaken
                    {
                        FurtherActionTakenWarehouseID = x.Id.Value,
                        Remark = x.Remark,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        CreatedBy = userId,
                        UpdatedBy = userId,
                        IsDeleted = false
                    }).ToList();
            }

            // Add FormStatus
            if (createDto.FormStatus != null && createDto.FormStatus.Any())
            {
                serviceReport.FormStatus = createDto.FormStatus
                    .Where(x => x.Id != null && x.Id != Guid.Empty)
                    .Select(x => new FormStatus
                    {
                        FormStatusWarehouseID = x.Id.Value,
                        Remark = x.Remark,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        CreatedBy = userId,
                        UpdatedBy = userId,
                        IsDeleted = false
                    }).ToList();
            }

            // Add the ServiceReportForm to the DbContext
            _context.ServiceReportForms.Add(serviceReport);

            // Save all changes
            await _context.SaveChangesAsync();

            // Map response
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
                .Include(s => s.MaterialsUsed) // Add this line
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

            // Update ServiceType if changed - only if valid value provided
            if (updateDto.ServiceType?.Any() == true &&
            updateDto.ServiceType[0].Id != null && updateDto.ServiceType[0].Id != Guid.Empty)
            {
                var existingServiceType = serviceReport.ServiceType.FirstOrDefault();
                if (existingServiceType != null)
                {
                    existingServiceType.ServiceTypeWarehouseID = updateDto.ServiceType[0].Id.Value;
                    existingServiceType.Remark = updateDto.ServiceType[0].Remark;
                    existingServiceType.UpdatedDate = DateTime.UtcNow;
                    existingServiceType.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);
                }
                else
                {
                    // Create new ServiceType if none exists
                    serviceReport.ServiceType = new List<ServiceType>
                    {
                        new ServiceType
                        {
                            ServiceTypeWarehouseID = updateDto.ServiceType[0].Id.Value,
                            Remark = updateDto.ServiceType[0].Remark,
                            ServiceReportFormID = serviceReport.ID,
                            CreatedDate = DateTime.UtcNow,
                            UpdatedDate = DateTime.UtcNow,
                            CreatedBy = Guid.Parse(updateDto.UpdatedBy),
                            UpdatedBy = Guid.Parse(updateDto.UpdatedBy),
                            IsDeleted = false,
                        }
                    };
                }
            }
            else if (updateDto.ServiceType?.Any() == true &&
                     (updateDto.ServiceType[0].Id == null || updateDto.ServiceType[0].Id == Guid.Empty))
            {
                // Remove existing ServiceType if empty value is provided
                var existingServiceType = serviceReport.ServiceType.FirstOrDefault();
                if (existingServiceType != null)
                {
                    existingServiceType.IsDeleted = true;
                    existingServiceType.UpdatedDate = DateTime.UtcNow;
                    existingServiceType.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);
                }
            }

            // Update FormStatus if changed - only if valid value provided
            if (updateDto.FormStatus?.Any() == true &&
            updateDto.FormStatus[0].Id != null && updateDto.FormStatus[0].Id != Guid.Empty)
            {
                var existingFormStatus = serviceReport.FormStatus.FirstOrDefault();
                if (existingFormStatus != null)
                {
                    existingFormStatus.FormStatusWarehouseID = updateDto.FormStatus[0].Id.Value;
                    existingFormStatus.Remark = updateDto.FormStatus[0].Remark;
                    existingFormStatus.UpdatedDate = DateTime.UtcNow;
                    existingFormStatus.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);
                }
                else
                {
                    // Create new FormStatus if none exists
                    serviceReport.FormStatus = new List<FormStatus>
                    {
                        new FormStatus
                        {
                            FormStatusWarehouseID = updateDto.FormStatus[0].Id.Value,
                            ServiceReportFormID = serviceReport.ID,
                            Remark = updateDto.FormStatus[0].Remark,
                            CreatedDate = DateTime.UtcNow,
                            UpdatedDate = DateTime.UtcNow,
                            CreatedBy = Guid.Parse(updateDto.UpdatedBy),
                            UpdatedBy = Guid.Parse(updateDto.UpdatedBy),
                            IsDeleted = false,
                        }
                    };
                }
            }
            else if (updateDto.FormStatus?.Any() == true &&
                     (updateDto.FormStatus[0].Id == null || updateDto.FormStatus[0].Id == Guid.Empty))
            {
                // Remove existing FormStatus if empty value is provided
                var existingFormStatus = serviceReport.FormStatus.FirstOrDefault();
                if (existingFormStatus != null)
                {
                    existingFormStatus.IsDeleted = true;
                    existingFormStatus.UpdatedDate = DateTime.UtcNow;
                    existingFormStatus.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);
                }
            }

            // Update FurtherAction if changed - only if valid value provided
            if (updateDto.FurtherAction?.Any() == true &&
            updateDto.FurtherAction[0].Id != null && updateDto.FurtherAction[0].Id != Guid.Empty)
            {
                var existingFurtherAction = serviceReport.FurtherActionTaken.FirstOrDefault();
                if (existingFurtherAction != null)
                {
                    existingFurtherAction.FurtherActionTakenWarehouseID = updateDto.FurtherAction[0].Id.Value;
                    existingFurtherAction.Remark = updateDto.FurtherAction[0].Remark;
                    existingFurtherAction.UpdatedDate = DateTime.UtcNow;
                    existingFurtherAction.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);
                }
                else
                {
                    // Create new FurtherActionTaken if none exists
                    serviceReport.FurtherActionTaken = new List<FurtherActionTaken>
                    {
                        new FurtherActionTaken
                        {
                            FurtherActionTakenWarehouseID = updateDto.FurtherAction[0].Id.Value,
                            ServiceReportFormID = serviceReport.ID,
                            Remark = updateDto.FurtherAction[0].Remark,
                            CreatedDate = DateTime.UtcNow,
                            UpdatedDate = DateTime.UtcNow,
                            CreatedBy = Guid.Parse(updateDto.UpdatedBy),
                            UpdatedBy = Guid.Parse(updateDto.UpdatedBy),
                            IsDeleted = false,
                        }
                    };
                }
            }
            else if (updateDto.FurtherAction?.Any() == true &&
                     (updateDto.FurtherAction[0].Id == null || updateDto.FurtherAction[0].Id == Guid.Empty))
            {
                // Remove existing FurtherAction if empty value is provided
                var existingFurtherAction = serviceReport.FurtherActionTaken.FirstOrDefault();
                if (existingFurtherAction != null)
                {
                    existingFurtherAction.IsDeleted = true;
                    existingFurtherAction.UpdatedDate = DateTime.UtcNow;
                    existingFurtherAction.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);
                }
            }


            // Update MaterialsUsed with proper concurrency handling
            if (updateDto.MaterialsUsed != null)
            {
                    // Get fresh data to avoid concurrency issues
                    var currentMaterials = await _context.MaterialsUsed
                        .Where(m => m.ServiceReportFormID == id && !m.IsDeleted)
                        .ToListAsync();

                    var dtoMaterialIds = updateDto.MaterialsUsed
                        .Where(m => m.ID.HasValue && m.ID.Value != Guid.Empty)
                        .Select(m => m.ID.Value)
                        .ToHashSet();

                    // 1. Mark materials for deletion if they're not in the update list
                    var materialsToDelete = currentMaterials
                        .Where(m => !dtoMaterialIds.Contains(m.ID))
                        .ToList();

                    foreach (var material in materialsToDelete)
                    {
                        material.IsDeleted = true;
                        material.UpdatedDate = DateTime.UtcNow;
                        material.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);
                    }

                    // 2. Update existing materials
                    foreach (var materialDto in updateDto.MaterialsUsed.Where(m => m.ID.HasValue && m.ID.Value != Guid.Empty))
                    {
                        var existingMaterial = currentMaterials.FirstOrDefault(m => m.ID == materialDto.ID.Value);
                        if (existingMaterial != null)
                        {
                            // Only update if values have actually changed
                            if (existingMaterial.Quantity != materialDto.Quantity ||
                                existingMaterial.Description != materialDto.Description ||
                                existingMaterial.SerialNo != materialDto.SerialNo)
                            {
                                existingMaterial.Quantity = materialDto.Quantity;
                                existingMaterial.Description = materialDto.Description;
                                existingMaterial.SerialNo = materialDto.SerialNo;
                                existingMaterial.UpdatedDate = DateTime.UtcNow;
                                existingMaterial.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);
                            }
                        }
                    }

                    // 3. Add new materials
                    var newMaterials = updateDto.MaterialsUsed
                        .Where(m => !m.ID.HasValue || m.ID.Value == Guid.Empty)
                        .Select(materialDto => new MaterialUsed
                        {
                            ID = Guid.NewGuid(),
                            ServiceReportFormID = serviceReport.ID,
                            Quantity = materialDto.Quantity,
                            Description = materialDto.Description,
                            SerialNo = materialDto.SerialNo,
                            CreatedDate = DateTime.UtcNow,
                            UpdatedDate = DateTime.UtcNow,
                            CreatedBy = Guid.Parse(updateDto.UpdatedBy),
                            UpdatedBy = Guid.Parse(updateDto.UpdatedBy),
                            IsDeleted = false
                        })
                        .ToList();

                    if (newMaterials.Any())
                    {
                        await _context.MaterialsUsed.AddRangeAsync(newMaterials);
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

        [HttpGet("dashboard-stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            try
            {
                // Get the latest FormStatus for each ServiceReportForm (ordered by CreatedDate)
                var formStatusStats = await _context.ServiceReportForms
                    .Where(sr => !sr.IsDeleted)
                    .Select(sr => new {
                        ServiceReportId = sr.ID,
                        LatestFormStatus = sr.FormStatus
                            .Where(fs => !fs.IsDeleted)
                            .OrderByDescending(fs => fs.CreatedDate)
                            .FirstOrDefault()
                    })
                    .Where(x => x.LatestFormStatus != null)
                    .GroupBy(x => x.LatestFormStatus.FormStatusWarehouse.Name)
                    .Select(g => new {
                        StatusName = g.Key ?? "Unknown",
                        Count = g.Count()
                    })
                    .OrderByDescending(x => x.Count)
                    .ToListAsync();

                var totalReports = await _context.ServiceReportForms
                    .CountAsync(sr => !sr.IsDeleted);

                var recentReports = await _context.ServiceReportForms
                    .Where(sr => !sr.IsDeleted && sr.CreatedDate >= DateTime.UtcNow.AddDays(-30))
                    .CountAsync();

                // Get completion rate using latest FormStatus (ordered by CreatedDate)
                var completedReports = await _context.ServiceReportForms
                    .Where(sr => !sr.IsDeleted)
                    .Where(sr => sr.FormStatus
                        .Where(fs => !fs.IsDeleted)
                        .OrderByDescending(fs => fs.CreatedDate)
                        .FirstOrDefault().FormStatusWarehouse.Name.ToLower() == "close")
                    .CountAsync();

                var completionRate = totalReports > 0 ? Math.Round((double)completedReports / totalReports * 100, 1) : 0;

                var result = new {
                    FormStatusDistribution = formStatusStats,
                    TotalReports = totalReports,
                    RecentReports = recentReports,
                    CompletedReports = completedReports,
                    CompletionRate = completionRate,
                    LastUpdated = DateTime.UtcNow
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching dashboard statistics.", error = ex.Message });
            }
        }

        private bool ServiceReportExists(Guid id)
        {
            return _context.ServiceReportForms.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}
