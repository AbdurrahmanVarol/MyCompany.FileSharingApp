using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCompany.FileSharingApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.FileSharingApp.DataAccess.Concrete.Mapping
{
    public class FolderMap : IEntityTypeConfiguration<Folder>
    {
        public void Configure(EntityTypeBuilder<Folder> builder)
        {
            builder.HasKey(p => p.FolderId);

            builder.Property(p => p.FolderId).HasDefaultValueSql("NEWID()");

            builder.Property(p => p.FolderName).HasMaxLength(200);
            builder.Property(p => p.FolderDescription).HasMaxLength(500);

            builder.HasMany(p => p.Files)
                .WithOne(p => p.Folder)
                .HasPrincipalKey(p => p.FolderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
