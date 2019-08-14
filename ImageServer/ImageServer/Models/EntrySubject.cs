using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageServer.Models
{
    /// <summary>
    /// Basic main content of a media entry.
    /// </summary>
    [Table("Subjects")]
    internal class EntrySubject
    {
        [Key]
        public long Id { get; protected set; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

    }
}
