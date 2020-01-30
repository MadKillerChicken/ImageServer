using System.Collections.Generic;

namespace MediaHub.Models
{
    public interface ISubject
    {
        string Description { get; set; }
        ICollection<FileSystemItem> Items { get; set; }
        long Id { get; set; }
        string Name { get; set; }
    }
}