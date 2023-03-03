using MyCompany.FileSharingApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.Business.Abstract
{
    public interface IServiceRepository<T> where T : class,IEntity, new()
    {
        List<T> GetAll();
        T GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
