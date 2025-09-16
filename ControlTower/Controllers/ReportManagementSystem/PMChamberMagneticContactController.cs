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
    public class PMChamberMagneticContactController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PMChamberMagneticContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PMChamberMagneticContact
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PMChamberMagneticContactDto>>> GetPMChamberMagneticContacts()
        {
            var pmChamberMagneticContacts = await _context.PMChamberMagneticContacts
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Include(p => p.PMReportForm)
                .Where(p => !p.IsDeleted)
                .Select(p => new PMChamberMagneticContactDto
                {
                    ID = p.ID,
                    PMReportFormRTUID = p.PMReportFormRTUID,
                    Remarks = p.Remarks,
                    ChamberNumber = p.ChamberNumber,
                    ChamberOGBox = p.ChamberOGBox,
                    ChamberContact1 = p.ChamberContact1,
                    ChamberContact2 = p.ChamberContact2,
                    ChamberContact3 = p.ChamberContact3,
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

            return Ok(pmChamberMagneticContacts);
        }

        // GET: api/PMChamberMagneticContact/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PMChamberMagneticContactDto>> GetPMChamberMagneticContact(Guid id)
        {
            var pmChamberMagneticContact = await _context.PMChamberMagneticContacts
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Include(p => p.PMReportForm)
                .Where(p => p.ID == id && !p.IsDeleted)
                .Select(p => new PMChamberMagneticContactDto
                {
                    ID = p.ID,
                    PMReportFormRTUID = p.PMReportFormRTUID,
                    Remarks = p.Remarks,
                    ChamberNumber = p.ChamberNumber,
                    ChamberOGBox = p.ChamberOGBox,
                    ChamberContact1 = p.ChamberContact1,
                    ChamberContact2 = p.ChamberContact2,
                    ChamberContact3 = p.ChamberContact3,
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

            if (pmChamberMagneticContact == null)
            {
                return NotFound();
            }

            return Ok(pmChamberMagneticContact);
        }

        // POST: api/PMChamberMagneticContact
        [HttpPost]
        public async Task<ActionResult<PMChamberMagneticContactDto>> PostPMChamberMagneticContact(CreatePMChamberMagneticContactDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userGuid = userId != null ? Guid.Parse(userId) : (Guid?)null;

            var pmChamberMagneticContact = new PMChamberMagneticContact
            {
                ID = Guid.NewGuid(),
                PMReportFormRTUID = createDto.PMReportFormRTUID,
                Remarks = createDto.Remarks,
                ChamberNumber = createDto.ChamberNumber,
                ChamberOGBox = createDto.ChamberOGBox,
                ChamberContact1 = createDto.ChamberContact1,
                ChamberContact2 = createDto.ChamberContact2,
                ChamberContact3 = createDto.ChamberContact3,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                CreatedBy = userGuid,
                UpdatedBy = userGuid
            };

            _context.PMChamberMagneticContacts.Add(pmChamberMagneticContact);
            await _context.SaveChangesAsync();

            var createdPMChamberMagneticContact = await _context.PMChamberMagneticContacts
                .Include(p => p.CreatedByUser)
                .Include(p => p.UpdatedByUser)
                .Include(p => p.PMReportForm)
                .Where(p => p.ID == pmChamberMagneticContact.ID)
                .Select(p => new PMChamberMagneticContactDto
                {
                    ID = p.ID,
                    PMReportFormRTUID = p.PMReportFormRTUID,
                    Remarks = p.Remarks,
                    ChamberNumber = p.ChamberNumber,
                    ChamberOGBox = p.ChamberOGBox,
                    ChamberContact1 = p.ChamberContact1,
                    ChamberContact2 = p.ChamberContact2,
                    ChamberContact3 = p.ChamberContact3,
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

            return CreatedAtAction("GetPMChamberMagneticContact", new { id = pmChamberMagneticContact.ID }, createdPMChamberMagneticContact);
        }

        // PUT: api/PMChamberMagneticContact/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPMChamberMagneticContact(Guid id, UpdatePMChamberMagneticContactDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userGuid = userId != null ? Guid.Parse(userId) : (Guid?)null;

            var pmChamberMagneticContact = await _context.PMChamberMagneticContacts.FindAsync(id);
            if (pmChamberMagneticContact == null || pmChamberMagneticContact.IsDeleted)
            {
                return NotFound();
            }

            pmChamberMagneticContact.PMReportFormRTUID = updateDto.PMReportFormRTUID;
            pmChamberMagneticContact.Remarks = updateDto.Remarks;
            pmChamberMagneticContact.ChamberNumber = updateDto.ChamberNumber;
            pmChamberMagneticContact.ChamberOGBox = updateDto.ChamberOGBox;
            pmChamberMagneticContact.ChamberContact1 = updateDto.ChamberContact1;
            pmChamberMagneticContact.ChamberContact2 = updateDto.ChamberContact2;
            pmChamberMagneticContact.ChamberContact3 = updateDto.ChamberContact3;
            pmChamberMagneticContact.UpdatedDate = DateTime.Now;
            pmChamberMagneticContact.UpdatedBy = userGuid;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PMChamberMagneticContactExists(id))
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

        // DELETE: api/PMChamberMagneticContact/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePMChamberMagneticContact(Guid id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userGuid = userId != null ? Guid.Parse(userId) : (Guid?)null;

            var pmChamberMagneticContact = await _context.PMChamberMagneticContacts.FindAsync(id);
            if (pmChamberMagneticContact == null || pmChamberMagneticContact.IsDeleted)
            {
                return NotFound();
            }

            // Soft delete
            pmChamberMagneticContact.IsDeleted = true;
            pmChamberMagneticContact.UpdatedDate = DateTime.Now;
            pmChamberMagneticContact.UpdatedBy = userGuid;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PMChamberMagneticContactExists(Guid id)
        {
            return _context.PMChamberMagneticContacts.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}