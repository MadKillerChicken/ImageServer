using System.IO;

namespace MediaHub.Models
{
    public class ImageItem : FileItem
    {
        public ImageItem() : base() { }

        public ImageItem(FileSystemInfo referencedObject)
            : base(referencedObject) { }
    }
}
