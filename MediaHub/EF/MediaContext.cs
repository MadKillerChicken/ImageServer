using MediaHub.Models;
using MediaHub.Models.Containers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaHub.EF
{
    public class MediaContext : DbContext
    {

        public MediaContext() : base("MediaDB-EF6CodeFirst")
        {
            // Database.Connection.ConnectionString = "Data Source=srv;Initial Catalog=EFTest;Integrated Security=True";
            Database.Connection.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EFTest;Integrated Security=True";
            Database.SetInitializer(new MediaDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Adds configurations for Student from separate class
            modelBuilder.Configurations.Add(new MediaContainerConfigurations());
            modelBuilder.Configurations.Add(new FileSystemItemConfigurations());
            modelBuilder.Configurations.Add(new DirectoryItemConfigurations());
            modelBuilder.Configurations.Add(new FileItemConfigurations());
            modelBuilder.Configurations.Add(new SubjectConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfigurations());
        }

        public DbSet<MediaContainer> Containers { get; set; }

        public DbSet<FileSystemItem> Items { get; set; }

        public DbSet<FileItem> Files { get; set; }

        public DbSet<DirectoryItem> Directories { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Subject> Subjects { get; set; }

    }
}
