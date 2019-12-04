using Microsoft.AspNetCore.Http;
using Nintek.Cms.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Nintek.Cms.Users
{
    public class AuthenticationMiddleware
    {
        readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, Bank bank)
        {
            var exists = context.Request.Cookies.TryGetValue(Cookies.Auth, out var sessionKey);
            if (!exists)
            {
                await _next(context);
                return;
            }

            var session = bank
                .Session
                .Query<Session>()
                .FirstOrDefault(x => x.Key == sessionKey);
            if (session == null)
            {
                await _next(context);
                return;
            }

            var user = await bank.Get<User>(session.UserId);
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(Claims.SessionId, session.Id.ToString()));
            identity.AddClaim(new Claim(Claims.UserId, session.UserId.ToString()));
            identity.AddClaim(new Claim(Claims.SessionKey, session.Key));
            identity.AddClaim(new Claim(Claims.Roles, string.Join(",", user.Roles)));
            context.User.AddIdentity(identity);

            await _next(context);
        }
    }
}
