using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
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
            var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                var user = await _userService.GetUserByIdAsync(userId);
                if (user != null)
                {
                    context.Succeed(requirement);
                    return;
                }
            }

            context.Fail();
        }
    }
}
