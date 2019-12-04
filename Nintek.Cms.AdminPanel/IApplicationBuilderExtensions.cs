using Microsoft.AspNetCore.Builder;
using Nintek.Cms.AspNetCore;
using Nintek.Cms.Users;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nintek.Cms.AdminPanel
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder InstallAdminPanel(this IApplicationBuilder app)
        {
            app.InstallUsersPlugin();
            app.InstallPlugin<AdminPanelPluginSetup>();
            return app;
        }
    }
}
