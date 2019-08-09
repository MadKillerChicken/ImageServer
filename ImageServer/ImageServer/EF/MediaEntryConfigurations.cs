using ImageServer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServer.EF
{

    internal class MediaEntryConfigurations : EntityTypeConfiguration<MediaEntry>
    {
        public MediaEntryConfigurations()
        {
            this.Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(32)
                .IsFixedLength();

            this.Property(e => e.Id)
                .IsConcurrencyToken();

            this.HasMany(e => e.Categories)
                .WithOptional();

            this.HasMany(e => e.Subjects)
                .WithOptional();


            /*
            // Configure a one-to-one relationship between Student & StudentAddress
            this.HasOptional(e => e.Address) // Mark Student.Address property optional (nullable)
                .WithRequired(ad => ad.Student); // Mark StudentAddress.Student property as required (NotNull).
            */
        }
    }
}
