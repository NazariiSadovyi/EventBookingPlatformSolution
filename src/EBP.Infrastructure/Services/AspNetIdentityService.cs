using EBP.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EBP.Infrastructure.Services
{
    public class AspNetIdentityService(UserManager<IdentityUser> _userManager) : IIdentityService
    {
        public async Task<(bool Succeeded, IReadOnlyList<string> Errors)> CreateUserAsync(string email, string password)
        {
            var user = new IdentityUser
            {
                UserName = email,
                Email = email
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
                return (true, Array.Empty<string>());

            var errors = result.Errors.Select(e => e.Description).ToArray();
            return (false, errors);
        }

        public async Task<(bool Found, string? UserId, string? Email)> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return (false, null, null);

            return (true, user.Id, user.Email);
        }

        public async Task<bool> CheckPasswordAsync(string userId, string password)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return false;

            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
