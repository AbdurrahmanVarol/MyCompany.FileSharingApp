using MyCompany.FileSharingApp.Business.Abstract;
using MyCompany.FileSharingApp.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.Business.Concrete
{
    public class FileManager : IFileService
    {
        private readonly IFileDal _fileDal;

        public FileManager(IFileDal fileDal)
        {
            _fileDal = fileDal;
        }

        public void Add(Entities.Concrete.File file)
        {            
            _fileDal.Add(file);
        }

        public void Delete(Entities.Concrete.File file)
        {
            _fileDal.Delete(file);
        }

        public List<Entities.Concrete.File> GetAll()
        {
            return _fileDal.GetAll();
        }

        public Entities.Concrete.File GetById(Guid id)
        {
            return _fileDal.Get(p => p.FileId == id);
        }

        public void Update(Entities.Concrete.File file)
        {
            _fileDal.Update(file);
        }
    }
}
