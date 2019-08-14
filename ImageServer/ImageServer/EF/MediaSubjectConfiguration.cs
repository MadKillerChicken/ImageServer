using ImageServer.Models;
using System.Data.Entity.ModelConfiguration;

namespace ImageServer.EF
{
    internal class MediaSubjectConfiguration : EntityTypeConfiguration<EntrySubject>
    {
        public MediaSubjectConfiguration()
        {
            this.Property(s => s.Id)
                .IsRequired()
                .IsConcurrencyToken();
        }
    }
}
