using System.Runtime.Serialization;
using System.Text.Json;
using FastFoodOrderingSystem.Infrastructure.Eventing.Abstractions;
using FastFoodOrderingSystem.Infrastructure.Persistence.Database.Entities;

namespace FastFoodOrderingSystem.Infrastructure.Eventing.JsonSerializers;

public static class OutboxMessagePayloadSerializer
{
    public static string Serialize(IEvent e)
    {
        return JsonSerializer.Serialize(e, e.GetType());
    }

    public static IEvent Deserialize(string payload, string typeName)
    {
        var asm = typeof(IEvent).Assembly;
        var type = asm.GetType(typeName);
        
        if (type is null)
            throw new SerializationException($"Not found type: {typeName}");

        var result = (IEvent) (JsonSerializer.Deserialize(payload, type) ??
                         throw new SerializationException($"Cannot deserialize {type.Name}"));

        return result;
    }
}