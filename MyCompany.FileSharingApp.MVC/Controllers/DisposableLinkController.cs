using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using MyCompany.FileSharingApp.Business.Abstract;
using MyCompany.FileSharingApp.Entities.Concrete;

namespace MyCompany.FileSharingApp.MVC.Controllers
{

    public class DisposableLinkController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFolderService _folderService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IFileProvider _fileProvider;
        private readonly IConfiguration _configuration;

        private readonly IDisposableLinkService _disposableLinkService;
        public DisposableLinkController(IFolderService folderService, IDisposableLinkService disposableLinkService, IFileService fileService)
        {
            _folderService = folderService;
            _disposableLinkService = disposableLinkService;
            _fileService = fileService;
        }



        public IActionResult Download(Guid id)
        {
            var disposableLink = _disposableLinkService.GetById(id);
            var expire = TimeSpan.FromMilliseconds(disposableLink.Expire);

            if (disposableLink.IsUsed || disposableLink.CreatedAt.Add(expire) < DateTime.UtcNow)
            {
                TempData["message"] = "The disposable link has already been used.";
                return RedirectToAction("index", "home");
            }
            disposableLink.IsUsed = true;
            _disposableLinkService.Update(disposableLink);
            return RedirectToAction("DownloadFile", "file", new { fileId = disposableLink.FileId });
        }

        public IActionResult CreateLink(Guid fileId)
        {
            var file = _fileService.GetById(fileId);

            var disposableLink = new DisposableLink
            {
                FileId = fileId,
                CreatedAt = DateTime.Now,
                Expire = (int)TimeSpan.FromDays(1).TotalMilliseconds,
                IsUsed = false
            };
            _disposableLinkService.Add(disposableLink);

            var url = $"https://localhost:44355/disposablelink/download/{disposableLink.Id}";

            return Json(url);
        }
    }
}
