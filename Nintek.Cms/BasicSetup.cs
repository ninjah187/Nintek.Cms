using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nintek.Cms
{
    public class CmsSetup
    {
        protected Bank Bank { get; }

        readonly ILogger<CmsSetup> _logger;

        public CmsSetup(Bank bank, ILogger<CmsSetup> logger)
        {
            Bank = bank;
            _logger = logger;
        }

        public async Task Setup()
        {
            _logger.LogInformation("Setting Nintek CMS.");

            if (await IsInitialized())
            {
                _logger.LogInformation("App already initialized. Skipping initialization.");
                return;
            }

            _logger.LogInformation("Initializing new application.");

            await Initialize();

            BuildMetadata();
            await Bank.Commit();

            _logger.LogInformation("New application initialized successfully.");
        }

        protected virtual Task Initialize()
        {
            return Task.CompletedTask;
        }

        async Task<bool> IsInitialized()
        {
            var app = await Bank.FirstOrDefault<AppMeta>();
            return app?.Initialized ?? false;
        }

        void BuildMetadata()
        {
            var app = new AppMeta(DateTime.UtcNow, true);
            var models = new ModelMetaBuilder().Build();
            Bank.Save(app);
            Bank.Save(models);
        }
    }
}
