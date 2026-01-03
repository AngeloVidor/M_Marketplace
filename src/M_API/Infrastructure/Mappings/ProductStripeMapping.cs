using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class ProductStripeMapping : IEntityTypeConfiguration<ProductStripe>
    {
        public void Configure(EntityTypeBuilder<ProductStripe> builder)
        {
            builder.HasKey(ps => ps.Id);

            builder.Property(ps => ps.Id)
                   .ValueGeneratedNever();

            builder.Property(ps => ps.ProductId)
                   .IsRequired();

            builder.Property(ps => ps.StripeProductId)
                   .IsRequired();

            builder.Property(ps => ps.StripePriceId)
                   .IsRequired();

            builder.Property(ps => ps.CreatedAt)
                   .IsRequired();

            builder.HasIndex(ps => ps.ProductId)
                   .IsUnique();

            builder.HasOne<Product>()
                   .WithOne()
                   .HasForeignKey<ProductStripe>(ps => ps.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
