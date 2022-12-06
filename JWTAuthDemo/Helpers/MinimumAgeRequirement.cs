using Microsoft.AspNetCore.Authorization;

namespace JWTAuthDemo.Helpers
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public MinimumAgeRequirement(int age)
        {
            MinimumAge = age;
        }
        public int MinimumAge { get; }
    }
}
