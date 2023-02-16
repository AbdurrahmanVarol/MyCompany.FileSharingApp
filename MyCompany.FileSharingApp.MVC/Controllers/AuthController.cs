using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyCompany.FileSharingApp.MVC.Models;

namespace MyCompany.FileSharingApp.MVC.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserModel userModel)
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserModel userModel)
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Login");
        }
    }
}
