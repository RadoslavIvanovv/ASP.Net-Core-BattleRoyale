

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace BattleRoyale.Infrastructure
{
    public static class EndPointRouteBuilderExtentions
    {
        public static void MapDefaultAreaRoute(this IEndpointRouteBuilder endpoints)
            => endpoints.MapControllerRoute(
                name: "Areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    }
}
