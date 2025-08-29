using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using System.Security.Claims;

namespace ControlTower.Services
{
    public interface INewsPortalAuthorizationService
    {
        Task<bool> HasAccessAsync(Guid userId, string requiredAccessLevel = "User");
        Task<string?> GetUserAccessLevelAsync(Guid userId);
        Task<Guid?> GetApplicationIdAsync();
    }

    public class NewsPortalAuthorizationService : INewsPortalAuthorizationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string _applicationName;

        public NewsPortalAuthorizationService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _applicationName = _configuration["NewsPortalSettings:ApplicationName"] ?? "News Portal System";
        }

        public async Task<Guid?> GetApplicationIdAsync()
        {
            var application = await _context.Applications
                .FirstOrDefaultAsync(app => app.ApplicationName == _applicationName && !app.IsDeleted);
            return application?.ID;
        }

        public async Task<bool> HasAccessAsync(Guid userId, string requiredAccessLevel = "User")
        {
            var applicationId = await GetApplicationIdAsync();
            if (applicationId == null)
                return false;

            var userAccess = await _context.UserApplicationAccesses
                .Include(uaa => uaa.AccessLevel)
                .FirstOrDefaultAsync(uaa => 
                    uaa.UserID == userId && 
                    uaa.ApplicationID == applicationId && 
                    !uaa.IsDeleted && 
                    !uaa.IsRevoked);

            if (userAccess == null)
                return false;

            var userAccessLevel = userAccess.AccessLevel.LevelName;

            // Admin can access everything
            if (userAccessLevel == "Admin")
                return true;

            // User can only access User-level operations
            if (userAccessLevel == "User" && requiredAccessLevel == "User")
                return true;

            return false;
        }

        public async Task<string?> GetUserAccessLevelAsync(Guid userId)
        {
            var applicationId = await GetApplicationIdAsync();
            if (applicationId == null)
                return null;

            var userAccess = await _context.UserApplicationAccesses
                .Include(uaa => uaa.AccessLevel)
                .FirstOrDefaultAsync(uaa => 
                    uaa.UserID == userId && 
                    uaa.ApplicationID == applicationId && 
                    !uaa.IsDeleted && 
                    !uaa.IsRevoked);

            return userAccess?.AccessLevel.LevelName;
        }
    }
}