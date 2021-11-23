using System.Security.Claims;

namespace DatingAppApi.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user) 
        {
            return user.FindFirst(ClaimTypes.Name)?.Value;
        }
        public static int GetUserID(this ClaimsPrincipal user) 
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}