using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.DataAccess.Concrete.Mapping
{
    public class FileMap : IEntityTypeConfiguration<MyCompany.FileSharingApp.Entities.Concrete.File>
    {
        public void Configure(EntityTypeBuilder<Entities.Concrete.File> builder)
        {
            builder.HasKey(p => p.FileId);

            builder.Property(p => p.FileId).HasDefaultValueSql("NEWID()");

        }
    }
}
