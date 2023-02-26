using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using MyCompany.FileSharingApp.Business.Abstract;
using MyCompany.FileSharingApp.Entities.Concrete;
using MyCompany.FileSharingApp.MVC.Models;
using System.Security.Claims;

namespace MyCompany.FileSharingApp.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFolderService _folderService;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, IMapper mapper, IFileProvider fileProvider, IFolderService folderService)
        {
            _userService = userService;
            _mapper = mapper;
            _fileProvider = fileProvider;
            _folderService = folderService;
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
                    new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                    new Claim("Id",user.UserId.ToString())
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

            var newFolder = new Folder
            {
                FolderId = newUser.UserId,
                UserId = newUser.UserId,
                FolderName = "Main Folder",
                FolderDescription = "Main Folder"
            };

            _folderService.Add(newFolder);

            try
            {
                var appData = _fileProvider.GetDirectoryContents("").First(p => p.Name.Equals("App_Data")).PhysicalPath;
                string path = Path.Combine(appData, newUser.UserId.ToString());

                // If directory does not exist, create it
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception)
            {
            }
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
