using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity;
public class SeedUserRoleInitial : ISeedUserRoleInitial
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedUserRoleInitial(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void SeedUsers()
    {
        if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = "usuario@localhost",
                Email = "usuario@localhost",
                NormalizedUserName = "USUARIO@LOCALHOST",
                NormalizedEmail = "USUARIO@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = _userManager.CreateAsync(user, "Numesy#2024").Result;

            if (result.Succeeded)
            {
                if (_roleManager.RoleExistsAsync("User").Result)
                {
                    _userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
        }

        if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = "admin@localhost",
                Email = "admin@localhost",
                NormalizedUserName = "ADMIN@LOCALHOST",
                NormalizedEmail = "ADMIN@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = _userManager.CreateAsync(user, "Numesy#2024").Result;

            if (result.Succeeded)
            {
                if (_roleManager.RoleExistsAsync("Admin").Result)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }

    public void SeedRoles()
    {
        if (!_roleManager.RoleExistsAsync("User").Result) // Verifica se o papel NÃO existe
        {
            IdentityRole role = new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER"
            };
            _roleManager.CreateAsync(role).Wait();
        }

        if (!_roleManager.RoleExistsAsync("Admin").Result) // Verifica se o papel NÃO existe
        {
            IdentityRole role = new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            _roleManager.CreateAsync(role).Wait();
        }
    }

}
