using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assembly = System.Reflection.Assembly;

namespace Nintek.Cms.AspNetCore
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddNintekCms(this IServiceCollection services, string connectionString)
        {
            return services.AddNintekCms<CmsSetup>(connectionString);
        }

        public static IServiceCollection AddNintekCms<TSetup>(this IServiceCollection services, string connectionString)
            where TSetup : CmsSetup
        {
            var modelTypes = Assembly
                .GetEntryAssembly()
                .GetTypes()
                .Where(type => type.IsSubclassOf(typeof(Model)))
                .ToArray();

            services.AddScoped(provider => new Bank(connectionString, modelTypes));
            services.AddScoped<CmsSetup, TSetup>();
            return services;
        }
    }
}
