using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using NotificationManagement.Application.Behaviors;

namespace NotificationManagement.Application;

public static class ConfigureService
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidatorBehavior<,>));
        });
        return services;
    }
}
