using Microsoft.AspNetCore.Builder;
using Nintek.Cms.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nintek.Cms.Users
{
    public static class IApplicationBuilderExtensions
    {
        // rename install to: use, setup?
        public static IApplicationBuilder InstallUsersPlugin(this IApplicationBuilder app)
        {
            app.InstallPlugin<UsersPluginSetup>();
            app.UseMiddleware<AuthenticationMiddleware>();
            return app;
        }
    }
}
