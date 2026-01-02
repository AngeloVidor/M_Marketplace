using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username).IsRequired();
            builder.Property(u => u.IsActive);

            builder.OwnsOne(u => u.Email, e =>
            {
                e.Property(p => p.Value)
                 .HasColumnName("Email")
                 .IsRequired();
            });

            builder.OwnsOne(u => u.Password, p =>
            {
                p.Property(v => v.Value)
                 .HasColumnName("PasswordHash")
                 .IsRequired();
            });
        }
    }
}
