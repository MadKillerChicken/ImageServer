using System.Collections.Generic;

namespace MediaHub.Models
{
    public interface IDirectoryItem : IFileSystemItem
    {
        ICollection<FileSystemItem> Children { get; set; }
    }
}