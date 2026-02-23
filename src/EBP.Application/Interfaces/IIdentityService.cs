namespace EBP.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<(bool Succeeded, IReadOnlyList<string> Errors)> CreateUserAsync(string email, string password, bool isAdmin);
        Task<(bool Found, string? UserId, string? Email, IList<string> Roles)> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(string userId, string password);
    }
}
