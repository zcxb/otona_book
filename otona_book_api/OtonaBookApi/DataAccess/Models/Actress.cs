using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace OtonaBookApi.DataAccess.Models;

[Table("actress")]
public partial class Actress
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 名字
    /// </summary>
    [Column("name")]
    [StringLength(32)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 头像
    /// </summary>
    [Column("avatar")]
    [StringLength(1000)]
    public string? Avatar { get; set; }

    /// <summary>
    /// 信息
    /// </summary>
    [Column("info", TypeName = "jsonb")]
    public JsonDocument? Info { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    [Column("deleted_at", TypeName = "timestamp(0) without time zone")]
    public DateTime? DeletedAt { get; set; }
}
