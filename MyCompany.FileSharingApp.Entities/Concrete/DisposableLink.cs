using MyCompany.FileSharingApp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.Entities.Concrete
{
    public class DisposableLink : IEntity
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UsedAt { get; set; }
        public int Expire { get; set; }
        public bool IsUsed { get; set; }

        public Guid FileId { get; set; }
        public MyCompany.FileSharingApp.Entities.Concrete.File File { get; set; } 
    }
}
