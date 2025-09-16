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
    public class PMRTUCabinetCoolingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PMRTUCabinetCoolingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PMRTUCabinetCooling
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PMRTUCabinetCoolingDto>>> GetPMRTUCabinetCoolings()
        {
            var pmRTUCabinetCoolings = await _context.PMRTUCabinetCoolings
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Include(p => p.PMReportForm)
                .Where(p => !p.IsDeleted)
                .Select(p => new PMRTUCabinetCoolingDto
                {
                    ID = p.ID,
                    PMReportFormRTUID = p.PMReportFormRTUID,
                    Remarks = p.Remarks,

                    FanNumber = p.FanNumber,
                    FunctionalStatus = p.FunctionalStatus,
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

            return Ok(pmRTUCabinetCoolings);
        }

        // GET: api/PMRTUCabinetCooling/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PMRTUCabinetCoolingDto>> GetPMRTUCabinetCooling(Guid id)
        {
            var pmRTUCabinetCooling = await _context.PMRTUCabinetCoolings
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Include(p => p.PMReportForm)
                .Where(p => p.ID == id && !p.IsDeleted)
                .Select(p => new PMRTUCabinetCoolingDto
                {
                    ID = p.ID,
                    PMReportFormRTUID = p.PMReportFormRTUID,
                    Remarks = p.Remarks,
                    FanNumber = p.FanNumber,
                    FunctionalStatus = p.FunctionalStatus,
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

            if (pmRTUCabinetCooling == null)
            {
                return NotFound();
            }

            return Ok(pmRTUCabinetCooling);
        }

        // POST: api/PMRTUCabinetCooling
        [HttpPost]
        public async Task<ActionResult<PMRTUCabinetCoolingDto>> PostPMRTUCabinetCooling(CreatePMRTUCabinetCoolingDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userGuid = userId != null ? Guid.Parse(userId) : (Guid?)null;

            var pmRTUCabinetCooling = new PMRTUCabinetCooling
            {
                ID = Guid.NewGuid(),
                PMReportFormRTUID = createDto.PMReportFormRTUID,
                Remarks = createDto.Remarks,
                FanNumber = createDto.FanNumber,
                FunctionalStatus = createDto.FunctionalStatus,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CreatedBy = userGuid,
                UpdatedBy = userGuid
            };

            _context.PMRTUCabinetCoolings.Add(pmRTUCabinetCooling);
            await _context.SaveChangesAsync();

            var createdPMRTUCabinetCooling = await _context.PMRTUCabinetCoolings
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Include(p => p.PMReportForm)
                .Where(p => p.ID == pmRTUCabinetCooling.ID)
                .Select(p => new PMRTUCabinetCoolingDto
                {
                    ID = p.ID,
                    PMReportFormRTUID = p.PMReportFormRTUID,
                    Remarks = p.Remarks,
                    FanNumber = p.FanNumber,
                    FunctionalStatus = p.FunctionalStatus,
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

            return CreatedAtAction("GetPMRTUCabinetCooling", new { id = pmRTUCabinetCooling.ID }, createdPMRTUCabinetCooling);
        }

        // PUT: api/PMRTUCabinetCooling/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPMRTUCabinetCooling(Guid id, UpdatePMRTUCabinetCoolingDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userGuid = userId != null ? Guid.Parse(userId) : (Guid?)null;

            var pmRTUCabinetCooling = await _context.PMRTUCabinetCoolings.FindAsync(id);
            if (pmRTUCabinetCooling == null || pmRTUCabinetCooling.IsDeleted)
            {
                return NotFound();
            }

            pmRTUCabinetCooling.PMReportFormRTUID = updateDto.PMReportFormRTUID;
            pmRTUCabinetCooling.Remarks = updateDto.Remarks;
            pmRTUCabinetCooling.FanNumber = updateDto.FanNumber;
            pmRTUCabinetCooling.FunctionalStatus = updateDto.FunctionalStatus;
            pmRTUCabinetCooling.UpdatedDate = DateTime.Now;
            pmRTUCabinetCooling.UpdatedBy = userGuid;

            _context.Entry(pmRTUCabinetCooling).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PMRTUCabinetCoolingExists(id))
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

        // DELETE: api/PMRTUCabinetCooling/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePMRTUCabinetCooling(Guid id)
        {
            var pmRTUCabinetCooling = await _context.PMRTUCabinetCoolings.FindAsync(id);
            if (pmRTUCabinetCooling == null || pmRTUCabinetCooling.IsDeleted)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userGuid = userId != null ? Guid.Parse(userId) : (Guid?)null;

            pmRTUCabinetCooling.IsDeleted = true;
            pmRTUCabinetCooling.UpdatedDate = DateTime.Now;
            pmRTUCabinetCooling.UpdatedBy = userGuid;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PMRTUCabinetCoolingExists(Guid id)
        {
            return _context.PMRTUCabinetCoolings.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}