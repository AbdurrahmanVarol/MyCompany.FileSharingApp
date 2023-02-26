using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using MyCompany.FileSharingApp.Business.Abstract;
using MyCompany.FileSharingApp.Entities.Concrete;
using MyCompany.FileSharingApp.MVC.Models;

namespace MyCompany.FileSharingApp.MVC.Controllers
{
    public class FolderController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFolderService _folderService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IFileProvider _fileProvider;

        private Guid UserId => Guid.Parse(User.Claims.First().Value);
        public FolderController(IUserService userService, IFolderService folderService, IFileService fileService, IMapper mapper, IFileProvider fileProvider)
        {
            _userService = userService;
            _folderService = folderService;
            _fileService = fileService;
            _mapper = mapper;
            _fileProvider = fileProvider;
        }

        public IActionResult GetFolders()
        {
            var folders = _folderService.GetFoldersWithFilesByUserId(UserId);
            return Json(folders);
        }

        public IActionResult AddFolder(Guid? folderId)
        {
            if (folderId == null)
            {
                return View();
            }
            var folder = new FolderModel
            {
                ParentFolderId = (Guid)folderId
            };
            return View(folder);
        }
        [HttpPost]
        public IActionResult AddFolder(FolderModel folderModel)
        {

            var newFolder = new Folder
            {
                FolderName = folderModel.FolderName,
                FolderDescription = folderModel.FolderDescription
            };
            newFolder.ParentFolderId = folderModel.ParentFolderId ?? UserId;
            newFolder.UserId = Guid.Parse(User.Claims.First().Value);

            _folderService.Add(newFolder);
            var s = newFolder;
            var x = _folderService.GetFolderPath(newFolder.FolderId);
            try
            {
                var appData = _fileProvider.GetDirectoryContents("").First(p => p.Name.Equals("App_Data")).PhysicalPath;
                string path = Path.Combine(appData, x);

                // If directory does not exist, create it
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception)
            {
            }
            return RedirectToAction("Index", "home");
        }
        public IActionResult UpdateFolder(Guid folderId)
        {
            var folder = _folderService.GetById(folderId);
            if (folder is null)
            {
                return RedirectToAction("Index", "home");
            }
            var folderModel = _mapper.Map<FolderModel>(folder);
            return View(folderModel);
        }
        [HttpPost]
        public IActionResult UpdateFolder(FolderModel folderModel)
        {
            var folder = _folderService.GetById(folderModel.FolderId);
            if (folder is null)
            {
                TempData["message"] = "Folder Not Found.";
                return RedirectToAction("Index", "home");
            }
            folder.FolderName = folderModel.FolderName;
            folder.FolderDescription = folderModel.FolderDescription;
            _folderService.Update(folder);
            TempData["message"] = "Folder Updated.";
            return RedirectToAction("Index", "home");
        }
        public IActionResult DeleteFolder(Guid folderId)
        {
            var folder = _folderService.GetById(folderId);
            var folderPath = _folderService.GetFolderPath(folderId);
            _folderService.Delete(folder);
            var appData = _fileProvider.GetDirectoryContents("").First(p => p.Name.Equals("App_Data")).PhysicalPath;
            string path = Path.Combine(appData, folderPath);

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            return Json("");
        }
    }
}
