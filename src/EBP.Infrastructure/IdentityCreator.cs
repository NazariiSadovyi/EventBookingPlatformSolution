using EBP.Application.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EBP.Infrastructure
{
    public static class IdentityCreator
    {
        public static async Task CreateRolesAsync(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string[] roles = [AppRoles.User, AppRoles.Admin];

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
