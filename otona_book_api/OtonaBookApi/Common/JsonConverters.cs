using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OtonaBookApi.Common
{
    public class NullToEmptyArrayConverter : JsonConverter<JsonDocument?>
    {
        public override JsonDocument Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                return doc;
            }
        }

        public override void Write(Utf8JsonWriter writer, JsonDocument? value, JsonSerializerOptions options)
        {
            if (value == null || value.RootElement.ValueKind == JsonValueKind.Null)
            {
                writer.WriteStartArray(); // Serialize an empty array
                writer.WriteEndArray();
            }
            else
            {
                value.WriteTo(writer);
            }
        }
    }
}

