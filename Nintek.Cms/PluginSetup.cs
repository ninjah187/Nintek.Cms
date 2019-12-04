using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintek.Cms
{
    public abstract class PluginSetup
    {
        public async Task Setup(IServiceProvider serviceProvider)
        {
            var logger = serviceProvider.GetService<ILogger<PluginSetup>>();
            var bank = serviceProvider.GetService<Bank>();

            var assemblyName = GetType().Assembly.GetName();
            var name = assemblyName.Name;
            var version = assemblyName.Version.ToString();

            logger.LogInformation($"Setting Nintek CMS plugin {name}:{version}.");

            var installed = bank
                .Session
                .Query<Plugin>()
                .FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.InvariantCultureIgnoreCase));

            if (IsInitialized(installed))
            {
                ValidateVersions(installed, version);
                logger.LogInformation($"Plugin {name} already initialized. Skipping initialization.");
                return;
            }

            await Initialize(serviceProvider);

            var plugin = new Plugin(0, name, version, true);
            await bank.Save(plugin).Commit();

            logger.LogInformation($"Plugin {name} initialized successfully.");
        }

        protected virtual Task Initialize(IServiceProvider serviceProvider)
        {
            return Task.CompletedTask;
        }

        static bool IsInitialized(Plugin plugin)
        {
            return plugin != null && plugin.Initialized;
        }

        static void ValidateVersions(Plugin plugin, string currentVersion)
        {
            if (plugin.Version != currentVersion)
            {
                throw new CmsException($"Plugin {plugin.Name} version mismatch: " +
                    $"{currentVersion} (current), {plugin.Version} (installed).");
            }
        }
    }
}
