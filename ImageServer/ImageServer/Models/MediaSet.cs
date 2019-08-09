using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ImageServer.Models
{
    internal abstract class MediaSet
    {
        [Key]
        public long Id { get; protected set; }

        public string Name { get; protected set; }

        public string Descritpion { get; protected set; }

        public IList<MediaCategory> Categories { get; protected set; }

        public IList<MediaSubject> Subjects { get; protected set; }

        public IList<MediaEntry> Entries { get; protected set; }

        public string LocationId { get; protected set; }

        public bool IsDirectory { get; protected set; }

        public string Location { get; protected set; }

    }
}
