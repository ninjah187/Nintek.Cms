using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Nintek.Cms.Users.Areas.Users.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintek.Cms.Users
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        readonly string[] _allowedRoles;

        public AuthorizationFilter(string[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var roles = context
                .HttpContext
                .User
                .Roles();

            if (roles == null)
            {
                context.Result = RedirectToLogin(context);
                return;
            }
            
            if (roles.Any(role => _allowedRoles.Contains(role)))
            {
                return;
            }

            context.Result = RedirectToLogin(context);
        }

        IActionResult RedirectToLogin(AuthorizationFilterContext context)
        {
            return new RedirectToActionResult(
                nameof(UsersController.Login),
                nameof(UsersController).Replace("Controller", ""),
                new { area = nameof(Areas.Users), redirect = context.HttpContext.Request.Path.Value });
        }
    }
}
