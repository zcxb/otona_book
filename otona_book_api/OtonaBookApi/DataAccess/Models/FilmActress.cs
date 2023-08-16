using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OtonaBookApi.DataAccess.Models;

[Table("film_actress")]
[Index("ActressId", Name = "idx_actress")]
[Index("FilmId", Name = "idx_film")]
public partial class FilmActress
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 演员id
    /// </summary>
    [Column("actress_id")]
    public int ActressId { get; set; }

    /// <summary>
    /// 电影id
    /// </summary>
    [Column("film_id")]
    public int FilmId { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    [Column("deleted_at", TypeName = "timestamp without time zone")]
    public DateTime? DeletedAt { get; set; }
}
