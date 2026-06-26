// using MVCDynamicRbacDatabase.AppDbContext;
using MVCDynamicRbacDatabase.AppDbContext;

using Microsoft.EntityFrameworkCore;
namespace DynamicRbacMvc.Features.Auth;

public class AuthService
{
    private readonly MvcDynamicRbacContext _context;
    public AuthService(MvcDynamicRbacContext context)
    {
        _context = context;
    }

    public async Task<(AppUser? User, string? RoleName)> LoginAsync(AuthRequest request)
    {
        var user = await _context.AppUsers.FirstOrDefaultAsync(x =>
                    x.Username == request.Username &&
                    x.Password == request.Password);
        if (user == null)
        {
            return (null, null);
        }
        var role = await _context.TblRoles.FirstOrDefaultAsync(r => r.Id == user.RoleId);
        return (user, role?.RoleName);
    }
    public MvcDynamicRbacContext GetContext() => _context;
}