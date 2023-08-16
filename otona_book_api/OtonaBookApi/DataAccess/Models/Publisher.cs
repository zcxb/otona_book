using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OtonaBookApi.DataAccess.Models;

[Table("publisher")]
public partial class Publisher
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 发行商名
    /// </summary>
    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;
}
