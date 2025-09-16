using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.DTOs.ReportManagementSystem;
using ControlTower.Models.ReportManagementSystem;
using System.Security.Claims;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PMDVREquipmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PMDVREquipmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PMDVREquipment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PMDVREquipmentDto>>> GetPMDVREquipments()
        {
            var pmDVREquipments = await _context.PMDVREquipments
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Include(p => p.PMReportForm)
                .Where(p => !p.IsDeleted)
                .Select(p => new PMDVREquipmentDto
                {
                    ID = p.ID,
                    PMReportFormRTUID = p.PMReportFormRTUID,
                    Remarks = p.Remarks,
                    DVRComm = p.DVRComm,
                    DVRRAIDComm = p.DVRRAIDComm,
                    TimeSyncNTPServer = p.TimeSyncNTPServer,
                    Recording24x7 = p.Recording24x7,
                    IsDeleted = p.IsDeleted,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    CreatedBy = p.CreatedBy,
                    UpdatedBy = p.UpdatedBy,
                    CreatedByUserName = p.CreatedByUser != null ? p.CreatedByUser.FirstName + " " + p.CreatedByUser.LastName : null,
                    UpdatedByUserName = p.UpdatedByUser != null ? p.UpdatedByUser.FirstName + " " + p.UpdatedByUser.LastName : null,
                    PMReportFormName = p.PMReportForm != null ? p.PMReportForm.ReportTitle : null
                })
                .ToListAsync();

            return Ok(pmDVREquipments);
        }

        // GET: api/PMDVREquipment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PMDVREquipmentDto>> GetPMDVREquipment(Guid id)
        {
            var pmDVREquipment = await _context.PMDVREquipments
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Include(p => p.PMReportForm)
                .Where(p => p.ID == id && !p.IsDeleted)
                .Select(p => new PMDVREquipmentDto
                {
                    ID = p.ID,
                    PMReportFormRTUID = p.PMReportFormRTUID,
                    Remarks = p.Remarks,
                    DVRComm = p.DVRComm,
                    DVRRAIDComm = p.DVRRAIDComm,
                    TimeSyncNTPServer = p.TimeSyncNTPServer,
                    Recording24x7 = p.Recording24x7,
                    IsDeleted = p.IsDeleted,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    CreatedBy = p.CreatedBy,
                    UpdatedBy = p.UpdatedBy,
                    CreatedByUserName = p.CreatedByUser != null ? p.CreatedByUser.FirstName + " " + p.CreatedByUser.LastName : null,
                    UpdatedByUserName = p.UpdatedByUser != null ? p.UpdatedByUser.FirstName + " " + p.UpdatedByUser.LastName : null,
                    PMReportFormName = p.PMReportForm != null ? p.PMReportForm.ReportTitle : null
                })
                .FirstOrDefaultAsync();

            if (pmDVREquipment == null)
            {
                return NotFound();
            }

            return Ok(pmDVREquipment);
        }

        // POST: api/PMDVREquipment
        [HttpPost]
        public async Task<ActionResult<PMDVREquipmentDto>> PostPMDVREquipment(CreatePMDVREquipmentDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userGuid = userId != null ? Guid.Parse(userId) : (Guid?)null;

            var pmDVREquipment = new PMDVREquipment
            {
                ID = Guid.NewGuid(),
                PMReportFormRTUID = createDto.PMReportFormRTUID,
                Remarks = createDto.Remarks,
                DVRComm = createDto.DVRComm,
                DVRRAIDComm = createDto.DVRRAIDComm,
                TimeSyncNTPServer = createDto.TimeSyncNTPServer,
                Recording24x7 = createDto.Recording24x7,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CreatedBy = userGuid,
                UpdatedBy = userGuid
            };

            _context.PMDVREquipments.Add(pmDVREquipment);
            await _context.SaveChangesAsync();

            var createdPMDVREquipment = await _context.PMDVREquipments
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Include(p => p.PMReportForm)
                .Where(p => p.ID == pmDVREquipment.ID)
                .Select(p => new PMDVREquipmentDto
                {
                    ID = p.ID,
                    PMReportFormRTUID = p.PMReportFormRTUID,
                    Remarks = p.Remarks,
                    DVRComm = p.DVRComm,
                    DVRRAIDComm = p.DVRRAIDComm,
                    TimeSyncNTPServer = p.TimeSyncNTPServer,
                    Recording24x7 = p.Recording24x7,
                    IsDeleted = p.IsDeleted,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdatedDate,
                    CreatedBy = p.CreatedBy,
                    UpdatedBy = p.UpdatedBy,
                    CreatedByUserName = p.CreatedByUser != null ? p.CreatedByUser.FirstName + " " + p.CreatedByUser.LastName : null,
                    UpdatedByUserName = p.UpdatedByUser != null ? p.UpdatedByUser.FirstName + " " + p.UpdatedByUser.LastName : null,
                    PMReportFormName = p.PMReportForm != null ? p.PMReportForm.ReportTitle : null
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction("GetPMDVREquipment", new { id = pmDVREquipment.ID }, createdPMDVREquipment);
        }

        // PUT: api/PMDVREquipment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPMDVREquipment(Guid id, UpdatePMDVREquipmentDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userGuid = userId != null ? Guid.Parse(userId) : (Guid?)null;

            var pmDVREquipment = await _context.PMDVREquipments.FindAsync(id);
            if (pmDVREquipment == null || pmDVREquipment.IsDeleted)
            {
                return NotFound();
            }

            pmDVREquipment.PMReportFormRTUID = updateDto.PMReportFormRTUID;
            pmDVREquipment.Remarks = updateDto.Remarks;
            pmDVREquipment.DVRComm = updateDto.DVRComm;
            pmDVREquipment.DVRRAIDComm = updateDto.DVRRAIDComm;
            pmDVREquipment.TimeSyncNTPServer = updateDto.TimeSyncNTPServer;
            pmDVREquipment.Recording24x7 = updateDto.Recording24x7;
            pmDVREquipment.UpdatedDate = DateTime.Now;
            pmDVREquipment.UpdatedBy = userGuid;

            _context.Entry(pmDVREquipment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PMDVREquipmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/PMDVREquipment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePMDVREquipment(Guid id)
        {
            var pmDVREquipment = await _context.PMDVREquipments.FindAsync(id);
            if (pmDVREquipment == null || pmDVREquipment.IsDeleted)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userGuid = userId != null ? Guid.Parse(userId) : (Guid?)null;

            pmDVREquipment.IsDeleted = true;
            pmDVREquipment.UpdatedDate = DateTime.Now;
            pmDVREquipment.UpdatedBy = userGuid;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PMDVREquipmentExists(Guid id)
        {
            return _context.PMDVREquipments.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}