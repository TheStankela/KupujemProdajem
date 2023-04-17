using System.Security.Claims;

namespace KupujemProdajem.API.Extensions
{
    public static class ClamsPrincipalExtensions
    {
        public static string? GetUserId(this ClaimsPrincipal? user)
        {
            return user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
