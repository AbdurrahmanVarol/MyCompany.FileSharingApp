using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.FileProviders;
using MyCompany.FileSharingApp.Business.Abstract;
using MyCompany.FileSharingApp.Entities.Concrete;
using MyCompany.FileSharingApp.MVC.Filters;
using MyCompany.FileSharingApp.MVC.Models;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;

namespace MyCompany.FileSharingApp.MVC.Controllers
{
    [Authorize]
    [CustomExceptionFilter]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly IFolderService _folderService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IFileProvider _fileProvider;

        private Guid UserId => Guid.Parse(User.Claims.First().Value);


        public HomeController(ILogger<HomeController> logger, IFolderService folderService, IUserService userService, IMapper mapper, IFileProvider fileProvider, IFileService fileService)
        {

            _logger = logger;
            _folderService = folderService;
            _userService = userService;
            _mapper = mapper;
            _fileProvider = fileProvider;
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            var a = HttpContext.User;

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }          

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




    }
}