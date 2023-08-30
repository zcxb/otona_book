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
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("bango")]
        public string Bango { get; set; }

        [JsonPropertyName("published_at")]
        public string PublishedAt { get; set; }

        [JsonPropertyName("tags")]
        public SaveFilmItemRequest_Tag[] Tags { get; set; }

        [JsonPropertyName("actress")]
        public string[] Actress { get; set; }

        [JsonPropertyName("cover_images")]
        public JsonDocument CoverImages { get; set; }

        [JsonPropertyName("sample_images")]
        public JsonDocument SampleImages { get; set; }
    }

    public class SaveFilmItemRequest_Tag
    {
        [JsonPropertyName("tag_uid")]
        public string TagUid { get; set; }

        [JsonPropertyName("tag_name")]
        public string TagName { get; set; }
    }
}

