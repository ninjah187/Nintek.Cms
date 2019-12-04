using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nintek.Cms.Web.Controllers
{
    public class TestAdminController : Controller
    {
        [Route("test-admin")]
        public IActionResult Test()
        {
            return RedirectToAction("Login", "Users", new { area = nameof(Nintek.Cms.Users.Areas.Users) });
        }
    }
}
