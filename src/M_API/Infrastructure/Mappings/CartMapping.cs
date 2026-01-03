using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

public class CartMapping : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasMany(c => c.Items)
               .WithOne()
               .HasForeignKey(i => i.CartId);

        builder.Property(c => c.UserId).IsRequired();
        builder.Property(c => c.IsConverted).IsRequired();
        builder.Property(c => c.CreatedAt).IsRequired();
    }
}
