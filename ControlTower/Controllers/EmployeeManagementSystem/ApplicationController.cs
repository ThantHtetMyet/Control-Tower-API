using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.EmployeeManagementSystem;
using ControlTower.DTOs.EmployeeManagementSystem;

namespace ControlTower.Controllers.EmployeeManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApplicationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Application
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationDto>>> GetApplications()
        {
            var applications = await _context.Applications
                .Include(a => a.CreatedByEmployee)
                .Include(a => a.UpdatedByEmployee)
                .Where(a => !a.IsDeleted)
                .Select(a => new ApplicationDto
                {
                    ID = a.ID,
                    ApplicationName = a.ApplicationName,
                    Description = a.Description,
                    Remark = a.Remark,
                    Rating = a.Rating,
                    CreatedDate = a.CreatedDate,
                    UpdatedDate = a.UpdatedDate,
                    CreatedByUserName = a.CreatedByEmployee != null ? $"{a.CreatedByEmployee.FirstName} {a.CreatedByEmployee.LastName}" : null,
                    UpdatedByUserName = a.UpdatedByEmployee != null ? $"{a.UpdatedByEmployee.FirstName} {a.UpdatedByEmployee.LastName}" : null
                })
                .ToListAsync();

            return Ok(applications);
        }

        // GET: api/Application/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationDto>> GetApplication(Guid id)
        {
            var application = await _context.Applications
                .Include(a => a.CreatedByEmployee)
                .Include(a => a.UpdatedByEmployee)
                .Where(a => a.ID == id && !a.IsDeleted)
                .Select(a => new ApplicationDto
                {
                    ID = a.ID,
                    ApplicationName = a.ApplicationName,
                    Description = a.Description,
                    Remark = a.Remark,
                    Rating = a.Rating,
                    CreatedDate = a.CreatedDate,
                    UpdatedDate = a.UpdatedDate,
                    CreatedByUserName = a.CreatedByEmployee != null ? $"{a.CreatedByEmployee.FirstName} {a.CreatedByEmployee.LastName}" : null,
                    UpdatedByUserName = a.UpdatedByEmployee != null ? $"{a.UpdatedByEmployee.FirstName} {a.UpdatedByEmployee.LastName}" : null
                })
                .FirstOrDefaultAsync();

            if (application == null)
            {
                return NotFound();
            }

            return application;
        }

        // POST: api/Application
        [HttpPost]
        public async Task<ActionResult<ApplicationDto>> PostApplication(CreateApplicationDto createApplicationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = new Application
            {
                ID = Guid.NewGuid(),
                ApplicationName = createApplicationDto.ApplicationName,
                Description = createApplicationDto.Description,
                Remark = createApplicationDto.Remark,
                Rating = 0,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = createApplicationDto.CreatedBy
            };

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplication", new { id = application.ID }, application);
        }

        // PUT: api/Application/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(Guid id, UpdateApplicationDto updateApplicationDto)
        {
            if (id != updateApplicationDto.ID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null || application.IsDeleted)
            {
                return NotFound();
            }

            application.ApplicationName = updateApplicationDto.ApplicationName;
            application.Description = updateApplicationDto.Description;
            application.Remark = updateApplicationDto.Remark;
            application.UpdatedDate = DateTime.UtcNow;
            application.UpdatedBy = updateApplicationDto.UpdatedBy;

            _context.Entry(application).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(id))
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

        // DELETE: api/Application/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(Guid id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null || application.IsDeleted)
            {
                return NotFound();
            }

            application.IsDeleted = true;
            application.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicationExists(Guid id)
        {
            return _context.Applications.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}