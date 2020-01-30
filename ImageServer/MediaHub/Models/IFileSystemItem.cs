using System.Collections.Generic;

namespace MediaHub.Models
{
    public interface IFileSystemItem
    {
        string Id { get; }
        string Location { get; }
        string Name { get; set; }
        ICollection<DirectoryItem> Parents { get; }
    }
}