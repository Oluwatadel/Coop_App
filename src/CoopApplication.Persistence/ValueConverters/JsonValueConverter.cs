using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoopApplication.Persistence.ValueConverters;

public class JsonValueConverter<T> : ValueConverter<T, string>
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    public JsonValueConverter()
        : base(
            v => JsonSerializer.Serialize(v, JsonSerializerOptions),
            v => DeserializeOrDefault(v)
        )
    {
    }

    private static T DeserializeOrDefault(string v)
    {
        var result = JsonSerializer.Deserialize<T>(v, JsonSerializerOptions);

        if (result is not null)
            return result;

        // Only use fallback for reference types or nullable value types
        if (typeof(T).IsClass || Nullable.GetUnderlyingType(typeof(T)) != null)
        {
            return Activator.CreateInstance<T>()!;
        }

        throw new InvalidOperationException($"Deserialization returned null for non-nullable type {typeof(T).Name}");
    }
}