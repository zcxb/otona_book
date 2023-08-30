using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace OtonaBookApi.DataAccess.Models;

[Table("film")]
public partial class Film
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 番号
    /// </summary>
    [Column("bango")]
    [StringLength(32)]
    public string Bango { get; set; } = null!;

    /// <summary>
    /// 标题
    /// </summary>
    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 发行商id
    /// </summary>
    [Column("publisher_id")]
    public int? PublisherId { get; set; }

    /// <summary>
    /// 封面
    /// </summary>
    [Column("cover_images", TypeName = "jsonb")]
    public JsonDocument CoverImages { get; set; } = null!;

    /// <summary>
    /// 样品图
    /// </summary>
    [Column("sample_images", TypeName = "jsonb")]
    public JsonDocument SampleImages { get; set; } = null!;

    /// <summary>
    /// 发行时间
    /// </summary>
    [Column("published_at", TypeName = "timestamp without time zone")]
    public DateTime? PublishedAt { get; set; }

    /// <summary>
    /// 系列id
    /// </summary>
    [Column("series_id")]
    public int? SeriesId { get; set; }

    /// <summary>
    /// 标签
    /// </summary>
    [Column("tags", TypeName = "jsonb")]
    public JsonDocument Tags { get; set; } = null!;
}
