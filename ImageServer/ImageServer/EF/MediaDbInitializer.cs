using ImageServer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ImageServer.EF
{
    internal class MediaDbInitializer : CreateDatabaseIfNotExists<MediaContext> //DropCreateDatabaseAlways
    {
        protected override void Seed(MediaContext context)
        {
            /*IList<Grade> grades = new List<Grade>();

            grades.Add(new Grade() { GradeName = "Grade 1", Section = "A" });
            grades.Add(new Grade() { GradeName = "Grade 1", Section = "B" });
            grades.Add(new Grade() { GradeName = "Grade 1", Section = "C" });
            grades.Add(new Grade() { GradeName = "Grade 2", Section = "A" });
            grades.Add(new Grade() { GradeName = "Grade 3", Section = "A" });

            context.Grades.AddRange(grades);*/

            var result = 
                ReadFilesystemToEntries(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)))
                .ToList();

            context.Entries.AddRange(result);

            base.Seed(context);
        }

        private IEnumerable<BaseEntry> ReadFilesystemToEntries(DirectoryInfo dir)
        {
            List<BaseEntry> results = new List<BaseEntry>();
            var fsEntries = dir.GetFileSystemInfos().ToList();
            var parent = new MediaEntrySet() {
                Id = CreateMD5(dir.FullName),
                Name = dir.Name,
                Location = dir.FullName
            };
            results.Add(parent);

            var dbEntries = fsEntries
                .Where(e => !File.GetAttributes(e.FullName).HasFlag(FileAttributes.Directory))
                .ToList()
                .ConvertAll(e =>
                    (BaseEntry)new ImageEntry() {
                        Id = CreateMD5(e.FullName),
                        Name = e.Name,
                        Location = e.FullName,
                        Parent = parent
                    });

            parent.Children = dbEntries;
            results.AddRange(dbEntries);

            foreach (var fsDir in fsEntries.Where(e => File.GetAttributes(e.FullName).HasFlag(FileAttributes.Directory))) {
                results.AddRange(ReadFilesystemToEntries(new DirectoryInfo(fsDir.FullName)));
            }
            return results;
        }

        public static string CreateMD5(string input)
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

    }
}
