using FastFoodOrderingSystem.Domain.Common.Enums;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Users;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFoodOrderingSystem.Infrastructure.Persistence.Database.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(entity => entity.Id);
        builder.HasIndex(u => u.Email)
            .IsUnique();

        /*
         * Config The Fields
         */
        builder.Property(u => u.Id)
            .HasColumnName("id");

        builder.Property(u => u.FullName)
            .HasConversion(
                fullName => fullName.Value,
                value => FullName.Create(value).Value!)
            .IsRequired()
            .HasMaxLength(FullName.MaxLength)
            .IsUnicode()
            .HasColumnName("full_name");

        builder.Property(u => u.Email)
            .HasConversion(
                email => email.Value,
                value => Email.Create(value).Value!)
            .IsRequired()
            .HasMaxLength(Email.MaxLength)
            .HasColumnName("email");

        builder.Property(u => u.PasswordHash)
            .HasConversion(
                passwordHash => passwordHash.Value,
                value => PasswordHash.Create(value).Value!)
            .IsRequired()
            .HasMaxLength(PasswordHash.MaxLength)
            .HasColumnName("password_hash");

        builder.Property(u => u.PhoneNumber)
            .HasConversion(
                phoneNumber => phoneNumber.Value,
                value => PhoneNumber.Create(value).Value!)
            .IsRequired()
            .HasMaxLength(PhoneNumber.MaxLength)
            .HasColumnName("phone_number");

        builder.Property(u => u.AvatarImagePath)
            .HasConversion(
                imagePath => imagePath.Value,
                value => AvatarImagePath.Create(value).Value!)
            .HasMaxLength(AvatarImagePath.MaxLength)
            .IsUnicode()
            .HasColumnName("avatar_image_path");

        builder.Property(u => u.Role)
            .HasConversion(
                role => role.Code,
                value => UserRole.FromCode(value))
            .HasMaxLength(UserRole.MaxLengthCode)
            .HasColumnName("role");

        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(u => u.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired(false);

        builder.Property(u => u.DeletedAt)
            .HasColumnName("deleted_at")
            .IsRequired(false);

        builder.Property(u => u.LockedAt)
            .HasColumnName("locked_at")
            .IsRequired(false);

        /*
         * Config Password History Table
         */
        builder.OwnsMany(
            u => u.PasswordHistories,
            passwordHistory =>
            {
                passwordHistory.ToTable("user_password_histories");
                passwordHistory.HasKey(p => p.Id);

                passwordHistory.Property(p => p.Id)
                    .HasColumnName("id");
                passwordHistory.Property(p => p.PasswordHash)
                    .HasConversion(
                        passwordHash => passwordHash.Value,
                        value => PasswordHash.Create(value).Value!)
                    .HasColumnName("password_hash")
                    .HasMaxLength(PasswordHash.MaxLength);
                passwordHistory.Property(p => p.ChangedAt)
                    .HasColumnName("changed_at");

                passwordHistory.WithOwner()
                    .HasForeignKey("user_id");
            });

        /*
         * Config Shipping Address Table
         */
        builder.OwnsMany(
            u => u.ShippingAddresses,
            shippingAddress =>
            {
                shippingAddress.ToTable("user_shipping_addresses");
                shippingAddress.HasKey(a => a.Id);

                shippingAddress.Property(a => a.Id)
                    .HasColumnName("id");
                shippingAddress.Property(a => a.RecipientName)
                    .HasConversion(
                        fullName => fullName.Value,
                        value => FullName.Create(value).Value!)
                    .HasMaxLength(FullName.MaxLength)
                    .IsUnicode()
                    .HasColumnName("recipient_name");
                shippingAddress.Property(a => a.PhoneNumber)
                    .HasConversion(
                        phoneNumber => phoneNumber.Value,
                        value => PhoneNumber.Create(value).Value!)
                    .HasMaxLength(PhoneNumber.MaxLength)
                    .HasColumnName("phone_number");
                shippingAddress.Property(a => a.Address)
                    .HasConversion(
                        address => address.ToDatabaseString(),
                        value => Address.ParseFromDatabase(value))
                    .HasMaxLength(Address.MaxLength)
                    .IsUnicode()
                    .HasColumnName("full_address");

                shippingAddress.WithOwner()
                    .HasForeignKey("user_id");
            });
    }
}