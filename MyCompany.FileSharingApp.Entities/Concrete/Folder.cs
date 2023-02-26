using MyCompany.FileSharingApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.Entities.Concrete
{
    public class Folder : IEntity
    {
        public Guid FolderId { get; set; }
        public string FolderName { get; set; }
        public string FolderDescription { get; set; }

        public Guid? ParentFolderId { get; set; }
        public Folder? ParentFolder { get; set; }

        public ICollection<Folder>? ChildFolders { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<File> Files { get; set; }
    }
}
