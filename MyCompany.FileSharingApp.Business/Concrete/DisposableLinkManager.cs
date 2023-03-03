using MyCompany.FileSharingApp.Business.Abstract;
using MyCompany.FileSharingApp.DataAccess.Abstract;
using MyCompany.FileSharingApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.Business.Concrete
{
    public class DisposableLinkManager : IDisposableLinkService
    {
        private readonly IDosposableLinkDal _dosposableLinkDal;

        public DisposableLinkManager(IDosposableLinkDal dosposableLinkDal)
        {
            _dosposableLinkDal = dosposableLinkDal;
        }

        public void Add(DisposableLink entity)
        {
            _dosposableLinkDal.Add(entity);
        }

        public void Delete(DisposableLink entity)
        {
            _dosposableLinkDal.Delete(entity);
        }

        public List<DisposableLink> GetAll()
        {
            return _dosposableLinkDal.GetAll();
        }

        public DisposableLink GetById(Guid id)
        {
            return _dosposableLinkDal.Get(p=>p.Id== id);
        }

        public void Update(DisposableLink entity)
        {
            _dosposableLinkDal.Update(entity);
        }
    }
}
