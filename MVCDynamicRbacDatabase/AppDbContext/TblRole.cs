using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MVCDynamicRbacDatabase.AppDbContext;

[Table("Tbl_Role")]
[Index("RoleName", Name = "UQ__Tbl_Role__8A2B616066DC3005", IsUnique = true)]
public partial class TblRole
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string RoleName { get; set; } = null!;

    [InverseProperty("Role")]
    public virtual ICollection<AppUser> AppUsers { get; set; } = new List<AppUser>();

    [InverseProperty("Role")]
    public virtual ICollection<TblRolePermission> TblRolePermissions { get; set; } = new List<TblRolePermission>();
}
