using FastFoodOrderingSystem.Domain.Common.Enums;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.PendingRegistrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFoodOrderingSystem.Infrastructure.Persistence.Database.Configurations;

public sealed class PendingRegistrationConfiguration : IEntityTypeConfiguration<PendingRegistration>
{
    public void Configure(EntityTypeBuilder<PendingRegistration> builder)
    {
        builder.ToTable("pending_registrations");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Email)
            .IsUnique();

        builder.Property(p => p.Id)
            .HasColumnName("id");
        builder.Property(p => p.FullName)
            .HasColumnName("full_name")
            .HasConversion(
                fullName => fullName.Value,
                value => FullName.Create(value))
            .HasMaxLength(FullName.MaxLength)
            .IsUnicode();
        builder.Property(p => p.Email)
            .HasColumnName("email")
            .HasConversion(
                email => email.Value,
                value => Email.Create(value))
            .HasMaxLength(Email.MaxLength);
        builder.Property(p => p.PasswordHash)
            .HasColumnName("password_hash")
            .HasConversion(
                passwordHash => passwordHash.Value,
                value => PasswordHash.Create(value))
            .HasMaxLength(PasswordHash.MaxLength);
        builder.Property(p => p.PhoneNumber)
            .HasColumnName("phone_number")
            .HasConversion(
                phoneNumber => phoneNumber.Value,
                value => PhoneNumber.Create(value))
            .HasMaxLength(PhoneNumber.MaxLength);
        builder.Property(p => p.Role)
            .HasColumnName("role")
            .HasConversion(
                role => role.Code,
                value => UserRole.FromCode(value))
            .HasMaxLength(UserRole.MaxLengthCode);
    }
}