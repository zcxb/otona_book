using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OtonaBookApi.DataAccess.Models;

[Table("user_collect")]
public partial class UserCollect
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 用户id
    /// </summary>
    [Column("user_id")]
    public int UserId { get; set; }

    /// <summary>
    /// 收藏夹名
    /// </summary>
    [Column("collect_name")]
    [StringLength(20)]
    public string CollectName { get; set; } = null!;

    /// <summary>
    /// 排序
    /// </summary>
    [Column("sort_no")]
    public short? SortNo { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    [Column("deleted_at", TypeName = "timestamp without time zone")]
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// 收藏夹类型 1-演员2-影片
    /// </summary>
    [Column("collect_type")]
    public short CollectType { get; set; }
}
