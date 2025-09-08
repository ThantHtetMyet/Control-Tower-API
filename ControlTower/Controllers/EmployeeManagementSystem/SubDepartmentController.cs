using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.EmployeeManagementSystem;
using ControlTower.DTOs.EmployeeManagementSystem;
using ControlTower.DTOs;
using System.Security.Claims;

namespace ControlTower.Controllers.EmployeeManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubDepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubDepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SubDepartment
        [HttpGet]
        public async Task<ActionResult<PagedResult<SubDepartmentDto>>> GetSubDepartments(
            int page = 1, 
            int pageSize = 10, 
            string? search = null,
            Guid? departmentId = null)
        {
            var query = _context.SubDepartments
                .Include(sd => sd.Department)
                .Include(sd => sd.CreatedByUser)
                .Include(sd => sd.UpdatedByUser)
                .Where(sd => !sd.IsDeleted);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(sd => sd.Name.Contains(search) || 
                                         sd.Description.Contains(search));
            }

            if (departmentId.HasValue)
            {
                query = query.Where(sd => sd.DepartmentID == departmentId.Value);
            }

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(sd => sd.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(sd => new SubDepartmentDto
                {
                    ID = sd.ID,
                    DepartmentID = sd.DepartmentID,
                    DepartmentName = sd.Department.Name,
                    Name = sd.Name,
                    Description = sd.Description,
                    Remark = sd.Remark,
                    Rating = sd.Rating,
                    IsDeleted = sd.IsDeleted,
                    CreatedDate = sd.CreatedDate,
                    UpdatedDate = sd.UpdatedDate,
                    CreatedBy = sd.CreatedBy,
                    CreatedByName = sd.CreatedByUser != null ? $"{sd.CreatedByUser.FirstName} {sd.CreatedByUser.LastName}" : null,
                    UpdatedBy = sd.UpdatedBy,
                    UpdatedByName = sd.UpdatedByUser != null ? $"{sd.UpdatedByUser.FirstName} {sd.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            return Ok(new PagedResult<SubDepartmentDto>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            });
        }

        // GET: api/SubDepartment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubDepartmentDto>> GetSubDepartment(Guid id)
        {
            var subDepartment = await _context.SubDepartments
                .Include(sd => sd.Department)
                .Include(sd => sd.CreatedByUser)
                .Include(sd => sd.UpdatedByUser)
                .Where(sd => sd.ID == id && !sd.IsDeleted)
                .Select(sd => new SubDepartmentDto
                {
                    ID = sd.ID,
                    DepartmentID = sd.DepartmentID,
                    DepartmentName = sd.Department.Name,
                    Name = sd.Name,
                    Description = sd.Description,
                    Remark = sd.Remark,
                    Rating = sd.Rating,
                    IsDeleted = sd.IsDeleted,
                    CreatedDate = sd.CreatedDate,
                    UpdatedDate = sd.UpdatedDate,
                    CreatedBy = sd.CreatedBy,
                    CreatedByName = sd.CreatedByUser != null ? $"{sd.CreatedByUser.FirstName} {sd.CreatedByUser.LastName}" : null,
                    UpdatedBy = sd.UpdatedBy,
                    UpdatedByName = sd.UpdatedByUser != null ? $"{sd.UpdatedByUser.FirstName} {sd.UpdatedByUser.LastName}" : null
                })
                .FirstOrDefaultAsync();

            if (subDepartment == null)
            {
                return NotFound();
            }

            return Ok(subDepartment);
        }

        // POST: api/SubDepartment
        [HttpPost]
        public async Task<ActionResult<SubDepartmentDto>> CreateSubDepartment(CreateSubDepartmentDto createDto)
        {
            var currentUserId = GetCurrentUserId();
            
            var subDepartment = new SubDepartment
            {
                ID = Guid.NewGuid(),
                DepartmentID = createDto.DepartmentID,
                Name = createDto.Name,
                Description = createDto.Description,
                Remark = createDto.Remark,
                Rating = createDto.Rating,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = currentUserId,
                UpdatedBy = currentUserId
            };

            _context.SubDepartments.Add(subDepartment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSubDepartment), new { id = subDepartment.ID }, 
                await GetSubDepartmentDto(subDepartment.ID));
        }

        // PUT: api/SubDepartment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubDepartment(Guid id, UpdateSubDepartmentDto updateDto)
        {
            if (id != updateDto.ID)
            {
                return BadRequest();
            }

            var subDepartment = await _context.SubDepartments.FindAsync(id);
            if (subDepartment == null || subDepartment.IsDeleted)
            {
                return NotFound();
            }

            var currentUserId = GetCurrentUserId();

            subDepartment.DepartmentID = updateDto.DepartmentID;
            subDepartment.Name = updateDto.Name;
            subDepartment.Description = updateDto.Description;
            subDepartment.Remark = updateDto.Remark;
            subDepartment.Rating = updateDto.Rating;
            subDepartment.UpdatedDate = DateTime.UtcNow;
            subDepartment.UpdatedBy = currentUserId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubDepartmentExists(id))
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

        // DELETE: api/SubDepartment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubDepartment(Guid id)
        {
            var subDepartment = await _context.SubDepartments.FindAsync(id);
            if (subDepartment == null || subDepartment.IsDeleted)
            {
                return NotFound();
            }

            var currentUserId = GetCurrentUserId();

            // Soft delete
            subDepartment.IsDeleted = true;
            subDepartment.UpdatedDate = DateTime.UtcNow;
            subDepartment.UpdatedBy = currentUserId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubDepartmentExists(Guid id)
        {
            return _context.SubDepartments.Any(e => e.ID == id && !e.IsDeleted);
        }

        private Guid? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userIdClaim, out var userId) ? userId : null;
        }

        private async Task<SubDepartmentDto> GetSubDepartmentDto(Guid id)
        {
            return await _context.SubDepartments
                .Include(sd => sd.Department)
                .Include(sd => sd.CreatedByUser)
                .Include(sd => sd.UpdatedByUser)
                .Where(sd => sd.ID == id)
                .Select(sd => new SubDepartmentDto
                {
                    ID = sd.ID,
                    DepartmentID = sd.DepartmentID,
                    DepartmentName = sd.Department.Name,
                    Name = sd.Name,
                    Description = sd.Description,
                    Remark = sd.Remark,
                    Rating = sd.Rating,
                    IsDeleted = sd.IsDeleted,
                    CreatedDate = sd.CreatedDate,
                    UpdatedDate = sd.UpdatedDate,
                    CreatedBy = sd.CreatedBy,
                    CreatedByName = sd.CreatedByUser != null ? $"{sd.CreatedByUser.FirstName} {sd.CreatedByUser.LastName}" : null,
                    UpdatedBy = sd.UpdatedBy,
                    UpdatedByName = sd.UpdatedByUser != null ? $"{sd.UpdatedByUser.FirstName} {sd.UpdatedByUser.LastName}" : null
                })
                .FirstOrDefaultAsync();
        }
    }
}