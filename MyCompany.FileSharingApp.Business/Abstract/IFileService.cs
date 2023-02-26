using MyCompany.FileSharingApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.Business.Abstract
{
    public interface IFileService
    {
        List<MyCompany.FileSharingApp.Entities.Concrete.File> GetAll();
        MyCompany.FileSharingApp.Entities.Concrete.File GetById(Guid id);
        void Add(MyCompany.FileSharingApp.Entities.Concrete.File file);
        void Update(MyCompany.FileSharingApp.Entities.Concrete.File file);
        void Delete(MyCompany.FileSharingApp.Entities.Concrete.File file);
    }
}
