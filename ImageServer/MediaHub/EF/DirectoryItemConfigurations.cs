using MediaHub.Models;
using System.Data.Entity.ModelConfiguration;

namespace MediaHub.EF
{
    public class DirectoryItemConfigurations : EntityTypeConfiguration<DirectoryItem>
    {
        public DirectoryItemConfigurations()
        {
            HasMany(c => c.Parents);

            HasMany(c => c.Children)
                .WithMany(c => c.Parents);
        }
    }
}
