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

    public virtual DbSet<Actress> Actresses { get; set; }

   // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
     //   => optionsBuilder.UseNpgsql("Host=localhost;Port=5455;Database=otona_book;Username=zcxb;Password=123456");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("actress_pkey");

            entity.ToTable("actress");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Avatar)
                .HasMaxLength(1000)
                .HasComment("头像")
                .HasColumnName("avatar");
            entity.Property(e => e.Info)
                .HasComment("信息")
                .HasColumnType("jsonb")
                .HasColumnName("info");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasComment("名字")
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
