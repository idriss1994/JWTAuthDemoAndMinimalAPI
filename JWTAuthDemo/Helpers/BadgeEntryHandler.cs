using Microsoft.AspNetCore.Authorization;

namespace JWTAuthDemo.Helpers
{
    public class BadgeEntryHandler : AuthorizationHandler<BuildingEntryRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BuildingEntryRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "BadgeId" && c.Issuer == "https://microsoftsecurit"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
