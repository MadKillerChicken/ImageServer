﻿using ImageServer.Models;
using System.Data.Entity.ModelConfiguration;

namespace ImageServer.EF
{
    internal class MediaSetConfigurations : EntityTypeConfiguration<MediaSet>
    {
        public MediaSetConfigurations()
        {
            this.Property(s => s.Id)
                .IsRequired()
                .IsConcurrencyToken();
        }
    }
}