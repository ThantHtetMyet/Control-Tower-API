using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using ControlTower.Models.EmployeeManagementSystem;
using ControlTower.DTOs.EmployeeManagementSystem;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Drawing;
// Add IConfiguration for appsettings access
using Microsoft.Extensions.Configuration;

namespace ControlTower.Controllers.EmployeeManagementSystem
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public EmployeeController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var Users = await _context.Users
                .Include(e => e.Company)
                .Include(e => e.Department)
                .Include(e => e.Occupation)
                .Include(e => e.CreatedByUser)
                .Include(e => e.UpdatedByUser)
                .Where(e => !e.IsDeleted)
                .Select(e => new UserDto
                {
                    ID = e.ID,
                    CompanyID = e.CompanyID,
                    DepartmentID = e.DepartmentID,
                    OccupationID = e.OccupationID,
                    StaffCardID = e.StaffCardID,
                    StaffRFIDCardID = e.StaffRFIDCardID,
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
                    CompanyName = e.Company != null ? e.Company.Name : null,
                    DepartmentName = e.Department != null ? e.Department.Name : null,
                    OccupationName = e.Occupation != null ? e.Occupation.OccupationName : null,
                    CreatedByUserName = e.CreatedByUser != null ? $"{e.CreatedByUser.FirstName} {e.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = e.UpdatedByUser != null ? $"{e.UpdatedByUser.FirstName} {e.UpdatedByUser.LastName}" : null
                })
                .ToListAsync();

            return Ok(Users);
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetEmployee(Guid id)
        {
            // Get the base URL from configuration
            var apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:7145";
            
            var employee = await _context.Users
                .Include(e => e.Company)
                .Include(e => e.Department)
                .Include(e => e.Occupation)
                .Include(e => e.CreatedByUser)
                .Include(e => e.UpdatedByUser)
                .Where(e => e.ID == id && !e.IsDeleted)
                .Select(e => new UserDto
                {
                    ID = e.ID,
                    CompanyID = e.CompanyID,
                    DepartmentID = e.DepartmentID,
                    OccupationID = e.OccupationID,
                    StaffCardID = e.StaffCardID,
                    StaffRFIDCardID = e.StaffRFIDCardID,
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
                    EmergencyContactName = e.EmergencyContactName,
                    EmergencyContactNumber = Convert.ToString(e.EmergencyContactNumber),
                    EmergencyRelationship = e.EmergencyRelationship,
                    // Add ProfileImageUrl construction similar to NewsController
                    ProfileImageUrl = _context.UserImages
                        .Where(ui => ui.UserID == e.ID && 
                                   ui.ImageType.ImageTypeName == "User Profile Image" && 
                                   !ui.IsDeleted)
                        .Select(ui => $"{apiBaseUrl}/api/EmployeeImage/{e.ID}")
                        .FirstOrDefault(),
                    CompanyName = e.Company.Name,
                    DepartmentName = e.Department != null ? e.Department.Name : null,
                    OccupationName = e.Occupation != null ? e.Occupation.OccupationName : null,
                    CreatedByUserName = e.CreatedByUser != null ? $"{e.CreatedByUser.FirstName} {e.CreatedByUser.LastName}" : null,
                    UpdatedByUserName = e.UpdatedByUser != null ? $"{e.UpdatedByUser.FirstName} {e.UpdatedByUser.LastName}" : null
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
            employee.CompanyID = updateEmployeeDto.CompanyID;  // Add this line
            employee.DepartmentID = updateEmployeeDto.DepartmentID;
            employee.OccupationID = updateEmployeeDto.OccupationID;
            employee.StaffCardID = updateEmployeeDto.StaffCardID;
            employee.StaffRFIDCardID = updateEmployeeDto.StaffRFIDCardID;
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
            // In the UpdateEmployee method
            employee.WorkPassCardNumber = updateEmployeeDto.WorkPassCardNumber;
            employee.WorkPassCardIssuedDate = updateEmployeeDto.WorkPassCardIssuedDate;
            employee.WorkPassCardExpiredDate = updateEmployeeDto.WorkPassCardExpiredDate;
            employee.EmergencyContactName = updateEmployeeDto.EmergencyContactName;
            if (!string.IsNullOrEmpty(updateEmployeeDto.EmergencyContactNumber) &&
                    int.TryParse(updateEmployeeDto.EmergencyContactNumber, out int number))
            {
                employee.EmergencyContactNumber = number;
            }
            else
            {
                employee.EmergencyContactNumber = null; // or keep existing value
            }

            employee.EmergencyRelationship = updateEmployeeDto.EmergencyRelationship;
            employee.UpdatedDate = DateTime.UtcNow;
            employee.UpdatedBy = updateEmployeeDto.UpdatedBy;

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return NoContent();
        }

        // POST: api/Employee
        // Remove the ValidateStaffCardImage method call from CreateEmployee
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateEmployee([FromForm] CreateEmployeeDto createEmployeeDto)
        {
            // Remove this line:
            // var validationResult = ValidateStaffCardImage(createEmployeeDto.ProfileImage);

            // Validate CompanyID exists
            var companyExists = await _context.Company.AnyAsync(c => c.ID == createEmployeeDto.CompanyID && !c.IsDeleted);
            if (!companyExists)
            {
                return BadRequest("Invalid Company ID");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Create the employee first
                var employee = new User
                {
                    ID = Guid.NewGuid(),
                    CompanyID = createEmployeeDto.CompanyID,
                    DepartmentID = createEmployeeDto.DepartmentID,
                    OccupationID = createEmployeeDto.OccupationID,
                    StaffCardID = createEmployeeDto.StaffCardID,
                    StaffRFIDCardID = createEmployeeDto.StaffRFIDCardID,
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
                    EmergencyContactName = createEmployeeDto.EmergencyContactName,
                    EmergencyContactNumber = createEmployeeDto.EmergencyContactNumber,
                    EmergencyRelationship = createEmployeeDto.EmergencyRelationship,
                    CreatedBy = createEmployeeDto.CreatedBy,
                    UpdatedBy = createEmployeeDto.CreatedBy
                };

                _context.Users.Add(employee);
                await _context.SaveChangesAsync();

                // Handle application accesses if provided
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

                // Return the created employee with image details
                var result = await GetEmployeeWithDetails(employee.ID);
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

        [HttpPost("{employeeId}/application-access")]
        public async Task<IActionResult> CreateApplicationAccess(Guid employeeId, [FromBody] CreateApplicationAccessDto dto)
        {
            // Validate employee exists
            var employeeExists = await _context.Users
                .AnyAsync(e => e.ID == employeeId && !e.IsDeleted);
            if (!employeeExists)
                return NotFound("Employee not found");

            // Validate application exists
            var applicationExists = await _context.Applications
                .AnyAsync(a => a.ID == dto.ApplicationID && !a.IsDeleted);
            if (!applicationExists)
                return BadRequest($"Application with ID {dto.ApplicationID} not found.");

            // Validate access level exists
            var accessLevelExists = await _context.AccessLevels
                .AnyAsync(al => al.ID == dto.AccessLevelID && !al.IsDeleted);
            if (!accessLevelExists)
                return BadRequest($"Access level with ID {dto.AccessLevelID} not found.");

            // Check if access already exists (not soft deleted)
            var existingAccess = await _context.UserApplicationAccesses
                .FirstOrDefaultAsync(a => a.UserID == employeeId && a.ApplicationID == dto.ApplicationID && !a.IsDeleted);
            if (existingAccess != null)
                return BadRequest("Application access already exists for this employee.");

            // Create the application access
            var applicationAccess = new UserApplicationAccess
            {
                ID = Guid.NewGuid(),
                UserID = employeeId,
                ApplicationID = dto.ApplicationID,
                AccessLevelID = dto.AccessLevelID,
                GrantedDate = dto.GrantedDate,
                IsRevoked = false,
                GrantedBy = dto.GrantedBy,
                Remark = dto.Remark,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                CreatedBy = dto.CreatedBy,
                UpdatedBy = dto.CreatedBy
            };

            _context.UserApplicationAccesses.Add(applicationAccess);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employeeId }, new { id = applicationAccess.ID });
        }

        [HttpPut("{employeeId}/application-access/{accessId}")]
        public async Task<IActionResult> UpdateApplicationAccess(Guid employeeId, Guid accessId, [FromBody] UpdateApplicationAccessDto dto)
        {
            var access = await _context.UserApplicationAccesses
                .FirstOrDefaultAsync(a => a.ID == accessId && a.UserID == employeeId && !a.IsDeleted);
        
            if (access == null)
                return NotFound();
        
            // Validate access level exists
            var accessLevelExists = await _context.AccessLevels
                .AnyAsync(al => al.ID == dto.AccessLevelID && !al.IsDeleted);
            if (!accessLevelExists)
                return BadRequest($"Access level with ID {dto.AccessLevelID} not found.");
        
            // Update the access level
            access.AccessLevelID = dto.AccessLevelID;
            access.UpdatedBy = dto.UpdatedBy;
            access.UpdatedDate = DateTime.UtcNow;
        
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool EmployeeExists(Guid id)
        {
            return _context.Users.Any(e => e.ID == id && !e.IsDeleted);
        }


        // Add these methods at the end of the class, before the closing brace
        // Image validation method
        private (bool IsValid, string ErrorMessage) ValidateStaffCardImage(IFormFile imageFile)
        {
            // Check file size (max 5MB)
            if (imageFile.Length > 5 * 1024 * 1024)
            {
                return (false, "Image file size must not exceed 5MB");
            }

            // Check file type
            var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png" };
            if (!allowedTypes.Contains(imageFile.ContentType.ToLower()))
            {
                return (false, "Only JPEG and PNG image formats are allowed");
            }

            // Check image dimensions for staff card (recommended: 350x220 pixels)
            try
            {
                using var stream = imageFile.OpenReadStream();
                using var image = System.Drawing.Image.FromStream(stream);

                // Recommended staff card dimensions
                const int recommendedWidth = 350;
                const int recommendedHeight = 220;
                const double tolerance = 0.1; // 10% tolerance

                var widthDiff = Math.Abs(image.Width - recommendedWidth) / (double)recommendedWidth;
                var heightDiff = Math.Abs(image.Height - recommendedHeight) / (double)recommendedHeight;

                if (widthDiff > tolerance || heightDiff > tolerance)
                {
                    return (false, $"Image dimensions should be approximately {recommendedWidth}x{recommendedHeight} pixels for optimal staff card display. Current: {image.Width}x{image.Height}");
                }
            }
            catch
            {
                return (false, "Invalid image file");
            }

            return (true, string.Empty);
        }


        // Get employee with image details
        private async Task<UserDto> GetEmployeeWithDetails(Guid employeeId)
        {
            var employee = await _context.Users
                .Include(u => u.Company)
                .Include(u => u.Department)
                .Include(u => u.Occupation)
                .Include(u => u.CreatedByUser)
                .Include(u => u.UpdatedByUser)
                .Include(u => u.UserImages.Where(ui => !ui.IsDeleted))
                    .ThenInclude(ui => ui.ImageType)
                .FirstOrDefaultAsync(u => u.ID == employeeId && !u.IsDeleted);

            if (employee == null) return null;

            var userDto = new UserDto
            {
                ID = employee.ID,
                CompanyID = employee.CompanyID,
                DepartmentID = employee.DepartmentID,
                OccupationID = employee.OccupationID,
                StaffCardID = employee.StaffCardID,
                StaffRFIDCardID = employee.StaffRFIDCardID,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                MobileNo = employee.MobileNo,
                Gender = employee.Gender,
                Remark = employee.Remark,
                Rating = employee.Rating,
                CreatedDate = employee.CreatedDate,
                UpdatedDate = employee.UpdatedDate,
                LastLogin = employee.LastLogin,
                StartWorkingDate = employee.StartWorkingDate,
                LastWorkingDate = employee.LastWorkingDate,
                WorkPermit = employee.WorkPermit,
                Nationality = employee.Nationality,
                Religion = employee.Religion,
                DateOfBirth = employee.DateOfBirth,
                WorkPassCardNumber = employee.WorkPassCardNumber,
                WorkPassCardIssuedDate = employee.WorkPassCardIssuedDate,
                WorkPassCardExpiredDate = employee.WorkPassCardExpiredDate,
                EmergencyContactName = employee.EmergencyContactName,
                EmergencyContactNumber = Convert.ToString(employee.EmergencyContactNumber),
                EmergencyRelationship = employee.EmergencyRelationship,
                DepartmentName = employee.Department?.Name,
                OccupationName = employee.Occupation?.OccupationName,
                CreatedByUserName = employee.CreatedByUser != null ? employee.CreatedByUser.FirstName + " " + employee.CreatedByUser.LastName : null,
                UpdatedByUserName = employee.UpdatedByUser != null ? employee.UpdatedByUser.FirstName + " " + employee.UpdatedByUser.LastName : null,
                // Add profile image URL if exists
                ProfileImageUrl = employee.UserImages
                    .FirstOrDefault(ui => ui.ImageType.ImageTypeName == "User Profile Image" && !ui.IsDeleted)
                    ?.StoredDirectory != null ?
                    $"/uploads/profile-images/{employee.UserImages.FirstOrDefault(ui => ui.ImageType.ImageTypeName == "User Profile Image" && !ui.IsDeleted)?.ImageName}" : null
            };

            return userDto;
        }

        // Add new endpoint for profile image upload
        [HttpPost("{id}/profile-image")]
        public async Task<IActionResult> UploadProfileImage(Guid id, [FromForm] UploadProfileImageDto uploadDto)
        {
            var employee = await _context.Users.FindAsync(id);
            if (employee == null || employee.IsDeleted)
            {
                return NotFound("Employee not found");
            }

            if (uploadDto.ProfileImage == null)
            {
                return BadRequest("Profile image is required");
            }

            // Remove existing profile image if any
            var existingImage = await _context.UserImages
                .FirstOrDefaultAsync(img => img.UserID == id && !img.IsDeleted);
            if (existingImage != null)
            {
                existingImage.IsDeleted = true;
            }

            var basePath = _configuration["EmployeeManagementSystemFileStorage:BasePath"] ?? "C:\\Temp\\EmployeeImages";
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            await SaveEmployeeImage(uploadDto.ProfileImage, id, basePath, uploadDto.UploadedBy);
            await _context.SaveChangesAsync();

            return Ok("Profile image uploaded successfully");
        }

        // Helper method for saving employee images
        private async Task SaveEmployeeImage(IFormFile image, Guid userId, string basePath, Guid uploadedBy)
        {
            // Create employee-specific directory
            var userDirectory = Path.Combine(basePath, userId.ToString());
            if (!Directory.Exists(userDirectory))
            {
                Directory.CreateDirectory(userDirectory);
            }

            // Generate unique filename
            var fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();
            var fileName = $"profile_{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid():N}{fileExtension}";
            var filePath = Path.Combine(userDirectory, fileName);

            // Save file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // Get default image type
            var imageType = await _context.ImageTypes
                .FirstOrDefaultAsync(it => it.ImageTypeName == "User Profile Image");

            if (imageType == null)
            {
                // Create default image type if it doesn't exist
                imageType = new ImageType
                {
                    ID = Guid.NewGuid(),
                    ImageTypeName = "User Profile Image",
                    IsDeleted = false,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = uploadedBy
                };
                _context.ImageTypes.Add(imageType);
                await _context.SaveChangesAsync();
            }

            // Create database record
            var userImage = new UserImage
            {
                ID = Guid.NewGuid(),
                UserID = userId,
                ImageTypeID = imageType.ID,
                ImageName = fileName,
                StoredDirectory = userDirectory,
                UploadedStatus = "Uploaded",
                IsDeleted = false,
                UploadedDate = DateTime.UtcNow,
                UploadedBy = uploadedBy
            };

            _context.UserImages.Add(userImage);
        }
    }
}
