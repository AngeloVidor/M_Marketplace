using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace M_API.Infrastructure.Mappings
{
    public class CartItemMapping : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.UnitPrice)
                   .HasPrecision(14, 2)
                   .IsRequired();

            builder.Property(i => i.UnitPrice)
                   .HasPrecision(14, 2)
                   .IsRequired();

            builder.Property(i => i.Quantity).IsRequired();
        }
    }

}