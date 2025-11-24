using ControlTower.Data;
using ControlTower.DTOs.ReportManagementSystem;
using ControlTower.Models.ReportManagementSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportFormController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportFormController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the current user ID from JWT token claims
        /// </summary>
        /// <returns>Current user ID or null if not found/invalid</returns>
        private Guid? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var currentUserId))
            {
                return null;
            }
            return currentUserId;
        }

        // GET: api/ReportForm
        [HttpGet]
        public async Task<ActionResult<object>> GetReportForms(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string search = "",
            [FromQuery] Guid? reportFormTypeId = null,
            [FromQuery] Guid? SystemNameWarehouseID = null,
            [FromQuery] Guid? StationNameWarehouseID = null,
            [FromQuery] string sortField = "",
            [FromQuery] string sortDirection = "asc")
        {
            var query = _context.ReportForms
                .Where(rf => !rf.IsDeleted)
                .Include(rf => rf.ReportFormType)
                .Include(rf => rf.SystemNameWarehouse)
                .Include(rf => rf.StationNameWarehouse)
                .Include(rf => rf.CreatedByUser)
                .Include(rf => rf.UpdatedByUser)
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(rf => rf.JobNo.Contains(search) ||
                                         rf.ReportFormType.Name.Contains(search) ||
                                         rf.SystemNameWarehouse.Name.Contains(search) ||
                                         rf.StationNameWarehouse.Name.Contains(search) ||
                                         rf.CreatedByUser.FirstName.Contains(search) ||
                                         rf.CreatedByUser.LastName.Contains(search));
            }

            // Apply filters
            if (reportFormTypeId.HasValue)
            {
                query = query.Where(rf => rf.ReportFormTypeID == reportFormTypeId.Value);
            }

            if (SystemNameWarehouseID.HasValue)
            {
                query = query.Where(rf => rf.SystemNameWarehouseID == SystemNameWarehouseID.Value);
            }

            if (StationNameWarehouseID.HasValue)
            {
                query = query.Where(rf => rf.StationNameWarehouseID == StationNameWarehouseID.Value);
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(sortField))
            {
                switch (sortField.ToLower())
                {
                    case "reportformtypename":
                    case "specificreporttypename":
                        query = sortDirection.ToLower() == "desc" 
                            ? query.OrderByDescending(rf => rf.ReportFormType.Name)
                            : query.OrderBy(rf => rf.ReportFormType.Name);
                        break;
                    case "jobno":
                        query = sortDirection.ToLower() == "desc" 
                            ? query.OrderByDescending(rf => rf.JobNo)
                            : query.OrderBy(rf => rf.JobNo);
                        break;
                    case "systemname":
                        query = sortDirection.ToLower() == "desc" 
                            ? query.OrderByDescending(rf => rf.SystemNameWarehouse.Name)
                            : query.OrderBy(rf => rf.SystemNameWarehouse.Name);
                        break;
                    case "stationname":
                        query = sortDirection.ToLower() == "desc" 
                            ? query.OrderByDescending(rf => rf.StationNameWarehouse.Name)
                            : query.OrderBy(rf => rf.StationNameWarehouse.Name);
                        break;
                    case "formstatus":
                        query = sortDirection.ToLower() == "desc" 
                            ? query.OrderByDescending(rf => rf.FormStatus)
                            : query.OrderBy(rf => rf.FormStatus);
                        break;
                    case "createddate":
                        query = sortDirection.ToLower() == "desc" 
                            ? query.OrderByDescending(rf => rf.CreatedDate)
                            : query.OrderBy(rf => rf.CreatedDate);
                        break;
                    case "createdby":
                        query = sortDirection.ToLower() == "desc" 
                            ? query.OrderByDescending(rf => rf.CreatedByUser.FirstName + " " + rf.CreatedByUser.LastName)
                            : query.OrderBy(rf => rf.CreatedByUser.FirstName + " " + rf.CreatedByUser.LastName);
                        break;
                    case "updateddate":
                        query = sortDirection.ToLower() == "desc" 
                            ? query.OrderByDescending(rf => rf.UpdatedDate)
                            : query.OrderBy(rf => rf.UpdatedDate);
                        break;
                    default:
                        query = query.OrderByDescending(rf => rf.CreatedDate);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(rf => rf.CreatedDate);
            }

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var reportFormsRaw = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(rf => new
                {
                    ID = rf.ID,
                    ReportFormTypeID = rf.ReportFormTypeID,
                    ReportFormTypeName = rf.ReportFormType.Name,
                    JobNo = rf.JobNo,
                    SystemNameWarehouseID = rf.SystemNameWarehouseID,
                    SystemNameWarehouseName = rf.SystemNameWarehouse.Name,
                    StationNameWarehouseID = rf.StationNameWarehouseID,
                    StationNameWarehouseName = rf.StationNameWarehouse.Name,
                    // Get CM Report data if exists
                    CMCustomer = _context.CMReportForms
                        .Where(cm => cm.ReportFormID == rf.ID && !cm.IsDeleted)
                        .Select(cm => cm.Customer)
                        .FirstOrDefault(),
                    CMProjectNo = _context.CMReportForms
                        .Where(cm => cm.ReportFormID == rf.ID && !cm.IsDeleted)
                        .Select(cm => cm.ProjectNo)
                        .FirstOrDefault(),
                    PMRTUInfo = _context.PMReportFormRTU
                        .Where(pm => pm.ReportFormID == rf.ID && !pm.IsDeleted)
                        .Select(pm => new
                        {
                            pm.PMReportFormTypeID,
                            TypeName = pm.PMReportFormType.Name,
                            pm.Customer,
                            pm.ProjectNo,
                            FormStatusName = pm.FormStatusWarehouse != null ? pm.FormStatusWarehouse.Name : null
                        })
                        .FirstOrDefault(),
                    PMServerInfo = _context.PMReportFormServer
                        .Where(pm => pm.ReportFormID == rf.ID && !pm.IsDeleted)
                        .Select(pm => new
                        {
                            pm.PMReportFormTypeID,
                            TypeName = pm.PMReportFormType.Name,
                            pm.Customer,
                            pm.ProjectNo,
                            FormStatusName = pm.FormStatusWarehouse != null ? pm.FormStatusWarehouse.Name : null
                        })
                        .FirstOrDefault(),
                    IsDeleted = rf.IsDeleted,
                    CreatedDate = rf.CreatedDate,
                    UpdatedDate = rf.UpdatedDate,
                    CreatedBy = rf.CreatedBy,
                    CreatedByUserName = rf.CreatedByUser.FirstName + " " + rf.CreatedByUser.LastName,
                    UpdatedBy = rf.UpdatedBy,
                    UpdatedByUserName = rf.UpdatedByUser != null ? rf.UpdatedByUser.FirstName + " " + rf.UpdatedByUser.LastName : null,
                    UploadStatus = rf.UploadStatus,
                    UploadHostname = rf.UploadHostname,
                    UploadIPAddress = rf.UploadIPAddress,
                    FormStatus = rf.FormStatus,
                    CMTypeInfo = _context.CMReportForms
                        .Where(cm => cm.ReportFormID == rf.ID && !cm.IsDeleted)
                        .Select(cm => new
                        {
                            cm.CMReportFormTypeID,
                            TypeName = cm.CMReportFormType != null ? cm.CMReportFormType.Name : null,
                            FormStatusName = cm.FormStatusWarehouse != null ? cm.FormStatusWarehouse.Name : null
                        })
                        .FirstOrDefault()
                })
                .ToListAsync();

            var reportForms = reportFormsRaw.Select(rf =>
            {
                var cmInfo = rf.CMTypeInfo;
                var pmInfo = rf.PMRTUInfo ?? rf.PMServerInfo;
                var hasPMData = pmInfo != null;
                var hasCMData = cmInfo != null;
                var specificTypeName = hasPMData
                    ? pmInfo!.TypeName
                    : hasCMData
                        ? cmInfo!.TypeName
                        : rf.ReportFormTypeName;
                var specificTypeId = hasPMData
                    ? (Guid?)pmInfo!.PMReportFormTypeID
                    : hasCMData
                        ? cmInfo!.CMReportFormTypeID
                        : null;
                var derivedStatus = !string.IsNullOrWhiteSpace(pmInfo?.FormStatusName)
                    ? pmInfo!.FormStatusName
                    : (!string.IsNullOrWhiteSpace(cmInfo?.FormStatusName)
                        ? cmInfo!.FormStatusName
                        : rf.FormStatus);

                return new
                {
                    rf.ID,
                    rf.ReportFormTypeID,
                    rf.ReportFormTypeName,
                    SpecificReportTypeName = specificTypeName,
                    SpecificReportTypeID = specificTypeId,
                    rf.JobNo,
                    rf.SystemNameWarehouseID,
                    rf.SystemNameWarehouseName,
                    rf.StationNameWarehouseID,
                    rf.StationNameWarehouseName,
                    rf.CMCustomer,
                    rf.CMProjectNo,
                    PMCustomer = pmInfo?.Customer,
                    PMProjectNo = pmInfo?.ProjectNo,
                    rf.IsDeleted,
                    rf.CreatedDate,
                    rf.UpdatedDate,
                    rf.CreatedBy,
                    rf.CreatedByUserName,
                    rf.UpdatedBy,
                    rf.UpdatedByUserName,
                    rf.UploadStatus,
                    rf.UploadHostname,
                    rf.UploadIPAddress,
                    FormStatus = derivedStatus,
                    HasPMRTUData = rf.PMRTUInfo != null,
                    HasPMServerData = rf.PMServerInfo != null,
                    HasCMData = cmInfo != null
                };
            }).ToList();

            return Ok(new
            {
                data = reportForms,
                totalCount,
                totalPages,
                currentPage = page,
                pageSize
            });
        }

        // GET: api/ReportForm/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportFormDto>> GetReportForm(Guid id)
        {
            var reportForm = await _context.ReportForms
                .Where(rf => rf.ID == id && !rf.IsDeleted)
                .Include(rf => rf.ReportFormType)
                .Include(rf => rf.SystemNameWarehouse)
                .Include(rf => rf.StationNameWarehouse)
                .Include(rf => rf.CreatedByUser)
                .Include(rf => rf.UpdatedByUser)
                .Select(rf => new ReportFormDto
                {
                    ID = rf.ID,
                    ReportFormTypeID = rf.ReportFormTypeID,
                    ReportFormTypeName = rf.ReportFormType.Name,
                    JobNo = rf.JobNo,
                    SystemNameWarehouseID = rf.SystemNameWarehouseID,
                    SystemNameWarehouseName = rf.SystemNameWarehouse.Name,
                    StationNameWarehouseID = rf.StationNameWarehouseID,
                    StationNameWarehouseName = rf.StationNameWarehouse.Name,
                    IsDeleted = rf.IsDeleted,
                    CreatedDate = rf.CreatedDate,
                    UpdatedDate = rf.UpdatedDate,
                    CreatedBy = rf.CreatedBy,
                    CreatedByUserName = rf.CreatedByUser.FirstName + " " + rf.CreatedByUser.LastName,
                    UpdatedBy = rf.UpdatedBy,
                    UpdatedByUserName = rf.UpdatedByUser != null ? rf.UpdatedByUser.FirstName + " " + rf.UpdatedByUser.LastName : null,
                    UploadStatus = rf.UploadStatus,
                    UploadHostname = rf.UploadHostname,
                    UploadIPAddress = rf.UploadIPAddress,
                    FormStatus = rf.FormStatus
                })
                .FirstOrDefaultAsync();

            if (reportForm == null)
            {
                return NotFound(new { message = "ReportForm not found" });
            }

            return Ok(reportForm);
        }

        // GET: api/ReportForm/RTUPMReportForm/{id}
        [HttpGet("RTUPMReportForm/{id}")]
        public async Task<ActionResult<object>> GetRTUPMReportForm(Guid id)
        {
            var reportForm = await _context.ReportForms
                .Where(rf => rf.ID == id && !rf.IsDeleted)
                .Include(rf => rf.ReportFormType)
                .Include(rf => rf.SystemNameWarehouse)
                .Include(rf => rf.StationNameWarehouse)
                .Include(rf => rf.CreatedByUser)
                .Include(rf => rf.UpdatedByUser)
                .FirstOrDefaultAsync();

            if (reportForm == null)
            {
                return NotFound(new { message = "Report form not found" });
            }

            // Check if this report form has PM RTU data
            var pmReportFormRTU = await _context.PMReportFormRTU
                .Where(pm => pm.ReportFormID == id && !pm.IsDeleted)
                .Include(pm => pm.PMReportFormType)
                .Include(pm => pm.CreatedByUser)
                .Include(pm => pm.UpdatedByUser)
                .FirstOrDefaultAsync();

            if (pmReportFormRTU == null)
            {
                return BadRequest(new { message = "This report form is not an RTU PM report form" });
            }

            // Get PM Main RTU Cabinet data
            var pmMainRtuCabinet = await _context.PMMainRtuCabinets
                .Where(mc => mc.PMReportFormRTUID == pmReportFormRTU.ID && !mc.IsDeleted)
                .Include(mc => mc.CreatedByUser)
                .Include(mc => mc.UpdatedByUser)
                .Select(mc => new
                {
                    ID = mc.ID,
                    PMReportFormRTUID = mc.PMReportFormRTUID,
                    RTUCabinet = mc.RTUCabinet,
                    EquipmentRack = mc.EquipmentRack,
                    Monitor = mc.Monitor,
                    MouseKeyboard = mc.MouseKeyboard,
                    CPU6000Card = mc.CPU6000Card,
                    InputCard = mc.InputCard,
                    MegapopNTU = mc.MegapopNTU,
                    NetworkRouter = mc.NetworkRouter,
                    NetworkSwitch = mc.NetworkSwitch,
                    DigitalVideoRecorder = mc.DigitalVideoRecorder,
                    RTUDoorContact = mc.RTUDoorContact,
                    PowerSupplyUnit = mc.PowerSupplyUnit,
                    UPSTakingOverTest = mc.UPSTakingOverTest,
                    UPSBattery = mc.UPSBattery,
                    Remarks = mc.Remarks,
                    CreatedDate = mc.CreatedDate,
                    UpdatedDate = mc.UpdatedDate,
                    CreatedBy = mc.CreatedBy,
                    CreatedByUserName = mc.CreatedByUser != null ? mc.CreatedByUser.FirstName + " " + mc.CreatedByUser.LastName : null,
                    UpdatedBy = mc.UpdatedBy,
                    UpdatedByUserName = mc.UpdatedByUser != null ? mc.UpdatedByUser.FirstName + " " + mc.UpdatedByUser.LastName : null
                })
                .ToListAsync();

            // Get PM Chamber Magnetic Contact data
            var pmChamberMagneticContact = await _context.PMChamberMagneticContacts
                .Where(cmc => cmc.PMReportFormRTUID == pmReportFormRTU.ID && !cmc.IsDeleted)
                .Include(cmc => cmc.CreatedByUser)
                .Include(cmc => cmc.UpdatedByUser)
                .Select(cmc => new
                {
                    ID = cmc.ID,
                    PMReportFormRTUID = cmc.PMReportFormRTUID,
                    ChamberNumber = cmc.ChamberNumber,
                    ChamberOGBox = cmc.ChamberOGBox,
                    ChamberContact1 = cmc.ChamberContact1,
                    ChamberContact2 = cmc.ChamberContact2,
                    ChamberContact3 = cmc.ChamberContact3,
                    Remarks = cmc.Remarks,
                    CreatedDate = cmc.CreatedDate,
                    UpdatedDate = cmc.UpdatedDate,
                    CreatedBy = cmc.CreatedBy,
                    CreatedByUserName = cmc.CreatedByUser != null ? cmc.CreatedByUser.FirstName + " " + cmc.CreatedByUser.LastName : null,
                    UpdatedBy = cmc.UpdatedBy,
                    UpdatedByUserName = cmc.UpdatedByUser != null ? cmc.UpdatedByUser.FirstName + " " + cmc.UpdatedByUser.LastName : null
                })
                .ToListAsync();

            // Get PM RTU Cabinet Cooling data
            var pmRTUCabinetCooling = await _context.PMRTUCabinetCoolings
                .Where(cc => cc.PMReportFormRTUID == pmReportFormRTU.ID && !cc.IsDeleted)
                .Include(cc => cc.CreatedByUser)
                .Include(cc => cc.UpdatedByUser)
                .Select(cc => new
                {
                    ID = cc.ID,
                    PMReportFormRTUID = cc.PMReportFormRTUID,
                    FanNumber = cc.FanNumber,
                    FunctionalStatus = cc.FunctionalStatus,
                    Remarks = cc.Remarks,
                    CreatedDate = cc.CreatedDate,
                    UpdatedDate = cc.UpdatedDate,
                    CreatedBy = cc.CreatedBy,
                    CreatedByUserName = cc.CreatedByUser != null ? cc.CreatedByUser.FirstName + " " + cc.CreatedByUser.LastName : null,
                    UpdatedBy = cc.UpdatedBy,
                    UpdatedByUserName = cc.UpdatedByUser != null ? cc.UpdatedByUser.FirstName + " " + cc.UpdatedByUser.LastName : null
                })
                .ToListAsync();

            // Get PM DVR Equipment data
            var pmDVREquipment = await _context.PMDVREquipments
                .Where(dvr => dvr.PMReportFormRTUID == pmReportFormRTU.ID && !dvr.IsDeleted)
                .Include(dvr => dvr.CreatedByUser)
                .Include(dvr => dvr.UpdatedByUser)
                .Select(dvr => new
                {
                    ID = dvr.ID,
                    PMReportFormRTUID = dvr.PMReportFormRTUID,
                    DVRComm = dvr.DVRComm,
                    DVRRAIDComm = dvr.DVRRAIDComm,
                    TimeSyncNTPServer = dvr.TimeSyncNTPServer,
                    Recording24x7 = dvr.Recording24x7,
                    Remarks = dvr.Remarks,
                    CreatedDate = dvr.CreatedDate,
                    UpdatedDate = dvr.UpdatedDate,
                    CreatedBy = dvr.CreatedBy,
                    CreatedByUserName = dvr.CreatedByUser != null ? dvr.CreatedByUser.FirstName + " " + dvr.CreatedByUser.LastName : null,
                    UpdatedBy = dvr.UpdatedBy,
                    UpdatedByUserName = dvr.UpdatedByUser != null ? dvr.UpdatedByUser.FirstName + " " + dvr.UpdatedByUser.LastName : null
                })
                .ToListAsync();

            // Get images for each RTU PM section
            var pmMainRtuCabinetImages = await _context.ReportFormImages
                .Where(img => img.ReportFormID == id && !img.IsDeleted &&
                             img.StoredDirectory.Contains("PMMainRtuCabinets"))
                .Include(img => img.ReportFormImageType)
                .Select(img => new
                {
                    ID = img.ID,
                    ImageName = img.ImageName,
                    StoredDirectory = img.StoredDirectory,
                    ImageTypeName = img.ReportFormImageType.ImageTypeName,
                    UploadedDate = img.UploadedDate
                })
                .ToListAsync();

            var pmChamberMagneticContactImages = await _context.ReportFormImages
                .Where(img => img.ReportFormID == id && !img.IsDeleted &&
                             img.StoredDirectory.Contains("PMChamberMagneticContacts"))
                .Include(img => img.ReportFormImageType)
                .Select(img => new
                {
                    ID = img.ID,
                    ImageName = img.ImageName,
                    StoredDirectory = img.StoredDirectory,
                    ImageTypeName = img.ReportFormImageType.ImageTypeName,
                    UploadedDate = img.UploadedDate
                })
                .ToListAsync();

            var pmRTUCabinetCoolingImages = await _context.ReportFormImages
                .Where(img => img.ReportFormID == id && !img.IsDeleted &&
                             img.StoredDirectory.Contains("PMRTUCabinetCoolings"))
                .Include(img => img.ReportFormImageType)
                .Select(img => new
                {
                    ID = img.ID,
                    ImageName = img.ImageName,
                    StoredDirectory = img.StoredDirectory,
                    ImageTypeName = img.ReportFormImageType.ImageTypeName,
                    UploadedDate = img.UploadedDate
                })
                .ToListAsync();

            var pmDVREquipmentImages = await _context.ReportFormImages
                .Where(img => img.ReportFormID == id && !img.IsDeleted &&
                             img.StoredDirectory.Contains("PMDVREquipments"))
                .Include(img => img.ReportFormImageType)
                .Select(img => new
                {
                    ID = img.ID,
                    ImageName = img.ImageName,
                    StoredDirectory = img.StoredDirectory,
                    ImageTypeName = img.ReportFormImageType.ImageTypeName,
                    UploadedDate = img.UploadedDate
                })
                .ToListAsync();

            var result = new
            {
                // Report Form basic information
                ID = reportForm.ID,
                ReportFormTypeID = reportForm.ReportFormTypeID,
                ReportFormTypeName = reportForm.ReportFormType.Name,
                JobNo = reportForm.JobNo,
                SystemNameWarehouseID = reportForm.SystemNameWarehouseID,
                SystemNameWarehouseName = reportForm.SystemNameWarehouse.Name,
                StationNameWarehouseID = reportForm.StationNameWarehouseID,
                StationNameWarehouseName = reportForm.StationNameWarehouse.Name,
                UploadStatus = reportForm.UploadStatus,
                UploadHostname = reportForm.UploadHostname,
                UploadIPAddress = reportForm.UploadIPAddress,
                FormStatus = reportForm.FormStatus,
                CreatedDate = reportForm.CreatedDate,
                UpdatedDate = reportForm.UpdatedDate,
                CreatedBy = reportForm.CreatedBy,
                CreatedByUserName = reportForm.CreatedByUser != null ? reportForm.CreatedByUser.FirstName + " " + reportForm.CreatedByUser.LastName : null,
                UpdatedBy = reportForm.UpdatedBy,
                UpdatedByUserName = reportForm.UpdatedByUser != null ? reportForm.UpdatedByUser.FirstName + " " + reportForm.UpdatedByUser.LastName : null,

                // PM Report RTU specific data
                PMReportFormRTU = new
                {
                    ID = pmReportFormRTU.ID,
                    PMReportFormTypeID = pmReportFormRTU.PMReportFormTypeID,
                    PMReportFormTypeName = pmReportFormRTU.PMReportFormType.Name,
                    ReportTitle = pmReportFormRTU.ReportTitle,
                    ProjectNo = pmReportFormRTU.ProjectNo,
                    Customer = pmReportFormRTU.Customer,
                    DateOfService = pmReportFormRTU.DateOfService,
                    CleaningOfCabinet = pmReportFormRTU.CleaningOfCabinet,
                    Remarks = pmReportFormRTU.Remarks,
                    AttendedBy = pmReportFormRTU.AttendedBy,
                    ApprovedBy = pmReportFormRTU.ApprovedBy,
                    CreatedDate = pmReportFormRTU.CreatedDate,
                    UpdatedDate = pmReportFormRTU.UpdatedDate,
                    CreatedBy = pmReportFormRTU.CreatedBy,
                    CreatedByUserName = pmReportFormRTU.CreatedByUser != null ? pmReportFormRTU.CreatedByUser.FirstName + " " + pmReportFormRTU.CreatedByUser.LastName : null,
                    UpdatedBy = pmReportFormRTU.UpdatedBy,
                    UpdatedByUserName = pmReportFormRTU.UpdatedByUser != null ? pmReportFormRTU.UpdatedByUser.FirstName + " " + pmReportFormRTU.UpdatedByUser.LastName : null
                },

                // PM Main RTU Cabinet data (array)
                PMMainRtuCabinet = pmMainRtuCabinet,

                // PM Chamber Magnetic Contact data (array)
                PMChamberMagneticContact = pmChamberMagneticContact,

                // PM RTU Cabinet Cooling data (array)
                PMRTUCabinetCooling = pmRTUCabinetCooling,

                // PM DVR Equipment data (array)
                PMDVREquipment = pmDVREquipment,

                // Images for each section
                PMMainRtuCabinetImages = pmMainRtuCabinetImages,
                PMChamberMagneticContactImages = pmChamberMagneticContactImages,
                PMRTUCabinetCoolingImages = pmRTUCabinetCoolingImages,
                PMDVREquipmentImages = pmDVREquipmentImages
            };

            return Ok(result);
        }

        // GET: api/ReportForm/ByReportFormType/5
        [HttpGet("ByReportFormType/{reportFormTypeId}")]
        public async Task<ActionResult<IEnumerable<ReportFormDto>>> GetReportFormsByType(Guid reportFormTypeId)
        {
            var reportForms = await _context.ReportForms
                .Where(rf => rf.ReportFormTypeID == reportFormTypeId && !rf.IsDeleted)
                .Include(rf => rf.ReportFormType)
                .Include(rf => rf.SystemNameWarehouse)
                .Include(rf => rf.StationNameWarehouse)
                .Include(rf => rf.CreatedByUser)
                .Include(rf => rf.UpdatedByUser)
                .Select(rf => new ReportFormDto
                {
                    ID = rf.ID,
                    ReportFormTypeID = rf.ReportFormTypeID,
                    ReportFormTypeName = rf.ReportFormType.Name,
                    JobNo = rf.JobNo,
                    SystemNameWarehouseID = rf.SystemNameWarehouseID,
                    SystemNameWarehouseName = rf.SystemNameWarehouse.Name,
                    StationNameWarehouseID = rf.StationNameWarehouseID,
                    StationNameWarehouseName = rf.StationNameWarehouse.Name,
                    IsDeleted = rf.IsDeleted,
                    CreatedDate = rf.CreatedDate,
                    UpdatedDate = rf.UpdatedDate,
                    CreatedBy = rf.CreatedBy,
                    CreatedByUserName = rf.CreatedByUser.FirstName + " " + rf.CreatedByUser.LastName,
                    UpdatedBy = rf.UpdatedBy,
                    UpdatedByUserName = rf.UpdatedByUser != null ? rf.UpdatedByUser.FirstName + " " + rf.UpdatedByUser.LastName : null,
                    UploadStatus = rf.UploadStatus,
                    UploadHostname = rf.UploadHostname,
                    UploadIPAddress = rf.UploadIPAddress,
                    FormStatus = rf.FormStatus
                })
                .OrderByDescending(rf => rf.CreatedDate)
                .ToListAsync();

            return Ok(reportForms);
        }

        // GET: api/ReportForm/BySystemName/5
        [HttpGet("BySystemName/{SystemNameWarehouseID}")]
        public async Task<ActionResult<IEnumerable<ReportFormDto>>> GetReportFormsBySystemName(Guid SystemNameWarehouseID)
        {
            var reportForms = await _context.ReportForms
                .Where(rf => rf.SystemNameWarehouseID == SystemNameWarehouseID && !rf.IsDeleted)
                .Include(rf => rf.ReportFormType)
                .Include(rf => rf.SystemNameWarehouse)
                .Include(rf => rf.StationNameWarehouse)
                .Include(rf => rf.CreatedByUser)
                .Include(rf => rf.UpdatedByUser)
                .Select(rf => new ReportFormDto
                {
                    ID = rf.ID,
                    ReportFormTypeID = rf.ReportFormTypeID,
                    ReportFormTypeName = rf.ReportFormType.Name,
                    JobNo = rf.JobNo,
                    SystemNameWarehouseID = rf.SystemNameWarehouseID,
                    SystemNameWarehouseName = rf.SystemNameWarehouse.Name,
                    StationNameWarehouseID = rf.StationNameWarehouseID,
                    StationNameWarehouseName = rf.StationNameWarehouse.Name,
                    IsDeleted = rf.IsDeleted,
                    CreatedDate = rf.CreatedDate,
                    UpdatedDate = rf.UpdatedDate,
                    CreatedBy = rf.CreatedBy,
                    CreatedByUserName = rf.CreatedByUser.FirstName + " " + rf.CreatedByUser.LastName,
                    UpdatedBy = rf.UpdatedBy,
                    UpdatedByUserName = rf.UpdatedByUser != null ? rf.UpdatedByUser.FirstName + " " + rf.UpdatedByUser.LastName : null,
                    UploadStatus = rf.UploadStatus,
                    UploadHostname = rf.UploadHostname,
                    UploadIPAddress = rf.UploadIPAddress,
                    FormStatus = rf.FormStatus
                })
                .OrderByDescending(rf => rf.CreatedDate)
                .ToListAsync();

            return Ok(reportForms);
        }

        // GET: api/ReportForm/ByStationName/5
        [HttpGet("ByStationName/{StationNameWarehouseID}")]
        public async Task<ActionResult<IEnumerable<object>>> GetReportFormsByStationName(Guid StationNameWarehouseID)
        {
            var reportForms = await _context.ReportForms
                .Where(rf => rf.StationNameWarehouseID == StationNameWarehouseID && !rf.IsDeleted)
                .Include(rf => rf.ReportFormType)
                .Include(rf => rf.SystemNameWarehouse)
                .Include(rf => rf.StationNameWarehouse)
                .Include(rf => rf.CreatedByUser)
                .Include(rf => rf.UpdatedByUser)
                .OrderByDescending(rf => rf.CreatedDate)
                .Select(rf => new
                {
                    ID = rf.ID,
                    ReportFormTypeID = rf.ReportFormTypeID,
                    ReportFormTypeName = rf.ReportFormType.Name,
                    JobNo = rf.JobNo,
                    SystemNameWarehouseID = rf.SystemNameWarehouseID,
                    SystemNameWarehouseName = rf.SystemNameWarehouse.Name,
                    StationNameWarehouseID = rf.StationNameWarehouseID,
                    StationNameWarehouseName = rf.StationNameWarehouse.Name,
                    CreatedDate = rf.CreatedDate,
                    CreatedBy = rf.CreatedBy,
                    CreatedByUserName = rf.CreatedByUser.FirstName + " " + rf.CreatedByUser.LastName,
                    UpdatedBy = rf.UpdatedBy,
                    UpdatedByUserName = rf.UpdatedByUser != null ? rf.UpdatedByUser.FirstName + " " + rf.UpdatedByUser.LastName : null,
                    FormStatus = rf.FormStatus
                })
                .ToListAsync();

            return Ok(reportForms);
        }

        // POST: api/ReportForm
        [HttpPost]
        public async Task<ActionResult<ReportFormDto>> CreateReportForm(CreateReportFormDto createDto)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            // Validate ReportFormType exists
            var reportFormTypeExists = await _context.ReportFormTypes
                .AnyAsync(rft => rft.ID == createDto.ReportFormTypeID && !rft.IsDeleted);
            if (!reportFormTypeExists)
            {
                return BadRequest(new { message = "Invalid ReportFormTypeID" });
            }

            // Validate SystemNameWarehouse exists
            var systemNameExists = await _context.SystemNameWarehouses
                .AnyAsync(s => s.ID == createDto.SystemNameWarehouseID && !s.IsDeleted);
            if (!systemNameExists)
            {
                return BadRequest(new { message = "Invalid SystemNameWarehouseID" });
            }

            // Validate StationNameWarehouse exists and belongs to the SystemName
            var stationNameExists = await _context.StationNameWarehouses
                .AnyAsync(s => s.ID == createDto.StationNameWarehouseID && s.SystemNameWarehouseID == createDto.SystemNameWarehouseID && !s.IsDeleted);
            if (!stationNameExists)
            {
                return BadRequest(new { message = "Invalid StationNameWarehouseID or StationName does not belong to the specified SystemName" });
            }

            var reportForm = new ReportForm
            {
                ID = Guid.NewGuid(),
                ReportFormTypeID = createDto.ReportFormTypeID,
                JobNo = createDto.JobNo,
                SystemNameWarehouseID = createDto.SystemNameWarehouseID,
                StationNameWarehouseID = createDto.StationNameWarehouseID,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = currentUserId.Value,
                UploadStatus = createDto.UploadStatus,
                UploadHostname = createDto.UploadHostname,
                UploadIPAddress = createDto.UploadIPAddress,
                FormStatus = createDto.FormStatus
            };

            _context.ReportForms.Add(reportForm);
            await _context.SaveChangesAsync();

            var result = await _context.ReportForms
                .Where(rf => rf.ID == reportForm.ID)
                .Include(rf => rf.ReportFormType)
                .Include(rf => rf.SystemNameWarehouse)
                .Include(rf => rf.StationNameWarehouse)
                .Include(rf => rf.CreatedByUser)
                .Select(rf => new ReportFormDto
                {
                    ID = rf.ID,
                    ReportFormTypeID = rf.ReportFormTypeID,
                    ReportFormTypeName = rf.ReportFormType.Name,
                    JobNo = rf.JobNo,
                    SystemNameWarehouseID = rf.SystemNameWarehouseID,
                    SystemNameWarehouseName = rf.SystemNameWarehouse.Name,
                    StationNameWarehouseID = rf.StationNameWarehouseID,
                    StationNameWarehouseName = rf.StationNameWarehouse.Name,
                    IsDeleted = rf.IsDeleted,
                    CreatedDate = rf.CreatedDate,
                    UpdatedDate = rf.UpdatedDate,
                    CreatedBy = rf.CreatedBy,
                    CreatedByUserName = rf.CreatedByUser.FirstName + " " + rf.CreatedByUser.LastName,
                    UpdatedBy = rf.UpdatedBy,
                    UpdatedByUserName = null,
                    UploadStatus = rf.UploadStatus,
                    UploadHostname = rf.UploadHostname,
                    UploadIPAddress = rf.UploadIPAddress,
                    FormStatus = rf.FormStatus
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction(nameof(GetReportForm), new { id = reportForm.ID }, result);
        }

        // PUT: api/ReportForm/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReportForm(Guid id, UpdateReportFormDto updateDto)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var reportForm = await _context.ReportForms.FindAsync(id);
            if (reportForm == null || reportForm.IsDeleted)
            {
                return NotFound(new { message = "ReportForm not found" });
            }

            // Validate SystemNameWarehouse exists
            var systemNameExists = await _context.SystemNameWarehouses
                .AnyAsync(s => s.ID == updateDto.SystemNameWarehouseID && !s.IsDeleted);
            if (!systemNameExists)
            {
                return BadRequest(new { message = "Invalid SystemNameWarehouseID" });
            }

            // Validate StationNameWarehouse exists and belongs to the SystemName
            var stationNameExists = await _context.StationNameWarehouses
                .AnyAsync(s => s.ID == updateDto.StationNameWarehouseID && s.SystemNameWarehouseID == updateDto.SystemNameWarehouseID && !s.IsDeleted);
            if (!stationNameExists)
            {
                return BadRequest(new { message = "Invalid StationNameWarehouseID or StationName does not belong to the specified SystemName" });
            }

            //reportForm.ReportFormTypeID = updateDto.ReportFormTypeID;
            reportForm.JobNo = updateDto.JobNo;
            reportForm.SystemNameWarehouseID = updateDto.SystemNameWarehouseID;
            reportForm.StationNameWarehouseID = updateDto.StationNameWarehouseID;
            reportForm.UploadStatus = updateDto.UploadStatus;
            reportForm.UploadHostname = updateDto.UploadHostname;
            reportForm.UploadIPAddress = updateDto.UploadIPAddress;
            reportForm.FormStatus = updateDto.FormStatus;
            reportForm.UpdatedDate = DateTime.UtcNow;
            reportForm.UpdatedBy = currentUserId.Value;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportFormExists(id))
                {
                    return NotFound(new { message = "ReportForm not found" });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ReportForm/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportForm(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var reportForm = await _context.ReportForms.FindAsync(id);
            if (reportForm == null || reportForm.IsDeleted)
            {
                return NotFound(new { message = "ReportForm not found" });
            }

            reportForm.IsDeleted = true;
            reportForm.UpdatedDate = DateTime.UtcNow;
            reportForm.UpdatedBy = currentUserId.Value;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportFormExists(Guid id)
        {
            return _context.ReportForms.Any(rf => rf.ID == id && !rf.IsDeleted);
        }

        // GET: api/ReportForm/NextJobNumber
        [HttpGet("NextJobNumber")]
        public async Task<ActionResult<object>> GetNextJobNumber()
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                if (currentUserId == null)
                {
                    return Unauthorized(new { message = "User not authenticated" });
                }

                // Get current user's StaffCardID
                var currentUser = await _context.Users
                    .Where(u => u.ID == currentUserId.Value && !u.IsDeleted)
                    .FirstOrDefaultAsync();

                if (currentUser == null)
                {
                    return NotFound(new { message = "Current user not found" });
                }

                // Count total existing ReportForms (not deleted) + 1
                var totalCount = await _context.ReportForms
                    .Where(rf => !rf.IsDeleted)
                    .CountAsync();

                var nextSequentialNumber = totalCount + 1;

                // Generate JobNo: StaffCardID + 4-digit sequential number
                var nextJobNo = $"{currentUser.StaffCardID}{nextSequentialNumber:D4}";

                return Ok(new { jobNumber = nextJobNo });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error generating next job number", error = ex.Message });
            }
        }

        // GET: api/ReportForm/CMReportForm/{id}
        [HttpGet("CMReportForm/{id}")]
        public async Task<ActionResult<object>> GetCMReportForm(Guid id)
        {
            var reportForm = await _context.ReportForms
                .Where(rf => rf.ID == id && !rf.IsDeleted)
                .Include(rf => rf.ReportFormType)
                .Include(rf => rf.SystemNameWarehouse)
                .Include(rf => rf.StationNameWarehouse)
                .Include(rf => rf.CreatedByUser)
                .Include(rf => rf.UpdatedByUser)
                .FirstOrDefaultAsync();

            if (reportForm == null)
            {
                return NotFound(new { message = "Report form not found" });
            }

            // Check if this report form has CM data
            var cmReportForm = await _context.CMReportForms
                .Where(cm => cm.ReportFormID == id && !cm.IsDeleted)
                .Include(cm => cm.FurtherActionTakenWarehouse)
                .Include(cm => cm.FormStatusWarehouse)
                .Include(cm => cm.CreatedByUser)
                .Include(cm => cm.UpdatedByUser)
                .FirstOrDefaultAsync();

            if (cmReportForm == null)
            {
                return BadRequest(new { message = "This report form is not a CM report form" });
            }

            // Get Material Used data
            var materialUsed = await _context.CMMaterialUsed
                .Where(mu => mu.CMReportFormID == cmReportForm.ID && !mu.IsDeleted)
                .Include(mu => mu.CreatedByUser)
                .Include(mu => mu.UpdatedByUser)
                .Select(mu => new
                {
                    ID = mu.ID,
                    CMReportFormID = mu.CMReportFormID,
                    MaterialDescription = mu.ItemDescription,
                    OldSerialNo = mu.OldSerialNo,
                    NewSerialNo = mu.NewSerialNo,
                    Remarks = mu.Remark,
                    CreatedDate = mu.CreatedDate,
                    UpdatedDate = mu.UpdatedDate,
                    CreatedBy = mu.CreatedBy,
                    CreatedByUserName = mu.CreatedByUser != null ? mu.CreatedByUser.FirstName + " " + mu.CreatedByUser.LastName : null,
                    UpdatedBy = mu.UpdatedBy,
                    UpdatedByUserName = mu.UpdatedByUser != null ? mu.UpdatedByUser.FirstName + " " + mu.UpdatedByUser.LastName : null
                })
                .ToListAsync();

            // Get images for CM report form sections
            var beforeIssueImages = await _context.ReportFormImages
                .Where(img => img.ReportFormID == id && !img.IsDeleted &&
                             img.StoredDirectory.Contains("CMBeforeIssueImage"))
                .Include(img => img.ReportFormImageType)
                .Select(img => new
                {
                    ID = img.ID,
                    ImageName = img.ImageName,
                    StoredDirectory = img.StoredDirectory,
                    ImageTypeName = img.ReportFormImageType.ImageTypeName,
                    UploadedDate = img.UploadedDate
                })
                .ToListAsync();

            var afterActionImages = await _context.ReportFormImages
                .Where(img => img.ReportFormID == id && !img.IsDeleted &&
                             img.StoredDirectory.Contains("CMAfterIssueImage"))
                .Include(img => img.ReportFormImageType)
                .Select(img => new
                {
                    ID = img.ID,
                    ImageName = img.ImageName,
                    StoredDirectory = img.StoredDirectory,
                    ImageTypeName = img.ReportFormImageType.ImageTypeName,
                    UploadedDate = img.UploadedDate
                })
                .ToListAsync();

            var materialUsedOldSerialImages = await _context.ReportFormImages
                .Where(img => img.ReportFormID == id && !img.IsDeleted &&
                             img.StoredDirectory.Contains("CMMaterialUsedOldSerialNo"))
                .Include(img => img.ReportFormImageType)
                .Select(img => new
                {
                    ID = img.ID,
                    ImageName = img.ImageName,
                    StoredDirectory = img.StoredDirectory,
                    ImageTypeName = img.ReportFormImageType.ImageTypeName,
                    UploadedDate = img.UploadedDate
                })
                .ToListAsync();

            var materialUsedNewSerialImages = await _context.ReportFormImages
                .Where(img => img.ReportFormID == id && !img.IsDeleted &&
                             img.StoredDirectory.Contains("CMMaterialUsedNewSerialNo"))
                .Include(img => img.ReportFormImageType)
                .Select(img => new
                {
                    ID = img.ID,
                    ImageName = img.ImageName,
                    StoredDirectory = img.StoredDirectory,
                    ImageTypeName = img.ReportFormImageType.ImageTypeName,
                    UploadedDate = img.UploadedDate
                })
                .ToListAsync();

            var result = new
            {
                // Report Form basic information
                ID = reportForm.ID,
                ReportFormTypeID = reportForm.ReportFormTypeID,
                ReportFormTypeName = reportForm.ReportFormType.Name,
                JobNo = reportForm.JobNo,
                SystemNameWarehouseID = reportForm.SystemNameWarehouseID,
                SystemNameWarehouseName = reportForm.SystemNameWarehouse?.Name,
                StationNameWarehouseID = reportForm.StationNameWarehouseID,
                StationNameWarehouseName = reportForm.StationNameWarehouse?.Name,
                UploadStatus = reportForm.UploadStatus,
                UploadHostname = reportForm.UploadHostname,
                UploadIPAddress = reportForm.UploadIPAddress,
                FormStatus = reportForm.FormStatus,
                CreatedDate = reportForm.CreatedDate,
                UpdatedDate = reportForm.UpdatedDate,
                CreatedBy = reportForm.CreatedBy,
                CreatedByUserName = reportForm.CreatedByUser != null ? reportForm.CreatedByUser.FirstName + " " + reportForm.CreatedByUser.LastName : null,
                UpdatedBy = reportForm.UpdatedBy,
                UpdatedByUserName = reportForm.UpdatedByUser != null ? reportForm.UpdatedByUser.FirstName + " " + reportForm.UpdatedByUser.LastName : null,

                // CM Report Form specific data
                CMReportForm = new
                {
                    ID = cmReportForm.ID,
                    ReportFormID = cmReportForm.ReportFormID,
                    CMReportFormTypeID = cmReportForm.CMReportFormTypeID,
                    FurtherActionTakenID = cmReportForm.FurtherActionTakenID,
                    FurtherActionTakenName = cmReportForm.FurtherActionTakenWarehouse?.Name,
                    FormstatusID = cmReportForm.FormstatusID,
                    FormStatusName = cmReportForm.FormStatusWarehouse?.Name,
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
                    Remark = cmReportForm.Remark,
                    CreatedDate = cmReportForm.CreatedDate,
                    UpdatedDate = cmReportForm.UpdatedDate,
                    CreatedBy = cmReportForm.CreatedBy,
                    CreatedByUserName = cmReportForm.CreatedByUser != null ? cmReportForm.CreatedByUser.FirstName + " " + cmReportForm.CreatedByUser.LastName : null,
                    UpdatedBy = cmReportForm.UpdatedBy,
                    UpdatedByUserName = cmReportForm.UpdatedByUser != null ? cmReportForm.UpdatedByUser.FirstName + " " + cmReportForm.UpdatedByUser.LastName : null
                },

                // Material Used data (array)
                MaterialUsed = materialUsed,

                // Images for each section
                BeforeIssueImages = beforeIssueImages,
                AfterActionImages = afterActionImages,
                MaterialUsedOldSerialImages = materialUsedOldSerialImages,
                MaterialUsedNewSerialImages = materialUsedNewSerialImages
            };

            return Ok(result);
        }
    }
}
