using System.Security.Claims;

namespace MatchUp.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            var username = user.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new Exception("Can't get username from token");
            return username;
        }
    }
}
