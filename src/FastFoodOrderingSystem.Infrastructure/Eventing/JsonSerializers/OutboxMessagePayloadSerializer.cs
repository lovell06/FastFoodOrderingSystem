using System.Runtime.Serialization;
using System.Text.Json;
using FastFoodOrderingSystem.Infrastructure.Eventing.Abstractions;
using FastFoodOrderingSystem.Infrastructure.Persistence.Database.Entities;

namespace FastFoodOrderingSystem.Infrastructure.Eventing.JsonSerializers;

public static class OutboxMessagePayloadSerializer
{
    public static string Serialize(IEvent e)
    {
        return JsonSerializer.Serialize(e);
    }

    public static IEvent Deserialize(OutboxMessage outboxMessage)
    {
        var type = Type.GetType(outboxMessage.Type);
        if (type is null)
            throw new SerializationException($"Not found type: {outboxMessage.Type}");
        
        var result = (IEvent?)JsonSerializer.Deserialize(outboxMessage.Payload, type);

        if (result is null)
            throw new SerializationException($"Cannot deserialize {type.Name}");

        return result;
    }
}