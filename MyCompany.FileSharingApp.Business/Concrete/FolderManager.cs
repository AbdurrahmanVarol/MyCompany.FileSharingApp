using MyCompany.FileSharingApp.Business.Abstract;
using MyCompany.FileSharingApp.DataAccess.Abstract;
using MyCompany.FileSharingApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.Business.Concrete
{
    public class FolderManager : IFolderService
    {
        private readonly IFolderDal _folderDal;

        public FolderManager(IFolderDal folderDal)
        {
            _folderDal = folderDal;
        }

        public void Add(Folder folder)
        {
            _folderDal.Add(folder);
        }

        public void Delete(Folder folder)
        {
            _folderDal.Delete(folder);
        }

        public List<Folder> GetAll()
        {
            return _folderDal.GetAll().OrderBy(p => p.FolderName).ToList();
        }

        public Folder GetById(Guid id)
        {
            return _folderDal.Get(p => p.FolderId == id);
        }

        public List<Folder> GetByUserId(Guid userId)
        {
            return _folderDal.GetAll(p => p.UserId == userId);
        }

        public string GetFolderPath(Guid folderId)
        {
            return _folderDal.GetFolderPath(folderId);
        }

        public List<Folder> GetFoldersWithFilesByUserId(Guid userId)
        {
            return _folderDal.GetFoldersWithFiles(p => p.UserId == userId).OrderBy(p => p.FolderName).ToList();
        }

        public void Update(Folder folder)
        {
            _folderDal.Update(folder);
        }
    }
}
