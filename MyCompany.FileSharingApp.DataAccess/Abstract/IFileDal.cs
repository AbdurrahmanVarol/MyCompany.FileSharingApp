using MyCompany.FileSharingApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.DataAccess.Abstract
{
    public interface IFileDal : IEntityRepository<MyCompany.FileSharingApp.Entities.Concrete.File>
    {
    }
}
