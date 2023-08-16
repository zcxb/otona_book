using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OtonaBookApi.DataAccess.Models;

[Table("genre")]
public partial class Genre
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Column("name")]
    [StringLength(32)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 删除时间
    /// </summary>
    [Column("deleted_at", TypeName = "timestamp(0) without time zone")]
    public DateTime? DeletedAt { get; set; }
}
