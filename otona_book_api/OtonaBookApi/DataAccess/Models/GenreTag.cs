using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OtonaBookApi.DataAccess.Models;

[Table("genre_tag")]
public partial class GenreTag
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// tag名
    /// </summary>
    [Column("name")]
    [StringLength(32)]
    public string Name { get; set; } = null!;

    [Column("parent_tag_id")]
    public int? ParentTagId { get; set; }

    [Column("tag_id_path")]
    [StringLength(1000)]
    public string? TagIdPath { get; set; }

    /// <summary>
    /// 外部tagid
    /// </summary>
    [Column("out_genre_id")]
    [StringLength(32)]
    public string? OutGenreId { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    [Column("deleted_at", TypeName = "timestamp(0) without time zone")]
    public DateTime? DeletedAt { get; set; }
}
