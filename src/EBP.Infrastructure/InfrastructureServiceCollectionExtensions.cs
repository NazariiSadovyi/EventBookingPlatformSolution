using EBP.Application.Interfaces;
using EBP.Domain.Providers;
using EBP.Domain.Repositories;
using EBP.Infrastructure.Options;
using EBP.Infrastructure.Repositories;
using EBP.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EBP.Infrastructure
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddPersistence(services, configuration);
            AddAuthentication(services, configuration);
            services.AddScoped<ITimeProvider, Providers.TimeProvider>();
        }

        private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<ApplicationDbContext>(_ => _.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDbSessionRepository, DbSessionRepository>();
            services.AddScoped<IBookingEventRepository, BookingEventRepository>();
            services.AddScoped<IBookingTicketRepository, BookingTicketRepository>();
        }

        private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, AspNetIdentityService>();
            services.AddScoped<ITokenService, JwtTokenService>();

            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>()!;

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey))
                    };
                });
        }
    }
}
