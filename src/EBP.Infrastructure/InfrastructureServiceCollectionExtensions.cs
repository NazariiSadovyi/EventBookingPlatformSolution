using EBP.Domain.Providers;
using EBP.Domain.Repositories;
using EBP.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EBP.Infrastructure
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<ApplicationDbContext>(_ => _.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IDbSessionRepository, DbSessionRepository>();
            services.AddScoped<IBookingEventRepository, BookingEventRepository>();
            services.AddScoped<IBookingTicketRepository, BookingTicketRepository>();
            services.AddScoped<ITimeProvider, Providers.TimeProvider>();
        }
    }
}
