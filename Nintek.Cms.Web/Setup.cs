using Exodus;
using Microsoft.Extensions.Logging;
using Nintek.Cms.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nintek.Cms.Web
{
    public class Setup : CmsSetup
    {
        public Setup(Bank bank, ILogger<CmsSetup> logger) : base(bank, logger)
        {
        }

        protected override async Task Initialize()
        {
            Bank.Save(new Product
            {
                Title = "Produkt 1",
                Description = "Produkt 1 Opis",
                Price = "100 zł"
            });
            Bank.Save(new Product
            {
                Title = "Produkt 2",
                Description = "Produkt 2 Opis",
                Price = "100 zł"
            });
        }
    }
}
