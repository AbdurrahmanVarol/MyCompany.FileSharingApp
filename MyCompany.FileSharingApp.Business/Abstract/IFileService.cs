﻿using MyCompany.FileSharingApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.Business.Abstract
{
    public interface IFileService : IServiceRepository<MyCompany.FileSharingApp.Entities.Concrete.File>
    {
    }
}
