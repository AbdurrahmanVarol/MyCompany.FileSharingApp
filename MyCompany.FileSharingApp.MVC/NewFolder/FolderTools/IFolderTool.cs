namespace MyCompany.FileSharingApp.MVC.NewFolder.FolderTools
{
    public interface IFolderTool
    {
        bool CreateFolder(string path);
        bool DeleteFolder(string path);
    }
}
