using EBP.API.Extensions;
using EBP.API.Middlewares;
using EBP.API.Providers;
using EBP.Application;
using EBP.Domain.Providers;
using EBP.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EBP.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwagger();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Services.AddScoped<IApplicationUserProvider, ApplicationUserProvider>();

            builder.Services.AddAuthorization();

            var app = builder.Build();

            if (builder.Configuration.GetValue<bool>("ApplyMigrationsOnStartup"))
                await ApplyMigration(app);

            await IdentityCreator.CreateRolesAsync(app.Services);

            app.UseMiddleware<ErrorHandlingMiddleware>();
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static async Task ApplyMigration(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
