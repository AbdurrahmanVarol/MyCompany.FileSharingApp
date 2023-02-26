namespace MyCompany.FileSharingApp.MVC.Models
{
    public class FileModel
    {
        public Guid? FileId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? FolderId { get; set; }
        public IFormFile FormFile { get; set; }
        public string FileName { get; set; }
    }
}
