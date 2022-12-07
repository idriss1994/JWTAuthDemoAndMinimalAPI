using Microsoft.AspNetCore.Authorization;

namespace JWTAuthDemo.Helpers
{
    public class TemporaryStickerHandler : AuthorizationHandler<BuildingEntryRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, BuildingEntryRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "TemporaryBadgeId" && c.Issuer == "https://microsoftsecurit"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
