using System.IO;

namespace MediaHub.Models
{
    public class VideoItem : FileItem
    {
        public VideoItem() : base() { }

        public VideoItem(FileSystemInfo referencedObject)
            : base(referencedObject) { }
    }
}
