using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.EmployeeManagementSystem;
using ControlTower.DTOs.EmployeeManagementSystem;

namespace ControlTower.Controllers.EmployeeManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Department
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartments()
        {
            var departments = await _context.Departments
                .Include(d => d.CreatedByUser)
                .Include(d => d.UpdatedByUser)
                .Include(d => d.Users.Where(e => !e.IsDeleted))
                .Where(d => !d.IsDeleted)
                .Select(d => new DepartmentDto
                {
                    ID = d.ID,
                    Name = d.Name,
                    Description = d.Description,
                    Remark = d.Remark,
                    Rating = d.Rating,
                    CreatedDate = d.CreatedDate,
                    UpdatedDate = d.UpdatedDate,
                    CreatedByUserName = d.CreatedByUser != null ? d.CreatedByUser.FirstName + " " + d.CreatedByUser.LastName : null,
                    UpdatedByUserName = d.UpdatedByUser != null ? d.UpdatedByUser.FirstName + " " + d.UpdatedByUser.LastName : null,
                    EmployeeCount = d.Users.Count(e => !e.IsDeleted)
                })
                .ToListAsync();

            return departments;
        }

        // GET: api/Department/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartment(Guid id)
        {
            var department = await _context.Departments
                .Include(d => d.CreatedByUser)
                .Include(d => d.UpdatedByUser)
                .Include(d => d.Users.Where(e => !e.IsDeleted))
                .Where(d => d.ID == id && !d.IsDeleted)
                .Select(d => new DepartmentDto
                {
                    ID = d.ID,
                    Name = d.Name,
                    Description = d.Description,
                    Remark = d.Remark,
                    Rating = d.Rating,
                    CreatedDate = d.CreatedDate,
                    UpdatedDate = d.UpdatedDate,
                    CreatedByUserName = d.CreatedByUser != null ? d.CreatedByUser.FirstName + " " + d.CreatedByUser.LastName : null,
                    UpdatedByUserName = d.UpdatedByUser != null ? d.UpdatedByUser.FirstName + " " + d.UpdatedByUser.LastName : null,
                    EmployeeCount = d.Users.Count(e => !e.IsDeleted)
                })
                .FirstOrDefaultAsync();

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // POST: api/Department
        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> PostDepartment(CreateDepartmentDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var department = new Department
            {
                ID = Guid.NewGuid(),
                Name = createDto.Name,
                Description = createDto.Description,
                Remark = createDto.Remark,
                Rating = 3, // Default rating since it's not provided from frontend
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                IsDeleted = false,
                CreatedBy = Guid.Parse(createDto.CreatedBy),
                UpdatedBy = Guid.Parse(createDto.CreatedBy)
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            var departmentDto = new DepartmentDto
            {
                ID = department.ID,
                Name = department.Name,
                Description = department.Description,
                Remark = department.Remark,
                Rating = department.Rating,
                CreatedDate = department.CreatedDate,
                UpdatedDate = department.UpdatedDate,
                EmployeeCount = 0
            };

            return CreatedAtAction("GetDepartment", new { id = department.ID }, departmentDto);
        }

        // PUT: api/Department/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(Guid id, UpdateDepartmentDto updateDto)
        {
            if (id != updateDto.ID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null || department.IsDeleted)
            {
                return NotFound();
            }

            department.Name = updateDto.Name;
            department.Description = updateDto.Description;
            department.Remark = updateDto.Remark;
            department.UpdatedDate = DateTime.UtcNow;
            
            if (!string.IsNullOrEmpty(updateDto.UpdatedBy))
            {
                department.UpdatedBy = Guid.Parse(updateDto.UpdatedBy);
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

        // DELETE: api/Department/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            // Check if department has active Users
            var hasActiveUsers = await _context.Users
                .AnyAsync(e => e.DepartmentID == id && !e.IsDeleted);

            if (hasActiveUsers)
            {
                return BadRequest("Cannot delete department with active Users.");
            }

            department.IsDeleted = true;
            department.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartmentExists(Guid id)
        {
            return _context.Departments.Any(d => d.ID == id && !d.IsDeleted);
        }
    }
}