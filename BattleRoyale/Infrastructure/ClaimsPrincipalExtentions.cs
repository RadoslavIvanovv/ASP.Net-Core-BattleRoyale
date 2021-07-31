

using System.Security.Claims;

namespace BattleRoyale.Infrastructure
{
    public static class ClaimsPrincipalExtentions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole("Administrator");
    }
}
