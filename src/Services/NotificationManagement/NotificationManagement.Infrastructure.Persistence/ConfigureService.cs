using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationManagement.Infrastructure.Mediator;
using NotificationManagement.Infrastructure.Persistence.Context;
using Services.Common;

namespace NotificationManagement.Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        services.AddDbContext<ApplicationDbContext>((IServiceProvider serviceProvider, DbContextOptionsBuilder options) =>
        {
            options.UseSqlServer(configuration.GetRequiredConnectionString("Application"), sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            });
        });

        var hcBuilder = services.AddHealthChecks();
        hcBuilder.AddDbContextCheck<ApplicationDbContext>(name: "ApplicationDB-check", tags: new string[] { "ready" });

        return services;
    }

}
