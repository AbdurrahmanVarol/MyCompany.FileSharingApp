using System.ComponentModel.DataAnnotations;

namespace MyCompany.FileSharingApp.MVC.Models
{
    public class FolderModel
    {
        public Guid FolderId { get; set; }
        public Guid? ParentFolderId { get; set; }
        [Required]
        public string FolderName { get; set; }
        public string FolderDescription { get; set; }
    }
}
