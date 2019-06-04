using System.Collections.Generic;
using System.IO;

namespace ImageServer.Models
{
    internal abstract class MediaEntry
    {
        public string Id { get; protected set; }

        public string Name { get; protected set; }

        public abstract IEnumerable<MediaCategory> Categories { get; protected set; }

        public abstract IEnumerable<MediaSubject> Subjects { get; protected set; }

        public FileInfo Location { get; protected set; } 
    }
}
