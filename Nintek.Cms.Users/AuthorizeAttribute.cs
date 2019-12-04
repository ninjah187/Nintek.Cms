using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nintek.Cms.Users
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(params string[] allowedRoles)
            : base(typeof(AuthorizationFilter))
        {
            Arguments = new object[] { allowedRoles };
        }
    }
}
