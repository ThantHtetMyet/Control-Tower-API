using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.EmployeeManagementSystem;
using ControlTower.DTOs.EmployeeManagementSystem;

namespace ControlTower.Controllers.EmployeeManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompanyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Company
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany()
        {
            var Company = await _context.Company
                .Include(c => c.CreatedByUser)
                .Include(c => c.UpdatedByUser)
                .Include(c => c.Users.Where(u => !u.IsDeleted))
                .Where(c => !c.IsDeleted)
                .Select(c => new CompanyDto
                {
                    ID = c.ID,
                    Name = c.Name,
                    Description = c.Description,
                    Remark = c.Remark,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate,
                    CreatedByUserName = c.CreatedByUser != null ? c.CreatedByUser.FirstName + " " + c.CreatedByUser.LastName : null,
                    UpdatedByUserName = c.UpdatedByUser != null ? c.UpdatedByUser.FirstName + " " + c.UpdatedByUser.LastName : null,
                    EmployeeCount = c.Users.Count(u => !u.IsDeleted)
                })
                .ToListAsync();

            return Ok(Company);
        }

        // GET: api/Company/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetCompany(Guid id)
        {
            var company = await _context.Company
                .Include(c => c.CreatedByUser)
                .Include(c => c.UpdatedByUser)
                .Include(c => c.Users.Where(u => !u.IsDeleted))
                .Where(c => c.ID == id && !c.IsDeleted)
                .Select(c => new CompanyDto
                {
                    ID = c.ID,
                    Name = c.Name,
                    Description = c.Description,
                    Remark = c.Remark,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate,
                    CreatedByUserName = c.CreatedByUser != null ? c.CreatedByUser.FirstName + " " + c.CreatedByUser.LastName : null,
                    UpdatedByUserName = c.UpdatedByUser != null ? c.UpdatedByUser.FirstName + " " + c.UpdatedByUser.LastName : null,
                    EmployeeCount = c.Users.Count(u => !u.IsDeleted)
                })
                .FirstOrDefaultAsync();

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        // POST: api/Company
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> CreateCompany(CreateCompanyDto createCompanyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if company name already exists
            var existingCompany = await _context.Company
                .Where(c => c.Name == createCompanyDto.Name && !c.IsDeleted)
                .FirstOrDefaultAsync();

            if (existingCompany != null)
            {
                return BadRequest("A company with this name already exists.");
            }

            var company = new Company
            {
                ID = Guid.NewGuid(),
                Name = createCompanyDto.Name,
                Description = createCompanyDto.Description,
                Remark = createCompanyDto.Remark,
                CreatedBy = createCompanyDto.CreatedBy,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Company.Add(company);
            await _context.SaveChangesAsync();

            // Return the created company with navigation properties
            var createdCompany = await _context.Company
                .Include(c => c.CreatedByUser)
                .Include(c => c.UpdatedByUser)
                .Where(c => c.ID == company.ID)
                .Select(c => new CompanyDto
                {
                    ID = c.ID,
                    Name = c.Name,
                    Description = c.Description,
                    Remark = c.Remark,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate,
                    CreatedByUserName = c.CreatedByUser != null ? c.CreatedByUser.FirstName + " " + c.CreatedByUser.LastName : null,
                    UpdatedByUserName = c.UpdatedByUser != null ? c.UpdatedByUser.FirstName + " " + c.UpdatedByUser.LastName : null,
                    EmployeeCount = 0
                })
                .FirstOrDefaultAsync();

            return CreatedAtAction(nameof(GetCompany), new { id = company.ID }, createdCompany);
        }

        // PUT: api/Company/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(Guid id, UpdateCompanyDto updateCompanyDto)
        {
            if (id != updateCompanyDto.ID)
            {
                return BadRequest("ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var company = await _context.Company.FindAsync(id);
            if (company == null || company.IsDeleted)
            {
                return NotFound();
            }

            // Check if company name already exists (excluding current company)
            var existingCompany = await _context.Company
                .Where(c => c.Name == updateCompanyDto.Name && c.ID != id && !c.IsDeleted)
                .FirstOrDefaultAsync();

            if (existingCompany != null)
            {
                return BadRequest("A company with this name already exists.");
            }

            company.Name = updateCompanyDto.Name;
            company.Description = updateCompanyDto.Description;
            company.Remark = updateCompanyDto.Remark;
            company.UpdatedBy = updateCompanyDto.UpdatedBy;
            company.UpdatedDate = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
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

        // DELETE: api/Company/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var company = await _context.Company.FindAsync(id);
            if (company == null || company.IsDeleted)
            {
                return NotFound();
            }

            // Check if company has active employees
            var hasActiveEmployees = await _context.Users
                .AnyAsync(u => u.CompanyID == id && !u.IsDeleted);

            if (hasActiveEmployees)
            {
                return BadRequest("Cannot delete company that has active employees. Please reassign or remove employees first.");
            }

            // Soft delete
            company.IsDeleted = true;
            company.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyExists(Guid id)
        {
            return _context.Company.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}