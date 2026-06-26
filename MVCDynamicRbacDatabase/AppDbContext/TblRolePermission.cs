using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MVCDynamicRbacDatabase.AppDbContext;

[Table("Tbl_RolePermission")]
public partial class TblRolePermission
{
    [Key]
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int PermissionId { get; set; }

    [ForeignKey("PermissionId")]
    [InverseProperty("TblRolePermissions")]
    public virtual TblPermission Permission { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("TblRolePermissions")]
    public virtual TblRole Role { get; set; } = null!;
}
