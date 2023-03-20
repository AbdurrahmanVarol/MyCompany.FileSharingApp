using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using MyCompany.FileSharingApp.Business.Abstract;
using MyCompany.FileSharingApp.Entities.Concrete;
using MyCompany.FileSharingApp.MVC.Filters;
using MyCompany.FileSharingApp.MVC.NewFolder.FileTools;
using MyCompany.FileSharingApp.MVC.NewFolder.FolderTools;

namespace MyCompany.FileSharingApp.MVC.Controllers
{
    [Authorize]
    [CustomExceptionFilter]
    public class FolderController : Controller
    {
        private readonly IFolderService _folderService;
        private readonly IMapper _mapper;
        private readonly IFolderTool _folderTool;

        private Guid UserId => Guid.Parse(User.Claims.First().Value);
        public FolderController(IFolderService folderService, IMapper mapper, IFolderTool folderTool)
        {
            _folderService = folderService;
            _mapper = mapper;
            _folderTool = folderTool;
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

            var folderPath = _folderService.GetFolderPath(newFolder.FolderId);
            var result = _folderTool.CreateFolder(folderPath);

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
        [HttpDelete]
        public IActionResult DeleteFolder(Guid folderId)
        {
            var folder = _folderService.GetById(folderId);
            var folderPath = _folderService.GetFolderPath(folderId);
            _folderService.Delete(folder);

            var result = _folderTool.DeleteFolder(folderPath);

            return Json(new { IsSuccess = true, Result = "Folder Deleted." });
        }
    }
}
