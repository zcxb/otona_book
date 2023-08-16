using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace OtonaBookApi.DataAccess.Models;

public partial class Actress: IDisposable
{
    public int Id { get; set; }

    /// <summary>
    /// 名字
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 信息
    /// </summary>
    //[Column(TypeName = "jsonb")]
    public JsonDocument? Info { get; set; }

    public void Dispose() => Info?.Dispose();
}
