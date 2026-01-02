using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class CustomerProfileMapping : IEntityTypeConfiguration<CustomerProfile>
    {
        public void Configure(EntityTypeBuilder<CustomerProfile> builder)
        {
            builder.ToTable("CustomerProfiles");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.OwnsOne(p => p.FullName, fn =>
            {
                fn.Property(f => f.FirstName).HasColumnName("FirstName").IsRequired();
                fn.Property(f => f.LastName).HasColumnName("LastName").IsRequired();
            });

            builder.OwnsOne(p => p.Address, a =>
            {
                a.Property(ad => ad.Street).HasColumnName("Street").IsRequired();
                a.Property(ad => ad.City).HasColumnName("City").IsRequired();
                a.Property(ad => ad.State).HasColumnName("State").IsRequired();
                a.Property(ad => ad.Country).HasColumnName("Country").IsRequired();
                a.Property(ad => ad.ZipCode).HasColumnName("ZipCode").IsRequired();
            });

            builder.OwnsOne(p => p.Phone, ph =>
            {
                ph.Property(pn => pn.Number).HasColumnName("Phone").IsRequired();
            });
        }
    }
}
