using System.Collections.Generic;
using System.IO;

namespace ImageServer
{
    internal abstract class MediaEntry
    {
        public abstract IEnumerable<MediaSubject> Subjects { get; protected set; }

        public FileInfo Location { get; protected set; } 
    }
}
