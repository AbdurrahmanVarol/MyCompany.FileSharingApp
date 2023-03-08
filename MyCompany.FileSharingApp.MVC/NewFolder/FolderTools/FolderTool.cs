using Microsoft.Extensions.FileProviders;

namespace MyCompany.FileSharingApp.MVC.NewFolder.FolderTools
{
    public class FolderTool : IFolderTool
    {
        private readonly IFileProvider _fileProvider;

        public FolderTool(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public bool CreateFolder(string folderPath)
        {
            try
            {
                var appData = _fileProvider.GetDirectoryContents("").First(p => p.Name.Equals("App_Data")).PhysicalPath;
                string path = Path.Combine(appData, folderPath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool DeleteFolder(string folderPath)
        {
            try
            {
                var appData = _fileProvider.GetDirectoryContents("").First(p => p.Name.Equals("App_Data")).PhysicalPath;
                string path = Path.Combine(appData, folderPath);
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
