using Domain.Entities;
using M_API.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Mappings
{
    public class PendingRegistrationMapping : IEntityTypeConfiguration<PendingRegistration>
    {
        public void Configure(EntityTypeBuilder<PendingRegistration> builder)
        {
            builder.HasKey(p => p.Id);

            builder.OwnsOne(p => p.Email, email =>
            {
                email.Property(e => e.Value)
                     .HasColumnName("Email")
                     .IsRequired();
            });

            builder.OwnsOne(p => p.Password, pw =>
            {
                pw.Property(p => p.Value)
                  .HasColumnName("PasswordHash")
                  .IsRequired();
            });

            builder.Property(p => p.Role)
                   .HasConversion<string>()
                   .IsRequired();

            builder.OwnsOne(p => p.ActivationToken, token =>
            {
                token.Property(t => t.Token)
                     .HasColumnName("ActivationCode")
                     .IsRequired();

                token.Property(t => t.ExpiresAt)
                     .HasColumnName("ExpiresAt")
                     .IsRequired();
            });

            builder.Property(p => p.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();
        }
    }
}
