using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OtonaBookApi.Areas.Actress
{
    public class ActressResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("avatar")]
        public string? Avatar { get; set; }
        public object? Info { get; set; }
    }

    public class SetActressRequest
    {
        public string Name { get; set; }

        public string? Avatar { get; set; }

        public JsonDocument? Info { get; set; }
    }
}

