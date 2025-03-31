using System.Text.Json;
using System.Text.Json.Serialization;

namespace ECommerce.Common.Utils
{
    public static class JsonUtil
    {

        private static readonly JsonSerializerOptions Options = new()
        {

            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };
        public static string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, Options);
        }
        public static T? Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, Options);
        }

        public static object? Deserialize(string json, Type type)
        {
            return JsonSerializer.Deserialize(json, type, Options);
        }
    }
}
