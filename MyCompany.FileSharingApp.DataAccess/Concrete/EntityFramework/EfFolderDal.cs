using Microsoft.EntityFrameworkCore;
using MyCompany.FileSharingApp.DataAccess.Abstract;
using MyCompany.FileSharingApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.DataAccess.Concrete.EntityFramework
{
    public class EfFolderDal : EfEntityRepositoryBase<FileSharingAppContext, Folder>, IFolderDal
    {
        private readonly FileSharingAppContext _context;
        public EfFolderDal(FileSharingAppContext context) : base(context)
        {
            _context = context;
        }

        public override void Delete(Folder entity)
        {
            var folder = Get(p => p.FolderId == entity.FolderId);
            if (folder is null)
                return;
            var childFolders = GetAll(p => p.ParentFolderId == entity.FolderId);
            if (childFolders is not null && childFolders.Any())
            {
                foreach (var chieldFolder in childFolders)
                {
                    Delete(chieldFolder);
                }
            }
            base.Delete(entity);
        }

        public List<Folder> GetFoldersWithFiles(Expression<Func<Folder, bool>> filter = null)
        {
            var x = filter;
            return filter == null ? _context.Folders.Include(p => p.Files).ToList() : _context.Folders.Include(p => p.Files)
                .Where(filter).ToList();
        }

        public string GetFolderPath(Guid folderId)
        {
            var folder = _context.Folders.FirstOrDefault(p => p.FolderId == folderId);
            if (folder is not null && folder.ParentFolderId is not null)
            {
                return $@"{GetFolderPath((Guid)folder.ParentFolderId)}\{folderId}";
            }
            return $@"{folder.FolderId}";
        }
    }
}
