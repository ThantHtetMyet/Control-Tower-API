using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.EmployeeManagementSystem;
using ControlTower.DTOs.EmployeeManagementSystem;

namespace ControlTower.Controllers.EmployeeManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var Users = await _context.Users
                .Include(e => e.Department)
                .Include(e => e.Occupation)
                .Include(e => e.CreatedByUser)
                .Include(e => e.UpdatedByUser)
                .Where(e => !e.IsDeleted)
                .Select(e => new UserDto
                {
                    ID = e.ID,
                    DepartmentID = e.DepartmentID,
                    OccupationID = e.OccupationID,
                    StaffCardID = e.StaffCardID,
                    StaffIDCardID = e.StaffRFIDCardID,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    MobileNo = e.MobileNo,
                    Gender = e.Gender,
                    Remark = e.Remark,
                    Rating = e.Rating,
                    CreatedDate = e.CreatedDate,
                    UpdatedDate = e.UpdatedDate,
                    LastLogin = e.LastLogin,
                    StartWorkingDate = e.StartWorkingDate,
                    LastWorkingDate = e.LastWorkingDate,
                    WorkPermit = e.WorkPermit,
                    Nationality = e.Nationality,
                    Religion = e.Religion,
                    DateOfBirth = e.DateOfBirth,
                    CreatedByUserName = e.CreatedByUser != null ? $"{e.CreatedByUser.FirstName} {e.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = e.UpdatedByUser != null ? $"{e.UpdatedByUser.FirstName} {e.UpdatedByUser.LastName}" : null,
                    DepartmentName = e.Department.Name,
                    OccupationName = e.Occupation.OccupationName
                })
                .ToListAsync();

            return Ok(Users);
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetEmployee(Guid id)
        {
            var employee = await _context.Users
                .Include(e => e.Department)
                .Include(e => e.Occupation)
                .Include(e => e.CreatedByUser)
                .Include(e => e.UpdatedByUser)
                .Where(e => e.ID == id && !e.IsDeleted)
                .Select(e => new UserDto
                {
                    ID = e.ID,
                    DepartmentID = e.DepartmentID,
                    OccupationID = e.OccupationID,
                    StaffCardID = e.StaffCardID,
                    StaffIDCardID = e.StaffRFIDCardID,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    MobileNo = e.MobileNo,
                    Gender = e.Gender,
                    Remark = e.Remark,
                    Rating = e.Rating,
                    CreatedDate = e.CreatedDate,
                    UpdatedDate = e.UpdatedDate,
                    LastLogin = e.LastLogin,
                    StartWorkingDate = e.StartWorkingDate,
                    LastWorkingDate = e.LastWorkingDate,
                    WorkPermit = e.WorkPermit,
                    Nationality = e.Nationality,
                    Religion = e.Religion,
                    DateOfBirth = e.DateOfBirth,
                    WorkPassCardNumber = e.WorkPassCardNumber,
                    WorkPassCardIssuedDate = e.WorkPassCardIssuedDate,
                    WorkPassCardExpiredDate = e.WorkPassCardExpiredDate,
                    DepartmentName = e.Department.Name,
                    OccupationName = e.Occupation.OccupationName,
                    CreatedByUserName = e.CreatedByUser != null ? e.CreatedByUser.FirstName + " " + e.CreatedByUser.LastName : null,
                    UpdatedByUserName = e.UpdatedByUser != null ? e.UpdatedByUser.FirstName + " " + e.UpdatedByUser.LastName : null
                })
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            // Get application access details
            var applicationAccesses = await _context.UserApplicationAccesses
                .Include(eaa => eaa.Application)
                .Include(eaa => eaa.AccessLevel)
                .Include(eaa => eaa.GrantedByUser)
                .Where(eaa => eaa.UserID == id && !eaa.IsDeleted)
                .Select(eaa => new EmployeeApplicationAccessDetailsDto
                {
                    ID = eaa.ID,
                    ApplicationID = eaa.ApplicationID,
                    ApplicationName = eaa.Application.ApplicationName,
                    ApplicationDescription = eaa.Application.Description,
                    AccessLevelID = eaa.AccessLevelID,
                    AccessLevelName = eaa.AccessLevel.LevelName,
                    GrantedDate = eaa.GrantedDate,
                    IsRevoked = eaa.IsRevoked,
                    RevokedDate = eaa.RevokedDate,
                    GrantedByUserName = eaa.GrantedByUser != null ? eaa.GrantedByUser.FirstName + " " + eaa.GrantedByUser.LastName : null,
                    Remark = eaa.Remark,
                    CreatedDate = eaa.CreatedDate
                })
                .ToListAsync();

            employee.ApplicationAccesses = applicationAccesses;

            return employee;
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            if (id != updateEmployeeDto.ID)
            {
                return BadRequest();
            }

            var employee = await _context.Users.FindAsync(id);
            if (employee == null || employee.IsDeleted)
            {
                return NotFound();
            }

            // Update employee properties
            employee.DepartmentID = updateEmployeeDto.DepartmentID;
            employee.OccupationID = updateEmployeeDto.OccupationID;
            employee.StaffCardID = updateEmployeeDto.StaffCardID;
            employee.StaffRFIDCardID = updateEmployeeDto.StaffIDCardID;
            employee.FirstName = updateEmployeeDto.FirstName;
            employee.LastName = updateEmployeeDto.LastName;
            employee.Email = updateEmployeeDto.Email;
            employee.MobileNo = updateEmployeeDto.MobileNo;
            employee.Gender = updateEmployeeDto.Gender;
            employee.Remark = updateEmployeeDto.Remark;
            employee.StartWorkingDate = updateEmployeeDto.StartWorkingDate;
            employee.LastWorkingDate = updateEmployeeDto.LastWorkingDate;
            employee.WorkPermit = updateEmployeeDto.WorkPermit;
            employee.Nationality = updateEmployeeDto.Nationality;
            employee.Religion = updateEmployeeDto.Religion;
            employee.DateOfBirth = updateEmployeeDto.DateOfBirth;
            // Add missing work pass card fields
            employee.WorkPassCardNumber = updateEmployeeDto.WorkPassCardNumber;
            employee.WorkPassCardIssuedDate = updateEmployeeDto.WorkPassCardIssuedDate;
            employee.WorkPassCardExpiredDate = updateEmployeeDto.WorkPassCardExpiredDate;
            employee.UpdatedDate = DateTime.UtcNow;
            employee.UpdatedBy = updateEmployeeDto.UpdatedBy;

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostEmployee(CreateEmployeeDto createEmployeeDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            
            try
            {
                // Create the employee first
                var employee = new User
                {
                    ID = Guid.NewGuid(),
                    DepartmentID = createEmployeeDto.DepartmentID,
                    OccupationID = createEmployeeDto.OccupationID,
                    StaffCardID = createEmployeeDto.StaffCardID,
                    StaffRFIDCardID = createEmployeeDto.StaffIDCardID,
                    FirstName = createEmployeeDto.FirstName,
                    LastName = createEmployeeDto.LastName,
                    Email = createEmployeeDto.Email,
                    MobileNo = createEmployeeDto.MobileNo,
                    Gender = createEmployeeDto.Gender,
                    LoginPassword = createEmployeeDto.LoginPassword,
                    Remark = createEmployeeDto.Remark,
                    Rating = 0,
                    IsDeleted = false,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    LastLogin = DateTime.UtcNow,
                    StartWorkingDate = createEmployeeDto.StartWorkingDate,
                    LastWorkingDate = createEmployeeDto.LastWorkingDate,
                    WorkPermit = createEmployeeDto.WorkPermit,
                    Nationality = createEmployeeDto.Nationality,
                    Religion = createEmployeeDto.Religion,
                    DateOfBirth = createEmployeeDto.DateOfBirth,
                    WorkPassCardNumber = createEmployeeDto.WorkPassCardNumber,
                    WorkPassCardIssuedDate = createEmployeeDto.WorkPassCardIssuedDate,
                    WorkPassCardExpiredDate = createEmployeeDto.WorkPassCardExpiredDate,
                    CreatedBy = createEmployeeDto.CreatedBy,
                    UpdatedBy = createEmployeeDto.CreatedBy
                };
            
                _context.Users.Add(employee);
                await _context.SaveChangesAsync();
            
                // Create application accesses if provided
                if (createEmployeeDto.ApplicationAccesses != null && createEmployeeDto.ApplicationAccesses.Any())
                {
                    foreach (var accessDto in createEmployeeDto.ApplicationAccesses)
                    {
                        // Validate application exists
                        var applicationExists = await _context.Applications
                            .AnyAsync(a => a.ID == accessDto.ApplicationID && !a.IsDeleted);
                        if (!applicationExists)
                        {
                            await transaction.RollbackAsync();
                            return BadRequest($"Application with ID {accessDto.ApplicationID} not found.");
                        }

                        // Validate access level exists
                        var accessLevelExists = await _context.AccessLevels
                            .AnyAsync(al => al.ID == accessDto.AccessLevelID && !al.IsDeleted);
                        if (!accessLevelExists)
                        {
                            await transaction.RollbackAsync();
                            return BadRequest($"Access level with ID {accessDto.AccessLevelID} not found.");
                        }

                        // Create the application access
                        var employeeApplicationAccess = new UserApplicationAccess
                        {
                            ID = Guid.NewGuid(),
                            UserID = employee.ID,
                            ApplicationID = accessDto.ApplicationID,
                            AccessLevelID = accessDto.AccessLevelID,
                            GrantedDate = accessDto.GrantedDate,
                            IsRevoked = false,
                            GrantedBy = accessDto.GrantedBy,
                            Remark = accessDto.Remark,
                            IsDeleted = false,
                            CreatedDate = DateTime.UtcNow,
                            UpdatedDate = DateTime.UtcNow,
                            CreatedBy = accessDto.CreatedBy,
                            UpdatedBy = accessDto.CreatedBy
                        };

                        _context.UserApplicationAccesses.Add(employeeApplicationAccess);
                    }
                    
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                // Return the created employee with department and occupation names
                var result = await _context.Users
                    .Where(e => e.ID == employee.ID)
                    .Select(e => new UserDto
                    {
                        ID = e.ID,
                        DepartmentID = e.DepartmentID,
                        OccupationID = e.OccupationID,
                        StaffCardID = e.StaffCardID,
                        StaffIDCardID = e.StaffRFIDCardID,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        Email = e.Email,
                        MobileNo = e.MobileNo,
                        Gender = e.Gender,
                        Remark = e.Remark,
                        Rating = e.Rating,
                        CreatedDate = e.CreatedDate,
                        UpdatedDate = e.UpdatedDate,
                        LastLogin = e.LastLogin,
                        StartWorkingDate = e.StartWorkingDate,
                        LastWorkingDate = e.LastWorkingDate,
                        WorkPermit = e.WorkPermit,
                        Nationality = e.Nationality,
                        Religion = e.Religion,
                        DateOfBirth = e.DateOfBirth,
                        WorkPassCardNumber = e.WorkPassCardNumber,
                        WorkPassCardIssuedDate = e.WorkPassCardIssuedDate,
                        WorkPassCardExpiredDate = e.WorkPassCardExpiredDate,
                        DepartmentName = e.Department.Name,
                        OccupationName = e.Occupation.OccupationName,
                        CreatedByUserName = e.CreatedByUser != null ? e.CreatedByUser.FirstName + " " + e.CreatedByUser.LastName : null,
                        UpdatedByUserName = e.UpdatedByUser != null ? e.UpdatedByUser.FirstName + " " + e.UpdatedByUser.LastName : null
                    })
                    .FirstOrDefaultAsync();

                return CreatedAtAction("GetEmployee", new { id = employee.ID }, result);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var employee = await _context.Users.FindAsync(id);
            if (employee == null || employee.IsDeleted)
            {
                return NotFound();
            }

            // Soft delete
            employee.IsDeleted = true;
            employee.UpdatedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(Guid id)
        {
            return _context.Users.Any(e => e.ID == id && !e.IsDeleted);
        }
    }
}