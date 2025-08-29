using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ControlTower.Data;
using System.Security.Claims;

namespace ControlTower.Attributes
{
    public class ApplicationAuthorizationAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _applicationName;
        private readonly string _requiredAccessLevel;

        public ApplicationAuthorizationAttribute(string applicationName, string requiredAccessLevel = "User")
        {
            _applicationName = applicationName;
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

            // Get database context from DI
            var dbContext = context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();

            // Find the application by name
            var application = await dbContext.Applications
                .FirstOrDefaultAsync(app => app.ApplicationName == _applicationName && !app.IsDeleted);

            if (application == null)
            {
                context.Result = new ForbidResult($"Application '{_applicationName}' not found");
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
                context.Result = new ForbidResult($"User does not have access to {_applicationName}");
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