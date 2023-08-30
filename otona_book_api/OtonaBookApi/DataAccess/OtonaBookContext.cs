using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OtonaBookApi.DataAccess.Models;

namespace OtonaBookApi.DataAccess;

public partial class OtonaBookContext : DbContext
{
    public OtonaBookContext()
    {
    }

    public OtonaBookContext(DbContextOptions<OtonaBookContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actress> Actress { get; set; }

    public virtual DbSet<Film> Film { get; set; }

    public virtual DbSet<FilmActress> FilmActress { get; set; }

    public virtual DbSet<Genre> Genre { get; set; }

    public virtual DbSet<GenreTag> GenreTag { get; set; }

    public virtual DbSet<Publisher> Publisher { get; set; }

    public virtual DbSet<Series> Series { get; set; }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<UserCollect> UserCollect { get; set; }

    public virtual DbSet<UserCollectItem> UserCollectItem { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseNpgsql("Host=localhost;Port=5455;Database=otona_book;Username=zcxb;Password=123456");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("actress_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Avatar).HasComment("头像");
            entity.Property(e => e.DeletedAt).HasComment("删除时间");
            entity.Property(e => e.Info).HasComment("信息");
            entity.Property(e => e.Name).HasComment("名字");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("film_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Bango).HasComment("番号");
            entity.Property(e => e.CoverImages)
                .HasDefaultValueSql("'[]'::jsonb")
                .HasComment("封面");
            entity.Property(e => e.PublishedAt).HasComment("发行时间");
            entity.Property(e => e.PublisherId).HasComment("发行商id");
            entity.Property(e => e.SampleImages)
                .HasDefaultValueSql("'[]'::jsonb")
                .HasComment("样品图");
            entity.Property(e => e.SeriesId).HasComment("系列id");
            entity.Property(e => e.Tags)
                .HasDefaultValueSql("'[]'::jsonb")
                .HasComment("标签");
            entity.Property(e => e.Title).HasComment("标题");
        });

        modelBuilder.Entity<FilmActress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("film_actress_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.ActressId).HasComment("演员id");
            entity.Property(e => e.DeletedAt).HasComment("删除时间");
            entity.Property(e => e.FilmId).HasComment("电影id");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("category_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.DeletedAt).HasComment("删除时间");
            entity.Property(e => e.Name).HasComment("名称");
        });

        modelBuilder.Entity<GenreTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tag_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.DeletedAt).HasComment("删除时间");
            entity.Property(e => e.Name).HasComment("tag名");
            entity.Property(e => e.OutGenreId).HasComment("外部tagid");
            entity.Property(e => e.ParentTagId).HasDefaultValueSql("0");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("publisher_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasComment("发行商名");
        });

        modelBuilder.Entity<Series>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("series_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasComment("系列名称");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Account).HasComment("账号");
            entity.Property(e => e.Email).HasComment("邮箱");
            entity.Property(e => e.InviterId).HasComment("邀请人id");
            entity.Property(e => e.NickName).HasComment("昵称");
            entity.Property(e => e.PasswordHash).HasComment("密码hash");
            entity.Property(e => e.PasswordSalt).HasComment("密码hashsalt");
            entity.Property(e => e.RegisterAt).HasComment("注册时间");
        });

        modelBuilder.Entity<UserCollect>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_collect_pkey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CollectName).HasComment("收藏夹名");
            entity.Property(e => e.CollectType).HasComment("收藏夹类型 1-演员2-影片");
            entity.Property(e => e.DeletedAt).HasComment("删除时间");
            entity.Property(e => e.SortNo).HasComment("排序");
            entity.Property(e => e.UserId).HasComment("用户id");
        });

        modelBuilder.Entity<UserCollectItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_collect_item_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.CollectId).HasComment("收藏夹id");
            entity.Property(e => e.DeletedAt).HasComment("删除时间");
            entity.Property(e => e.ItemId).HasComment("收藏项id");
            entity.Property(e => e.UserId).HasComment("用户id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
