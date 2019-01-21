using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SEContactManager.ApplicationCore.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEContactManager.Infrastructure.EntityConfig
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder
                .Property(model => model.Name)
                .HasColumnType("varchar(50)")
                .IsRequired(true);

            builder
                .Property(model => model.Phone)
                .HasColumnType("varchar(50)")
                .IsRequired(true);

            builder
                .Property(model => model.LastPurchase)
                .IsRequired(false);
        }
    }
}
