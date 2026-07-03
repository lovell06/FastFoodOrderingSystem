using System.Text.Json;
using System.Text.Json.Serialization;

namespace FastFoodOrderingSystem.Infrastructure.Serialization;

public abstract class SystemTextJsonConverter<T> : JsonConverter<T>
{
    protected abstract T? Create(string value);
    protected abstract string GetValue(T value);
    public override T? Read(
        ref Utf8JsonReader reader, 
        Type typeToConvert, 
        JsonSerializerOptions options)
    {
        return Create(reader.GetString()!);
    }

    public override void Write(
        Utf8JsonWriter writer, 
        T value, 
        JsonSerializerOptions options)
    {
        writer.WriteStringValue(GetValue(value));
    }
}