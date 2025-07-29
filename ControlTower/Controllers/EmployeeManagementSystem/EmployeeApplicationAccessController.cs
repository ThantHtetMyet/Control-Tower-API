using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.EmployeeManagementSystem;
using ControlTower.DTOs.EmployeeManagementSystem;

namespace ControlTower.Controllers.EmployeeManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeApplicationAccessController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeApplicationAccessController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeApplicationAccess
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserApplicationAccessDto>>> GetUserApplicationAccesses()
        {
            var accesses = await _context.UserApplicationAccesses
                .Include(eaa => eaa.Employee)
                .Include(eaa => eaa.Application)
                .Include(eaa => eaa.GrantedByEmployee)
                .Include(eaa => eaa.CreatedByEmployee)
                .Include(eaa => eaa.UpdatedByEmployee)
                .Where(eaa => !eaa.IsDeleted)
                .Select(eaa => new UserApplicationAccessDto
                {
                    ID = eaa.ID,
                    EmployeeID = eaa.EmployeeID,
                    ApplicationID = eaa.ApplicationID,
                    AccessLevelID = eaa.AccessLevelID,
                    GrantedDate = eaa.GrantedDate,
                    IsRevoked = eaa.IsRevoked,
                    RevokedDate = eaa.RevokedDate,
                    GrantedBy = eaa.GrantedBy,
                    Remark = eaa.Remark,
                    CreatedDate = eaa.CreatedDate,
                    UpdatedDate = eaa.UpdatedDate,
                    EmployeeName = $"{eaa.Employee.FirstName} {eaa.Employee.LastName}",
                    EmployeeStaffCardID = eaa.Employee.StaffCardID,
                    ApplicationName = eaa.Application.ApplicationName,
                    GrantedByUserName = eaa.GrantedByEmployee != null ? $"{eaa.GrantedByEmployee.FirstName} {eaa.GrantedByEmployee.LastName}" : null,
                    CreatedByUserName = eaa.CreatedByEmployee != null ? $"{eaa.CreatedByEmployee.FirstName} {eaa.CreatedByEmployee.LastName}" : null,
                    UpdatedByUserName = eaa.UpdatedByEmployee != null ? $"{eaa.UpdatedByEmployee.FirstName} {eaa.UpdatedByEmployee.LastName}" : null
                })
                .ToListAsync();

            return Ok(accesses);
        }

        // GET: api/EmployeeApplicationAccess/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserApplicationAccessDto>> GetEmployeeApplicationAccess(Guid id)
        {
            var access = await _context.UserApplicationAccesses
                .Include(eaa => eaa.Employee)
                .Include(eaa => eaa.Application)
                .Include(eaa => eaa.GrantedByEmployee)
                .Include(eaa => eaa.CreatedByEmployee)
                .Include(eaa => eaa.UpdatedByEmployee)
                .Where(eaa => eaa.ID == id && !eaa.IsDeleted)
                .Select(eaa => new UserApplicationAccessDto
                {
                    ID = eaa.ID,
                    EmployeeID = eaa.EmployeeID,
                    ApplicationID = eaa.ApplicationID,
                    AccessLevelID = eaa.AccessLevelID,
                    GrantedDate = eaa.GrantedDate,
                    IsRevoked = eaa.IsRevoked,
                    RevokedDate = eaa.RevokedDate,
                    GrantedBy = eaa.GrantedBy,
                    Remark = eaa.Remark,
                    CreatedDate = eaa.CreatedDate,
                    UpdatedDate = eaa.UpdatedDate,
                    EmployeeName = $"{eaa.Employee.FirstName} {eaa.Employee.LastName}",
                    EmployeeStaffCardID = eaa.Employee.StaffCardID,
                    ApplicationName = eaa.Application.ApplicationName,
                    GrantedByUserName = eaa.GrantedByEmployee != null ? $"{eaa.GrantedByEmployee.FirstName} {eaa.GrantedByEmployee.LastName}" : null,
                    CreatedByUserName = eaa.CreatedByEmployee != null ? $"{eaa.CreatedByEmployee.FirstName} {eaa.CreatedByEmployee.LastName}" : null,
                    UpdatedByUserName = eaa.UpdatedByEmployee != null ? $"{eaa.UpdatedByEmployee.FirstName} {eaa.UpdatedByEmployee.LastName}" : null
                })
                .FirstOrDefaultAsync();

            if (access == null)
            {
                return NotFound();
            }

            return access;
        }

        // GET: api/EmployeeApplicationAccess/employee/5
        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<UserApplicationAccessDto>>> GetUserApplicationAccessesByEmployee(Guid employeeId)
        {
            var accesses = await _context.UserApplicationAccesses
                .Include(eaa => eaa.Employee)
                .Include(eaa => eaa.Application)
                .Include(eaa => eaa.GrantedByEmployee)
                .Include(eaa => eaa.CreatedByEmployee)
                .Include(eaa => eaa.UpdatedByEmployee)
                .Where(eaa => eaa.EmployeeID == employeeId && !eaa.IsDeleted)
                .Select(eaa => new UserApplicationAccessDto
                {
                    ID = eaa.ID,
                    EmployeeID = eaa.EmployeeID,
                    ApplicationID = eaa.ApplicationID,
                    AccessLevelID = eaa.AccessLevelID,
                    GrantedDate = eaa.GrantedDate,
                    IsRevoked = eaa.IsRevoked,
                    RevokedDate = eaa.RevokedDate,
                    GrantedBy = eaa.GrantedBy,
                    Remark = eaa.Remark,
                    CreatedDate = eaa.CreatedDate,
                    UpdatedDate = eaa.UpdatedDate,
                    EmployeeName = $"{eaa.Employee.FirstName} {eaa.Employee.LastName}",
                    EmployeeStaffCardID = eaa.Employee.StaffCardID,
                    ApplicationName = eaa.Application.ApplicationName,
                    GrantedByUserName = eaa.GrantedByEmployee != null ? $"{eaa.GrantedByEmployee.FirstName} {eaa.GrantedByEmployee.LastName}" : null,
                    CreatedByUserName = eaa.CreatedByEmployee != null ? $"{eaa.CreatedByEmployee.FirstName} {eaa.CreatedByEmployee.LastName}" : null,
                    UpdatedByUserName = eaa.UpdatedByEmployee != null ? $"{eaa.UpdatedByEmployee.FirstName} {eaa.UpdatedByEmployee.LastName}" : null
                })
                .ToListAsync();

            return Ok(accesses);
        }

        // POST: api/EmployeeApplicationAccess
        [HttpPost]
        public async Task<ActionResult<UserApplicationAccessDto>> PostEmployeeApplicationAccess(CreateEmployeeApplicationAccessDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if employee exists
            var employeeExists = await _context.Users.AnyAsync(e => e.ID == createDto.EmployeeID && !e.IsDeleted);
            if (!employeeExists)
            {
                return BadRequest("Employee not found.");
            }

            // Check if application exists
            var applicationExists = await _context.Applications.AnyAsync(a => a.ID == createDto.ApplicationID && !a.IsDeleted);
            if (!applicationExists)
            {
                return BadRequest("Application not found.");
            }

            // Check if access already exists
            var existingAccess = await _context.UserApplicationAccesses
                .AnyAsync(eaa => eaa.EmployeeID == createDto.EmployeeID && 
                               eaa.ApplicationID == createDto.ApplicationID && 
                               !eaa.IsDeleted);
            if (existingAccess)
            {
                return BadRequest("Employee already has access to this application.");
            }

            var access = new UserApplicationAccess
            {
                ID = Guid.NewGuid(),
                EmployeeID = createDto.EmployeeID,
                ApplicationID = createDto.ApplicationID,
                AccessLevelID = createDto.AccessLevelID,
                GrantedDate = createDto.GrantedDate,
                IsRevoked = false,
                RevokedDate = null,
                GrantedBy = createDto.GrantedBy,
                Remark = createDto.Remark,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = createDto.CreatedBy
            };

            _context.UserApplicationAccesses.Add(access);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeApplicationAccess", new { id = access.ID }, access);
        }

        // PUT: api/EmployeeApplicationAccess/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeApplicationAccess(Guid id, UpdateEmployeeApplicationAccessDto updateDto)
        {
            if (id != updateDto.ID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var access = await _context.UserApplicationAccesses.FindAsync(id);
            if (access == null || access.IsDeleted)
            {
                return NotFound();
            }

            // Check if employee exists
            var employeeExists = await _context.Users.AnyAsync(e => e.ID == updateDto.EmployeeID && !e.IsDeleted);
            if (!employeeExists)
            {
                return BadRequest("Employee not found.");
            }

            // Check if application exists
            var applicationExists = await _context.Applications.AnyAsync(a => a.ID == updateDto.ApplicationID && !a.IsDeleted);
            if (!applicationExists)
            {
                return BadRequest("Application not found.");
            }

            // Update properties
            access.EmployeeID = updateDto.EmployeeID;
            access.ApplicationID = updateDto.ApplicationID;
            access.AccessLevelID = updateDto.AccessLevelID;
            access.GrantedDate = updateDto.GrantedDate;
            access.IsRevoked = updateDto.IsRevoked;
            access.RevokedDate = updateDto.RevokedDate;
            access.GrantedBy = updateDto.GrantedBy;
            access.Remark = updateDto.Remark;
            access.UpdatedDate = DateTime.UtcNow;
            access.UpdatedBy = updateDto.UpdatedBy;

            _context.Entry(access).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeApplicationAccessExists(id))
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

        // DELETE: api/EmployeeApplicationAccess/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeApplicationAccess(Guid id)
        {
            var access = await _context.UserApplicationAccesses.FindAsync(id);
            if (access == null || access.IsDeleted)
            {
                return NotFound();
            }

            access.IsDeleted = true;
            access.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/EmployeeApplicationAccess/5/revoke
        [HttpPut("{id}/revoke")]
        public async Task<IActionResult> RevokeEmployeeApplicationAccess(Guid id, [FromBody] Guid? revokedBy = null)
        {
            var access = await _context.UserApplicationAccesses.FindAsync(id);
            if (access == null || access.IsDeleted)
            {
                return NotFound();
            }

            access.IsRevoked = true;
            access.RevokedDate = DateTime.UtcNow;
            access.UpdatedDate = DateTime.UtcNow;
            access.UpdatedBy = revokedBy;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeApplicationAccessExists(Guid id)
        {
            return _context.UserApplicationAccesses.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}