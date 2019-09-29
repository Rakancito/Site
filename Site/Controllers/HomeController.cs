using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Site.Models;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IPlayerController _PlayerRepository;

        UserViewModel objUser = new UserViewModel();
        public HomeController(ILogger<HomeController> logger, IPlayerController playerRepository)
        {
            _PlayerRepository = playerRepository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var x = HttpContext.Session.GetString("Site_Session");
            TempData["OnlinePlayers"] = DBController.GetOnlinePlayes();
            TempData["TempLogin"] = x;
            return View();
        }

        public IActionResult Privacy()
        {
            var x = HttpContext.Session.GetString("Site_Session");
            TempData["TempLogin"] = x;
            return View();
        }

        public IActionResult Ranking()
        {
            var x = HttpContext.Session.GetString("Site_Session");
            TempData["TempLogin"] = x;
            var model = _PlayerRepository.GetAllPlayers();
            return View(model);
        }

        public IActionResult features()
        {
            var x = HttpContext.Session.GetString("Site_Session");
            TempData["TempLogin"] = x;
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            var x = HttpContext.Session.GetString("Site_Session");
            TempData["TempLogin"] = x;
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserViewModel Accounts)
        {
            if (ModelState.IsValid)
            {
                int retorno = DBController.RegisterAccount(Accounts.Account, Accounts.Password, Accounts.Email, Accounts.RemoveCode);
                if (retorno >= 1)
                {
                    TempData["Succees"] = "Se ha registrado con éxito.";
                    return View();
                }
                else
                {
                    TempData["FailRegister"] = "La cuenta que desea utilizar ya existe.";
                    return View();
                }
            }
            TempData["FailForm"] = "No ha llenado correctamente el formulario";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            var x = HttpContext.Session.GetString("Site_Session");
            TempData["TempLogin"] = x;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel a)
        {
            bool bResult = false;
            bResult = DBController.LoginAccount(a.Account, a.Password);
            var authProperties = new AuthenticationProperties
            {

            };
            if (bResult)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, a.Account)
                    };
                ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                //await HttpContext.SignInAsync(principal);

                //await HttpContext.SignInAsync(
                //  CookieAuthenticationDefaults.AuthenticationScheme,
                // new ClaimsPrincipal(userIdentity),
                //authProperties);
                HttpContext.Session.SetString("Site_Session", a.Account);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userIdentity), authProperties);
                return Redirect("/Settings");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("Site_Session");
            Response.Cookies.Delete("Site_Session");
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

    }
}
