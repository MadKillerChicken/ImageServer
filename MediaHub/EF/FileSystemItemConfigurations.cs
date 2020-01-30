using MediaHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaHub.EF
{

    public class FileSystemItemConfigurations : EntityTypeConfiguration<FileSystemItem>
    {
        public FileSystemItemConfigurations()
        {
            ToTable("FileSystemItems");

            HasKey(e => e.Id);

            Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(32)
                .IsFixedLength()
                .IsConcurrencyToken();

            HasMany(c => c.Parents)
                .WithMany(c => c.Children);

            HasMany(p => p.Categories)
                .WithMany(p => p.Items);

            HasMany(p => p.Subjects)
                .WithMany(p => p.Items);

        }
    }

    public class FileItemConfigurations : EntityTypeConfiguration<FileItem>
    {
        public FileItemConfigurations()
        {
            /*Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(32)
                .IsFixedLength()
                .IsConcurrencyToken();

            HasMany(c => c.Parents);*/
                //.WithMany(c => c.Children);

            /*HasMany(c => c.Children)
                .WithMany(c => c.Parents);*/

        }
    }
}
