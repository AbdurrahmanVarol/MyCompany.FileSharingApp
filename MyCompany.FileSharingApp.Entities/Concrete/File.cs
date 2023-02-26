using MyCompany.FileSharingApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.Entities.Concrete
{
    public class File : IEntity
    {
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public string FileExtention { get; set; }
        public long FileSize { get; set; }
        public DateTime FileUploadTime { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid? FolderId { get; set; }
        public Folder Folder { get; set; }
    }
}
