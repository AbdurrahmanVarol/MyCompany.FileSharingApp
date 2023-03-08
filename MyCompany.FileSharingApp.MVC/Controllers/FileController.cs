using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using MyCompany.FileSharingApp.Business.Abstract;
using MyCompany.FileSharingApp.Entities.Concrete;
using MyCompany.FileSharingApp.MVC.Filters;
using MyCompany.FileSharingApp.MVC.Models;
using MyCompany.FileSharingApp.MVC.NewFolder.FileTools;
using System.Security.Claims;
using static NuGet.Packaging.PackagingConstants;

namespace MyCompany.FileSharingApp.MVC.Controllers
{
    [Authorize]
    [CustomExceptionFilter]
    public class FileController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFolderService _folderService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IFileProvider _fileProvider;
        private readonly IFileTool _fileTool;
        private Guid UserId => Guid.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).First().Value);

        public FileController(IUserService userService, IFolderService folderService, IFileService fileService, IMapper mapper, IFileProvider fileProvider, IFileTool fileTool)
        {
            _userService = userService;
            _folderService = folderService;
            _fileService = fileService;
            _mapper = mapper;
            _fileProvider = fileProvider;
            _fileTool = fileTool;
        }

        public IActionResult AddFile(Guid? folderId)
        {
            var a = UserId;
            return View(new FileModel { FolderId = folderId ?? UserId });
        }
        [HttpPost]
        public IActionResult AddFile(FileModel fileModel)
        {
            string fullName = fileModel.FormFile.FileName;
            var fileName = Path.GetFileNameWithoutExtension(fullName);
            var extension = Path.GetExtension(fullName);
            var file = new MyCompany.FileSharingApp.Entities.Concrete.File
            {
                FileName = fileName,
                FileExtention = extension,
                FileUploadTime = DateTime.UtcNow,
                FileSize = fileModel.FormFile.Length,
                UserId = UserId,
                FolderId = fileModel.FolderId
            };
            _fileService.Add(file);

            var path = _fileProvider.GetDirectoryContents("").First(x => x.Name == "App_Data").PhysicalPath;

            var folderPath = _folderService.GetFolderPath((Guid)fileModel.FolderId);

            var fullPath = Path.Combine(path, folderPath, file.FileId.ToString());

            var stream = new FileStream(fullPath, FileMode.Create);

            fileModel.FormFile.CopyTo(stream);
            stream.Close();
            return RedirectToAction("Index","home");
        }
        public IActionResult UpdateFile(Guid fileId)
        {
            var file = _fileService.GetById(fileId);
            if (file is null)
                return RedirectToAction("index", "home");
            var fileModel = _mapper.Map<FileModel>(file);
            return View(fileModel);
        }
        [HttpPost]
        public IActionResult UpdateFile(FileModel fileModel)
        {
            var file = _fileService.GetById((Guid)fileModel.FileId);

            if (file is null)
                return RedirectToAction("index", "home");

            file.FileName = fileModel.FileName;
            _fileService.Update(file);
            TempData["message"] = "File Name Updated";
            return RedirectToAction("index", "home");
        }
        [HttpDelete]
        public IActionResult DeleteFile(Guid fileId)
        {
            var file = _fileService.GetById(fileId);
            _fileService.Delete(file);
            var folderPath = _folderService.GetFolderPath((Guid)file.FolderId);
            var filePath = Path.Combine(folderPath, file.FileId.ToString());
            var result = _fileTool.DeleteFile(filePath);          

            return Json(new {IsSuccess = true, Result = "File Deleted."});
        }
        
        public IActionResult DownloadFile(Guid? fileId)
        {
            var file = _fileService.GetById((Guid)fileId);
            var path = _fileProvider.GetDirectoryContents("").First(x => x.Name == "App_Data").PhysicalPath;
            var fullPath = file.FolderId == null ?
                Path.Combine(path, UserId.ToString(), file.FileId.ToString()) :
                Path.Combine(path, _folderService.GetFolderPath((Guid)file.FolderId), file.FileId.ToString());


            var mimeType = MimeMapping.MimeUtility.GetMimeMapping(file.FileName + file.FileExtention);
            byte[] buffer = System.IO.File.ReadAllBytes(fullPath);
            return File(buffer, mimeType, file.FileName + file.FileExtention);
        }

    }
}
