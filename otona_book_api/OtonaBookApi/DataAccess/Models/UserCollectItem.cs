using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OtonaBookApi.DataAccess.Models;

[Table("user_collect_item")]
public partial class UserCollectItem
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
    /// 收藏夹id
    /// </summary>
    [Column("collect_id")]
    public int CollectId { get; set; }

    /// <summary>
    /// 收藏项id
    /// </summary>
    [Column("item_id")]
    public int ItemId { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    [Column("deleted_at", TypeName = "timestamp without time zone")]
    public DateTime? DeletedAt { get; set; }
}
