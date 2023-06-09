using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using bill.Models;

namespace bill.Context;

public partial class BillDbContext : DbContext
{
    public BillDbContext()
    {
    }

    public BillDbContext(DbContextOptions<BillDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<item> items { get; set; }

    public virtual DbSet<receipt> receipts { get; set; }

    public virtual DbSet<receipt_detail> receipt_details { get; set; }

    public virtual DbSet<unit> units { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=10000;database=bill;uid=root;password=Pond03@pp", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<item>(entity =>
        {
            entity.HasKey(e => e.item_id).HasName("PRIMARY");

            entity.ToTable("item");

            entity.HasIndex(e => e.unit_id, "unitid_idx");

            entity.Property(e => e.item_id).ValueGeneratedNever();
            entity.Property(e => e.code).HasMaxLength(45);
            entity.Property(e => e.name).HasMaxLength(45);
            entity.Property(e => e.price).HasPrecision(2);

            entity.HasOne(d => d.unit).WithMany(p => p.items)
                .HasForeignKey(d => d.unit_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("unit_id");
        });

        modelBuilder.Entity<receipt>(entity =>
        {
            entity.HasKey(e => e.receipt_id).HasName("PRIMARY");

            entity.ToTable("receipt");

            entity.Property(e => e.receipt_id).ValueGeneratedNever();
            entity.Property(e => e.code).HasMaxLength(45);
            entity.Property(e => e.date).HasMaxLength(45);
            entity.Property(e => e.total_price).HasMaxLength(45);
        });

        modelBuilder.Entity<receipt_detail>(entity =>
        {
            entity.HasKey(e => e.receipt_detail_id).HasName("PRIMARY");

            entity.ToTable("receipt_detail");

            entity.HasIndex(e => e.item_id, "item_id_idx");

            entity.HasIndex(e => e.receipt_id, "receipt_id_idx");

            entity.HasOne(d => d.item).WithMany(p => p.receipt_details)
                .HasForeignKey(d => d.item_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("item_id");

            entity.HasOne(d => d.receipt).WithMany(p => p.receipt_details)
                .HasForeignKey(d => d.receipt_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("receipt_id");
        });

        modelBuilder.Entity<unit>(entity =>
        {
            entity.HasKey(e => e.unit_id).HasName("PRIMARY");

            entity.ToTable("unit");

            entity.Property(e => e.name).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
