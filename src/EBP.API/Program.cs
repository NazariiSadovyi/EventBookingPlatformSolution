using EBP.API.Extensions;
using EBP.API.Middlewares;
using EBP.API.Providers;
using EBP.Application;
using EBP.Domain.Providers;
using EBP.Infrastructure;
using EBP.Infrastructure.BackgroundJob;
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
            builder.Services.AddInfrastructureBackgroundJob(builder.Configuration);

            builder.Services.AddScoped<IApplicationUserProvider, ApplicationUserProvider>();

            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.Services.AddAuthorization();

            var app = builder.Build();

            // ONLY FOR DEMO PURPOSE, DO NOT USE THIS APPROACH IN PRODUCTION ENVIRONMENT !!!
            await ApplyMigration(app);

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
