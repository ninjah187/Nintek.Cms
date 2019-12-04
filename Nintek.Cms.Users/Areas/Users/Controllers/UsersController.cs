using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nintek.Cms.Users.Models;
using Nintek.Cms.Users.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nintek.Cms.Users.Areas.Users.Controllers
{
    [Area(nameof(Users))]
    public class UsersController : Controller
    {
        readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("users/register")]
        public async Task<IActionResult> Register(string email, string password)
        {
            await _userService.RegisterUser(email, password);
            return NoContent();
        }

        [HttpGet]
        [Route("users/login")]
        public IActionResult Login(string redirect = null)
        {
            if (User.IsLoggedIn())
            {
                return RedirectToUrlOrRoot(redirect);
            }
            return View(new LoginViewModel(false));
        }

        [HttpPost]
        [Route("users/login")]
        public async Task<IActionResult> Login(string email, string password, string redirect = null)
        {
            try
            {
                var session = await _userService.SignIn(email, password);
                Response.Cookies.Append(Cookies.Auth, session.Key.ToString());
            }
            catch
            {
                // unauthorized
                return View(new LoginViewModel(true));
            }
            return RedirectToUrlOrRoot(redirect);
        }

        IActionResult RedirectToUrlOrRoot(string url)
        {
            return Redirect(string.IsNullOrEmpty(url) ? "/" : url);
        }
    }
}
