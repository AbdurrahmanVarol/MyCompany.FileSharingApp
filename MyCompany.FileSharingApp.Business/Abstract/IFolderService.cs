using MyCompany.FileSharingApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.Business.Abstract
{
    public interface IFolderService : IServiceRepository<Folder>
    {
        List<Folder> GetByUserId(Guid userId);
        List<Folder> GetFoldersWithFilesByUserId(Guid userId);
        string GetFolderPath(Guid folderId);
    }
}
