using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MVCDynamicRbacDatabase.AppDbContext;

[Table("Tbl_Permission")]
[Index("PermissionName", Name = "UQ__Tbl_Perm__0FFDA357E4F09DAE", IsUnique = true)]
public partial class TblPermission
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string PermissionName { get; set; } = null!;

    [InverseProperty("Permission")]
    public virtual ICollection<TblRolePermission> TblRolePermissions { get; set; } = new List<TblRolePermission>();
}
