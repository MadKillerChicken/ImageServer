using System.Collections.Generic;
using System.IO;

namespace ImageServer.Models
{
    internal abstract class MediaSet
    {
        public long Id { get; protected set; }

        public string Name { get; protected set; }

        public string Descritpion { get; protected set; }

        public abstract IEnumerable<MediaCategory> Categories { get; protected set; }

        public abstract IEnumerable<MediaSubject> Subjects { get; protected set; }

        public abstract IEnumerable<MediaEntry> Entries { get; protected set; }

        public string LocationId { get; protected set; }

        public bool IsDirectory { get; protected set; }

        public DirectoryInfo Location { get; protected set; }

    }
}
