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
    public class PMMainRtuCabinetController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PMMainRtuCabinetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PMMainRtuCabinet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PMMainRtuCabinetDto>>> GetPMMainRtuCabinets()
        {
            var pmMainRtuCabinets = await _context.PMMainRtuCabinets
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Include(p => p.PMReportForm)
                .Where(p => !p.IsDeleted)
                .Select(p => new PMMainRtuCabinetDto
                {
                    ID = p.ID,
                    PMReportFormRTUID = p.PMReportFormRTUID,
                    RTUCabinet = p.RTUCabinet,
                    EquipmentRack = p.EquipmentRack,
                    Monitor = p.Monitor,
                    MouseKeyboard = p.MouseKeyboard,
                    CPU6000Card = p.CPU6000Card,
                    InputCard = p.InputCard,
                    MegapopNTU = p.MegapopNTU,
                    NetworkRouter = p.NetworkRouter,
                    NetworkSwitch = p.NetworkSwitch,
                    DigitalVideoRecorder = p.DigitalVideoRecorder,
                    RTUDoorContact = p.RTUDoorContact,
                    PowerSupplyUnit = p.PowerSupplyUnit,
                    UPSTakingOverTest = p.UPSTakingOverTest,
                    UPSBattery = p.UPSBattery,
                    Remarks = p.Remarks,
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

            return Ok(pmMainRtuCabinets);
        }

        // GET: api/PMMainRtuCabinet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PMMainRtuCabinetDto>> GetPMMainRtuCabinet(Guid id)
        {
            var pmMainRtuCabinet = await _context.PMMainRtuCabinets
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Include(p => p.PMReportForm)
                .Where(p => p.ID == id && !p.IsDeleted)
                .Select(p => new PMMainRtuCabinetDto
                {
                    ID = p.ID,
                    PMReportFormRTUID = p.PMReportFormRTUID,
                    RTUCabinet = p.RTUCabinet,
                    EquipmentRack = p.EquipmentRack,
                    Monitor = p.Monitor,
                    MouseKeyboard = p.MouseKeyboard,
                    CPU6000Card = p.CPU6000Card,
                    InputCard = p.InputCard,
                    MegapopNTU = p.MegapopNTU,
                    NetworkRouter = p.NetworkRouter,
                    NetworkSwitch = p.NetworkSwitch,
                    DigitalVideoRecorder = p.DigitalVideoRecorder,
                    RTUDoorContact = p.RTUDoorContact,
                    PowerSupplyUnit = p.PowerSupplyUnit,
                    UPSTakingOverTest = p.UPSTakingOverTest,
                    UPSBattery = p.UPSBattery,
                    Remarks = p.Remarks,
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

            if (pmMainRtuCabinet == null)
            {
                return NotFound();
            }

            return Ok(pmMainRtuCabinet);
        }

        // POST: api/PMMainRtuCabinet
        [HttpPost]
        public async Task<ActionResult<PMMainRtuCabinetDto>> PostPMMainRtuCabinet(CreatePMMainRtuCabinetDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userGuid = userId != null ? Guid.Parse(userId) : (Guid?)null;

            var pmMainRtuCabinet = new PMMainRtuCabinet
            {
                ID = Guid.NewGuid(),
                PMReportFormRTUID = createDto.PMReportFormRTUID,
                RTUCabinet = createDto.RTUCabinet,
                EquipmentRack = createDto.EquipmentRack,
                Monitor = createDto.Monitor,
                MouseKeyboard = createDto.MouseKeyboard,
                CPU6000Card = createDto.CPU6000Card,
                InputCard = createDto.InputCard,
                MegapopNTU = createDto.MegapopNTU,
                NetworkRouter = createDto.NetworkRouter,
                NetworkSwitch = createDto.NetworkSwitch,
                DigitalVideoRecorder = createDto.DigitalVideoRecorder,
                RTUDoorContact = createDto.RTUDoorContact,
                PowerSupplyUnit = createDto.PowerSupplyUnit,
                UPSTakingOverTest = createDto.UPSTakingOverTest,
                UPSBattery = createDto.UPSBattery,
                Remarks = createDto.Remarks,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CreatedBy = userGuid,
                UpdatedBy = userGuid
            };

            _context.PMMainRtuCabinets.Add(pmMainRtuCabinet);
            await _context.SaveChangesAsync();

            var createdPMMainRtuCabinet = await _context.PMMainRtuCabinets
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Include(p => p.PMReportForm)
                .Where(p => p.ID == pmMainRtuCabinet.ID)
                .Select(p => new PMMainRtuCabinetDto
                {
                    ID = p.ID,
                    PMReportFormRTUID = p.PMReportFormRTUID,
                    RTUCabinet = p.RTUCabinet,
                    EquipmentRack = p.EquipmentRack,
                    Monitor = p.Monitor,
                    MouseKeyboard = p.MouseKeyboard,
                    CPU6000Card = p.CPU6000Card,
                    InputCard = p.InputCard,
                    MegapopNTU = p.MegapopNTU,
                    NetworkRouter = p.NetworkRouter,
                    NetworkSwitch = p.NetworkSwitch,
                    DigitalVideoRecorder = p.DigitalVideoRecorder,
                    RTUDoorContact = p.RTUDoorContact,
                    PowerSupplyUnit = p.PowerSupplyUnit,
                    UPSTakingOverTest = p.UPSTakingOverTest,
                    UPSBattery = p.UPSBattery,
                    Remarks = p.Remarks,
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

            return CreatedAtAction("GetPMMainRtuCabinet", new { id = pmMainRtuCabinet.ID }, createdPMMainRtuCabinet);
        }

        // PUT: api/PMMainRtuCabinet/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPMMainRtuCabinet(Guid id, UpdatePMMainRtuCabinetDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userGuid = userId != null ? Guid.Parse(userId) : (Guid?)null;

            var pmMainRtuCabinet = await _context.PMMainRtuCabinets.FindAsync(id);
            if (pmMainRtuCabinet == null || pmMainRtuCabinet.IsDeleted)
            {
                return NotFound();
            }

            pmMainRtuCabinet.PMReportFormRTUID = updateDto.PMReportFormRTUID;
            pmMainRtuCabinet.RTUCabinet = updateDto.RTUCabinet;
            pmMainRtuCabinet.EquipmentRack = updateDto.EquipmentRack;
            pmMainRtuCabinet.Monitor = updateDto.Monitor;
            pmMainRtuCabinet.MouseKeyboard = updateDto.MouseKeyboard;
            pmMainRtuCabinet.CPU6000Card = updateDto.CPU6000Card;
            pmMainRtuCabinet.InputCard = updateDto.InputCard;
            pmMainRtuCabinet.MegapopNTU = updateDto.MegapopNTU;
            pmMainRtuCabinet.NetworkRouter = updateDto.NetworkRouter;
            pmMainRtuCabinet.NetworkSwitch = updateDto.NetworkSwitch;
            pmMainRtuCabinet.DigitalVideoRecorder = updateDto.DigitalVideoRecorder;
            pmMainRtuCabinet.RTUDoorContact = updateDto.RTUDoorContact;
            pmMainRtuCabinet.PowerSupplyUnit = updateDto.PowerSupplyUnit;
            pmMainRtuCabinet.UPSTakingOverTest = updateDto.UPSTakingOverTest;
            pmMainRtuCabinet.UPSBattery = updateDto.UPSBattery;
            pmMainRtuCabinet.Remarks = updateDto.Remarks;
            pmMainRtuCabinet.UpdatedDate = DateTime.Now;
            pmMainRtuCabinet.UpdatedBy = userGuid;

            _context.Entry(pmMainRtuCabinet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PMMainRtuCabinetExists(id))
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

        // DELETE: api/PMMainRtuCabinet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePMMainRtuCabinet(Guid id)
        {
            var pmMainRtuCabinet = await _context.PMMainRtuCabinets.FindAsync(id);
            if (pmMainRtuCabinet == null || pmMainRtuCabinet.IsDeleted)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userGuid = userId != null ? Guid.Parse(userId) : (Guid?)null;

            pmMainRtuCabinet.IsDeleted = true;
            pmMainRtuCabinet.UpdatedDate = DateTime.Now;
            pmMainRtuCabinet.UpdatedBy = userGuid;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PMMainRtuCabinetExists(Guid id)
        {
            return _context.PMMainRtuCabinets.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}