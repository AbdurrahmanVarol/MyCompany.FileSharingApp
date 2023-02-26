using MyCompany.FileSharingApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.Business.Abstract
{
    public interface IFolderService
    {
        List<Folder> GetAll();
        List<Folder> GetByUserId(Guid userId);
        List<Folder> GetFoldersWithFilesByUserId(Guid userId);
        Folder GetById(Guid id);
        void Add(Folder folder);
        void Update(Folder folder);
        void Delete(Folder folder);
        string GetFolderPath(Guid folderId);
    }
}
