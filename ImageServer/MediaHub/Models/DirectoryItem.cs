using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaHub.Models
{
    public class DirectoryItem : FileSystemItem, IDirectoryItem
    {
        public virtual ICollection<FileSystemItem> Children { get; set; } = new HashSet<FileSystemItem>();

        public DirectoryItem() : base() { }

        public DirectoryItem(FileSystemInfo referencedObject)
            : base(referencedObject) { }
    }
}
