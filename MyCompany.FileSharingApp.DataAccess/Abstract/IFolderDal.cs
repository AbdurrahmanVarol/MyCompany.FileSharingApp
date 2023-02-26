using MyCompany.FileSharingApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.DataAccess.Abstract
{
    public interface IFolderDal : IEntityRepository<Folder>
    {
        List<Folder> GetFoldersWithFiles(Expression<Func<Folder, bool>> filter = null);
        string GetFolderPath(Guid folderId);
    }
}
