using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace ImageServer.Models
{
    [Table("MediaEntrySets")]
    internal class MediaEntrySet : BaseEntry
    {
        public IList<BaseEntry> Children { get; set; }
    }
}
