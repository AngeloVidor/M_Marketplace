using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace M_API.Infrastructure.Mappings
{
    public class PurchaseHistoryMapping : IEntityTypeConfiguration<PurchaseHistory>
    {
        public void Configure(EntityTypeBuilder<PurchaseHistory> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserId)
                   .IsRequired();

            builder.Property(p => p.ProductId)
                   .IsRequired();

            builder.Property(p => p.ProductName)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(p => p.Quantity)
                   .IsRequired();

            builder.Property(p => p.UnitPrice)
                   .HasPrecision(14, 2)
                   .IsRequired();

            builder.Property(p => p.PurchasedAt)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();
        }
    }
}
