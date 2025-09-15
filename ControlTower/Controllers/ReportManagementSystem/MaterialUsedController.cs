using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.ReportManagementSystem;
using ControlTower.DTOs.ReportManagementSystem;

namespace ControlTower.Controllers.ReportManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialUsedController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MaterialUsedController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MaterialUsed
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialUsedDto>>> GetMaterialUsed()
        {
            try
            {
                var materialUsed = await _context.MaterialUsed
                    .Include(m => m.CMReportForm)
                    .Include(m => m.CreatedByUser)
                    .Include(m => m.UpdatedByUser)
                    .Select(m => new MaterialUsedDto
                    {
                        ID = m.ID,
                        CMReportFormID = m.CMReportFormID,
                        CMReportFormName = m.CMReportForm.Customer ?? "N/A",
                        Quantity = m.Quantity,
                        Description = m.Description,
                        SerialNo = m.SerialNo,
                        CreatedDate = m.CreatedDate,
                        UpdatedDate = m.UpdatedDate,
                        CreatedBy = m.CreatedBy,
                        CreatedByName = m.CreatedByUser.FirstName + " " + m.CreatedByUser.LastName,
                        UpdatedBy = m.UpdatedBy,
                        UpdatedByName = m.UpdatedByUser != null ? m.UpdatedByUser.FirstName + " " + m.UpdatedByUser.LastName : null
                    })
                    .ToListAsync();

                return Ok(materialUsed);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/MaterialUsed/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialUsedDto>> GetMaterialUsed(Guid id)
        {
            try
            {
                var materialUsed = await _context.MaterialUsed
                    .Include(m => m.CMReportForm)
                    .Include(m => m.CreatedByUser)
                    .Include(m => m.UpdatedByUser)
                    .Where(m => m.ID == id)
                    .Select(m => new MaterialUsedDto
                    {
                        ID = m.ID,
                        CMReportFormID = m.CMReportFormID,
                        CMReportFormName = m.CMReportForm.Customer ?? "N/A",
                        Quantity = m.Quantity,
                        Description = m.Description,
                        SerialNo = m.SerialNo,
                        CreatedDate = m.CreatedDate,
                        UpdatedDate = m.UpdatedDate,
                        CreatedBy = m.CreatedBy,
                        CreatedByName = m.CreatedByUser.FirstName + " " + m.CreatedByUser.LastName,
                        UpdatedBy = m.UpdatedBy,
                        UpdatedByName = m.UpdatedByUser != null ? m.UpdatedByUser.FirstName + " " + m.UpdatedByUser.LastName : null
                    })
                    .FirstOrDefaultAsync();

                if (materialUsed == null)
                {
                    return NotFound($"Material Used with ID {id} not found.");
                }

                return Ok(materialUsed);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/MaterialUsed/ByCMReportForm/5
        [HttpGet("ByCMReportForm/{cmReportFormId}")]
        public async Task<ActionResult<IEnumerable<MaterialUsedDto>>> GetMaterialUsedByCMReportForm(Guid cmReportFormId)
        {
            try
            {
                var materialUsed = await _context.MaterialUsed
                    .Include(m => m.CMReportForm)
                    .Include(m => m.CreatedByUser)
                    .Include(m => m.UpdatedByUser)
                    .Where(m => m.CMReportFormID == cmReportFormId)
                    .Select(m => new MaterialUsedDto
                    {
                        ID = m.ID,
                        CMReportFormID = m.CMReportFormID,
                        CMReportFormName = m.CMReportForm.Customer ?? "N/A",
                        Quantity = m.Quantity,
                        Description = m.Description,
                        SerialNo = m.SerialNo,
                        CreatedDate = m.CreatedDate,
                        UpdatedDate = m.UpdatedDate,
                        CreatedBy = m.CreatedBy,
                        CreatedByName = m.CreatedByUser.FirstName + " " + m.CreatedByUser.LastName,
                        UpdatedBy = m.UpdatedBy,
                        UpdatedByName = m.UpdatedByUser != null ? m.UpdatedByUser.FirstName + " " + m.UpdatedByUser.LastName : null
                    })
                    .ToListAsync();

                return Ok(materialUsed);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/MaterialUsed/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaterialUsed(Guid id, UpdateMaterialUsedDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var materialUsed = await _context.MaterialUsed.FindAsync(id);
                if (materialUsed == null)
                {
                    return NotFound($"Material Used with ID {id} not found.");
                }

                // Validate UpdatedBy user exists
                var updatedByUser = await _context.Users.FindAsync(updateDto.UpdatedBy);
                if (updatedByUser == null)
                {
                    return BadRequest("Invalid UpdatedBy user ID.");
                }

                // Update properties
                materialUsed.Quantity = updateDto.Quantity;
                materialUsed.Description = updateDto.Description;
                materialUsed.SerialNo = updateDto.SerialNo;
                materialUsed.UpdatedBy = updateDto.UpdatedBy;
                materialUsed.UpdatedDate = DateTime.Now;

                _context.Entry(materialUsed).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialUsedExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/MaterialUsed
        [HttpPost]
        public async Task<ActionResult<MaterialUsedDto>> PostMaterialUsed(CreateMaterialUsedDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Validate foreign keys
                var cmReportForm = await _context.CMReportForms.FindAsync(createDto.CMReportFormID);
                if (cmReportForm == null)
                {
                    return BadRequest("Invalid CM Report Form ID.");
                }

                var createdByUser = await _context.Users.FindAsync(createDto.CreatedBy);
                if (createdByUser == null)
                {
                    return BadRequest("Invalid CreatedBy user ID.");
                }

                var materialUsed = new MaterialUsed
                {
                    ID = Guid.NewGuid(),
                    CMReportFormID = createDto.CMReportFormID,
                    Quantity = createDto.Quantity,
                    Description = createDto.Description,
                    SerialNo = createDto.SerialNo,
                    CreatedBy = createDto.CreatedBy,
                    CreatedDate = DateTime.Now
                };

                _context.MaterialUsed.Add(materialUsed);
                await _context.SaveChangesAsync();

                // Return the created material used with related data
                var createdMaterialUsed = await _context.MaterialUsed
                    .Include(m => m.CMReportForm)
                    .Include(m => m.CreatedByUser)
                    .Where(m => m.ID == materialUsed.ID)
                    .Select(m => new MaterialUsedDto
                    {
                        ID = m.ID,
                        CMReportFormID = m.CMReportFormID,
                        CMReportFormName = m.CMReportForm.Customer ?? "N/A",
                        Quantity = m.Quantity,
                        Description = m.Description,
                        SerialNo = m.SerialNo,
                        CreatedDate = m.CreatedDate,
                        UpdatedDate = m.UpdatedDate,
                        CreatedBy = m.CreatedBy,
                        CreatedByName = m.CreatedByUser.FirstName + " " + m.CreatedByUser.LastName,
                        UpdatedBy = m.UpdatedBy,
                        UpdatedByName = null
                    })
                    .FirstOrDefaultAsync();

                return CreatedAtAction("GetMaterialUsed", new { id = materialUsed.ID }, createdMaterialUsed);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/MaterialUsed/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterialUsed(Guid id)
        {
            try
            {
                var materialUsed = await _context.MaterialUsed.FindAsync(id);
                if (materialUsed == null)
                {
                    return NotFound($"Material Used with ID {id} not found.");
                }

                _context.MaterialUsed.Remove(materialUsed);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private bool MaterialUsedExists(Guid id)
        {
            return _context.MaterialUsed.Any(e => e.ID == id);
        }
    }
}