using Microsoft.Extensions.DependencyInjection;
using Nintek.Cms.Users.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nintek.Cms.Users
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddUsersPlugin(this IServiceCollection services)
        {
            services
                .AddSingleton<SaltGenerator>()
                .AddSingleton<HashGenerator>()
                .AddScoped<UserService>()
                .AddScoped<UsersPluginSetup>();
            return services;
        }
    }
}
