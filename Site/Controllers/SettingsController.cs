using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Site.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Site.Controllers
{
    public class SettingsController : Controller
    {
        // GET: /<controller>/

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var x = HttpContext.Session.GetString("Site_Session");
            TempData["Email"] = DBController.GetEmailByAccount(x.ToString());
            TempData["RemoveCode"] = DBController.GetRemoveCodeByAccount(x.ToString());
            TempData["Coins"] = DBController.GetCoinsByAccount(x.ToString());
            TempData["TempLogin"] = x;
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Index(AccountViewModel a)
        {
            var x = HttpContext.Session.GetString("Site_Session");
            TempData["Email"] = DBController.GetEmailByAccount(x.ToString());
            TempData["RemoveCode"] = DBController.GetRemoveCodeByAccount(x.ToString());
            TempData["Coins"] = DBController.GetCoinsByAccount(x.ToString());
            TempData["TempLogin"] = x;
            if (ModelState.IsValid)
            {
                bool bretorno = DBController.ChangePasswordByPasswordAndAccount(a.Account, a.Password, a.ConfirmPassword);
                if (bretorno == true)
                {
                    TempData["Succees"] = "Se ha registrado con éxito su contraseña nueva.";
                    return View();
                }
                else
                {
                    TempData["FailRegister"] = "Ha habido un error al cambiar la contraseña.";
                    return View();
                }
            }
            TempData["FailForm"] = "No ha llenado correctamente el formulario";
            return View();
        }
    }
}
