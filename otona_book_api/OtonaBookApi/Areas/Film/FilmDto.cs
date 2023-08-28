using System;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using OtonaBookApi.Common;

namespace OtonaBookApi.Areas.Film
{
    public class QueryFilmListRequest : QueryByPageRequest
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("bango")]
        public string? Bango { get; set; }

        [JsonPropertyName("actresses")]
        public int[]? ActressIds { get; set; }

        [JsonPropertyName("series")]
        public int? SeriesId { get; set; }

        [JsonPropertyName("tags")]
        public int[]? TagIds { get; set; }

        //[JsonPropertyName("published_at")]
        //public string PublishedAt { get; set; } = null!;
    }

    public class QueryFilmListResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("bango")]
        public string Bango { get; set; } = null!;

        [JsonPropertyName("title")]
        public string Title { get; set; } = null!;

        [JsonPropertyName("published_at")]
        public DateTime? PublishedAt { get; set; }

        [JsonPropertyName("cover_images")]
        public JsonDocument CoverImages { get; set; } = null!;
    }

    public class SaveFilmItemRequest
    {
        public string Title { get; set; }

        public string Bango { get; set; }

        public string PublishedAt { get; set; }

        public string[] Tags { get; set; }

        public string[] Actress { get; set; }

        public string[] CoverImages { get; set; }

    }
}

