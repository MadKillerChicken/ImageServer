using MediaHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MediaHub.EF
{
    public class MediaDbInitializer : CreateDatabaseIfNotExists<MediaContext> //DropCreateDatabaseAlways
    {
        protected override void Seed(MediaContext context)
        {
            base.Seed(context);
        }
    }
}
