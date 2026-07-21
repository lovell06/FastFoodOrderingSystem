using FastFoodOrderingSystem.Infrastructure.Persistence.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FastFoodOrderingSystem.Infrastructure.Persistence.Database.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages");
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.OccurredAtUtc);

        builder.Property(e => e.Id)
            .HasColumnName("id");

        builder.Property(e => e.Type)
            .HasMaxLength(500)
            .HasColumnName("type");

        builder.Property(e => e.Payload)
            .HasMaxLength(1000)
            .HasColumnType("jsonb")
            .HasColumnName("payload")
            .IsRequired();
        
        builder.Property(e => e.OccurredAtUtc)
            .HasColumnName("occurred_at_utc");
        
        builder.Property(e => e.ProcessedAtUtc)
            .HasColumnName("processed_at_utc");
        
        builder.Property(e => e.Error)
            .HasColumnName("error")
            .HasMaxLength(500);

        builder.Property(e => e.RetryCount)
            .HasColumnName("retry_count");
    }
}