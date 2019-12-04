using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nintek.Cms.AspNetCore
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNintekCms(this IApplicationBuilder app)
        {
            var serviceProvider = app.ApplicationServices;
            var setup = (CmsSetup) serviceProvider.GetService(typeof(CmsSetup));
            setup.Setup().Wait();
            return app;
        }

        public static IApplicationBuilder InstallPlugin<TSetup>(this IApplicationBuilder app)
            where TSetup : PluginSetup
        {
            var setup = app.ApplicationServices.GetService<TSetup>();
            setup.Setup(app.ApplicationServices).Wait();
            return app;
        }
    }
}
