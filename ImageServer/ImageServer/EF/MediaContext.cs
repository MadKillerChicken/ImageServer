using ImageServer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServer.EF
{
    internal class MediaContext : DbContext
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
            modelBuilder.Configurations.Add(new MediaEntryConfigurations()); // Was: StudentConfigurations
            modelBuilder.Configurations.Add(new MediaSetConfigurations());
            modelBuilder.Configurations.Add(new MediaSubjectConfiguration());
            modelBuilder.Configurations.Add(new MediaCategoryConfigurations());

            /*modelBuilder.Entity<Teacher>()
                .ToTable("TeacherInfo");

            modelBuilder.Entity<Teacher>()
                .MapToStoredProcedures();*/
        }

        public DbSet<MediaEntry> Entries { get; set; }
        public DbSet<MediaSet> Sets { get; set; }
        public DbSet<MediaCategory> Categories { get; set; }
        public DbSet<MediaSubject> Subjects { get; set; }

    }
}
