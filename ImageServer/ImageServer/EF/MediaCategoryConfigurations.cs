using ImageServer.Models;
using System.Data.Entity.ModelConfiguration;

namespace ImageServer.EF
{
    internal class MediaCategoryConfigurations : EntityTypeConfiguration<MediaCategory>
    {
        public MediaCategoryConfigurations()
        {
            this.Property(c => c.Id)
                .IsRequired()
                .IsConcurrencyToken();

            this.HasMany(c => c.Entries)
                .WithOptional();

            this.HasMany(c => c.Subjects)
                .WithOptional();
        }
    }
}
