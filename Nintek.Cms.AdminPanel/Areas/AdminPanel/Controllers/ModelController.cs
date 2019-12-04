using Marten.Linq;
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
    public class ModelController : Controller
    {
        readonly Bank _bank;

        public ModelController(Bank bank)
        {
            _bank = bank;
        }

        [Route("admin/models/{slug}")]
        public async Task<IActionResult> Index(string slug)
        {
            var meta = GetMeta(slug);
            var models = await _bank.GetAll(meta.FullName);
            var vm = new ModelListVm(meta, models
                .Select(model => new ModelVm(model, model.Entries().Take(4).ToArray())).ToArray());
            return View(vm);
        }

        [HttpGet]
        [Route("admin/models/{slug}/{id}")]
        public async Task<IActionResult> Edit(string slug, string id)
        {
            var meta = GetMeta(slug);
            if (meta == null)
            {
                throw new CmsException($"No model {slug} found.");
            }
            if (id == "new")
            {
                return View(new ModelEditVm(meta, _bank.Create(slug)));
            }
            var parsed = int.TryParse(id, out var modelId);
            if (!parsed)
            {
                throw new CmsException($"Invalid id {id}.");
            }
            var model = await _bank.Get(meta.FullName, modelId);
            return View(new ModelEditVm(meta, model));
        }

        [HttpPost]
        [Route("admin/models/{slug}/{id}")]
        public async Task<IActionResult> Edit(string slug, int id, Dictionary<string, string> model)
        {
            var savedModel = _bank.Save(slug, id, model);
            await _bank.Commit();
            return RedirectToAction(
                nameof(Edit),
                nameof(ModelController).Replace("Controller", ""),
                new { slug, id = savedModel.Id });
        }

        ModelMeta GetMeta(string slug)
        {
            return _bank
                .Session
                .Query<ModelMeta>()
                .FirstOrDefault(x => x.Slug == slug);
        }
    }
}
