using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using System.Security.Claims;

namespace ControlTower.Attributes
{
    public class NewsPortalAuthorizationAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _requiredAccessLevel;

        public NewsPortalAuthorizationAttribute(string requiredAccessLevel = "User")
        {
            _requiredAccessLevel = requiredAccessLevel;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // Check if user is authenticated
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Get user ID from JWT claims
            var userIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Get services from DI
            var dbContext = context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            
            // Get application name from configuration
            var applicationName = configuration["NewsPortalSettings:ApplicationName"] ?? "News Portal System";

            // Find the application by name
            var application = await dbContext.Applications
                .FirstOrDefaultAsync(app => app.ApplicationName == applicationName && !app.IsDeleted);

            if (application == null)
            {
                context.Result = new ForbidResult($"Application '{applicationName}' not found");
                return;
            }

            // Check user's access to the application
            var userAccess = await dbContext.UserApplicationAccesses
                .Include(uaa => uaa.AccessLevel)
                .FirstOrDefaultAsync(uaa => 
                    uaa.UserID == userId && 
                    uaa.ApplicationID == application.ID && 
                    !uaa.IsDeleted && 
                    !uaa.IsRevoked);

            if (userAccess == null)
            {
                context.Result = new ForbidResult($"User does not have access to {applicationName}");
                return;
            }

            // Check access level
            var userAccessLevel = userAccess.AccessLevel.LevelName;
            
            // If Admin access is required, only allow Admin users
            if (_requiredAccessLevel == "Admin" && userAccessLevel != "Admin")
            {
                context.Result = new ForbidResult("Admin access required for this operation");
                return;
            }

            // If User access is required, allow both User and Admin
            if (_requiredAccessLevel == "User" && userAccessLevel != "User" && userAccessLevel != "Admin")
            {
                context.Result = new ForbidResult("User access required for this operation");
                return;
            }
        }
    }
}