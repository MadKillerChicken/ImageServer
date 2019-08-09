using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ImageServer.Models
{

    internal abstract class MediaEntry
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public IList<MediaCategory> Categories { get; protected set; }

        public IList<MediaSubject> Subjects { get; protected set; }

        public string Location { get; set; }

        public ContainerEntry Parent { get; set; }
    }
}
