﻿using MyCompany.FileSharingApp.DataAccess.Abstract;
using MyCompany.FileSharingApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.DataAccess.Concrete.EntityFramework
{
    public class EfFolderDal : EfEntityRepositoryBase<FileSharingAppContext, Folder>, IFolderDal
    {
        public EfFolderDal(FileSharingAppContext context) : base(context)
        {
        }
    }
}
