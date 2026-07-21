using FastFoodOrderingSystem.Infrastructure.Eventing.Abstractions;
using FastFoodOrderingSystem.Infrastructure.Eventing.JsonSerializers;

namespace FastFoodOrderingSystem.Infrastructure.Persistence.Database.Entities;

public sealed class OutboxMessage
{
    public required Guid Id { get; init; }
    public required string Type { get; init; }
    public required string Payload { get; init; }
    public required DateTime OccurredAtUtc { get; init; }
    public DateTime? ProcessedAtUtc { get; set; }
    public string? Error { get; set; }
    public int RetryCount { get; set; }

    public static OutboxMessage Create(IEvent e)
    {
        var json = OutboxMessagePayloadSerializer.Serialize(e);
        return new OutboxMessage
        {
            Id = Guid.NewGuid(),
            Type = e.GetType().FullName!,
            Payload = json,
            OccurredAtUtc = e.OccurredAtUtc,
            ProcessedAtUtc = null,
            Error = null
        };
    }
}