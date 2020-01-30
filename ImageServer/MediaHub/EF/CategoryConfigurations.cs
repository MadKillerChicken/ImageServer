using MediaHub.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MediaHub.EF
{
    public class CategoryConfigurations : EntityTypeConfiguration<Category>
    {
        public CategoryConfigurations()
        {
            HasKey(p => p.Id);
            
            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired()
                .IsConcurrencyToken();

            HasMany(p => p.Items)
                .WithMany(p => p.Categories);
        }
    }
}
