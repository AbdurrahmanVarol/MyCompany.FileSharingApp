﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MyCompany.FileSharingApp.Business.Abstract;
using MyCompany.FileSharingApp.MVC.Models;
using System.Security.Claims;

namespace MyCompany.FileSharingApp.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            var user = _userService.GetByUserName(loginModel.UserName);
            if (user != null && loginModel.Password.Equals(user.Password))
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim("Id",user.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties authenticationProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = loginModel.IsKeepLoggedIn
                };

                HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authenticationProperties).Wait();
                return RedirectToAction("index", "home");
            }
            TempData["Message"] = "Kullanıcı adı ya da şifre yanlış";
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserModel userModel)
        {
            var newUser = _mapper.Map<MyCompany.FileSharingApp.Entities.Concrete.User>(userModel);
            _userService.Add(newUser);
            TempData["Message"] = "Kullanıcı Eklendi!!!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return RedirectToAction("Login");
        }
    }
}
