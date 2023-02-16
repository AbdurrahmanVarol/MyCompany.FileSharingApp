using MyCompany.FileSharingApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.Business.Abstract
{
    public interface IUserService
    {
        List<User> GetAll();
        User GetById(Guid id);
        User GetByUserName(string userName);
        void Add(User user);
        void Update(User user);
        void Delete(User user);
    }
}
