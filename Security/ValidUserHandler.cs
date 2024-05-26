using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TravelBuddyAPI.DTOs;
using TravelBuddyAPI.Services.Interfaces;

namespace TravelBuddyAPI.Security
{
    public class ValidUserHandler : AuthorizationHandler<ValidUserRequirement>
    {
        private readonly IUserService _userService;

        public ValidUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidUserRequirement requirement)
        {
            if (context.Resource is not HttpContext httpContext)
            {
                context.Fail();

                return;
            }

            var userIdClaim = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                UserResponseDto? user = await _userService.GetUserByIdAsync(userId);
                if (user != null)
                {
                    context.Succeed(requirement);

                    // Store the user in HttpContext.Items
                    httpContext.Items["User"] = user;

                    return;
                }
            }

            context.Fail();

            return;
        }
    }
}
