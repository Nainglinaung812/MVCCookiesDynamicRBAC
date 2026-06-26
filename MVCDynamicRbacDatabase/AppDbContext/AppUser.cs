using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MVCDynamicRbacDatabase.AppDbContext;

[Table("AppUser")]
[Index("Username", Name = "UQ__AppUser__536C85E412F5DF3C", IsUnique = true)]
public partial class AppUser
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Username { get; set; } = null!;

    [StringLength(255)]
    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("AppUsers")]
    public virtual TblRole Role { get; set; } = null!;
}
