using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SEContactManager.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEContactManager.Infrastructure.EntityConfig
{
    public class RegionMap : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {

            builder
                .Property(model => model.Name)
                .HasColumnType("varchar(20)")
                .IsRequired(true);

        }
    }
}
