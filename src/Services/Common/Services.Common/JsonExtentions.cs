using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Common;
public static class JsonExtentions
{
    public static readonly JsonSerializerOptions DefaultOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };


    public static string Serialize(object @object) => JsonSerializer.Serialize(@object, DefaultOptions);
    public static T Deserialize<T>(string input) where T : class
    {
        ArgumentException.ThrowIfNullOrEmpty(input, nameof(input));

        var @object = JsonSerializer.Deserialize<T>(input, DefaultOptions);
        if (@object is null)
            return default(T)!;

        return @object;
    }

}
