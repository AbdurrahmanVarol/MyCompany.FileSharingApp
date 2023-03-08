using Microsoft.Extensions.FileProviders;

namespace MyCompany.FileSharingApp.MVC.NewFolder.FileTools
{
    public class FileTool : IFileTool
    {
        private readonly IFileProvider _fileProvider;

        public FileTool(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }


        public bool DeleteFile(string filePath)
        {
            try
            {
                var appData = _fileProvider.GetDirectoryContents("").First(p => p.Name.Equals("App_Data")).PhysicalPath;
                string path = Path.Combine(appData, filePath);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
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
