using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings
{
    public class VendorProfileMapping : IEntityTypeConfiguration<VendorProfile>
    {
        public void Configure(EntityTypeBuilder<VendorProfile> builder)
        {
            builder.HasKey(v => v.UserId);

            builder.OwnsOne(v => v.OwnerName, o =>
            {
                o.Property(p => p.FirstName).HasColumnName("OwnerFirstName").IsRequired();
                o.Property(p => p.LastName).HasColumnName("OwnerLastName").IsRequired();
            });

            builder.OwnsOne(v => v.CompanyName, c =>
            {
                c.Property(p => p.Value).HasColumnName("CompanyName").IsRequired();
            });

            builder.OwnsOne(v => v.Cnpj, c =>
            {
                c.Property(p => p.Value).HasColumnName("Cnpj").IsRequired();
            });

            builder.OwnsOne(v => v.Address, a =>
            {
                a.Property(p => p.Street).HasColumnName("Street").IsRequired();
                a.Property(p => p.Number).HasColumnName("Number").IsRequired();
                a.Property(p => p.City).HasColumnName("City").IsRequired();
                a.Property(p => p.State).HasColumnName("State").IsRequired();
                a.Property(p => p.Country).HasColumnName("Country").IsRequired();
                a.Property(p => p.ZipCode).HasColumnName("ZipCode").IsRequired();
                a.Property(p => p.Complement).HasColumnName("Complement");
                a.Property(p => p.Neighborhood).HasColumnName("Neighborhood");
            });

            builder.OwnsOne(v => v.Phone, phone =>
            {
                phone.Property(p => p.Number)
                     .HasColumnName("Phone")
                     .IsRequired();
            });

        }
    }
}
