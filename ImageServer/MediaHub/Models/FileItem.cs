using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaHub.Models
{
    public class FileItem : FileSystemItem
    {
        public FileItem() : base() { }

        public FileItem(FileSystemInfo referencedObject)
            : base(referencedObject) { }
    }
}
