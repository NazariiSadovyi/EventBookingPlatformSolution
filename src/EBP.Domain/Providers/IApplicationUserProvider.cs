using EBP.Domain.Entities;

namespace EBP.Domain.Providers
{
    public interface IApplicationUserProvider
    {
        public ApplicationUser Current { get; }
    }
}
