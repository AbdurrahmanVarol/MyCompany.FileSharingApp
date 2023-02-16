using MyCompany.FileSharingApp.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.DataAccess.Concrete.EntityFramework
{
    public class EfFileDal : EfEntityRepositoryBase<FileSharingAppContext, MyCompany.FileSharingApp.Entities.Concrete.File>, IFileDal
    {
        public EfFileDal(FileSharingAppContext context) : base(context)
        {
        }
    }
}
