using System.Collections.Generic;

namespace MediaHub.Models
{
    public interface ICategory
    {
        string Description { get; }
        long Id { get; }
        string Name { get; }

        ICollection<FileSystemItem> Items { get; }

    }
}