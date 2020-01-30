using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace MediaHub.Models.Containers
{
    /// <summary>
    /// References a local directory containing media
    /// </summary>
    
    public class FileSystemMediaContainer : MediaContainer
    {
        public DirectoryInfo RootPath => 
            new DirectoryInfo(new Uri(Url).LocalPath);

        public FileSystemMediaContainer(string path, params FileTypes[] filters) : 
            base (path, filters)
        {
        }

        public FileSystemMediaContainer() : base ("file:///empty", null) { }
    }
}
