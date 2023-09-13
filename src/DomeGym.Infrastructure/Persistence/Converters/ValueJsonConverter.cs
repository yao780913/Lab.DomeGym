using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DomeGym.Infrastructure.Persistence.Converters;

public class ValueJsonConverter<T> : ValueConverter<T, string>
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public ValueJsonConverter(ConverterMappingHints? mappingHints = null)
        : base(
            v => JsonSerializer.Serialize(v, JsonSerializerOptions),
            v => JsonSerializer.Deserialize<T>(v, JsonSerializerOptions)!,
            mappingHints)
    {
    }
}

public class ValueJsonComparer<T> : ValueComparer<T>
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public ValueJsonComparer () : base(
        (l, r) => JsonSerializer.Serialize(l, JsonSerializerOptions) == JsonSerializer.Serialize(r, JsonSerializerOptions),
        v => v == null ? 0 : JsonSerializer.Serialize(v, JsonSerializerOptions).GetHashCode(),
        v => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(v, JsonSerializerOptions), JsonSerializerOptions)!) { }
}
