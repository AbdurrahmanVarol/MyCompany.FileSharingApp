using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using MyCompany.FileSharingApp.Business.Abstract;
using MyCompany.FileSharingApp.Entities.Concrete;
using MyCompany.FileSharingApp.MVC.Filters;
using MyCompany.FileSharingApp.MVC.NewFolder.FolderTools;
using System.Security.Claims;

namespace MyCompany.FileSharingApp.MVC.Controllers
{

    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFolderService _folderService;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;
        private readonly IFolderTool _folderTool;
        IAuthService _authService;

        public AuthController(IUserService userService, IMapper mapper, IFileProvider fileProvider, IFolderService folderService, IFolderTool folderTool, IAuthService authService)
        {
            _userService = userService;
            _mapper = mapper;
            _fileProvider = fileProvider;
            _folderService = folderService;
            _folderTool = folderTool;
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            var user = _authService.Login(loginModel);
            if (user != null)
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
                    IsPersistent = loginModel.IsKeepLoggedIn,
                };

                HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), authenticationProperties).GetAwaiter().GetResult();
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
            var newUser = _authService.Register(userModel);

            var newFolder = new Folder
            {
                FolderId = newUser.UserId,
                UserId = newUser.UserId,
                FolderName = "Main Folder",
                FolderDescription = "Main Folder"
            };

            _folderService.Add(newFolder);

            var result = _folderTool.CreateFolder(newUser.UserId.ToString());

            TempData["Message"] = "Kullanıcı Eklendi!!!";
            return RedirectToAction("index", "home");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction("Login");
        }
    }
}
