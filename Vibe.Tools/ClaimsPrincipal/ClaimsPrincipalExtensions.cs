using System.Security.Claims;

namespace Vibe.Tools.ControllerExtenstions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetId(this ClaimsPrincipal User)
        {
            return new Guid(User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
