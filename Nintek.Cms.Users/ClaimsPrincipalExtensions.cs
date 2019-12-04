using Nintek.Cms.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Nintek.Cms.Users
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsLoggedIn(this ClaimsPrincipal principal)
        {
            return principal.GetClaim(Claims.SessionId, id => id != null);
        }

        public static int UserId(this ClaimsPrincipal principal)
        {
            return principal.GetClaim(Claims.UserId, ParseInt);
        }

        public static int SessionId(this ClaimsPrincipal principal)
        {
            return principal.GetClaim(Claims.SessionId, ParseInt);
        }

        public static string SessionKey(this ClaimsPrincipal principal)
        {
            return principal.GetClaim(Claims.SessionKey, x => x);
        }

        public static string[] Roles(this ClaimsPrincipal principal)
        {
            return principal.GetClaim(Claims.Roles, roles => roles == null ? Array.Empty<string>() : roles.Split(','));
        }

        public static T GetClaim<T>(this ClaimsPrincipal principal, string type, Func<string, T> mapper)
        {
            var value = principal.GetClaim(type);
            return mapper(value);
        }

        public static string GetClaim(this ClaimsPrincipal principal, string type)
        {
            return principal.Claims.FirstOrDefault(x => x.Type == type)?.Value;
        }

        public static bool HasRole(this ClaimsPrincipal principal, string role)
        {
            var roles = (principal.GetClaim(Claims.Roles) ?? "").Split(',');
            return roles.Contains(role);
        }

        public static bool IsAdmin(this ClaimsPrincipal principal) => principal.HasRole(Role.Admin);
        public static bool IsUser(this ClaimsPrincipal principal) => principal.HasRole(Role.User) || principal.HasRole(Role.Admin);

        static int ParseInt(string value)
        {
            return int.TryParse(value, out var parsed) ? parsed : -1;
        }
    }
}
