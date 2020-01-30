using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MediaHub.Models
{
    public abstract class FileSystemItem : IFileSystemItem
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Title { get; set; }

        public byte Rating { get; set; }

        public string Comments { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; } = new HashSet<Subject>();

        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();

        public virtual ICollection<DirectoryItem> Parents { get; set; } = new HashSet<DirectoryItem>();

        public FileSystemItem() { }

        protected FileSystemItem(FileSystemInfo referencedObject)
        {
            if (referencedObject == null) { throw new ArgumentNullException(nameof(referencedObject)); }

            Id = CreateMD5(referencedObject.FullName);
            Name = referencedObject.Name;
            Location = referencedObject.FullName;
        }

        public static FileSystemItem CreateFromFileSystemInfo(FileSystemInfo fileSystemInfo)
        {
            if (fileSystemInfo.Attributes.HasFlag(FileAttributes.Directory)) {
                return new DirectoryItem(fileSystemInfo);
            } else {
                FileExtension fileExt = FileExtension.GetInstanceFromString(fileSystemInfo.Extension);
                if (fileExt?.ValueType == FileTypes.Image) {
                    return new ImageItem(fileSystemInfo);
                } else if (fileExt?.ValueType == FileTypes.Image) {
                    return new VideoItem(fileSystemInfo);
                } else {
                    return new FileItem(fileSystemInfo);
                }
            }
        }


        public static string GenerateId(FileSystemInfo fileSystemInfo) => 
            CreateMD5(fileSystemInfo.FullName);

        public static string GenerateId(string data) => 
            CreateMD5(data);

        #region Helpers

        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create()) {

                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++) {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        #endregion Helpers

    }
}
