using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired();

            builder.OwnsOne(p => p.Price, m =>
            {
                m.Property(v => v.Value)
                 .HasColumnName("Price")
                 .HasPrecision(18, 2)
                 .IsRequired();
            });

            builder.OwnsOne(p => p.Stock, s =>
            {
                s.Property(v => v.Quantity)
                 .HasColumnName("StockQuantity")
                 .IsRequired();
            });

            builder.Property(p => p.OwnerId).IsRequired();
        }
    }
}
