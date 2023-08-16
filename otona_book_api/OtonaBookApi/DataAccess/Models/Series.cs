using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OtonaBookApi.DataAccess.Models;

[Table("series")]
public partial class Series
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 系列名称
    /// </summary>
    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;
}
