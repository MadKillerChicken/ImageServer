using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageServer.Models
{
    [Table("MediaCategories")]
    internal class EntryCategory
    {
        [Key]
        public long Id { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public IList<MediaEntry> Entries { get; protected set; }

        public IList<EntrySubject> Subjects { get; protected set; }
    }
}
