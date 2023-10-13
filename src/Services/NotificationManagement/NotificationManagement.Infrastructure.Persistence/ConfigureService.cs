using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationManagement.Application.Adapters;
using NotificationManagement.Domain.Contracts;
using NotificationManagement.Domain.Entities.NotificationAggregate;
using NotificationManagement.Infrastructure.Adapters;
using NotificationManagement.Infrastructure.Mediator;
using NotificationManagement.Infrastructure.Persistence.Context;
using NotificationManagement.Infrastructure.Persistence.Repositories;
using Services.Common;

namespace NotificationManagement.Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        services.AddScoped<ISmsAdapter, SmsAdapter>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IUnitofWork, UnitofWork>();

        var dbConnectionString = configuration.GetRequiredConnectionString("NotificationManagement");
        services.AddDbContext<NotificationManagementContext>((IServiceProvider serviceProvider, DbContextOptionsBuilder options) =>
        {
            options.UseSqlServer(dbConnectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            });
        });

        var hcBuilder = services.AddHealthChecks();

        hcBuilder.AddSqlServer(_ => dbConnectionString, name: "NotificationManagementDB-check", tags: new string[] { "ready" });

        return services;
    }
}
