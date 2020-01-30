using MediaHub.Models.Containers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MediaHub.EF
{
    public class MediaContainerConfigurations : EntityTypeConfiguration<MediaContainer>
    {
        public void MediaCategoryConfigurations()
        {
            ToTable("MediaContainers");

            HasKey(p => p.Id);

            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(c => c.Url)
                .IsRequired();
        }
    }
}
