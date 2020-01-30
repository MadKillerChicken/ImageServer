using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaHub.Models
{
    public class FileExtension : ValuesEnumeration<FileTypes>
    {
        public static FileExtension Jpg = new FileExtension(FileTypes.Image, "jpg", "jpeg");
        public static FileExtension Png = new FileExtension(FileTypes.Image, "png");
        public static FileExtension Mp4 = new FileExtension(FileTypes.Video, "mp4");

        private FileExtension(FileTypes fileType, params string[] extensions) :
            base(fileType, extensions) { }

        private static Dictionary<string, FileExtension> _index = null;

        public static FileExtension GetInstanceFromString(string extension)
        {
            // Initialize index if its empty
            if (_index == null) {
                _index = new Dictionary<string, FileExtension>();
                foreach(var fileExt in GetAll<FileExtension>()) {
                    foreach(var ext in fileExt.Values) {
                        _index.Add(ext, fileExt);
                    }
                }
            }

            // Check the index if an extension exists
            return 
                _index.TryGetValue(
                    extension?.Trim(new char[] { '.', ' ' })?.ToLower() ?? throw new ArgumentNullException(nameof(extension)),
                    out FileExtension result)
                    ? result
                    : null;
        }

        public static HashSet<string> GetFsExtensionFilter(params FileTypes[] fileTypes) =>
            (fileTypes == null || !fileTypes.Any())
                ? new HashSet<string>()
                : new HashSet<string>(
                    GetAll<FileExtension>()
                        .Where(e => fileTypes.Contains(e.ValueType))
                        .SelectMany(e => e.Values));

    }
}
