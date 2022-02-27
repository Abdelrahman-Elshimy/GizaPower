using Microsoft.AspNetCore.Identity;
using GizaPower.Constants;
using System.Threading.Tasks;
using GizaPower.Data;

namespace GizaPower.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<GiazUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
        }
    }
}