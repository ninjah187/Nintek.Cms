using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nintek.Cms.Web.Models;

namespace Nintek.Cms.Web.Controllers
{
    public class ProductsController : Controller
    {
        readonly Bank _bank;

        public ProductsController(Bank bank)
        {
            _bank = bank;
        }

        [HttpGet]
        [Route("produkty")]
        public async Task<IActionResult> Index()
        {
            var products = await _bank.GetAll<Product>();
            return View(products.ToArray());
        }
    }
}