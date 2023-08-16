using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OtonaBookApi.DataAccess.Models;

[Table("user")]
public partial class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 账号
    /// </summary>
    [Column("account")]
    [StringLength(20)]
    public string Account { get; set; } = null!;

    /// <summary>
    /// 邮箱
    /// </summary>
    [Column("email")]
    [StringLength(255)]
    public string Email { get; set; } = null!;

    /// <summary>
    /// 密码hash
    /// </summary>
    [Column("password_hash")]
    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    /// <summary>
    /// 密码hashsalt
    /// </summary>
    [Column("password_salt")]
    [StringLength(32)]
    public string PasswordSalt { get; set; } = null!;

    /// <summary>
    /// 昵称
    /// </summary>
    [Column("nick_name")]
    [StringLength(32)]
    public string NickName { get; set; } = null!;

    /// <summary>
    /// 注册时间
    /// </summary>
    [Column("register_at", TypeName = "timestamp without time zone")]
    public DateTime RegisterAt { get; set; }

    /// <summary>
    /// 邀请人id
    /// </summary>
    [Column("inviter_id")]
    public int? InviterId { get; set; }
}
