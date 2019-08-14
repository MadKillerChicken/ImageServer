using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ImageServer.Models
{
    internal abstract class BaseEntry
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public IList<EntryCategory> Categories { get; set; }

        public IList<EntrySubject> Subjects { get; set; }

        public string Location { get; set; }

        public bool IsFsObject => Location != null;

        public MediaEntrySet Parent { get; set; }
    }
}
