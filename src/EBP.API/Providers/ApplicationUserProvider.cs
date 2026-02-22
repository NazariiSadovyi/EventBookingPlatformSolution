using EBP.Domain.Entities;
using EBP.Domain.Providers;

namespace EBP.API.Providers
{
    public class ApplicationUserProvider : IApplicationUserProvider
    {
        private readonly ApplicationUser? _current;

        public ApplicationUser Current => _current ?? throw new InvalidOperationException("User not found in the current context.");

        public ApplicationUserProvider(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            if (user is null)
                return;

            var userId = user.Claims.First(_ => _.Type.EndsWith("identifier"))?.Value;
            var email = user.Claims.First(_ => _.Type.EndsWith("address"))?.Value;
            if (userId != null && email != null)
                _current = ApplicationUser.Create(userId, email);
        }
    }
}
