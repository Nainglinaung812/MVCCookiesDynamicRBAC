using MVCDynamicRbacDatabase;
using MVCDynamicRbacDatabase.AppDbContext;
using DynamicRbacMvc.Features.Auth;
using DynamicRbacMvc.Features.Product;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MvcDynamicRbacContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

var app = builder.Build();

// Seed initial data စနစ်တကျ ပြန်လည်ပြင်ဆင်ခြင်း
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MvcDynamicRbacContext>();
    context.Database.EnsureCreated();

    if (!context.TblRoles.Any())
    {
        var adminRole = new TblRole { RoleName = "Admin" };
        var staffRole = new TblRole { RoleName = "Staff" };
        context.TblRoles.AddRange(adminRole, staffRole);
        context.SaveChanges(); 

        var viewPerm = new TblPermission { PermissionName = "Product.View" };
        var createPerm = new TblPermission { PermissionName = "Product.Create" };
        var updatePerm = new TblPermission { PermissionName = "Product.Update" };
        var deletePerm = new TblPermission { PermissionName = "Product.Delete" };
        context.TblPermissions.AddRange(viewPerm, createPerm, updatePerm, deletePerm);
        context.SaveChanges(); 

        context.TblRolePermissions.AddRange(
            new TblRolePermission { RoleId = adminRole.Id, PermissionId = viewPerm.Id },
            new TblRolePermission { RoleId = adminRole.Id, PermissionId = createPerm.Id },
            new TblRolePermission { RoleId = adminRole.Id, PermissionId = updatePerm.Id },
            new TblRolePermission { RoleId = adminRole.Id, PermissionId = deletePerm.Id },
            new TblRolePermission { RoleId = staffRole.Id, PermissionId = viewPerm.Id }
        );

        context.AppUsers.AddRange(
            new AppUser { Username = "admin", Password = "123", RoleId = adminRole.Id },
            new AppUser { Username = "staff", Password = "123", RoleId = staffRole.Id }
        );
        
        context.SaveChanges(); 
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();