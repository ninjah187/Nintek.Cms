using Microsoft.Extensions.DependencyInjection;
using Nintek.Cms.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nintek.Cms.AdminPanel
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAdminPanel(this IServiceCollection services)
        {
            services.AddUsersPlugin();
            services.AddScoped<AdminPanelPluginSetup>();
            return services;
        }
    }
}
