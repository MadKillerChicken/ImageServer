using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageServer.Models
{
    [Table("ContainerEntries")]
    internal class ContainerEntry : MediaEntry
    {
        public IList<MediaEntry> Entries { get; set; }
    }
}
