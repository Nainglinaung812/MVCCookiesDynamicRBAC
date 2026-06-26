using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVCDynamicRbacDatabase.AppDbContext;

public partial class MvcDynamicRbacContext : DbContext
{
    public MvcDynamicRbacContext()
    {
    }

    public MvcDynamicRbacContext(DbContextOptions<MvcDynamicRbacContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppUser> AppUsers { get; set; }

    public virtual DbSet<TblPermission> TblPermissions { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblRolePermission> TblRolePermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AppUser__3214EC078503C874");

            entity.HasOne(d => d.Role).WithMany(p => p.AppUsers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AppUser__RoleId__3E52440B");
        });

        modelBuilder.Entity<TblPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Perm__3214EC075A0BBA6E");
        });

        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Role__3214EC076A10C78E");
        });

        modelBuilder.Entity<TblRolePermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tbl_Role__3214EC07A1469E01");

            entity.HasOne(d => d.Permission).WithMany(p => p.TblRolePermissions).HasConstraintName("FK__Tbl_RoleP__Permi__4222D4EF");

            entity.HasOne(d => d.Role).WithMany(p => p.TblRolePermissions).HasConstraintName("FK__Tbl_RoleP__RoleI__412EB0B6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
