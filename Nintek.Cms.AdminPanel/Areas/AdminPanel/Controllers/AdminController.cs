using Microsoft.AspNetCore.Mvc;
using Nintek.Cms.AdminPanel.Models;
using Nintek.Cms.Users;
using Nintek.Cms.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintek.Cms.AdminPanel.Areas.AdminPanel.Controllers
{
    [Area(nameof(AdminPanel))]
    [Authorize(Role.Admin)]
    public class AdminController : Controller
    {
        readonly Bank _bank;

        public AdminController(Bank bank)
        {
            _bank = bank;
        }

        [HttpGet]
        [Route("admin")]
        public async Task<IActionResult> Index()
        {
            var models = await _bank.GetAll<ModelMeta>();
            var vm = models.Select(model =>
            {
                var shortName = model.GetShortName();
                return new ModelMetaViewModel(
                    shortName, 
                    shortName.ToLower(), 
                    model.Fields.Length);
            })
            .ToList();
            return View(vm);
        }
    }
}
